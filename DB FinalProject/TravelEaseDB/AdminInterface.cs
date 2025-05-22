using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace TravelEaseDB
{
    public partial class AdminInterface : Form
    {

        public class NewUserRegistration
        {
            public string RegistrationMonth { get; set; }
            public string UserRole { get; set; }
            public int NewUsers { get; set; }
        }

        public class ActiveUser
        {
            public string ActiveMonth { get; set; }
            public string UserRole { get; set; }
            public int ActiveUsers { get; set; }
        }

        public class NewTourOperator
        {
            public string JoinMonth { get; set; }
            public int NewTourOperators { get; set; }
        }

        public class NewDestination
        {
            public string AddedMonth { get; set; }
            public int NewDestinations { get; set; }
        }
        public class AbandonmentRate
        {
            public double AbandonmentRateValue { get; set; }
        }

        public class CancellationReason
        {
            public string Reason { get; set; }
            public int Count { get; set; }
        }

        public class RecoveryRate
        {
            public double RecoveryRateValue { get; set; }
        }

        public class PotentialRevenueLoss
        {
            public decimal PotentialRevenueLossAmount { get; set; }
        }

        public class DestinationBooking
        {
            public string City { get; set; }
            public string Country { get; set; }
            public int TotalBookings { get; set; }
        }

        public class MonthlyBookingTrend
        {
            public string BookingMonth { get; set; }
            public int TotalBookings { get; set; }
        }

        public class DestinationSatisfaction
        {
            public string City { get; set; }
            public string Country { get; set; }
            public decimal AverageSatisfactionScore { get; set; }
        }

        public class YearlyBookingTrend
        {
            public string City { get; set; }
            public string Country { get; set; }
            public int BookingYear { get; set; }
            public int TotalBookings { get; set; }
        }
        public class NationalityDistribution
        {
            public string Nationality { get; set; }
            public int TravelerCount { get; set; }
        }

        public class CategoryPreference
        {
            public string CategoryName { get; set; }
            public int BookingCount { get; set; }
        }

        public class DestinationPopularity
        {
            public string City { get; set; }
            public string Country { get; set; }
            public int BookingCount { get; set; }
        }

        public class TravelerSpending
        {
            public int UserID { get; set; }
            public decimal AvgSpending { get; set; }
        }

        public class OverallSpending
        {
            public decimal OverallAvgSpending { get; set; }
        }


        public class HotelBooking
        {
            public string HotelName { get; set; }
            public int TotalBookings { get; set; }
        }

        public class GuideRating
        {
            public int G_Id { get; set; }
            public string LicenseNo { get; set; }
            public decimal AverageRating { get; set; }
        }


        public class TourOperatorRating
        {
            public int TourOperatorID { get; set; }
            public decimal AverageRating { get; set; }
        }

        public class TourOperatorRevenue
        {
            public int TourOperatorID { get; set; }
            public decimal TotalRevenue { get; set; }
        }

        public class TourOperatorResponseTime
        {
            public int TOUR_OPERATOR_ID { get; set; }
            public decimal AverageResponseTime_Minutes { get; set; }
        }

        public class TotalBookings
        {
            public int TotalBookingsCount { get; set; }
        }

        public class RevenueByCategory
        {
            public string CategoryName { get; set; }
            public decimal TotalRevenue { get; set; }
        }

        public class CancellationRate
        {
            public double CancellationRateValue { get; set; }
        }

        public class PeakBookingPeriod
        {
            public string Month { get; set; }
            public int Bookings { get; set; }
        }

        public class AverageBookingValue
        {
            public decimal AverageBookingValueAmount { get; set; }
        }
        private AppUser_Form _parentForm;  // Declare a variable to hold the parent Form1 instance
        string connectionString = "Data Source=CODE-TECH\\SQLEXPRESS;Initial Catalog=TravelEaseDB;Integrated Security=True;";

        public AdminInterface(AppUser_Form parent)
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

            // DataGridView settings
            // DataGridView settings
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToResizeRows = false; // Lock row height
            dataGridView1.AllowUserToResizeColumns = false; // Lock column width
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Auto-size columns
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Auto-size rows
            dataGridView1.RowHeadersVisible = false; // Hide row header arrow

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToResizeRows = false; // Lock row height
            dataGridView2.AllowUserToResizeColumns = false; // Lock column width
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Auto-size columns
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Auto-size rows
            dataGridView2.RowHeadersVisible = false; // Hide row header arrow


            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.ReadOnly = true;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AllowUserToResizeRows = false; // Lock row height
            dataGridView3.AllowUserToResizeColumns = false; // Lock column width
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Auto-size columns
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; // Auto-size rows
            dataGridView3.RowHeadersVisible = false; // Hide row header arrow



            panel1.Visible = false; // Hide the panel initially
            panel2.Visible = false; // Hide the panel initially
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;
            panel8.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible= false;
            panel12.Visible = false;
            reportViewer1.Visible = false; // Hide the report viewer initially
            reportViewer2.Visible = false; // Hide the report viewer initially
            reportViewer3.Visible = false;
            reportViewer4.Visible = false;
            reportViewer5.Visible = false;
            reportViewer6.Visible = false; // Hide the report viewer initially
            reportViewer7.Visible = false;
        }

        private DataTable GetNewUserRegistrations()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                FORMAT(CreatedAt, 'yyyy-MM') AS RegistrationMonth,
                UserRole,
                COUNT(*) AS NewUsers
            FROM AppUsers
            GROUP BY FORMAT(CreatedAt, 'yyyy-MM'), UserRole
            ORDER BY RegistrationMonth";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetNewUserRegistrations: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private DataTable GetActiveUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                FORMAT(BookDate, 'yyyy-MM') AS ActiveMonth,
                AU.UserRole,
                COUNT(DISTINCT AU.UserID) AS ActiveUsers
            FROM BOOKINGS B
            JOIN AppUsers AU ON AU.UserID = B.UserID
            WHERE AU.UserRole IN ('Traveler', 'TourOperator')
            GROUP BY FORMAT(BookDate, 'yyyy-MM'), AU.UserRole
            ORDER BY ActiveMonth";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetActiveUsers: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private DataTable GetNewTourOperators()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                FORMAT(AU.CreatedAt, 'yyyy-MM') AS JoinMonth,
                COUNT(*) AS NewTourOperators
            FROM TOUR_OPERATOR TOU
            JOIN AppUsers AU ON AU.UserID = TOU.UserID
            GROUP BY FORMAT(AU.CreatedAt, 'yyyy-MM')";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetNewTourOperators: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private DataTable GetNewDestinations()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                FORMAT(LocationID, 'yyyy-MM') AS AddedMonth,
                COUNT(*) AS NewDestinations
            FROM LOCATION
            GROUP BY FORMAT(LocationID, 'yyyy-MM')
            ORDER BY AddedMonth";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetNewDestinations: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private void LockFormExceptReport7()
        {
            this.SuspendLayout();
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel12);
            }
            foreach (Control child in panel12.Controls)
            {
                child.Enabled = true;
            }
            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void GeneratePlatformGrowthReport()
        {
            try
            {
                // Make Report Viewer visible and hide other panels
                panel12.BringToFront();
                reportViewer7.Visible = true;
                reportViewer7.Enabled = true;
                reportViewer7.Dock = DockStyle.Fill;
                System.Diagnostics.Debug.WriteLine($"ReportViewer7 Visible: {reportViewer7.Visible}, Enabled: {reportViewer7.Enabled}");
                System.Diagnostics.Debug.WriteLine($"Panel12 Visible: {panel12.Visible}, Enabled: {panel12.Enabled}");
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
                panel7.Visible = false;
                panel8.Visible = false;
                panel9.Visible = false;
                panel10.Visible = false;
                panel11.Visible = false;
                panel12.Visible = true;
                panel12.Enabled = true;
                LockFormExceptReport7();

                // Initialize Report Viewer
                reportViewer7.ProcessingMode = ProcessingMode.Local;
                reportViewer7.LocalReport.ReportEmbeddedResource = "TravelEaseDB.PlatformGrowthReport.rdlc";
                reportViewer7.LocalReport.DataSources.Clear();
                System.Diagnostics.Debug.WriteLine($"Report Path: {reportViewer7.LocalReport.ReportEmbeddedResource}");

                // Fetch data
                var newUserRegistrations = new List<NewUserRegistration>();
                foreach (DataRow row in GetNewUserRegistrations().Rows)
                {
                    newUserRegistrations.Add(new NewUserRegistration
                    {
                        RegistrationMonth = row["RegistrationMonth"].ToString(),
                        UserRole = row["UserRole"].ToString(),
                        NewUsers = Convert.ToInt32(row["NewUsers"])
                    });
                }

                var activeUsers = new List<ActiveUser>();
                foreach (DataRow row in GetActiveUsers().Rows)
                {
                    activeUsers.Add(new ActiveUser
                    {
                        ActiveMonth = row["ActiveMonth"].ToString(),
                        UserRole = row["UserRole"].ToString(),
                        ActiveUsers = Convert.ToInt32(row["ActiveUsers"])
                    });
                }

                var newTourOperators = new List<NewTourOperator>();
                foreach (DataRow row in GetNewTourOperators().Rows)
                {
                    newTourOperators.Add(new NewTourOperator
                    {
                        JoinMonth = row["JoinMonth"].ToString(),
                        NewTourOperators = Convert.ToInt32(row["NewTourOperators"])
                    });
                }

                var newDestinations = new List<NewDestination>();
                foreach (DataRow row in GetNewDestinations().Rows)
                {
                    newDestinations.Add(new NewDestination
                    {
                        AddedMonth = row["AddedMonth"].ToString(),
                        NewDestinations = Convert.ToInt32(row["NewDestinations"])
                    });
                }

                // Add data sources to Report Viewer
                reportViewer7.LocalReport.DataSources.Add(new ReportDataSource("NewUserRegistrationsDataSet", newUserRegistrations));
                reportViewer7.LocalReport.DataSources.Add(new ReportDataSource("ActiveUsersDataSet", activeUsers));
                reportViewer7.LocalReport.DataSources.Add(new ReportDataSource("NewTourOperatorsDataSet", newTourOperators));
                reportViewer7.LocalReport.DataSources.Add(new ReportDataSource("NewDestinationsDataSet", newDestinations));

                // Refresh the report
                reportViewer7.RefreshReport();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Report Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                MessageBox.Show($"ERROR GENERATING REPORT: {ex.Message}\nStack Trace: {ex.StackTrace}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reportViewer7.Visible = false;
                UnlockForm();
            }
        }


        private DataTable GetAbandonmentRate()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                (CAST(SUM(CASE WHEN BookingStatus = 0 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(*)) * 100 AS AbandonmentRate
            FROM BOOKINGS";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetAbandonmentRate: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private DataTable GetCancellationReasons()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                CAST(Reason AS NVARCHAR(MAX)) AS Reason,
                COUNT(*) AS Count
            FROM CANCELLATION C
            JOIN BOOKINGS B ON C.BookingID = B.BookingID
            WHERE B.BookingStatus = 0
            GROUP BY CAST(Reason AS NVARCHAR(MAX))
            ORDER BY Count DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetCancellationReasons: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private DataTable GetRecoveryRate()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                (CAST(COUNT(DISTINCT Recovered.BookingID) AS FLOAT) / NULLIF(COUNT(DISTINCT Abandoned.BookingID), 0)) * 100 AS RecoveryRate
            FROM BOOKINGS Abandoned
            JOIN BOOKINGS Recovered 
                ON Abandoned.UserID = Recovered.UserID 
                AND Abandoned.TripID = Recovered.TripID
                AND Abandoned.BookingStatus = 0
                AND Recovered.BookingStatus = 1
                AND Abandoned.BookingID < Recovered.BookingID";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetRecoveryRate: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private DataTable GetPotentialRevenueLoss()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                SUM(T.Price) AS PotentialRevenueLoss
            FROM BOOKINGS B
            JOIN TRIP T ON B.TripID = T.TripID
            WHERE B.BookingStatus = 0";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetPotentialRevenueLoss: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }


        private void LockFormExceptReport6()
        {
            this.SuspendLayout();
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel11);
            }
            foreach (Control child in panel11.Controls)
            {
                child.Enabled = true;
            }
            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void GenerateAbandonedBookingAnalysisReport()
        {
            try
            {
                // Make Report Viewer visible and hide other panels
                panel11.BringToFront();
                reportViewer6.Visible = true;
                reportViewer6.Enabled = true;
                reportViewer6.Dock = DockStyle.Fill;
                System.Diagnostics.Debug.WriteLine($"ReportViewer6 Visible: {reportViewer6.Visible}, Enabled: {reportViewer6.Enabled}");
                System.Diagnostics.Debug.WriteLine($"Panel11 Visible: {panel11.Visible}, Enabled: {panel11.Enabled}");
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
                panel7.Visible = false;
                panel8.Visible = false;
                panel9.Visible = false;
                panel10.Visible = false;
                panel11.Visible = true;
                panel11.Enabled = true;
                LockFormExceptReport6();

                // Initialize Report Viewer
                reportViewer6.ProcessingMode = ProcessingMode.Local;
                reportViewer6.LocalReport.ReportEmbeddedResource = "TravelEaseDB.AbandonedBookingAnalysisReport.rdlc";
                reportViewer6.LocalReport.DataSources.Clear();
                System.Diagnostics.Debug.WriteLine($"Report Path: {reportViewer6.LocalReport.ReportEmbeddedResource}");

                // Fetch data
                var abandonmentRate = new List<AbandonmentRate>();
                var abandonmentRateTable = GetAbandonmentRate();
                if (abandonmentRateTable.Rows.Count > 0)
                {
                    abandonmentRate.Add(new AbandonmentRate
                    {
                        AbandonmentRateValue = Convert.ToDouble(abandonmentRateTable.Rows[0]["AbandonmentRate"])
                    });
                }
                else
                {
                    abandonmentRate.Add(new AbandonmentRate { AbandonmentRateValue = 0 });
                    System.Diagnostics.Debug.WriteLine("No abandonment rate data available.");
                }

                var cancellationReasons = new List<CancellationReason>();
                foreach (DataRow row in GetCancellationReasons().Rows)
                {
                    cancellationReasons.Add(new CancellationReason
                    {
                        Reason = row["Reason"].ToString(),
                        Count = Convert.ToInt32(row["Count"])
                    });
                }

                var recoveryRate = new List<RecoveryRate>();
                var recoveryRateTable = GetRecoveryRate();
                if (recoveryRateTable.Rows.Count > 0 && recoveryRateTable.Rows[0]["RecoveryRate"] != DBNull.Value)
                {
                    recoveryRate.Add(new RecoveryRate
                    {
                        RecoveryRateValue = Convert.ToDouble(recoveryRateTable.Rows[0]["RecoveryRate"])
                    });
                }
                else
                {
                    recoveryRate.Add(new RecoveryRate { RecoveryRateValue = 0 });
                    System.Diagnostics.Debug.WriteLine("No recovery rate data available or division by zero.");
                }

                var potentialRevenueLoss = new List<PotentialRevenueLoss>();
                var potentialRevenueLossTable = GetPotentialRevenueLoss();
                if (potentialRevenueLossTable.Rows.Count > 0 && potentialRevenueLossTable.Rows[0]["PotentialRevenueLoss"] != DBNull.Value)
                {
                    potentialRevenueLoss.Add(new PotentialRevenueLoss
                    {
                        PotentialRevenueLossAmount = Convert.ToDecimal(potentialRevenueLossTable.Rows[0]["PotentialRevenueLoss"])
                    });
                }
                else
                {
                    potentialRevenueLoss.Add(new PotentialRevenueLoss { PotentialRevenueLossAmount = 0 });
                    System.Diagnostics.Debug.WriteLine("No potential revenue loss data available.");
                }

                // Add data sources to Report Viewer
                reportViewer6.LocalReport.DataSources.Add(new ReportDataSource("AbandonmentRateDataSet", abandonmentRate));
                reportViewer6.LocalReport.DataSources.Add(new ReportDataSource("CancellationReasonsDataSet", cancellationReasons));
                reportViewer6.LocalReport.DataSources.Add(new ReportDataSource("RecoveryRateDataSet", recoveryRate));
                reportViewer6.LocalReport.DataSources.Add(new ReportDataSource("PotentialRevenueLossDataSet", potentialRevenueLoss));

                // Refresh the report
                reportViewer6.RefreshReport();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Report Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                MessageBox.Show($"ERROR GENERATING REPORT: {ex.Message}\nStack Trace: {ex.StackTrace}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reportViewer6.Visible = false;
                UnlockForm();
            }
        }
        private DataTable GetDestinationBookings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                L.City,
                L.Country,
                COUNT(B.BookingID) AS TotalBookings
            FROM BOOKINGS B
            JOIN TRIP T ON B.TripID = T.TripID
            JOIN TRIP_SERVICE_ASSIGNMENT TSA ON T.TripID = TSA.TripID
            JOIN SERVICE_PROVIDER SP ON TSA.SERVICE_PROVIDER_ID = SP.UserID
            JOIN LOCATION L ON SP.LocationID = L.LocationID
            GROUP BY L.City, L.Country
            ORDER BY TotalBookings DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetDestinationBookings: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private DataTable GetMonthlyBookingTrends()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                DATENAME(MONTH, B.BookDate) AS BookingMonth,
                COUNT(*) AS TotalBookings
            FROM BOOKINGS B
            GROUP BY DATENAME(MONTH, B.BookDate), MONTH(B.BookDate)
            ORDER BY MONTH(B.BookDate)";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetMonthlyBookingTrends: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private DataTable GetDestinationSatisfactionScores()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                L.City,
                L.Country,
                AVG(R.Rating) AS AverageSatisfactionScore
            FROM REVIEW R
            JOIN BOOKINGS B ON R.BookingID = B.BookingID
            JOIN TRIP T ON B.TripID = T.TripID
            JOIN TRIP_SERVICE_ASSIGNMENT TSA ON T.TripID = TSA.TripID
            JOIN SERVICE_PROVIDER SP ON TSA.SERVICE_PROVIDER_ID = SP.UserID
            JOIN LOCATION L ON SP.LocationID = L.LocationID
            GROUP BY L.City, L.Country
            ORDER BY AverageSatisfactionScore DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetDestinationSatisfactionScores: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private DataTable GetYearlyBookingTrends()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                L.City,
                L.Country,
                YEAR(B.BookDate) AS BookingYear,
                COUNT(*) AS TotalBookings
            FROM BOOKINGS B
            JOIN TRIP T ON B.TripID = T.TripID
            JOIN TRIP_SERVICE_ASSIGNMENT TSA ON T.TripID = TSA.TripID
            JOIN SERVICE_PROVIDER SP ON TSA.SERVICE_PROVIDER_ID = SP.UserID
            JOIN LOCATION L ON SP.LocationID = L.LocationID
            GROUP BY L.City, L.Country, YEAR(B.BookDate)
            ORDER BY L.City, BookingYear";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    System.Diagnostics.Debug.WriteLine($"GetYearlyBookingTrends: Rows returned = {dt.Rows.Count}");
                    return dt;
                }
            }
        }

        private void LockFormExceptReport5()
        {
            this.SuspendLayout();
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel10);
            }
            foreach (Control child in panel10.Controls)
            {
                child.Enabled = true;
            }
            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void GenerateDestinationBookingInsightsReport()
        {
            try
            {
                // Make Report Viewer visible and hide other panels
                panel10.BringToFront();
                reportViewer5.Visible = true;
                reportViewer5.Enabled = true;
                reportViewer5.Dock = DockStyle.Fill;
                System.Diagnostics.Debug.WriteLine($"ReportViewer5 Visible: {reportViewer5.Visible}, Enabled: {reportViewer5.Enabled}");
                System.Diagnostics.Debug.WriteLine($"Panel10 Visible: {panel10.Visible}, Enabled: {panel10.Enabled}");
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
                panel7.Visible = false;
                panel8.Visible = false;
                panel9.Visible = false;
                panel10.Visible = true;
                panel10.Enabled = true;
                LockFormExceptReport5();

                // Initialize Report Viewer
                reportViewer5.ProcessingMode = ProcessingMode.Local;
                reportViewer5.LocalReport.ReportEmbeddedResource = "TravelEaseDB.DestinationBookingInsightsReport.rdlc";
                reportViewer5.LocalReport.DataSources.Clear();
                System.Diagnostics.Debug.WriteLine($"Report Path: {reportViewer5.LocalReport.ReportEmbeddedResource}");

                // Fetch data
                var destinationBookings = new List<DestinationBooking>();
                foreach (DataRow row in GetDestinationBookings().Rows)
                {
                    destinationBookings.Add(new DestinationBooking
                    {
                        City = row["City"].ToString(),
                        Country = row["Country"].ToString(),
                        TotalBookings = Convert.ToInt32(row["TotalBookings"])
                    });
                }

                var monthlyBookingTrends = new List<MonthlyBookingTrend>();
                foreach (DataRow row in GetMonthlyBookingTrends().Rows)
                {
                    monthlyBookingTrends.Add(new MonthlyBookingTrend
                    {
                        BookingMonth = row["BookingMonth"].ToString(),
                        TotalBookings = Convert.ToInt32(row["TotalBookings"])
                    });
                }

                var destinationSatisfactionScores = new List<DestinationSatisfaction>();
                foreach (DataRow row in GetDestinationSatisfactionScores().Rows)
                {
                    destinationSatisfactionScores.Add(new DestinationSatisfaction
                    {
                        City = row["City"].ToString(),
                        Country = row["Country"].ToString(),
                        AverageSatisfactionScore = Convert.ToDecimal(row["AverageSatisfactionScore"])
                    });
                }

                var yearlyBookingTrends = new List<YearlyBookingTrend>();
                foreach (DataRow row in GetYearlyBookingTrends().Rows)
                {
                    yearlyBookingTrends.Add(new YearlyBookingTrend
                    {
                        City = row["City"].ToString(),
                        Country = row["Country"].ToString(),
                        BookingYear = Convert.ToInt32(row["BookingYear"]),
                        TotalBookings = Convert.ToInt32(row["TotalBookings"])
                    });
                }

                // Add data sources to Report Viewer
                reportViewer5.LocalReport.DataSources.Add(new ReportDataSource("DestinationBookingsDataSet", destinationBookings));
                reportViewer5.LocalReport.DataSources.Add(new ReportDataSource("MonthlyBookingTrendsDataSet", monthlyBookingTrends));
                reportViewer5.LocalReport.DataSources.Add(new ReportDataSource("DestinationSatisfactionDataSet", destinationSatisfactionScores));
                reportViewer5.LocalReport.DataSources.Add(new ReportDataSource("YearlyBookingTrendsDataSet", yearlyBookingTrends));

                // Refresh the report
                reportViewer5.RefreshReport();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Report Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                MessageBox.Show($"ERROR GENERATING REPORT: {ex.Message}\nStack Trace: {ex.StackTrace}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reportViewer5.Visible = false;
                UnlockForm();
            }
        }

        private DataTable GetNationalityDistribution()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        L.Country AS Nationality,
                        COUNT(*) AS TravelerCount
                    FROM Traveler T
                    JOIN SERVICE_PROVIDER SP ON T.UserID = SP.UserID
                    JOIN LOCATION L ON SP.LocationID = L.LocationID
                    GROUP BY L.Country
                    ORDER BY TravelerCount DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetCategoryPreferences()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        C.CategoryName,
                        COUNT(*) AS BookingCount
                    FROM BOOKINGS B
                    JOIN TRIP T ON B.TripID = T.TripID
                    JOIN CATEGORY C ON T.CategoryID = C.CategoryID
                    GROUP BY C.CategoryName
                    ORDER BY BookingCount DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetDestinationPopularity()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        L.City,
                        L.Country,
                        COUNT(*) AS BookingCount
                    FROM BOOKINGS B
                    JOIN TRIP T ON B.TripID = T.TripID
                    JOIN TRIP_SERVICE_ASSIGNMENT TSA ON T.TripID = TSA.TripID
                    JOIN SERVICE_PROVIDER SP ON TSA.SERVICE_PROVIDER_ID = SP.UserID
                    JOIN LOCATION L ON SP.LocationID = L.LocationID
                    GROUP BY L.City, L.Country
                    ORDER BY BookingCount DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetTravelerSpending()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        B.UserID,
                        AVG(T.Price) AS AvgSpending
                    FROM BOOKINGS B
                    JOIN TRIP T ON B.TripID = T.TripID
                    GROUP BY B.UserID
                    ORDER BY AvgSpending DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetOverallSpending()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        AVG(T.Price) AS OverallAvgSpending
                    FROM BOOKINGS B
                    JOIN TRIP T ON B.TripID = T.TripID";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }


        private void GenerateTravelerDemographicsAndPreferencesReport()
        {
            try
            {
                panel9.BringToFront();
                System.Diagnostics.Debug.WriteLine($"Bringing panel9 to front. Z-Order: {this.Controls.GetChildIndex(panel9)}");
                // Make Report Viewer visible and hide other panels
                reportViewer4.Visible = true;
                reportViewer4.Enabled = true;
                reportViewer4.Dock = DockStyle.Fill;
                System.Diagnostics.Debug.WriteLine($"ReportViewer4 Visible: {reportViewer4.Visible}, Enabled: {reportViewer4.Enabled}");
                System.Diagnostics.Debug.WriteLine($"Panel9 Visible: {panel9.Visible}, Enabled: {panel9.Enabled}");
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
                panel7.Visible = false;
                panel8.Visible = false;
                panel9.Visible = true;
                panel9.Enabled = true;
                LockFormExceptReport4();

                // Initialize Report Viewer
                reportViewer4.ProcessingMode = ProcessingMode.Local;
                reportViewer4.LocalReport.ReportEmbeddedResource = "TravelEaseDB.TravelerDemographicsAndPreferencesReport.rdlc";
                reportViewer4.LocalReport.DataSources.Clear();
                System.Diagnostics.Debug.WriteLine($"Report Path: {reportViewer4.LocalReport.ReportEmbeddedResource}");

                // Fetch data
                var nationalities = new List<NationalityDistribution>();
                foreach (DataRow row in GetNationalityDistribution().Rows)
                {
                    nationalities.Add(new NationalityDistribution
                    {
                        Nationality = row["Nationality"].ToString(),
                        TravelerCount = Convert.ToInt32(row["TravelerCount"])
                    });
                }

                var categories = new List<CategoryPreference>();
                foreach (DataRow row in GetCategoryPreferences().Rows)
                {
                    categories.Add(new CategoryPreference
                    {
                        CategoryName = row["CategoryName"].ToString(),
                        BookingCount = Convert.ToInt32(row["BookingCount"])
                    });
                }

                var destinations = new List<DestinationPopularity>();
                foreach (DataRow row in GetDestinationPopularity().Rows)
                {
                    destinations.Add(new DestinationPopularity
                    {
                        City = row["City"].ToString(),
                        Country = row["Country"].ToString(),
                        BookingCount = Convert.ToInt32(row["BookingCount"])
                    });
                }

                var spendings = new List<TravelerSpending>();
                foreach (DataRow row in GetTravelerSpending().Rows)
                {
                    spendings.Add(new TravelerSpending
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        AvgSpending = Convert.ToDecimal(row["AvgSpending"])
                    });
                }

                var overallSpending = new List<OverallSpending>
                {
                    new OverallSpending
                    {
                        OverallAvgSpending = Convert.ToDecimal(GetOverallSpending().Rows[0]["OverallAvgSpending"])
                    }
                };

                // Add data sources to Report Viewer
                reportViewer4.LocalReport.DataSources.Add(new ReportDataSource("NationalityDataSet", nationalities));
                reportViewer4.LocalReport.DataSources.Add(new ReportDataSource("CategoryDataSet", categories));
                reportViewer4.LocalReport.DataSources.Add(new ReportDataSource("DestinationDataSet", destinations));
                reportViewer4.LocalReport.DataSources.Add(new ReportDataSource("SpendingDataSet", spendings));
                reportViewer4.LocalReport.DataSources.Add(new ReportDataSource("OverallSpendingDataSet", overallSpending));

                // Refresh the report
                reportViewer4.RefreshReport();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Report Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                MessageBox.Show($"ERROR GENERATING REPORT: {ex.Message}\nStack Trace: {ex.StackTrace}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reportViewer4.Visible = false;
                UnlockForm();
            }
        }

        // Add new form locking method below LockFormExceptReport3
        private void LockFormExceptReport4()
        {
            this.SuspendLayout();
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel9);
            }
            foreach (Control child in panel9.Controls)
            {
                child.Enabled = true;
            }
            this.ControlBox = false;
            this.ResumeLayout(true);
        }




        private void GenerateTripBookingRevenueReport()
        {
            try
            {
                // Make Report Viewer visible and hide other panels
                reportViewer1.Visible = true;
                reportViewer1.Enabled = true;
                reportViewer1.Dock = DockStyle.Fill; // Ensure it fills panel6
                System.Diagnostics.Debug.WriteLine($"ReportViewer Visible: {reportViewer1.Visible}, Enabled: {reportViewer1.Enabled}");
                System.Diagnostics.Debug.WriteLine($"Panel6 Visible: {panel6.Visible}, Enabled: {panel6.Enabled}"); panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = true;
                panel6.Enabled = true; // Enable the panel for report generation
                LockFormExceptReport1(); // Reuse existing method to lock form

                // Initialize Report Viewer
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                reportViewer1.LocalReport.ReportEmbeddedResource = "TravelEaseDB.TripBookingRevenueReport.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                System.Diagnostics.Debug.WriteLine($"Report Path: {reportViewer1.LocalReport.ReportEmbeddedResource}");
                // Fetch data
                var totalBookings = new List<TotalBookings>
        {
            new TotalBookings { TotalBookingsCount = Convert.ToInt32(GetTotalBookings().Rows[0]["TotalBookings"]) }
        };

                var revenueByCategory = new List<RevenueByCategory>();
                foreach (DataRow row in GetRevenueByCategory().Rows)
                {
                    revenueByCategory.Add(new RevenueByCategory
                    {
                        CategoryName = row["CategoryName"].ToString(),
                        TotalRevenue = Convert.ToDecimal(row["TotalRevenue"])
                    });
                }

                var cancellationRate = new List<CancellationRate>
        {
            new CancellationRate { CancellationRateValue = Convert.ToDouble(GetCancellationRate().Rows[0]["CancellationRate"]) }
        };

                var peakBookingPeriods = new List<PeakBookingPeriod>();
                foreach (DataRow row in GetPeakBookingPeriods().Rows)
                {
                    peakBookingPeriods.Add(new PeakBookingPeriod
                    {
                        Month = row["Month"].ToString(),
                        Bookings = Convert.ToInt32(row["Bookings"])
                    });
                }

                var averageBookingValue = new List<AverageBookingValue>
        {
            new AverageBookingValue { AverageBookingValueAmount = Convert.ToDecimal(GetAverageBookingValue().Rows[0]["AverageBookingValue"]) }
        };

                // Add data sources to Report Viewer
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TotalBookingsDataSet", totalBookings));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("RevenueByCategoryDataSet", revenueByCategory));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CancellationRateDataSet", cancellationRate));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("PeakBookingPeriodsDataSet", peakBookingPeriods));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("AverageBookingValueDataSet", averageBookingValue));

                // Refresh the report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Report Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                MessageBox.Show($"ERROR GENERATING REPORT: {ex.Message}\nStack Trace: {ex.StackTrace}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reportViewer1.Visible = false;
                UnlockForm();
            }
        }



        private DataTable GetHotelBookings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        H.HotelName,
                        COUNT(B.BookingID) AS TotalBookings
                    FROM HOTEL H
                    JOIN SERVICE_PROVIDER SP ON H.LicenseNo = SP.LicenseNo
                    JOIN TRIP_SERVICE_ASSIGNMENT TSA ON TSA.SERVICE_PROVIDER_ID = SP.UserID
                    JOIN TRIP T ON T.TripID = TSA.TripID
                    JOIN BOOKINGS B ON B.TripID = T.TripID
                    GROUP BY H.HotelName
                    ORDER BY TotalBookings DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetGuideRatings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        G.G_Id,
                        G.LicenseNo,
                        AVG(R.Rating) AS AverageRating
                    FROM GUIDE G
                    JOIN SERVICE_PROVIDER SP ON G.LicenseNo = SP.LicenseNo
                    JOIN TRIP_SERVICE_ASSIGNMENT TSA ON TSA.SERVICE_PROVIDER_ID = SP.UserID
                    JOIN TRIP T ON T.TripID = TSA.TripID
                    JOIN BOOKINGS B ON B.TripID = T.TripID
                    JOIN REVIEW R ON R.BookingID = B.BookingID
                    GROUP BY G.G_Id, G.LicenseNo
                    ORDER BY AverageRating DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        // Add new report generation method below GenerateTourOperatorPerformanceReport
        private void GenerateServiceProviderEfficiencyReport()
        {
            try
            {
                // Make Report Viewer visible and hide other panels
                reportViewer3.Visible = true;
                reportViewer3.Enabled = true;
                reportViewer3.Dock = DockStyle.Fill;
                System.Diagnostics.Debug.WriteLine($"ReportViewer3 Visible: {reportViewer3.Visible}, Enabled: {reportViewer3.Enabled}");
                System.Diagnostics.Debug.WriteLine($"Panel8 Visible: {panel8.Visible}, Enabled: {panel8.Enabled}");
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
                panel7.Visible = false;
                panel8.Visible = true;
                panel8.Enabled = true;
                LockFormExceptReport3();

                // Initialize Report Viewer
                reportViewer3.ProcessingMode = ProcessingMode.Local;
                reportViewer3.LocalReport.ReportEmbeddedResource = "TravelEaseDB.ServiceProviderEfficiencyReport.rdlc";
                reportViewer3.LocalReport.DataSources.Clear();
                System.Diagnostics.Debug.WriteLine($"Report Path: {reportViewer3.LocalReport.ReportEmbeddedResource}");

                // Fetch data
                var hotelBookings = new List<HotelBooking>();
                foreach (DataRow row in GetHotelBookings().Rows)
                {
                    hotelBookings.Add(new HotelBooking
                    {
                        HotelName = row["HotelName"].ToString(),
                        TotalBookings = Convert.ToInt32(row["TotalBookings"])
                    });
                }

                var guideRatings = new List<GuideRating>();
                foreach (DataRow row in GetGuideRatings().Rows)
                {
                    guideRatings.Add(new GuideRating
                    {
                        G_Id = Convert.ToInt32(row["G_Id"]),
                        LicenseNo = row["LicenseNo"].ToString(),
                        AverageRating = Convert.ToDecimal(row["AverageRating"])
                    });
                }

                // Add data sources to Report Viewer
                reportViewer3.LocalReport.DataSources.Add(new ReportDataSource("HotelBookingsDataSet", hotelBookings));
                reportViewer3.LocalReport.DataSources.Add(new ReportDataSource("GuideRatingsDataSet", guideRatings));

                // Refresh the report
                reportViewer3.RefreshReport();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Report Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                MessageBox.Show($"ERROR GENERATING REPORT: {ex.Message}\nStack Trace: {ex.StackTrace}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reportViewer3.Visible = false;
                UnlockForm();
            }
        }

        // Add new form locking method below LockFormExceptReport2
        private void LockFormExceptReport3()
        {
            this.SuspendLayout();
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel8);
            }
            foreach (Control child in panel8.Controls)
            {
                child.Enabled = true;
            }
            this.ControlBox = false;
            this.ResumeLayout(true);
        }



        private DataTable GetTotalBookings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) AS TotalBookings FROM BOOKINGS WHERE BookingStatus = 1";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetRevenueByCategory()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT C.CategoryName, SUM(T.Price) AS TotalRevenue
            FROM BOOKINGS B
            JOIN TRIP T ON B.TripID = T.TripID
            JOIN CATEGORY C ON T.CategoryID = C.CategoryID
            WHERE B.BookingStatus = 1
            GROUP BY C.CategoryName";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetCancellationRate()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                CAST(COUNT(C.BookingID) AS FLOAT) / NULLIF(COUNT(B.BookingID), 0) * 100 AS CancellationRate
            FROM BOOKINGS B
            LEFT JOIN CANCELLATION C ON B.BookingID = C.BookingID";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetPeakBookingPeriods()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT DATENAME(MONTH, BookDate) AS Month, COUNT(*) AS Bookings
            FROM BOOKINGS
            WHERE BookingStatus = 1
            GROUP BY DATENAME(MONTH, BookDate)
            ORDER BY Bookings DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetAverageBookingValue()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                AVG(T.Price) AS AverageBookingValue
            FROM BOOKINGS B
            JOIN TRIP T ON B.TripID = T.TripID
            WHERE B.BookingStatus = 1";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetTourOperatorRatings()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                TOU.UserID AS TourOperatorID,
                AVG(R.Rating) AS AverageRating
            FROM TOUR_OPERATOR TOU
            JOIN TRIP T ON TOU.UserID = T.TourOperatorID
            JOIN BOOKINGS B ON T.TripID = B.TripID
            JOIN REVIEW R ON R.BookingID = B.BookingID
            GROUP BY TOU.UserID
            ORDER BY AverageRating DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetTourOperatorRevenue()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                TOU.UserID AS TourOperatorID,
                SUM(T.Price) AS TotalRevenue
            FROM TOUR_OPERATOR TOU
            JOIN TRIP T ON TOU.UserID = T.TourOperatorID
            JOIN BOOKINGS B ON T.TripID = B.TripID
            WHERE B.BookingStatus = 1
            GROUP BY TOU.UserID
            ORDER BY TotalRevenue DESC";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable GetTourOperatorResponseTime()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                TOUR_OPERATOR_ID,
                AVG(DATEDIFF(MINUTE, AssignedDate, ResponseDate)) AS AverageResponseTime_Minutes
            FROM TRIP_SERVICE_ASSIGNMENT
            WHERE ResponseDate IS NOT NULL
            GROUP BY TOUR_OPERATOR_ID
            ORDER BY AverageResponseTime_Minutes";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        private void GenerateTourOperatorPerformanceReport()
        {
            try
            {
                // Make Report Viewer visible and hide other panels
                reportViewer2.Visible = true;
                reportViewer2.Enabled = true;
                reportViewer2.Dock = DockStyle.Fill; // Ensure it fills panel7
                System.Diagnostics.Debug.WriteLine($"ReportViewer2 Visible: {reportViewer2.Visible}, Enabled: {reportViewer2.Enabled}");
                System.Diagnostics.Debug.WriteLine($"Panel7 Visible: {panel7.Visible}, Enabled: {panel7.Enabled}");
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                panel6.Visible = false;
                panel7.Visible = true;
                panel7.Enabled = true; // Enable the panel for report generation
                LockFormExceptReport2(); // New method to lock form except panel7

                // Initialize Report Viewer
                reportViewer2.ProcessingMode = ProcessingMode.Local;
                reportViewer2.LocalReport.ReportEmbeddedResource = "TravelEaseDB.TourOperatorReport.rdlc";
                reportViewer2.LocalReport.DataSources.Clear();
                System.Diagnostics.Debug.WriteLine($"Report Path: {reportViewer2.LocalReport.ReportEmbeddedResource}");

                // Fetch data
                var ratings = new List<TourOperatorRating>();
                foreach (DataRow row in GetTourOperatorRatings().Rows)
                {
                    ratings.Add(new TourOperatorRating
                    {
                        TourOperatorID = Convert.ToInt32(row["TourOperatorID"]),
                        AverageRating = Convert.ToDecimal(row["AverageRating"])
                    });
                }

                var revenues = new List<TourOperatorRevenue>();
                foreach (DataRow row in GetTourOperatorRevenue().Rows)
                {
                    revenues.Add(new TourOperatorRevenue
                    {
                        TourOperatorID = Convert.ToInt32(row["TourOperatorID"]),
                        TotalRevenue = Convert.ToDecimal(row["TotalRevenue"])
                    });
                }

                var responseTimes = new List<TourOperatorResponseTime>();
                foreach (DataRow row in GetTourOperatorResponseTime().Rows)
                {
                    responseTimes.Add(new TourOperatorResponseTime
                    {
                        TOUR_OPERATOR_ID = Convert.ToInt32(row["TOUR_OPERATOR_ID"]),
                        AverageResponseTime_Minutes = Convert.ToDecimal(row["AverageResponseTime_Minutes"])
                    });
                }

                // Add data sources to Report Viewer
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("RatingsDataSet", ratings));
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("RevenueDataSet", revenues));
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("ResponseTimeDataSet", responseTimes));

                // Refresh the report
                reportViewer2.RefreshReport();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Report Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                MessageBox.Show($"ERROR GENERATING REPORT: {ex.Message}\nStack Trace: {ex.StackTrace}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                reportViewer2.Visible = false;
                UnlockForm();
            }
        }


        private void LockFormExceptReport2()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel7);
            }

            // Enable all children of panel7
            foreach (Control child in panel7.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }


        private void LoadReviews()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT ReviewID, BookingID, Rating, Comment
                        FROM REVIEW                         
                        WHERE ApproveStatus = 0;";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Clear existing columns
                        dataGridView3.Columns.Clear();

                        // Add columns programmatically
                        dataGridView3.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "ReviewID",
                            HeaderText = "ReviewID",
                            DataPropertyName = "ReviewID"
                        });
                        dataGridView3.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "BookingID",
                            HeaderText = "BookingID",
                            DataPropertyName = "BookingID"
                        });
                        dataGridView3.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "Rating",
                            HeaderText = "Rating",
                            DataPropertyName = "Rating"
                        });
                        dataGridView3.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "Comment",
                            HeaderText = "Comment",
                            DataPropertyName = "Comment"
                        });
                        dataGridView3.Columns.Add(new DataGridViewButtonColumn
                        {
                            Name = "Approve",
                            HeaderText = "Approve",
                            Text = "Approve",
                            UseColumnTextForButtonValue = true
                        });
                        dataGridView3.Columns.Add(new DataGridViewButtonColumn
                        {
                            Name = "Reject",
                            HeaderText = "Reject",
                            Text = "Reject",
                            UseColumnTextForButtonValue = true
                        });

                        // Set DataSource
                        dataGridView3.DataSource = dt;

                      
                        // Ensure button columns are at the end
                        foreach (DataGridViewColumn col in dataGridView3.Columns)
                        {
                            if (col.Name == "Approve" || col.Name == "Reject")
                                col.DisplayIndex = dataGridView3.Columns.Count - 1;
                        }

                        // Hide UserID column
                        if (dataGridView3.Columns["ReviewID"] != null)
                            dataGridView3.Columns["ReviewID"].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR LOADING REVIEWS: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadPendingUsers()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT UserID, UserName, UserRole, ContactNumber, CreatedAt
                        FROM AppUsers
                        WHERE UserStatus = 0";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Clear existing columns
                        dataGridView1.Columns.Clear();

                        // Add columns programmatically
                        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "UserID",
                            HeaderText = "UserID",
                            DataPropertyName = "UserID",
                            Visible = false
                        });
                        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "UserName",
                            HeaderText = "UserName",
                            DataPropertyName = "UserName"
                        });
                        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "UserRole",
                            HeaderText = "UserRole",
                            DataPropertyName = "UserRole"
                        });
                        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "ContactNumber",
                            HeaderText = "UserContact#",
                            DataPropertyName = "ContactNumber"
                        });
                        dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "CreatedAt",
                            HeaderText = "CreatedAt",
                            DataPropertyName = "CreatedAt"
                        });
                        dataGridView1.Columns.Add(new DataGridViewButtonColumn
                        {
                            Name = "Approve",
                            HeaderText = "Approve",
                            Text = "Approve",
                            UseColumnTextForButtonValue = true
                        });
                        dataGridView1.Columns.Add(new DataGridViewButtonColumn
                        {
                            Name = "Reject",
                            HeaderText = "Reject",
                            Text = "Reject",
                            UseColumnTextForButtonValue = true
                        });

                        // Set DataSource
                        dataGridView1.DataSource = dt;

                        // Ensure button columns are at the end
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            if (col.Name == "Approve" || col.Name == "Reject")
                                col.DisplayIndex = dataGridView1.Columns.Count - 1;
                        }

                        // Hide UserID column
                        if (dataGridView1.Columns["UserID"] != null)
                            dataGridView1.Columns["UserID"].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR LOADING PENDING USERS: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void ApproveUser(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        // Update UserStatus
                        string updateQuery = @"
                            UPDATE AppUsers
                            SET UserStatus = 1
                            WHERE UserID = @userId AND UserStatus = 0";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@userId", userId);
                            int rowsAffected = updateCmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                MessageBox.Show("USER NOT FOUND OR ALREADY PROCESSED.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }
                        }

                        transaction.Commit();
                        LoadPendingUsers(); // Auto-refresh grid
                        MessageBox.Show("USER APPROVED!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR APPROVING USER: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Reject user
        private void RejectUser(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        // Delete user
                        string deleteQuery = @"
                            DELETE FROM AppUsers
                            WHERE UserID = @userId AND UserStatus = 0";
                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@userId", userId);
                            int rowsAffected = deleteCmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                MessageBox.Show("USER NOT FOUND OR ALREADY PROCESSED.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }
                        }

                        transaction.Commit();
                        LoadPendingUsers(); // Auto-refresh grid
                        MessageBox.Show("USER REJECTED AND DELETED!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR REJECTING USER: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT CategoryID, CategoryName
                        FROM CATEGORY";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Clear existing columns
                        dataGridView2.Columns.Clear();

                        // Add columns programmatically
                        dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "CategoryID",
                            HeaderText = "CategoryID",
                            DataPropertyName = "CategoryID",
                            Visible = false
                        });
                        dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
                        {
                            Name = "CategoryName",
                            HeaderText = "CategoryName",
                            DataPropertyName = "CategoryName"
                        });
       
                        // Set DataSource
                        dataGridView2.DataSource = dt;

                        // Hide CategoryID column
                        //if (dataGridView2.Columns["CategoryID"] != null)
                        //    dataGridView2.Columns["CategoryID"].Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR LOADING CATEGORIES: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                else if (control is Label)
                {
                    ((Label)control).Text = "";
                }
            }
        }

     
        private void AdminInterface_Load(object sender, EventArgs e)
        {
            UnlockForm();

            ClearForm();
            string[] reportOptions = 
            {
    "Trip Booking and Revenue Report",
    "Traveler Demographics and Preferences Report",
    "Tour Operator Performance Report",
    "Service Provider Efficiency Report",
    "Destination Popularity Report",
    "Abandoned Booking Analysis Report",
    "Platform Growth Report"
           };

            comboBox1.Items.AddRange(reportOptions);
            LoadPendingUsers();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            LockFormExceptUserRequests();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            _parentForm.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            UnlockForm();
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


        private void LockFormExceptUserRequests()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel1);
            }

            // Enable all children of HotelPanel
            foreach (Control child in panel1.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void LockFormExceptREVENUE()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel5);
            }

            // Enable all children of HotelPanel
            foreach (Control child in panel5.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void LockFormExceptReport1()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel6);
            }

            // Enable all children of HotelPanel
            foreach (Control child in panel6.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }


        private void LockFormExceptReviewRequests()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel4);
            }

            // Enable all children of HotelPanel
            foreach (Control child in panel4.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void LockFormExceptAddTourCategory()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel2);
            }

            // Enable all children of HotelPanel
            foreach (Control child in panel2.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void LockFormExceptDisplayTourCategory()
        {
            this.SuspendLayout();

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Enabled = (ctrl == panel3);
            }

            // Enable all children of HotelPanel
            foreach (Control child in panel3.Controls)
            {
                child.Enabled = true;
            }

            this.ControlBox = false;
            this.ResumeLayout(true);
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["UserID"].Value);

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Approve")
            {
                ApproveUser(userId);
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Reject")
            {
                RejectUser(userId);
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LockFormExceptAddTourCategory();
            panel2.Visible = true;
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            textBox9.Text = "";
            panel2.Visible=false;
            UnlockForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
            textBox9.Focus();
            LockFormExceptDisplayTourCategory();
            LoadCategories();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string input = textBox9.Text.Trim();

            // Length check
            if (input.Length < 8 || input.Length > 20)
            {
                textBox9.BackColor = Color.LightCoral;
                return;
            }

            // Check uniqueness from DB
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM CATEGORY WHERE CategoryName = @name";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", input);
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            textBox9.BackColor = Color.OrangeRed;
                            MessageBox.Show("THIS CATEGORY NAME ALREADY EXISTS. PLEASE ENTER A UNIQUE CATEGORY.", "DUPLICATE CATEGORY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            textBox9.BackColor = Color.White;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("DATABASE ERROR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
            UnlockForm();
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow alphabetic characters , spaces and control keys
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar!=' ')
            {
                e.Handled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string categoryName = textBox9.Text.Trim();

            if (textBox9.BackColor == Color.LightCoral)
            {
                MessageBox.Show("PLEASE ENTER A VALID CATEGORY NAME.", "INVALID CATEGORY", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString)) // replace with your actual connection string
            {
                try
                {
                    conn.Open();

                    //// Check if category already exists
                    //string checkQuery = "SELECT COUNT(*) FROM CATEGORY WHERE CategoryName = @name";
                    //using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    //{
                    //    checkCmd.Parameters.AddWithValue("@name", categoryName);
                    //    int count = (int)checkCmd.ExecuteScalar();

                    //    if (count > 0)
                    //    {
                    //        MessageBox.Show("CATEGORY ALREADY EXISTS.", "DUPLICATE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //}

                    // Insert new category
                    string insertQuery = "INSERT INTO CATEGORY (CategoryName) VALUES (@name)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@name", categoryName);
                        int rowsAffected = insertCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("CATEGORY ADDED SUCCESSFULLY!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            textBox9.Clear();
                        }
                        else
                        {
                            MessageBox.Show("FAILED TO ADD CATEGORY.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message, "DATABASE ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

                panel2.Visible = false;
            UnlockForm();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadReviews();
            panel4.Visible = true;
            LockFormExceptReviewRequests();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            UnlockForm();
        }


        private void ApproveReview(int reviewId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        // Update ApproveStatus
                        string updateQuery = @"
                            UPDATE REVIEW
                            SET ApproveStatus = 1
                            WHERE ReviewID = @reviewId AND ApproveStatus = 0";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction))
                        {
                            updateCmd.Parameters.AddWithValue("@reviewId", reviewId);
                            int rowsAffected = updateCmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                MessageBox.Show("REVIEW NOT FOUND OR ALREADY PROCESSED.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }
                        }

                        transaction.Commit();
                        LoadReviews(); // Auto-refresh grid
                        MessageBox.Show("REVIEW APPROVED!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR APPROVING REVIEW: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RejectReview(int reviewId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        // Delete review
                        string deleteQuery = @"
                            DELETE FROM REVIEW
                            WHERE ReviewID = @reviewId AND ApproveStatus = 0";
                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn, transaction))
                        {
                            deleteCmd.Parameters.AddWithValue("@reviewId", reviewId);
                            int rowsAffected = deleteCmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                MessageBox.Show("REVIEW NOT FOUND OR ALREADY PROCESSED.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                transaction.Rollback();
                                return;
                            }
                        }

                        transaction.Commit();
                        LoadReviews(); // Auto-refresh grid
                        MessageBox.Show("REVIEW REJECTED AND DELETED!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR REJECTING REVIEW: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int reviewId = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells["ReviewID"].Value);

            if (dataGridView3.Columns[e.ColumnIndex].Name == "Approve")
            {
                ApproveReview(reviewId);
            }
            else if (dataGridView3.Columns[e.ColumnIndex].Name == "Reject")
            {
                RejectReview(reviewId);
            }
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


        private void button6_Click(object sender, EventArgs e)
        {
            LockFormExceptREVENUE();
            panel5.Visible = true;
            LoadRevenue();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            UnlockForm();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


  
       



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "Trip Booking and Revenue Report")
            {
                GenerateTripBookingRevenueReport();
            }
            else if (comboBox1.SelectedItem.ToString() == "Traveler Demographics and Preferences Report")
            {
                GenerateTravelerDemographicsAndPreferencesReport();
            }
            else if (comboBox1.SelectedItem.ToString() == "Tour Operator Performance Report")
            {
                GenerateTourOperatorPerformanceReport();
            }
            else if (comboBox1.SelectedItem.ToString() == "Service Provider Efficiency Report")
            {
                GenerateServiceProviderEfficiencyReport();
            }
            else if (comboBox1.SelectedItem.ToString() == "Destination Popularity Report")
            {
                GenerateDestinationBookingInsightsReport();
            }
            else if (comboBox1.SelectedItem.ToString() == "Abandoned Booking Analysis Report")
            {
                GenerateAbandonedBookingAnalysisReport();
            }
            else if (comboBox1.SelectedItem.ToString() == "Platform Growth Report")
            {
                GeneratePlatformGrowthReport();
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            reportViewer1.Visible = false;
            panel6.Visible = false;
            UnlockForm();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            reportViewer2.Visible = false;
            panel7.Visible = false;
            UnlockForm();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            reportViewer3.Visible = false;
            panel8.Visible = false;
            UnlockForm();
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            reportViewer4.Visible = false;
            panel9.Visible = false;
            UnlockForm();
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            reportViewer4.Visible = false;
            panel9.Visible = false;
            UnlockForm();
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            reportViewer2.Visible = false;
            panel7.Visible = false;
            UnlockForm();
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            reportViewer3.Visible = false;
            panel8.Visible = false;
            UnlockForm();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            reportViewer5.Visible = false;
            panel10.Visible = false;
            UnlockForm();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            reportViewer6.Visible = false;
            panel11.Visible = false;
            UnlockForm();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            reportViewer7.Visible = false;
            panel12.Visible = false;
            UnlockForm(); 
        }
    }

}


