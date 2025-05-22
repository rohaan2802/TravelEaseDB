using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TravelEaseForm
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=DESKTOP-NO8G99V\\SQLEXPRESS;Initial Catalog=Travelease;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Add items to ComboBox on form load
            cmbStatus.Items.AddRange(new string[] { "Confirmed", "Pending", "Cancelled" });
            dtBookingDate.Value = DateTime.Now;  // Set current date as default booking date
            txtBookingID.Enabled = false;  // Disable the Booking ID textbox as it's auto-generated or non-editable
        }

        private void btnSaveBooking_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtTravelerID.Text) ||
                string.IsNullOrWhiteSpace(cmbStatus.Text) ||
                string.IsNullOrWhiteSpace(txtTotalAmount.Text))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Validate total amount is a valid number
            if (!decimal.TryParse(txtTotalAmount.Text, out decimal totalAmount))
            {
                MessageBox.Show("Total Amount must be a valid number.");
                return;
            }

            try
            {
                // Establish database connection
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();  // Open the connection
                    string insertQuery = @"
                        INSERT INTO Bookings (BookingDate, TravelerID, Status, GroupSize, TotalAmount)
                        VALUES (@BookingDate, @TravelerID, @Status, @GroupSize, @TotalAmount)";

                    // Prepare the SQL command with parameters
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingDate", dtBookingDate.Value.Date);
                        cmd.Parameters.AddWithValue("@TravelerID", txtTravelerID.Text);
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);
                        cmd.Parameters.AddWithValue("@GroupSize", numGroupSize.Value);
                        cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);

                        // Execute the command and insert the data
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Booking saved successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Error saving booking. Please try again.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnOpenPayment_Click(object sender, EventArgs e)
        {
            // Open the Payment form (add code for payment processing here)
            MessageBox.Show("Opening Payment Form");
        }

        private void btnOpenCancellation_Click(object sender, EventArgs e)
        {
            // Open the Cancellations Form
            CancellationsForm cancellationForm = new CancellationsForm();
            cancellationForm.ShowDialog();
        }
    }
}
