using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TravelEaseForm
{
    public partial class PaymentForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-NO8G99V\\SQLEXPRESS;Initial Catalog=Travelease;Integrated Security=True";

        public PaymentForm()
        {
            InitializeComponent();
        }

        private void btnSavePayment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookingID.Text) ||
                cmbStatus.SelectedIndex == -1 ||
                cmbMethod.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please fill all required fields.");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Amount must be a valid decimal number.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string insertQuery = @"
                        INSERT INTO Payments (BookingID, TransactionDate, Status, PaymentMethod, Amount)
                        VALUES (@BookingID, @TransactionDate, @Status, @Method, @Amount)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", txtBookingID.Text);
                        cmd.Parameters.AddWithValue("@TransactionDate", dtTransactionDate.Value);
                        cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Method", cmbMethod.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Amount", amount);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Payment recorded successfully.");
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtBookingID.Clear();
            cmbStatus.SelectedIndex = -1;
            cmbMethod.SelectedIndex = -1;
            txtAmount.Clear();
            dtTransactionDate.Value = DateTime.Now;
        }
    }
}
