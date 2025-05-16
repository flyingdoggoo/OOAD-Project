using System;
using System.Linq;
using System.Windows.Forms;
using CalendarApp.Data;    
using CalendarApp.Models;   

namespace CalendarApp
{
    public partial class signup : Form
    {
        public signup()
        {
            InitializeComponent();
            this.AcceptButton = signupBtn; 
            this.CancelButton = cancelbtn; 
        }

        private void signupBtn_Click(object sender, EventArgs e)
        {
            string username = userTxt.Text.Trim();
            string password = passTxt.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a username.", "Sign Up Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                userTxt.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(password) || password.Length < 3) 
            {
                MessageBox.Show("Password must be at least 3 characters long.", "Sign Up Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                passTxt.Focus();
                return;
            }

            try
            {
                using (var dbContext = new CalendarDbContext())
                {
                    bool usernameExists = dbContext.Users.Any(u => u.Username == username);
                    if (usernameExists)
                    {
                        MessageBox.Show("This username is already taken. Please choose a different one.", "Sign Up Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        userTxt.Focus();
                        userTxt.SelectAll(); 
                        return;
                    }

                    User newUser = new User
                    {
                        Username = username,
                        Password = password 
                    };

                    dbContext.Users.Add(newUser);
                    dbContext.SaveChanges();

                    MessageBox.Show("Sign up successful! You can now log in with your new account.", "Sign Up Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; 
                    this.Close(); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during sign up: {ex.Message}\n\nPlease ensure the database is accessible and try again.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Sign Up DB Error: {ex}"); 
            }
        }

        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close(); 
        }
    }
}