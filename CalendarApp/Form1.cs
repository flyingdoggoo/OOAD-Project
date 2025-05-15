// Form1.cs
using System;
using System.Linq;
using System.Windows.Forms;
using CalendarApp.Models;
using CalendarApp.Services;

namespace CalendarApp
{
    public partial class Form1 : Form
    {
        private User _loggedInUser;

        public Form1(User user)
        {
            InitializeComponent();
            _loggedInUser = user;

            // DateTimePicker formats
            dtpStartTime.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpEndTime.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpReminderTime.CustomFormat = "dd/MM/yyyy hh:mm tt";
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpReminderTime.Format = DateTimePickerFormat.Custom;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_loggedInUser == null)
            {
                MessageBox.Show("No user is logged in. Closing application.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblCurrentUser.Text = $"Current User: {_loggedInUser.Username}";
            InitializeInputFields(); // Initialize/reset all input fields on Form1
            RefreshLists();
        }

        private void InitializeInputFields()
        {
            txtName.Clear();
            txtLocation.Clear();

            var now = DateTime.Now;
            var nextQuarterHour = now.AddMinutes(15 - (now.Minute % 15)).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
            if (nextQuarterHour <= now) nextQuarterHour = nextQuarterHour.AddMinutes(15);

            dtpStartTime.Value = nextQuarterHour;
            dtpEndTime.Value = dtpStartTime.Value.AddHours(1);

            chkEnableReminder.Checked = false; // Default reminder to off
            // Call chkEnableReminder_CheckedChanged to handle visibility and default reminder time
            chkEnableReminder_CheckedChanged(null, null);
        }

        private void chkEnableReminder_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkEnableReminder.Checked;
            lblReminderTime.Visible = isChecked;
            dtpReminderTime.Visible = isChecked;
            if (isChecked)
            {
                dtpReminderTime.Value = dtpStartTime.Value.AddMinutes(-15);
                if (dtpReminderTime.Value >= dtpStartTime.Value)
                {
                    dtpReminderTime.Value = dtpStartTime.Value.AddMinutes(-1);
                }
            }
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            if (dtpEndTime.Value <= dtpStartTime.Value)
            {
                dtpEndTime.Value = dtpStartTime.Value.AddHours(1);
            }
            if (chkEnableReminder.Checked)
            {
                dtpReminderTime.Value = dtpStartTime.Value.AddMinutes(-15);
                if (dtpReminderTime.Value >= dtpStartTime.Value)
                {
                    dtpReminderTime.Value = dtpStartTime.Value.AddMinutes(-1);
                }
            }
        }

        // --- Button Click for Adding Individual Appointment ---
        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out string name, out string location, out DateTime startTime, out DateTime endTime, out DateTime? reminderTime))
            {
                return; // Validation failed
            }

            try
            {
                using (var manager = new CalendarManager(_loggedInUser.Username))
                {
                    var result = manager.AddAppointment(name, location, startTime, endTime, reminderTime, forceIndividual: false);
                    HandleManagerResultForAppointment(manager, result, name, location, startTime, endTime, reminderTime);
                }
            }
            catch (InvalidOperationException userEx)
            {
                MessageBox.Show(userEx.Message, "Operation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error adding appointment:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"AddAppointment_Click Error: {ex}");
            }
        }

        // --- Button Click for Adding Group Meeting ---
        private void btnAddGroupMeeting_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out string name, out string location, out DateTime startTime, out DateTime endTime, out DateTime? reminderTime))
            {
                return; // Validation failed
            }

            try
            {
                using (var manager = new CalendarManager(_loggedInUser.Username))
                {
                    var result = manager.AddGroupMeeting(name, location, startTime, endTime, reminderTime);
                    HandleManagerResultForGroupMeeting(result); // Use a specific handler for group meetings
                }
            }
            catch (InvalidOperationException userEx)
            {
                MessageBox.Show(userEx.Message, "Operation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error adding group meeting:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"AddGroupMeeting_Click Error: {ex}");
            }
        }

        // --- Helper for Input Validation (used by both buttons) ---
        private bool ValidateInput(out string name, out string location, out DateTime startTime, out DateTime endTime, out DateTime? reminderTime)
        {
            name = txtName.Text.Trim();
            location = txtLocation.Text.Trim();
            startTime = dtpStartTime.Value;
            endTime = dtpEndTime.Value;
            reminderTime = null;

            if (_loggedInUser == null) { MessageBox.Show("No user loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }

            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Name/Meeting Title is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtName.Focus(); return false; }
            if (endTime <= startTime) { MessageBox.Show("End time must be after start time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); dtpEndTime.Focus(); return false; }

            if (chkEnableReminder.Checked)
            {
                if (dtpReminderTime.Value >= dtpStartTime.Value)
                {
                    MessageBox.Show("Reminder time must be before the start time.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpReminderTime.Focus();
                    return false;
                }
                reminderTime = dtpReminderTime.Value;
            }
            return true;
        }

        // --- Result Handling for Individual Appointments ---
        private void HandleManagerResultForAppointment(CalendarManager manager, (AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result,
                                        string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            switch (result.Status)
            {
                case AppointmentActionResult.Success:
                    MessageBox.Show(result.Message, "Appointment Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitializeInputFields(); RefreshLists(); break;
                case AppointmentActionResult.ConflictDetected:
                    HandleConflict(manager, result, name, location, startTime, endTime, reminderTime); break;
                case AppointmentActionResult.AskToJoinGroupMeeting:
                    HandleAskToJoin(manager, result, name, location, startTime, endTime, reminderTime); break;
                case AppointmentActionResult.ValidationError:
                case AppointmentActionResult.DatabaseError:
                case AppointmentActionResult.UnknownError:
                default:
                    MessageBox.Show(result.Message, "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning); break;
            }
        }

        // --- Result Handling for Group Meetings ---
        private void HandleManagerResultForGroupMeeting((AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result)
        {
            switch (result.Status)
            {
                case AppointmentActionResult.Success:
                    MessageBox.Show(result.Message, "Group Meeting Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitializeInputFields(); RefreshLists(); break;
                case AppointmentActionResult.ConflictDetected: // Conflict with the creator's schedule
                    MessageBox.Show(result.Message + "\nPlease choose a different time for your new group meeting or resolve the conflict.", "Scheduling Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpStartTime.Focus(); // Let user change time on Form1
                    break;
                case AppointmentActionResult.ValidationError:
                case AppointmentActionResult.DatabaseError:
                case AppointmentActionResult.UnknownError:
                default:
                    MessageBox.Show(result.Message, "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning); break;
            }
        }


        // --- Dialog Logic for AskToJoin (Individual Appointment context) ---
        private void HandleAskToJoin(CalendarManager manager, (AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result,
                                     string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            string[] messageParts = result.Message.Split(':');
            string displayMessage = messageParts[0];
            if (messageParts.Length < 2 || !int.TryParse(messageParts[messageParts.Length - 1], out int meetingId))
            {
                MessageBox.Show("Error parsing meeting ID from manager message.", "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            string meetingName = (result.RelatedAppointment as GroupMeeting)?.Name ?? $"Meeting ID {meetingId}";
            var joinDialogResult = MessageBox.Show($"{displayMessage}\nWould you like to join meeting '{meetingName}' instead?", "Join Existing Meeting?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (joinDialogResult == DialogResult.Yes)
            {
                bool joined = manager.ConfirmJoinGroupMeeting(meetingId, manager.GetCurrentUser());
                if (joined) { MessageBox.Show($"Joined: {meetingName}", "Joined", MessageBoxButtons.OK, MessageBoxIcon.Information); InitializeInputFields(); RefreshLists(); }
                else { MessageBox.Show("Failed to join meeting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (joinDialogResult == DialogResult.No)
            {
                MessageBox.Show("Proceeding to create new individual appointment.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    var individualResult = manager.AddAppointment(name, location, startTime, endTime, reminderTime, forceIndividual: true);
                    if (individualResult.Status == AppointmentActionResult.Success)
                    {
                        MessageBox.Show("New individual appointment created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InitializeInputFields(); RefreshLists();
                    }
                    else if (individualResult.Status == AppointmentActionResult.ConflictDetected)
                    {
                        // Re-enter conflict handling if forcing individual still causes a conflict
                        HandleConflict(manager, individualResult, name, location, startTime, endTime, reminderTime);
                    }
                    else
                    { MessageBox.Show($"Failed to create individual appointment: {individualResult.Message}", "Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch (Exception ex) { MessageBox.Show($"Error creating individual appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // --- Dialog Logic for Conflict (General) ---
        private void HandleConflict(CalendarManager manager, (AppointmentActionResult Status, string Message, Appointment RelatedAppointment) result,
                                    string name, string location, DateTime startTime, DateTime endTime, DateTime? reminderTime)
        {
            var conflictDialogResult = MessageBox.Show(result.Message + "\n\nReplace existing scheduled item?", "Conflict", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (conflictDialogResult == DialogResult.Yes)
            {
                var replaceResult = manager.PerformReplaceAppointment(result.RelatedAppointment, name, location, startTime, endTime, reminderTime);
                if (replaceResult.success) { MessageBox.Show(replaceResult.message, "Replaced", MessageBoxButtons.OK, MessageBoxIcon.Information); InitializeInputFields(); RefreshLists(); }
                else { MessageBox.Show("Replace failed: " + replaceResult.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (conflictDialogResult == DialogResult.No)
            {
                MessageBox.Show("Modify start/end time and try again.", "Reschedule", MessageBoxButtons.OK, MessageBoxIcon.Information); dtpStartTime.Focus();
            }
        }

        // --- Refreshing UI Lists ---
        private void RefreshLists()
        {
            try
            {
                using (var manager = new CalendarManager(_loggedInUser.Username))
                {
                    RefreshAppointmentList(manager);
                    RefreshReminderList(manager);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Error refreshing data:\n{ex.Message}", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); Console.WriteLine($"RefreshLists Error: {ex}"); }
        }

        private void RefreshAppointmentList(CalendarManager manager)
        {
            dataGridView2.DataSource = null; 
            try
            {
                var allScheduledItems = manager.GetUserAppointments(_loggedInUser);

                if (allScheduledItems.Any())
                {
                    var itemsForDisplay = allScheduledItems.Select(item =>
                    {
                        string itemType = "Appointment";

                        if (item is GroupMeeting groupMeeting)
                        {
                            itemType = "Group Meeting";
                        }


                        return new
                        {
                            Type = itemType,
                            Name = item.Name,
                            StartTime = item.StartTime.ToString("dd/MM/yyyy hh:mm tt"),
                            EndTime = item.EndTime.ToString("dd/MM/yyyy hh:mm tt"),
                            Location = item.Location,
                        };
                    }).ToList();

                    dataGridView2.DataSource = itemsForDisplay;

                    dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    if (dataGridView2.Columns["Type"] != null)
                        dataGridView2.Columns["Type"].HeaderText = "Type";
                    if (dataGridView2.Columns["Name"] != null)
                        dataGridView2.Columns["Name"].HeaderText = "Title/Name";
                    if (dataGridView2.Columns["StartTime"] != null)
                        dataGridView2.Columns["StartTime"].HeaderText = "Starts At";
                    if (dataGridView2.Columns["EndTime"] != null)
                        dataGridView2.Columns["EndTime"].HeaderText = "Ends At";
                    if (dataGridView2.Columns["Location"] != null)
                        dataGridView2.Columns["Location"].HeaderText = "Location";
                }
                else
                {
                    dataGridView2.Rows.Clear();
                    dataGridView2.Columns.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RefreshAppointmentList: {ex.ToString()}");
                dataGridView2.DataSource = null;
                dataGridView2.Rows.Clear();
                dataGridView2.Columns.Clear();
            }
        }


        private void RefreshReminderList(CalendarManager manager)
        {
            dataGridView1.DataSource = null;
            try
            {
                User currentUser = manager.GetCurrentUser();
                if (currentUser == null)
                {
                    dataGridView1.Rows.Clear(); 
                    dataGridView1.Columns.Clear(); 
                    return;
                }

                var reminders = manager.GetUserReminders(currentUser);

                if (reminders.Any())
                {
                    var remindersForDisplay = reminders.Select(r => new
                    {
                        TriggerTime = r.TriggerTime.ToString("dd/MM/yyyy hh:mm tt"), 
                        AppointmentName = r.RelatedAppointment?.Name ?? "N/A (Appointment Deleted?)"
                    }).ToList();

                    dataGridView1.DataSource = remindersForDisplay;

                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                    if (dataGridView1.Columns["TriggerTime"] != null)
                        dataGridView1.Columns["TriggerTime"].HeaderText = "Reminder At";
                    if (dataGridView1.Columns["AppointmentName"] != null)
                        dataGridView1.Columns["AppointmentName"].HeaderText = "For Appointment";
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns.Clear(); 
                }
            }
            catch (Exception ex)
            {
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}