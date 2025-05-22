using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TravelEaseForm
{
    public partial class TransportForm : Form
    {
        public TransportForm()
        {
            InitializeComponent();
            LoadPerformanceOptions();
            button1.Click += new EventHandler(this.btnSaveTransport_Click);
        }

        private void LoadPerformanceOptions()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Excellent");
            comboBox1.Items.Add("Good");
            comboBox1.Items.Add("Average");
            comboBox1.Items.Add("Poor");
            comboBox1.SelectedIndex = 0;
        }

        private void btnSaveTransport_Click(object sender, EventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter Transport ID and Vehicle ID.");
                return;
            }

            string transportID = textBox1.Text.Trim();
            string vehicleID = textBox2.Text.Trim();
            int capacity = (int)numericUpDown1.Value;
            string performance = comboBox1.SelectedItem?.ToString();

            try
            {
                string connectionString = "Data Source=DESKTOP-NO8G99V\\SQLEXPRESS;Initial Catalog=Travelease;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Transport (TransportID, VehicleID, Capacity, OneTimePerformance) VALUES (@TransportID, @VehicleID, @Capacity, @Performance)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TransportID", transportID);
                        cmd.Parameters.AddWithValue("@VehicleID", vehicleID);
                        cmd.Parameters.AddWithValue("@Capacity", capacity);
                        cmd.Parameters.AddWithValue("@Performance", performance);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Transport record inserted successfully.");
                            textBox1.Clear();
                            textBox2.Clear();
                            numericUpDown1.Value = 0;
                            comboBox1.SelectedIndex = 0;
                        }
                        else
                        {
                            MessageBox.Show("Insertion failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
