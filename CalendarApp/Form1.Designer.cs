namespace CalendarApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            this.lstUserAppointments = new System.Windows.Forms.ListBox();
            this.lstUserReminders = new System.Windows.Forms.ListBox();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(49, 74);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(79, 16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên lịch hẹn";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(181, 68);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 22);
            this.txtName.TabIndex = 1;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(49, 121);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 16);
            this.lblLocation.TabIndex = 2;
            this.lblLocation.Text = "Địa Điểm";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(181, 115);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(200, 22);
            this.txtLocation.TabIndex = 3;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(49, 166);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(111, 16);
            this.lblStartTime.TabIndex = 4;
            this.lblStartTime.Text = "Thời gian bắt đầu";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(181, 166);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(200, 22);
            this.dtpStartTime.TabIndex = 5;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(181, 209);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(200, 22);
            this.dtpEndTime.TabIndex = 6;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(49, 209);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(111, 16);
            this.lblEndTime.TabIndex = 7;
            this.lblEndTime.Text = "Thời gian kết thúc";
            // 
            // chkEnableReminder
            // 
            this.chkEnableReminder.AutoSize = true;
            this.chkEnableReminder.Location = new System.Drawing.Point(55, 250);
            this.chkEnableReminder.Name = "chkEnableReminder";
            this.chkEnableReminder.Size = new System.Drawing.Size(134, 20);
            this.chkEnableReminder.TabIndex = 8;
            this.chkEnableReminder.Text = "Enable Reminder";
            this.chkEnableReminder.UseVisualStyleBackColor = true;
            // 
            // lblReminderTime
            // 
            this.lblReminderTime.AutoSize = true;
            this.lblReminderTime.Location = new System.Drawing.Point(52, 290);
            this.lblReminderTime.Name = "lblReminderTime";
            this.lblReminderTime.Size = new System.Drawing.Size(120, 16);
            this.lblReminderTime.TabIndex = 9;
            this.lblReminderTime.Text = "Thời gian nhắc nhở";
            this.lblReminderTime.Visible = false;
            // 
            // dtpReminderTime
            // 
            this.dtpReminderTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpReminderTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReminderTime.Location = new System.Drawing.Point(181, 284);
            this.dtpReminderTime.Name = "dtpReminderTime";
            this.dtpReminderTime.Size = new System.Drawing.Size(200, 22);
            this.dtpReminderTime.TabIndex = 10;
            this.dtpReminderTime.Visible = false;
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.Location = new System.Drawing.Point(55, 330);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.Size = new System.Drawing.Size(142, 49);
            this.btnAddAppointment.TabIndex = 11;
            this.btnAddAppointment.Text = "Thêm lịch hẹn";
            this.btnAddAppointment.UseVisualStyleBackColor = true;
            this.btnAddAppointment.Click += new System.EventHandler(this.btnAddAppointment_Click);
            // 
            // lstUserAppointments
            // 
            this.lstUserAppointments.FormattingEnabled = true;
            this.lstUserAppointments.ItemHeight = 16;
            this.lstUserAppointments.Location = new System.Drawing.Point(399, 32);
            this.lstUserAppointments.Name = "lstUserAppointments";
            this.lstUserAppointments.Size = new System.Drawing.Size(502, 500);
            this.lstUserAppointments.TabIndex = 12;
            // 
            // lstUserReminders
            // 
            this.lstUserReminders.FormattingEnabled = true;
            this.lstUserReminders.ItemHeight = 16;
            this.lstUserReminders.Location = new System.Drawing.Point(907, 32);
            this.lstUserReminders.Name = "lstUserReminders";
            this.lstUserReminders.Size = new System.Drawing.Size(420, 500);
            this.lstUserReminders.TabIndex = 13;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Location = new System.Drawing.Point(52, 32);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(76, 16);
            this.lblCurrentUser.TabIndex = 14;
            this.lblCurrentUser.Text = "Người dùng";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 709);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.lstUserReminders);
            this.Controls.Add(this.lstUserAppointments);
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
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.ListBox lstUserAppointments;
        private System.Windows.Forms.ListBox lstUserReminders;
        private System.Windows.Forms.Label lblCurrentUser;
    }
}

