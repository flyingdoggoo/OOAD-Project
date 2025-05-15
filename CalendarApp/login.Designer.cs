namespace CalendarApp
{
    partial class login
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
            this.userTxt = new System.Windows.Forms.TextBox();
            this.passTxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loginBtn = new System.Windows.Forms.Button();
            this.cancelbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userTxt
            // 
            this.userTxt.Location = new System.Drawing.Point(414, 97);
            this.userTxt.Name = "userTxt";
            this.userTxt.Size = new System.Drawing.Size(236, 31);
            this.userTxt.TabIndex = 0;
            // 
            // passTxt
            // 
            this.passTxt.Location = new System.Drawing.Point(414, 183);
            this.passTxt.Name = "passTxt";
            this.passTxt.Size = new System.Drawing.Size(236, 31);
            this.passTxt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "password:";
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(221, 286);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(120, 59);
            this.loginBtn.TabIndex = 3;
            this.loginBtn.Text = "log in";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // cancelbtn
            // 
            this.cancelbtn.Location = new System.Drawing.Point(450, 286);
            this.cancelbtn.Name = "cancelbtn";
            this.cancelbtn.Size = new System.Drawing.Size(120, 59);
            this.cancelbtn.TabIndex = 6;
            this.cancelbtn.Text = "cancel";
            this.cancelbtn.UseVisualStyleBackColor = true;
            this.cancelbtn.Click += new System.EventHandler(this.cancelbtn_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 483);
            this.Controls.Add(this.cancelbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.passTxt);
            this.Controls.Add(this.userTxt);
            this.Name = "login";
            this.Text = "login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userTxt;
        private System.Windows.Forms.TextBox passTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Button cancelbtn;
    }
}