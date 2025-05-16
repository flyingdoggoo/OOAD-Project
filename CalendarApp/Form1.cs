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
            RefreshLists();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e) 
        {
            DateTime preselectedStartTime = DateTime.Now.Date.AddHours(DateTime.Now.Hour + 1); 

            using (addAppointment appointmentForm = new addAppointment(_loggedInUser, preselectedStartTime))
            {
                if (appointmentForm.ShowDialog(this) == DialogResult.OK)
                {
                    RefreshLists(); 
                }
            }
        }


        private void RefreshLists()
        {
            if (_loggedInUser == null) return; // Basic check
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
                        string itemType = (item is GroupMeeting) ? "Group Meeting" : "Appointment";
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
                    dataGridView2.Rows.Clear(); dataGridView2.Columns.Clear(); /* Optionally add "No items" row */
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RefreshAppointmentList: {ex.ToString()}");
                dataGridView2.DataSource = null; dataGridView2.Rows.Clear(); dataGridView2.Columns.Clear();
            }
        }

        private void RefreshReminderList(CalendarManager manager)
        {
            dataGridView1.DataSource = null;
            try
            {
                var reminders = manager.GetUserReminders(_loggedInUser); // Pass _loggedInUser directly

                if (reminders.Any())
                {
                    var remindersForDisplay = reminders.Select(r => new
                    {
                        TriggerTime = r.TriggerTime.ToString("dd/MM/yyyy hh:mm tt"),
                        AppointmentName = r.RelatedAppointment?.Name ?? "N/A"
                    }).ToList();
                    dataGridView1.DataSource = remindersForDisplay;
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    // Optional: Set Headers
                    if (dataGridView1.Columns["TriggerTime"] != null) dataGridView1.Columns["TriggerTime"].HeaderText = "Reminder At";
                    if (dataGridView1.Columns["AppointmentName"] != null) dataGridView1.Columns["AppointmentName"].HeaderText = "For";
                }
                else
                {
                    dataGridView1.Rows.Clear(); dataGridView1.Columns.Clear();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RefreshReminderList: {ex.ToString()}");
                dataGridView1.DataSource = null; dataGridView1.Rows.Clear(); dataGridView1.Columns.Clear();
            }
        }

        private void exitBtn_Click(object sender, EventArgs e) 
        {
            this.Close();
        }
    }
}