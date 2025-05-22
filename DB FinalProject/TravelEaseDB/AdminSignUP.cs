﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Diagnostics;



namespace TravelEaseDB
{
    public partial class SignUp_Admin_form : Form
    {
        private AppUser_Form _parentForm;  // Declare a variable to hold the parent Form1 instance
        string connectionString = "Data Source=CODE-TECH\\SQLEXPRESS;Initial Catalog=TravelEaseDB;Integrated Security=True;";

        public SignUp_Admin_form(AppUser_Form parent)
        {
            InitializeComponent();

                this.Activated += (s, e) => textBox6.Focus();


            //try
            //{
            //    this.BackgroundImage = Image.FromFile("D:\TravelEaseDB\img\top-view-male-self-care-items.jpg"); // Update with actual path
            //    this.BackgroundImageLayout = ImageLayout.Stretch; // Adjust as needed
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error loading background image: " + ex.Message);
            //}



            this._parentForm = parent;

            this.WindowState = FormWindowState.Maximized;
            // Attach the Form2_Load event handler
            // Enable Double Buffering to prevent flickering
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();

            this.MinimizeBox = false; // Disable minimize button
            this.MaximizeBox = false; // Disable maximize button

        }

        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    base.OnPaintBackground(e); // Ensure background is painted
        //    e.Graphics.Clear(this.BackColor); // Optional: Force fill with background color
        //}


        ///  Add this to your SignUp_Admin_form class
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // WS_EX_COMPOSITED
                return cp;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Heavy operations here instead of Load
        }


        private void SignUp_Admin_Load(object sender, EventArgs e)
        {
            ClearForm(); // Clear form fields recursively
            textBox6.Focus();  // Set initial focus to AdminName textbox
            BeginInvoke((MethodInvoker)delegate 
            {
                textBox6.Focus();
            });
            textBox4.UseSystemPasswordChar = true;
            textBox5.UseSystemPasswordChar = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string userRole = "Admin"; // or dynamically set if needed

            if (username.Length < 8 || username.Length > 20)
            {
                textBox1.BackColor = Color.LightCoral;
                return;
            }

            // Check uniqueness
            using (SqlConnection conn = new SqlConnection("Data Source=CODE-TECH\\SQLEXPRESS;Initial Catalog=TravelEaseDB;Integrated Security=True;"))
            {
                string query = "SELECT COUNT(*) FROM AppUsers WHERE UserName = @UserName AND UserRole = @UserRole";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@UserRole", userRole);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        textBox1.BackColor = Color.LightCoral;
                        MessageBox.Show("USERNAME ALREADY EXISTS FOR ROLE ADMIN", "DUPLICATE USERNAME", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        textBox1.BackColor = Color.White;
                    }
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string email = textBox2.Text.Trim();

            if (email.Length > 20 || email.Length < 8 || !email.Contains("@") || !email.Contains("."))
            {
                textBox2.BackColor = Color.LightCoral;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM AppUsers WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        textBox2.BackColor = Color.LightCoral;
                        MessageBox.Show("EMAIL IS ALREADY REGISTERED FOR ROLE ADMIN.", "DUPLICATE EMAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        textBox2.BackColor = Color.White;
                    }
                }
            }
        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string contact = textBox3.Text.Trim();

            if (contact.Length != 11)
            {
                textBox3.BackColor = Color.LightCoral;
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM AppUsers WHERE ContactNumber = @ContactNumber";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ContactNumber", contact);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        textBox3.BackColor = Color.LightCoral;
                        MessageBox.Show("CONTACT NUMBER IS ALREADY IN USE FOR ROLE ADMIN.", "DUPLICATE CONTACT", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        textBox3.BackColor = Color.White;
                    }
                }
            }
        }


        private void ClearForm()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = "";
                    control.BackColor = Color.White;
                }
            }
           
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    ClearForm(); // Clear form fields recursively
        //    _parentForm.Show();  // Show Form1
        //    this.Close();
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if any TextBox has text
            bool hasData = ContainsFormData(this);

            if (hasData)
            {
                DialogResult result = MessageBox.Show(
                   "THIS WILL CLEAR THE FORM.DO YOU WANT TO CONTINUE ? ",
                    "CONFIRM CLEAR",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;
            }

            ClearForm();
            _parentForm.Show();
            this.Close();
        }

        private bool ContainsFormData(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is TextBox textBox && !string.IsNullOrEmpty(textBox.Text))
                    return true;
                if (control is ComboBox comboBox && comboBox.SelectedIndex != -1)
                    return true;
                if (control is CheckBox checkBox && checkBox.Checked)
                    return true;
                if (control is RadioButton radioButton && radioButton.Checked)
                    return true;
                if (control.HasChildren && ContainsFormData(control))
                    return true;
            }
            return false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.DoubleBuffered = true; // Reduces flickering
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox6.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("ADMIN NAME IS INVALID. IT SHOULD BE 8-20 CHARACTERS LONG AND CONTAIN ONLY ALPHABETS AND SPACES.", "INVALID INPUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox1.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("USERNAME IS INVALID. PLEASE ENSURE IT'S BETWEEN 8-20 CHARACTERS AND ONLY CONTAINS ALPHABETS.", "INVALID INPUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method
            }

            if (textBox2.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox2.Text) || !IsValidEmail(textBox2.Text.Trim()))
            {
                MessageBox.Show("EMAIL IS INVALID. PLEASE USE A VALID EMAIL FORMAT LIKE USER@EXAMPLE.COM.", "INVALID EMAIL", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox3.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("CONTACT NUMBER IS INVALID. IT SHOULD BE 11 DIGITS LONG.", "INVALID INPUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method
            }

            if (textBox4.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("PASSWORD IS INVALID. IT SHOULD BE AT LEAST 8 CHARACTERS.", "INVALID INPUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method
            }

            if (textBox5.BackColor == Color.LightCoral || string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("CONFIRM PASSWORD IS INVALID. IT SHOULD MATCH THE PASSWORD.", "INVALID INPUT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method
            }


            string hashedPassword = ComputeSha256Hash(textBox4.Text);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    // Begin the transaction
                    transaction = conn.BeginTransaction();

                    // Insert into AppUsers
                    string userQuery = @"
                INSERT INTO AppUsers (UserName, UserPassword, ContactNumber, Email, UserRole,UserStatus)
                VALUES (@username, @password, @contact, @email, 'Admin','1');
                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(userQuery, conn, transaction);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", hashedPassword.Trim());
                    cmd.Parameters.AddWithValue("@contact", textBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", textBox2.Text.Trim());

                    object userIdObj = cmd.ExecuteScalar();

                    if (userIdObj == null)
                    {
                        throw new Exception("FAILED TO INSERT USER RECORD");
                    }

                    int newUserId = Convert.ToInt32(userIdObj);

                    // Insert into ADMIN table
                    string adminQuery = @"
                INSERT INTO ADMIN (UserID, AdminName)
                VALUES (@userid, @adminname)";

                    SqlCommand adminCmd = new SqlCommand(adminQuery, conn, transaction);
                    adminCmd.Parameters.AddWithValue("@userid", newUserId);
                    adminCmd.Parameters.AddWithValue("@adminname", textBox6.Text.Trim());

                    int adminResult = adminCmd.ExecuteNonQuery();

                    if (adminResult <= 0)
                    {
                        throw new Exception("Failed to insert admin record");
                    }

                    // If we get here, both operations succeeded - commit the transaction
                    transaction.Commit();

                    MessageBox.Show("ADMIN REGISTERED SUCCESSFULLY!", "SUCCESS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    // Attempt to roll back the transaction if it exists
                    try
                    {
                        transaction?.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        // Log or handle rollback error if needed
                        Debug.WriteLine($"Rollback failed: {rollbackEx.Message}");
                    }

                    MessageBox.Show($"OPERATION FAILED:\n{ex.Message}", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            // Allow only digits or backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject input
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert to hex
                }
                return builder.ToString();
            }
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {
            ValidatePasswords();
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {
            ValidatePasswords();
        }

        private void ValidatePasswords()
        {
            if (textBox4.Text != textBox5.Text)
            {
                textBox5.BackColor = Color.LightCoral;
            }
            else
            {
                textBox5.BackColor = Color.White;
            }

            if (textBox4.Text.Length < 8  || textBox4.Text.Length > 15 )
            {
                textBox4.BackColor = Color.LightCoral;
            }
            else
            {
                textBox4.BackColor = Color.White;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only lowercase letters (a-z), digits (0-9), or control characters (like Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsLower(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Block invalid input
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            // Allow only:
            // - lowercase letters a-z
            // - digits 0-9
            // - @ and .
            // - control keys like Backspace
            if (!char.IsControl(ch) &&
                !(ch >= 'a' && ch <= 'z') &&
                !(ch >= '0' && ch <= '9') &&
                ch != '@' && ch != '.')
            {
                e.Handled = true; // Reject invalid characters
            }
        }

        private void SignUp_Admin_form_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string input = textBox6.Text.Trim();

            if (input.Length < 8 || input.Length > 20)
            {
                textBox6.BackColor = Color.LightCoral;
            }
            else
            {
                textBox6.BackColor = Color.White;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, space, or control keys (e.g., Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Reject character
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-alphanumeric characters
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true; // Block non-alphanumeric characters
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
