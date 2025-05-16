using System;
using System.Linq;
using System.Windows.Forms;
using CalendarApp.Data;
using CalendarApp.Models; 

namespace CalendarApp
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            passTxt.PasswordChar = '*';
            this.AcceptButton = loginBtn; 
            this.CancelButton = cancelbtn; 
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string username = userTxt.Text.Trim();
            string password = passTxt.Text.Trim(); 

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a username.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                userTxt.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                passTxt.Focus();
                return;
            }

            try
            {
                using (var dbContext = new CalendarDbContext())
                {
                    User authenticatedUser = dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                    if (authenticatedUser != null)
                    {
                        Form1 mainForm = new Form1(authenticatedUser); 
                        mainForm.FormClosed += MainForm_FormClosed;  
                        mainForm.Show();
                        this.Hide(); 
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        passTxt.Clear(); 
                        passTxt.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}\n\nEnsure the database is correctly set up and accessible.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Login DB Error: {ex}");
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.userTxt.Clear();
            this.passTxt.Clear();
            this.Show();
            this.userTxt.Focus();
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            userTxt.Clear();
            passTxt.Clear();
            userTxt.Focus();
        }

        private void signupBtn_Click(object sender, EventArgs e)
        {
            signup signupForm = new signup();
            signupForm.FormClosed += (s, args) => this.Show();
            signupForm.Show();
            this.Hide();
        }
    }
}