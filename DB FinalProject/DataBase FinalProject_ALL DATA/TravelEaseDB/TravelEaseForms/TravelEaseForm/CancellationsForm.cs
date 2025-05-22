using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TravelEaseForm
{
    public partial class CancellationsForm : Form
    {
        public string connectionString = "Server=DESKTOP-NO8G99V\\SQLEXPRESS;Database=Travelease;Integrated Security=True;";  // Your connection string

        public CancellationsForm()
        {
            InitializeComponent();
        }

        private void CancellationsForm_Load(object sender, EventArgs e)
        {
            // Populate status ComboBox with cancellation statuses
            comboBox1.Items.Add("Pending");
            comboBox1.Items.Add("Approved");
            comboBox1.Items.Add("Rejected");
            comboBox1.SelectedIndex = 0;  // Default to "Pending"
        }

        // Button click event to handle form submission
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Fetch values from the input fields
            int bookingID = Convert.ToInt32(textBox2.Text);
            string reason = textBox3.Text;
            string status = comboBox1.SelectedItem.ToString();
            DateTime cancellationDate = dateTimePicker1.Value;

            // Insert cancellation data into the database
            InsertCancellation(bookingID, reason, status, cancellationDate);
        }

        // Method to insert cancellation data into the database
        private void InsertCancellation(int bookingID, string reason, string status, DateTime cancellationDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO Cancellations (BookingID, Reason, Status, CancellationDate) VALUES (@BookingID, @Reason, @Status, @CancellationDate)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Adding parameters
                        cmd.Parameters.AddWithValue("@BookingID", bookingID);
                        cmd.Parameters.AddWithValue("@Reason", reason);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@CancellationDate", cancellationDate);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cancellation recorded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to record cancellation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
