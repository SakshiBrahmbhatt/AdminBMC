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
            filterMyData = new CheckBox();
            searchText = new TextBox();
            searchBtn = new Button();
            users = new DataGridView();
            addUser = new Button();
            topicBox = new TextBox();
            Connbtn = new Button();
            subBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)serverData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)subscribeTopic).BeginInit();
            ((System.ComponentModel.ISupportInitialize)users).BeginInit();
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
            subscribeTopic.CellContentClick += subscribeTopic_CellContentClick;
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
            // filterMyData
            // 
            filterMyData.AutoSize = true;
            filterMyData.Location = new Point(12, 237);
            filterMyData.Name = "filterMyData";
            filterMyData.Size = new Size(157, 24);
            filterMyData.TabIndex = 10;
            filterMyData.Text = "Only show my data";
            filterMyData.UseVisualStyleBackColor = true;
            // 
            // searchText
            // 
            searchText.Location = new Point(214, 238);
            searchText.Name = "searchText";
            searchText.PlaceholderText = "topic/#";
            searchText.Size = new Size(166, 27);
            searchText.TabIndex = 11;
            // 
            // searchBtn
            // 
            searchBtn.Location = new Point(386, 237);
            searchBtn.Name = "searchBtn";
            searchBtn.Size = new Size(94, 29);
            searchBtn.TabIndex = 12;
            searchBtn.Text = "Search";
            searchBtn.UseVisualStyleBackColor = true;
            // 
            // users
            // 
            users.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            users.BackgroundColor = SystemColors.InactiveCaption;
            users.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            users.Location = new Point(869, 12);
            users.Name = "users";
            users.RowHeadersWidth = 51;
            users.Size = new Size(455, 187);
            users.TabIndex = 13;
            // 
            // addUser
            // 
            addUser.Location = new Point(869, 205);
            addUser.Name = "addUser";
            addUser.Size = new Size(138, 29);
            addUser.TabIndex = 14;
            addUser.Text = "Add New User";
            addUser.UseVisualStyleBackColor = true;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1792, 554);
            Controls.Add(subBtn);
            Controls.Add(Connbtn);
            Controls.Add(topicBox);
            Controls.Add(addUser);
            Controls.Add(users);
            Controls.Add(searchBtn);
            Controls.Add(searchText);
            Controls.Add(filterMyData);
            Controls.Add(statLbl);
            Controls.Add(subscribeTopic);
            Controls.Add(serverData);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)serverData).EndInit();
            ((System.ComponentModel.ISupportInitialize)subscribeTopic).EndInit();
            ((System.ComponentModel.ISupportInitialize)users).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView serverData;
        private DataGridView subscribeTopic;
        private Label statLbl;
        private CheckBox filterMyData;
        private TextBox searchText;
        private Button searchBtn;
        private DataGridView users;
        private Button addUser;
        private TextBox topicBox;
        private Button Connbtn;
        private Button subBtn;
    }
}
