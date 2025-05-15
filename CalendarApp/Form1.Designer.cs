namespace CalendarApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.chkEnableReminder = new System.Windows.Forms.CheckBox();
            this.lblReminderTime = new System.Windows.Forms.Label();
            this.dtpReminderTime = new System.Windows.Forms.DateTimePicker();
            this.btnAddAppointment = new System.Windows.Forms.Button();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.exitBtn = new System.Windows.Forms.Button();
            this.btnAddGroupMeeting = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(74, 116);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(130, 25);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên lịch hẹn";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(272, 106);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(298, 31);
            this.txtName.TabIndex = 1;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(74, 189);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(99, 25);
            this.lblLocation.TabIndex = 2;
            this.lblLocation.Text = "Địa Điểm";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(272, 180);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(298, 31);
            this.txtLocation.TabIndex = 3;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(74, 259);
            this.lblStartTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(179, 25);
            this.lblStartTime.TabIndex = 4;
            this.lblStartTime.Text = "Thời gian bắt đầu";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(272, 259);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(298, 31);
            this.dtpStartTime.TabIndex = 5;
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(272, 327);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(298, 31);
            this.dtpEndTime.TabIndex = 6;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(74, 327);
            this.lblEndTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(183, 25);
            this.lblEndTime.TabIndex = 7;
            this.lblEndTime.Text = "Thời gian kết thúc";
            // 
            // chkEnableReminder
            // 
            this.chkEnableReminder.AutoSize = true;
            this.chkEnableReminder.Location = new System.Drawing.Point(82, 391);
            this.chkEnableReminder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkEnableReminder.Name = "chkEnableReminder";
            this.chkEnableReminder.Size = new System.Drawing.Size(209, 29);
            this.chkEnableReminder.TabIndex = 8;
            this.chkEnableReminder.Text = "Enable Reminder";
            this.chkEnableReminder.UseVisualStyleBackColor = true;
            // 
            // lblReminderTime
            // 
            this.lblReminderTime.AutoSize = true;
            this.lblReminderTime.Location = new System.Drawing.Point(78, 453);
            this.lblReminderTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReminderTime.Name = "lblReminderTime";
            this.lblReminderTime.Size = new System.Drawing.Size(196, 25);
            this.lblReminderTime.TabIndex = 9;
            this.lblReminderTime.Text = "Thời gian nhắc nhở";
            this.lblReminderTime.Visible = false;
            // 
            // dtpReminderTime
            // 
            this.dtpReminderTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpReminderTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReminderTime.Location = new System.Drawing.Point(272, 444);
            this.dtpReminderTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpReminderTime.Name = "dtpReminderTime";
            this.dtpReminderTime.Size = new System.Drawing.Size(298, 31);
            this.dtpReminderTime.TabIndex = 10;
            this.dtpReminderTime.Visible = false;
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.Location = new System.Drawing.Point(357, 557);
            this.btnAddAppointment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.Size = new System.Drawing.Size(213, 77);
            this.btnAddAppointment.TabIndex = 11;
            this.btnAddAppointment.Text = "Thêm lịch hẹn";
            this.btnAddAppointment.UseVisualStyleBackColor = true;
            this.btnAddAppointment.Click += new System.EventHandler(this.btnAddAppointment_Click);
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Location = new System.Drawing.Point(78, 50);
            this.lblCurrentUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(122, 25);
            this.lblCurrentUser.TabIndex = 14;
            this.lblCurrentUser.Text = "Người dùng";
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(211, 715);
            this.exitBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(213, 77);
            this.exitBtn.TabIndex = 15;
            this.exitBtn.Text = "Đăng xuất";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // btnAddGroupMeeting
            // 
            this.btnAddGroupMeeting.Location = new System.Drawing.Point(83, 557);
            this.btnAddGroupMeeting.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddGroupMeeting.Name = "btnAddGroupMeeting";
            this.btnAddGroupMeeting.Size = new System.Drawing.Size(228, 77);
            this.btnAddGroupMeeting.TabIndex = 16;
            this.btnAddGroupMeeting.Text = "Thêm lịch hẹn nhóm";
            this.btnAddGroupMeeting.UseVisualStyleBackColor = true;
            this.btnAddGroupMeeting.Click += new System.EventHandler(this.btnAddGroupMeeting_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(1468, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(597, 779);
            this.dataGridView1.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1709, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 25);
            this.label1.TabIndex = 18;
            this.label1.Text = "Nhắc nhở";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(896, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 25);
            this.label2.TabIndex = 19;
            this.label2.Text = "Danh sách cuộc họp";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(614, 106);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 82;
            this.dataGridView2.RowTemplate.Height = 33;
            this.dataGridView2.Size = new System.Drawing.Size(816, 779);
            this.dataGridView2.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2113, 1108);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnAddGroupMeeting);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.btnAddAppointment);
            this.Controls.Add(this.dtpReminderTime);
            this.Controls.Add(this.lblReminderTime);
            this.Controls.Add(this.chkEnableReminder);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.txtLocation);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.CheckBox chkEnableReminder;
        private System.Windows.Forms.Label lblReminderTime;
        private System.Windows.Forms.DateTimePicker dtpReminderTime;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button btnAddGroupMeeting;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}

