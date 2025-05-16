namespace CalendarApp
{
    partial class addAppointment
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
            this.btnAddGroupMeeting = new System.Windows.Forms.Button();
            this.btnAddAppointment = new System.Windows.Forms.Button();
            this.dtpReminderTime = new System.Windows.Forms.DateTimePicker();
            this.lblReminderTime = new System.Windows.Forms.Label();
            this.chkEnableReminder = new System.Windows.Forms.CheckBox();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddGroupMeeting
            // 
            this.btnAddGroupMeeting.Location = new System.Drawing.Point(161, 668);
            this.btnAddGroupMeeting.Margin = new System.Windows.Forms.Padding(7);
            this.btnAddGroupMeeting.Name = "btnAddGroupMeeting";
            this.btnAddGroupMeeting.Size = new System.Drawing.Size(341, 89);
            this.btnAddGroupMeeting.TabIndex = 30;
            this.btnAddGroupMeeting.Text = "Thêm lịch hẹn nhóm";
            this.btnAddGroupMeeting.UseVisualStyleBackColor = true;
            this.btnAddGroupMeeting.Click += new System.EventHandler(this.btnAddGroupMeeting_Click);
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.Location = new System.Drawing.Point(685, 668);
            this.btnAddAppointment.Margin = new System.Windows.Forms.Padding(7);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.Size = new System.Drawing.Size(339, 89);
            this.btnAddAppointment.TabIndex = 28;
            this.btnAddAppointment.Text = "Thêm lịch hẹn";
            this.btnAddAppointment.UseVisualStyleBackColor = true;
            this.btnAddAppointment.Click += new System.EventHandler(this.btnAddAppointment_Click);
            // 
            // dtpReminderTime
            // 
            this.dtpReminderTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpReminderTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpReminderTime.Location = new System.Drawing.Point(497, 571);
            this.dtpReminderTime.Margin = new System.Windows.Forms.Padding(7);
            this.dtpReminderTime.Name = "dtpReminderTime";
            this.dtpReminderTime.Size = new System.Drawing.Size(527, 38);
            this.dtpReminderTime.TabIndex = 27;
            // 
            // lblReminderTime
            // 
            this.lblReminderTime.AutoSize = true;
            this.lblReminderTime.Location = new System.Drawing.Point(155, 577);
            this.lblReminderTime.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblReminderTime.Name = "lblReminderTime";
            this.lblReminderTime.Size = new System.Drawing.Size(243, 31);
            this.lblReminderTime.TabIndex = 26;
            this.lblReminderTime.Text = "Thời gian nhắc nhở";
            // 
            // chkEnableReminder
            // 
            this.chkEnableReminder.AutoSize = true;
            this.chkEnableReminder.Location = new System.Drawing.Point(161, 504);
            this.chkEnableReminder.Margin = new System.Windows.Forms.Padding(7);
            this.chkEnableReminder.Name = "chkEnableReminder";
            this.chkEnableReminder.Size = new System.Drawing.Size(254, 35);
            this.chkEnableReminder.TabIndex = 25;
            this.chkEnableReminder.Text = "Enable Reminder";
            this.chkEnableReminder.UseVisualStyleBackColor = true;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(155, 422);
            this.lblEndTime.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(228, 31);
            this.lblEndTime.TabIndex = 24;
            this.lblEndTime.Text = "Thời gian kết thúc";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(497, 416);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(7);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(527, 38);
            this.dtpEndTime.TabIndex = 23;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(497, 326);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(7);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(527, 38);
            this.dtpStartTime.TabIndex = 22;
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(155, 332);
            this.lblStartTime.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(222, 31);
            this.lblStartTime.TabIndex = 21;
            this.lblStartTime.Text = "Thời gian bắt đầu";
            // 
            // txtLocation
            // 
            this.txtLocation.Location = new System.Drawing.Point(497, 226);
            this.txtLocation.Margin = new System.Windows.Forms.Padding(7);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(527, 38);
            this.txtLocation.TabIndex = 20;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(155, 233);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(125, 31);
            this.lblLocation.TabIndex = 19;
            this.lblLocation.Text = "Địa Điểm";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(497, 138);
            this.txtName.Margin = new System.Windows.Forms.Padding(7);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(527, 38);
            this.txtName.TabIndex = 18;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(145, 154);
            this.lblName.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(161, 31);
            this.lblName.TabIndex = 17;
            this.lblName.Text = "Tên lịch hẹn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(411, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(304, 42);
            this.label3.TabIndex = 31;
            this.label3.Text = "Add Appointment";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(465, 813);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(7);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(250, 78);
            this.cancelBtn.TabIndex = 29;
            this.cancelBtn.Text = "Hủy";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // addAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1174, 975);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddGroupMeeting);
            this.Controls.Add(this.cancelBtn);
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
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(1200, 1046);
            this.MinimumSize = new System.Drawing.Size(1200, 1046);
            this.Name = "addAppointment";
            this.Text = "addAppointment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAddGroupMeeting;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.DateTimePicker dtpReminderTime;
        private System.Windows.Forms.Label lblReminderTime;
        private System.Windows.Forms.CheckBox chkEnableReminder;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cancelBtn;
    }
}