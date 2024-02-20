namespace AdminBMC
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            serverData = new DataGridView();
            subscribeTopic = new DataGridView();
            statLbl = new Label();
            searchText = new TextBox();
            searchBtn = new Button();
            users1 = new DataGridView();
            addUser = new Button();
            topicBox = new TextBox();
            Connbtn = new Button();
            subBtn = new Button();
            groupBox1 = new GroupBox();
            topicValue = new ComboBox();
            label3 = new Label();
            passwordValue = new TextBox();
            label2 = new Label();
            usernameValue = new TextBox();
            label1 = new Label();
            colHeader = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)serverData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)subscribeTopic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)users1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // serverData
            // 
            serverData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            serverData.BackgroundColor = SystemColors.InactiveCaption;
            serverData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            serverData.Location = new Point(12, 272);
            serverData.Name = "serverData";
            serverData.RowHeadersWidth = 51;
            serverData.Size = new Size(1768, 270);
            serverData.TabIndex = 5;
            // 
            // subscribeTopic
            // 
            subscribeTopic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            subscribeTopic.BackgroundColor = SystemColors.InactiveCaption;
            subscribeTopic.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            subscribeTopic.Location = new Point(361, 12);
            subscribeTopic.Name = "subscribeTopic";
            subscribeTopic.RowHeadersWidth = 51;
            subscribeTopic.Size = new Size(455, 187);
            subscribeTopic.TabIndex = 8;
            // 
            // statLbl
            // 
            statLbl.AutoSize = true;
            statLbl.Location = new Point(122, 16);
            statLbl.Name = "statLbl";
            statLbl.Size = new Size(172, 20);
            statLbl.TabIndex = 9;
            statLbl.Text = "Not connected to broker";
            // 
            // searchText
            // 
            searchText.Location = new Point(198, 238);
            searchText.Name = "searchText";
            searchText.PlaceholderText = "topic/#";
            searchText.Size = new Size(166, 27);
            searchText.TabIndex = 11;
            // 
            // searchBtn
            // 
            searchBtn.Location = new Point(370, 236);
            searchBtn.Name = "searchBtn";
            searchBtn.Size = new Size(94, 29);
            searchBtn.TabIndex = 12;
            searchBtn.Text = "Search";
            searchBtn.UseVisualStyleBackColor = true;
            searchBtn.Click += searchBtn_Click;
            // 
            // users1
            // 
            users1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            users1.BackgroundColor = SystemColors.InactiveCaption;
            users1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            users1.Location = new Point(869, 12);
            users1.Name = "users1";
            users1.RowHeadersWidth = 51;
            users1.Size = new Size(455, 187);
            users1.TabIndex = 13;
            // 
            // addUser
            // 
            addUser.Location = new Point(29, 177);
            addUser.Name = "addUser";
            addUser.Size = new Size(138, 29);
            addUser.TabIndex = 14;
            addUser.Text = "Add New User";
            addUser.UseVisualStyleBackColor = true;
            addUser.Click += addUser_Click;
            // 
            // topicBox
            // 
            topicBox.Location = new Point(122, 61);
            topicBox.Name = "topicBox";
            topicBox.PlaceholderText = "topic/#";
            topicBox.Size = new Size(166, 27);
            topicBox.TabIndex = 15;
            // 
            // Connbtn
            // 
            Connbtn.Location = new Point(12, 12);
            Connbtn.Name = "Connbtn";
            Connbtn.Size = new Size(94, 29);
            Connbtn.TabIndex = 16;
            Connbtn.Text = "Connect";
            Connbtn.UseVisualStyleBackColor = true;
            Connbtn.Click += Connbtn_Click;
            // 
            // subBtn
            // 
            subBtn.Location = new Point(12, 59);
            subBtn.Name = "subBtn";
            subBtn.Size = new Size(94, 29);
            subBtn.TabIndex = 17;
            subBtn.Text = "Subscribe";
            subBtn.UseVisualStyleBackColor = true;
            subBtn.Click += subBtn_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(topicValue);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(passwordValue);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(addUser);
            groupBox1.Controls.Add(usernameValue);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(1367, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(304, 230);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Add User";
            // 
            // topicValue
            // 
            topicValue.FormattingEnabled = true;
            topicValue.Location = new Point(118, 130);
            topicValue.Name = "topicValue";
            topicValue.Size = new Size(151, 28);
            topicValue.TabIndex = 5;
            topicValue.Text = "Select";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 133);
            label3.Name = "label3";
            label3.Size = new Size(51, 20);
            label3.TabIndex = 4;
            label3.Text = "Topics";
            // 
            // passwordValue
            // 
            passwordValue.Location = new Point(118, 85);
            passwordValue.Name = "passwordValue";
            passwordValue.Size = new Size(151, 27);
            passwordValue.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 88);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 2;
            label2.Text = "Password";
            // 
            // usernameValue
            // 
            usernameValue.Location = new Point(118, 39);
            usernameValue.Name = "usernameValue";
            usernameValue.Size = new Size(151, 27);
            usernameValue.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 42);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // colHeader
            // 
            colHeader.FormattingEnabled = true;
            colHeader.Location = new Point(23, 238);
            colHeader.Name = "colHeader";
            colHeader.Size = new Size(151, 28);
            colHeader.TabIndex = 19;
            colHeader.Text = "Select Device ID";
            colHeader.SelectedIndexChanged += colHeader_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1792, 554);
            Controls.Add(colHeader);
            Controls.Add(groupBox1);
            Controls.Add(subBtn);
            Controls.Add(Connbtn);
            Controls.Add(topicBox);
            Controls.Add(users1);
            Controls.Add(searchBtn);
            Controls.Add(searchText);
            Controls.Add(statLbl);
            Controls.Add(subscribeTopic);
            Controls.Add(serverData);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)serverData).EndInit();
            ((System.ComponentModel.ISupportInitialize)subscribeTopic).EndInit();
            ((System.ComponentModel.ISupportInitialize)users1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView serverData;
        private DataGridView subscribeTopic;
        private Label statLbl;
        private TextBox searchText;
        private Button searchBtn;
        private DataGridView users1;
        private Button addUser;
        private TextBox topicBox;
        private Button Connbtn;
        private Button subBtn;
        private GroupBox groupBox1;
        private ComboBox topicValue;
        private Label label3;
        private TextBox passwordValue;
        private Label label2;
        private TextBox usernameValue;
        private Label label1;
        private ComboBox colHeader;
    }
}
