// Form1.cs
using System;
using System.Linq;
using System.Windows.Forms;
using CalendarApp.Models;
using CalendarApp.Services;
using CalendarApp.Data; // Chỉ dùng nếu cần truy cập trực tiếp DbContext (hạn chế)

namespace CalendarApp
{
    public partial class Form1 : Form
    {
        private User _loggedInUser;
        private string _loggedInUsername = "user1"; // Mặc định để test

        public Form1()
        {
            InitializeComponent();
            // Đảm bảo định dạng DateTimePickers trong trình thiết kế Form:
            // dtpStartTime.CustomFormat = "dd/MM/yyyy HH:mm";
            // dtpEndTime.CustomFormat = "dd/MM/yyyy HH:mm";
            // dtpReminderTime.CustomFormat = "dd/MM/yyyy HH:mm";
            // dtpStartTime.Format = DateTimePickerFormat.Custom;
            // dtpEndTime.Format = DateTimePickerFormat.Custom;
            // dtpReminderTime.Format = DateTimePickerFormat.Custom;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool loaded = LoadInitialData();
            if (loaded)
            {
                InitializeDateTimePickers();
                chkEnableReminder_CheckedChanged(null, null);
            }
            else
            {
                MessageBox.Show("Failed to load initial data. Application cannot continue.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAddAppointment.Enabled = false; // Ví dụ: Vô hiệu hóa nút
            }
        }

        private bool LoadInitialData()
        {
            try
            {
                using (var initialDb = new CalendarDbContext())
                { // Context tạm thời
                    _loggedInUser = initialDb.Users.FirstOrDefault(u => u.Username == _loggedInUsername);
                }
                if (_loggedInUser != null)
                {
                    lblCurrentUser.Text = $"Current User: {_loggedInUser.Username}";
                    RefreshLists();
                    return true;
                }
                else
                {
                    MessageBox.Show($"User '{_loggedInUsername}' not found.", "Init Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB Error on load:\n{ex.Message}", "DB Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"DB Load Error: {ex}"); return false;
            }
        }

        private void InitializeDateTimePickers()
        {
            var now = DateTime.Now;
            var nextQuarterHour = now.AddMinutes(15 - (now.Minute % 15)).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
            if (nextQuarterHour <= now) nextQuarterHour = nextQuarterHour.AddMinutes(15);
            dtpStartTime.Value = nextQuarterHour;
            dtpEndTime.Value = dtpStartTime.Value.AddHours(1);
            // Đảm bảo reminder time dựa trên start time mới
            dtpReminderTime.Value = dtpStartTime.Value.AddMinutes(-15);
        }

        private void chkEnableReminder_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkEnableReminder.Checked;
            dtpReminderTime.Visible = isChecked;
            lblReminderTime.Visible = isChecked;
            if (isChecked)
            {
                // Luôn cập nhật giá trị reminder dựa trên startTime hiện tại khi bật
                dtpReminderTime.Value = dtpStartTime.Value.AddMinutes(-15);
                // Kiểm tra lại nếu giá trị không hợp lệ (hiếm khi xảy ra với logic trên)
                if (dtpReminderTime.Value >= dtpStartTime.Value)
                {
                    dtpReminderTime.Value = dtpStartTime.Value.AddMinutes(-1); // Đảm bảo luôn trước
                }
            }
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (_loggedInUser == null) { MessageBox.Show("No user loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            string name = txtName.Text.Trim();
            string location = txtLocation.Text.Trim();
            DateTime startTime = dtpStartTime.Value;
            DateTime endTime = dtpEndTime.Value;
            DateTime? reminderTime = chkEnableReminder.Checked ? (DateTime?)dtpReminderTime.Value : null;

            // --- UI Validation ---
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Name required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtName.Focus(); return; }
            if (endTime <= startTime) { MessageBox.Show("End time must be after start.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtpEndTime.Focus(); return; }
            // Validation Reminder Time (Kiểm tra cả khi checkbox được check)
            if (chkEnableReminder.Checked && dtpReminderTime.Value >= dtpStartTime.Value)
            {
                MessageBox.Show("Reminder time must be before the start time. Please adjust.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpReminderTime.Focus();
                return;
            }
            // Optional: Gán lại reminderTime sau khi validate phòng trường hợp người dùng sửa tay
            if (chkEnableReminder.Checked) reminderTime = dtpReminderTime.Value;


            // --- Call CalendarManager ---
            try
            {
                using (var manager = new CalendarManager(_loggedInUser.Username))
                {
                    var result = manager.AddAppointment(name, location, startTime, endTime, reminderTime);
                    HandleManagerResult(manager, result, name, location, startTime, endTime, reminderTime);
                }
            }
            catch (InvalidOperationException userEx)
            {
                MessageBox.Show(userEx.Message, "Operation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Operation Error: {ex}");
            }
        }

        private void HandleManagerResult(CalendarManager manager, (AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result,
                                        string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            switch (result.Status)
            {
                case AppointmentActionResult.Success:
                    MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields(); RefreshLists(); break;
                case AppointmentActionResult.ConflictDetected:
                    HandleConflict(manager, result, name, location, startTime, endTime, reminderTime); break;
                case AppointmentActionResult.AskToJoinGroupMeeting:
                    HandleAskToJoin(manager, result, name, location, startTime, endTime, reminderTime); break;
                case AppointmentActionResult.ValidationError: // Lỗi validation từ Manager (bao gồm cả reminder)
                case AppointmentActionResult.DatabaseError:
                case AppointmentActionResult.UnknownError:
                default:
                    MessageBox.Show(result.Message, "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning); break;
            }
        }

        private void HandleAskToJoin(CalendarManager manager, (AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result,
                                     string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            if (!int.TryParse(result.Message.Split(':')[1], out int meetingId)) { MessageBox.Show("Error parsing meeting ID.", "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            string meetingName = (result.RelatedAppointment as GroupMeeting)?.Name ?? $"Meeting ID {meetingId}";
            var joinDialogResult = MessageBox.Show($"Join existing group meeting '{meetingName}'?", "Join Meeting?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (joinDialogResult == DialogResult.Yes)
            {
                bool joined = manager.ConfirmJoinGroupMeeting(meetingId, manager.GetCurrentUser());
                if (joined) { MessageBox.Show($"Joined: {meetingName}", "Joined", MessageBoxButtons.OK, MessageBoxIcon.Information); ClearInputFields(); RefreshLists(); }
                else { MessageBox.Show("Failed to join meeting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            { // User chose No -> Tạo lịch hẹn cá nhân
                MessageBox.Show("Creating new individual appointment.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                { // Dùng lại context của manager hiện tại
                    Appointment newAppt = new Appointment(name, location, startTime, endTime, manager.GetCurrentUser().Id) { Owner = manager.GetCurrentUser() };
                    manager.Context.Appointments.Add(newAppt);
                    if (reminderTime.HasValue && manager.IsValidReminderTime(reminderTime.Value, startTime))
                    {
                        Reminder newReminder = new Reminder(reminderTime.Value, 0, manager.GetCurrentUser().Id) { User = manager.GetCurrentUser(), RelatedAppointment = newAppt };
                        manager.Context.Reminders.Add(newReminder);
                    }
                    manager.Context.SaveChanges();
                    MessageBox.Show("New individual appointment created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields(); RefreshLists();
                }
                catch (Exception ex) { MessageBox.Show($"Error creating individual appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void HandleConflict(CalendarManager manager, (AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result,
                                    string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            var conflictDialogResult = MessageBox.Show(result.Message + "\n\nReplace existing appointment?", "Conflict", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (conflictDialogResult == DialogResult.Yes)
            { // Replace
                var replaceResult = manager.PerformReplaceAppointment(result.RelatedAppointment, name, location, startTime, endTime, reminderTime);
                if (replaceResult.success) { MessageBox.Show(replaceResult.message, "Replaced", MessageBoxButtons.OK, MessageBoxIcon.Information); ClearInputFields(); RefreshLists(); }
                else { MessageBox.Show("Replace failed: " + replaceResult.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (conflictDialogResult == DialogResult.No)
            { // Reschedule
                MessageBox.Show("Modify start/end time and try again.", "Reschedule", MessageBoxButtons.OK, MessageBoxIcon.Information); dtpStartTime.Focus();
            } // Nếu Cancel thì không làm gì
        }

        private void ClearInputFields()
        {
            txtName.Clear(); txtLocation.Clear();
            InitializeDateTimePickers(); // Reset time pickers
            chkEnableReminder.Checked = false;
        }

        private void RefreshLists()
        {
            if (_loggedInUser == null) { lstUserAppointments.Items.Clear(); lstUserReminders.Items.Clear(); return; }
            try
            {
                using (var manager = new CalendarManager(_loggedInUser.Username))
                {
                    RefreshAppointmentList(manager);
                    RefreshReminderList(manager);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Error refreshing data:\n{ex.Message}", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); Console.WriteLine($"Refresh Error: {ex}"); }
        }

        private void RefreshAppointmentList(CalendarManager manager)
        {
            lstUserAppointments.Items.Clear();
            try
            {
                var appointments = manager.GetUserAppointments(manager.GetCurrentUser());
                foreach (var appt in appointments) { lstUserAppointments.Items.Add(appt); }
            }
            catch (Exception ex) { lstUserAppointments.Items.Add($"Error loading appointments: {ex.Message}"); }
        }

        private void RefreshReminderList(CalendarManager manager)
        {
            lstUserReminders.Items.Clear();
            try
            {
                var reminders = manager.GetUserReminders(manager.GetCurrentUser());
                foreach (var reminder in reminders) { lstUserReminders.Items.Add(reminder); }
            }
            catch (Exception ex) { lstUserReminders.Items.Add($"Error loading reminders: {ex.Message}"); }
        }
    }
}