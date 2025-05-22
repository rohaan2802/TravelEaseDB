using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace TravelEaseDB
{
    public partial class ServiceProviderInterface : Form
    {

        private AppUser_Form _parentForm;  // Declare a variable to hold the parent Form1 instance
        string connectionString = "Data Source=CODE-TECH\\SQLEXPRESS;Initial Catalog=TravelEaseDB;Integrated Security=True;";

        public ServiceProviderInterface(AppUser_Form parent)
        {
            InitializeComponent();


            this._parentForm = parent;

            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();



            this.MinimizeBox = false; // Disable minimize button
            this.MaximizeBox = false; // Disable maximize button
            panel6.Visible = false; // Hide the panel by default
            panel5.Visible = false; // Hide the panel by default


            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.ReadOnly = true;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AllowUserToResizeRows = false; // Lock row height
            dataGridView3.AllowUserToResizeColumns = false; // Lock column width
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Auto-size columns
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Auto-size rows
            dataGridView3.RowHeadersVisible = false; // Hide row header arrow



        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // WS_EX_COMPOSITED
                return cp;
            }
        }


        private void LockFormExceptPanel6()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel6);
            }

            // Enable all children of TransportPanel
            foreach (Control child in panel6.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void LockFormExceptPanel5()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel5);
            }

            // Enable all children of TransportPanel
            foreach (Control child in panel5.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void UnlockForm()
        {
            this.SuspendLayout();
            this.ControlBox = true;

            // Bulk enable all controls (faster than recursive)
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }

            this.ResumeLayout(true);
        }


        private void ServiceProviderInterface_Load(object sender, EventArgs e)
        {

        }

        private void LoadRevenue()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
    SELECT COALESCE(SUM(t.Price), 0) AS TotalRevenue
    FROM BOOKINGS b
    JOIN TRIP t ON b.TripID = t.TripID
    WHERE b.BookingStatus = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        decimal revenue = (decimal)cmd.ExecuteScalar();
                        label2.Text = $"PKR {revenue:N2}";
                        System.Diagnostics.Debug.WriteLine($"LoadRevenue: TotalRevenue={revenue}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR LOADING REVENUE: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine($"LoadRevenue Error: {ex.Message}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
            LockFormExceptPanel5();
            LoadRevenue();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _parentForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            UnlockForm();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Get the current logged-in service provider's ID
            int serviceProviderId = SignINForm.LoggedInUserID; // You need to implement this

            // Show the assignment panel
            panel6.Visible = true;

            // Lock the rest of the form
            LockFormExceptPanel6();

            // Load assignments for this service provider
            LoadServiceProviderAssignments(serviceProviderId);
        }

        private void LoadServiceProviderAssignments(int serviceProviderId)
        {
            try
            {
                string query = @"SELECT a.AssignmentID, t.Title, t.TripDate, a.AssignmentStatus 
                        FROM TRIP_SERVICE_ASSIGNMENT a
                        JOIN TRIP t ON a.TripID = t.TripID
                        WHERE a.SERVICE_PROVIDER_ID = @ServiceProviderId
                        AND a.AssignmentStatus = 0"; // 0 = Pending assignments

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ServiceProviderId", serviceProviderId);

                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind to a DataGridView or other control in panel6
                    dataGridView3.DataSource = dt;

                    // Add Accept/Reject buttons if using DataGridView
                    AddActionButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading assignments: " + ex.Message);
            }
        }

        private void AddActionButtons()
        {
            // Add Accept button column
            DataGridViewButtonColumn acceptButton = new DataGridViewButtonColumn
            {
                Text = "Accept",
                Name = "Accept",
                UseColumnTextForButtonValue = true
            };
            dataGridView3.Columns.Add(acceptButton);

            // Add Reject button column
            DataGridViewButtonColumn rejectButton = new DataGridViewButtonColumn
            {
                Text = "Reject",
                Name = "Reject",
                UseColumnTextForButtonValue = true
            };
            dataGridView3.Columns.Add(rejectButton);

            // Handle button clicks
            dataGridView3.CellContentClick += DataGridViewAssignments_CellContentClick;
        }

        private void DataGridViewAssignments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var grid = (DataGridView)sender;

                if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    int assignmentId = (int)grid.Rows[e.RowIndex].Cells["AssignmentID"].Value;

                    if (grid.Columns[e.ColumnIndex].Name == "Accept")
                    {
                        UpdateAssignmentStatus(assignmentId, 1); // 1 = Accepted
                        MessageBox.Show("Assignment accepted successfully!");
                    }
                    else if (grid.Columns[e.ColumnIndex].Name == "Reject")
                    {
                        UpdateAssignmentStatus(assignmentId, 2); // 2 = Rejected
                        MessageBox.Show("Assignment rejected.");
                    }

                    // Refresh the assignments list
                    LoadServiceProviderAssignments(SignINForm.LoggedInUserID);
                }
            }
        }

        private void UpdateAssignmentStatus(int assignmentId, int status)
        {
            string query = @"UPDATE TRIP_SERVICE_ASSIGNMENT 
                    SET AssignmentStatus = @Status, 
                        ResponseDate = GETDATE() 
                    WHERE AssignmentID = @AssignmentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@AssignmentId", assignmentId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            UnlockForm();
        }
    }
}
