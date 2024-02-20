using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using MySql.Data.MySqlClient;
using System.Data;
using uPLibrary.Networking.M2Mqtt.Messages;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AdminBMC
{

    public partial class Form1 : Form
    {
        //mqtt connection
        bool connect = false;
        private MqttClient mqttClient;
        private List<string> subscribedTopics = new List<string>();
        private List<string> IDs = new List<string>();
        //private List<MessageData> messages = new List<MessageData>();

        // MySQL connection
        MySqlConnection con = new MySqlConnection("SERVER = 192.168.240.145; DATABASE = sys; UID = db; PASSWORD = Saks@2468;");

        // DeviceId field
        private string deviceId;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            deviceId = Properties.Settings.Default.DeviceId;

            subscribeTopic.CellContentClick += SubscribeTopic_CellContentClick; ;
            users1.CellContentClick += Users_CellContentClick; ;
        }

        private void Users_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 5)
            {
                ToggleUserStatus(e.RowIndex);
            }
        }

        private void SubscribeTopic_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            {
                ToggleSubscriptionStatus(e.RowIndex);
            }
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (connect)
            {
                mqttClient.Disconnect();
                Connbtn.Text = "Connect";
                statLbl.Text = "Not Connected to broker";
                connect = false;
                MessageBox.Show("Disconnected from MQTT broker!");
            }
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void Connbtn_Click(object sender, EventArgs e)
        {
            if (connect == false)
            {
                try
                {
                    mqttClient = new MqttClient("www.mqtt-dashboard.com", 1883, false, null, null, MqttSslProtocols.None);
                    mqttClient.Connect(Guid.NewGuid().ToString());
                    MessageBox.Show("Connected to MQTT broker!");
                    Connbtn.Text = "Disconnect";
                    statLbl.Text = "Connected to broker";
                    connect = true;
                    try
                    {
                        con.Open();
                        LoadSubscribedTopics();
                        LoadIds();
                        LoadUsersData();
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cannot open connection!");
                    }
                    MySqlDataAdapter da = new MySqlDataAdapter("select * from subscribe_topic", con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    subscribeTopic.DataSource = ds.Tables[0];
                    mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                    viewDataTable();
                }
                catch (MqttConnectionException ex)
                {
                    MessageBox.Show("Error connecting to MQTT broker: " + ex.Message);
                }
            }
            else
            {
                mqttClient.Disconnect();
                Connbtn.Text = "Connect";
                statLbl.Text = "Not Connected to broker";
                connect = false;
            }
        }

        private void subBtn_Click(object sender, EventArgs e)
        {
            if (connect)
            {
                if (!string.IsNullOrEmpty(topicBox.Text))
                {
                    string topic = topicBox.Text.Trim();

                    if (!string.IsNullOrEmpty(topic) && !subscribedTopics.Contains(topic))
                    {
                        mqttClient.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                        subscribedTopics.Add(topic);
                        try
                        {
                            con.Open();
                            string insertQuery = "INSERT INTO subscribe_topic (Topic, Status) VALUES (@topic, @status)";
                            MySqlCommand cmd = new MySqlCommand(insertQuery, con);
                            cmd.Parameters.AddWithValue("@Topic", topic);
                            cmd.Parameters.AddWithValue("@Status", 1);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Topic inserted into subscribe_topic table!");

                            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM subscribe_topic", con);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            subscribeTopic.DataSource = dt;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error inserting into subscribe_topic table: " + ex.Message);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter a topic to subscribe");
                }
            }
            else
            {
                MessageBox.Show("First connect to the broker");
            }
        }

        private void ToggleSubscriptionStatus(int rowIndex)
        {
            try
            {
                con.Open();

                // Get the topic and current status from the DataGridView
                string topic = subscribeTopic.Rows[rowIndex].Cells["Topic"].Value.ToString();
                int currentStatus = Convert.ToInt32(subscribeTopic.Rows[rowIndex].Cells["Status"].Value);

                // Toggle the status (0 to 1, 1 to 0)
                int newStatus = 1 - currentStatus;

                // Update the status in the DataGridView
                subscribeTopic.Rows[rowIndex].Cells["Status"].Value = newStatus;

                // Update the status in the database
                string updateQuery = "UPDATE subscribe_topic SET Status = @Status WHERE Topic = @Topic";
                MySqlCommand cmd = new MySqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@Topic", topic);
                cmd.ExecuteNonQuery();

                // Subscribe or unsubscribe based on the new status
                if (newStatus == 1)
                {
                    mqttClient.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                }
                else
                {
                    mqttClient.Unsubscribe(new string[] { topic });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating status: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        private void LoadIds()
        {
            try
            {
                IDs.Clear();
                colHeader.Items.Clear();

                string query = "SELECT DeviceID FROM users";
                MySqlCommand cmd = new MySqlCommand(query, con);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string id = reader["DeviceID"].ToString();
                        IDs.Add(id);
                    }
                    colHeader.Items.AddRange(IDs.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Device IDs: " + ex.Message);
            }
        }

        private void LoadSubscribedTopics()
        {
            try
            {
                subscribedTopics.Clear();
                topicValue.Items.Clear();

                string query = "SELECT Topic FROM subscribe_topic";
                MySqlCommand cmd = new MySqlCommand(query, con);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string topic = reader["Topic"].ToString();
                        subscribedTopics.Add(topic);
                        // Subscribe to the topic
                        mqttClient.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    }
                    topicValue.Items.AddRange(subscribedTopics.ToArray());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading subscribed topics: " + ex.Message);
            }
        }

        private void LoadUsersData()
        {
            try
            {
                string query = "SELECT * FROM users";
                MySqlDataAdapter cmd = new MySqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                cmd.Fill(dt);
                users1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message);
            }
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if (connect)
            {
                string messageText = System.Text.Encoding.UTF8.GetString(e.Message);
                string topic = e.Topic;
                List<string> parts = new List<string>(messageText.Split(','));
                BeginInvoke((Action)(() =>
                {
                    try
                    {
                        con.Open();
                        string insertQuery = "INSERT INTO bmcmqtt (DeviceId,BMCODE,Temperature,Pressure,Volume,Level,Generator,Grid,Aggregate,Compressor1,compressor2,VoltageU,VoltageV,VoltageW,CurrentU,CurrentV,CurrentW,Frequency,PwrF,TPwr,Time,Date,Topic) VALUES (@DeviceId,@BMCODE,@Temp,@Press,@Vol,@Lvl,@Gen,@Grid,@Agr,@Comp1,@Comp2,@VoltU,@VoltV,@VoltW,@CurrU,@CurrV,@CurrW,@Freq,@PwrF,@TPwr,@Time,@Date,@Topic)";
                        MySqlCommand cmd = new MySqlCommand(insertQuery, con);
                        cmd.Parameters.AddWithValue("@DeviceId", deviceId);
                        cmd.Parameters.AddWithValue("@BMCODE", parts[0]);
                        cmd.Parameters.AddWithValue("@Temp", parts[1]);
                        cmd.Parameters.AddWithValue("@Press", parts[2]);
                        cmd.Parameters.AddWithValue("@Vol", parts[3]);
                        cmd.Parameters.AddWithValue("@Lvl", parts[4]);
                        cmd.Parameters.AddWithValue("@Gen", parts[5]);
                        cmd.Parameters.AddWithValue("@Grid", parts[6]);
                        cmd.Parameters.AddWithValue("@Agr", parts[7]);
                        cmd.Parameters.AddWithValue("@Comp1", parts[8]);
                        cmd.Parameters.AddWithValue("@Comp2", parts[9]);
                        cmd.Parameters.AddWithValue("@VoltU", parts[10]);
                        cmd.Parameters.AddWithValue("@VoltV", parts[11]);
                        cmd.Parameters.AddWithValue("@VoltW", parts[12]);
                        cmd.Parameters.AddWithValue("@CurrU", parts[13]);
                        cmd.Parameters.AddWithValue("@CurrV", parts[14]);
                        cmd.Parameters.AddWithValue("@CurrW", parts[15]);
                        cmd.Parameters.AddWithValue("@Freq", parts[16]);
                        cmd.Parameters.AddWithValue("@Pwrf", parts[17]);
                        cmd.Parameters.AddWithValue("@TPwr", parts[18]);
                        cmd.Parameters.AddWithValue("@Time", parts[19]);
                        cmd.Parameters.AddWithValue("@Date", parts[20].Substring(0, 12));
                        cmd.Parameters.AddWithValue("@Topic", topic);
                        cmd.ExecuteNonQuery();

                        // Retrieve data from the database and update the DataGridView
                        MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM bmcmqtt", con);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        serverData.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error inserting into bmcmqtt table: {ex.Message}");
                    }
                    finally
                    {
                        con.Close();
                    }
                }));
            }
        }

        void viewDataTable()
        {
            try
            {
                con.Open();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open connection!");
            }
            MySqlDataAdapter da = new MySqlDataAdapter("select * from bmcmqtt", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            serverData.DataSource = ds.Tables[0];
        }

        private void addUser_Click(object sender, EventArgs e)
        {
            if (connect)
            {
                if(!string.IsNullOrEmpty(usernameValue.Text) && !string.IsNullOrEmpty(passwordValue.Text) && !string.IsNullOrEmpty(topicValue.Text))
                {
                    string username = usernameValue.Text;
                    string password = passwordValue.Text;
                    string topic = topicValue.SelectedItem.ToString();

                    string deviceId = GenerateRandomString(5);

                    InsertUserData(username, password, deviceId, topic);

                    usernameValue.Clear();
                    passwordValue.Clear();
                    topicValue.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Details are required!!");
                }
            }
            else
            {
                MessageBox.Show("First connect to broker");
            }

        }

        private void InsertUserData(string username, string password, string deviceId, string topic)
        {
            try
            {
                con.Open();
                string query = "INSERT INTO users (username, password, Topics, DeviceID, Status) VALUES (@username, @password, @topic, @deviceid, @status)";

                using (MySqlCommand command = new MySqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@deviceid", deviceId);
                    command.Parameters.AddWithValue("@topic", topic);
                    command.Parameters.AddWithValue("@status", 1);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User added successfully!");
                        string updateStatusQuery = "UPDATE users SET status = 1 WHERE DeviceID = @deviceId";
                        using (MySqlCommand updateStatusCommand = new MySqlCommand(updateStatusQuery, con))
                        {
                            updateStatusCommand.Parameters.AddWithValue("@deviceId", deviceId);
                            updateStatusCommand.ExecuteNonQuery();
                        }
                        LoadUsersData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add user. Please check your inputs.");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }

        private void ToggleUserStatus(int rowIndex)
        {
            try
            {
                con.Open();

                // Get the topic and current status from the DataGridView
                string device = users1.Rows[rowIndex].Cells["DeviceID"].Value.ToString();
                int currentStatus = Convert.ToInt32(users1.Rows[rowIndex].Cells["Status"].Value);

                // Toggle the status (0 to 1, 1 to 0)
                int newStatus = 1 - currentStatus;

                // Update the status in the DataGridView
                users1.Rows[rowIndex].Cells["Status"].Value = newStatus;

                // Update the status in the database
                string updateQuery = "UPDATE users SET Status = @Status WHERE DeviceID = @device";
                MySqlCommand cmd = new MySqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@device", device);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating status: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
