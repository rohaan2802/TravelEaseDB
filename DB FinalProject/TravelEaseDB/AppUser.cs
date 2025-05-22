using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TravelEaseDB;
using System.Runtime.CompilerServices;




namespace TravelEaseDB
{

    public partial class AppUser_Form : Form
    {

        System.Windows.Forms.Timer delayTimer;
        private SignUp_Admin_form form2;
        private ServiceProviderSignUP form3;
        private TourOperatorSignUP form4;
        private TravelerSignUPForm form5;
        private SignINForm form6;
        private AdminInterface form7;
        private TourOperatorInterface form8;
        private ServiceProviderInterface form9;
        private TravelerInterface form10;

        Control panelToHide = null;
        string connectionString = "Data Source=CODE-TECH\\SQLEXPRESS;Initial Catalog=TravelEaseDB;Integrated Security=True;";

        public AppUser_Form()
        {

            InitializeComponent();
            this.Activated += Form1_Activated;

            // Initialize timer
            delayTimer = new System.Windows.Forms.Timer();
            delayTimer.Interval = 1; //1 millisecond
            delayTimer.Tick += DelayTimer_Tick;

            this.form2 = new SignUp_Admin_form(this);
            this.form3 = new ServiceProviderSignUP(this);
            this.form4 = new TourOperatorSignUP(this);
            this.form5 = new TravelerSignUPForm(this);
            this.form6 = new SignINForm(this);
            this.form7 = new AdminInterface(this);
            this.form8 = new TourOperatorInterface(this);
            this.form9 = new ServiceProviderInterface(this);
            this.form10 = new TravelerInterface(this);



            this.WindowState = FormWindowState.Maximized;
            // Attach mouse events
            button1.MouseEnter += button1_MouseEnter;
            button1.MouseLeave += button1_MouseLeave;

            //button11.MouseEnter += button11_MouseEnter;
            //button11.MouseLeave += button11_MouseLeave;

            button2.MouseEnter += button1_MouseEnter;
            button2.MouseLeave += button1_MouseLeave;

            button3.MouseEnter += button1_MouseEnter;
            button3.MouseLeave += button1_MouseLeave;

            button4.MouseEnter += button1_MouseEnter;
            button4.MouseLeave += button1_MouseLeave;

            button5.MouseEnter += button1_MouseEnter;
            button5.MouseLeave += button1_MouseLeave;

            Panel_SignUP.MouseEnter += button1_MouseEnter;
            Panel_SignUP.MouseLeave += button1_MouseLeave;

            //Panel_SignIN.MouseEnter += button11_MouseEnter;
            //Panel_SignIN.MouseLeave += button11_MouseLeave;

            button5.MouseEnter += button1_MouseEnter;
            button5.MouseLeave += button1_MouseLeave;

            //button6.MouseEnter += button11_MouseEnter;
            //button6.MouseLeave += button11_MouseLeave;

            //button7.MouseEnter += button11_MouseEnter;
            //button7.MouseLeave += button11_MouseLeave;

            //button8.MouseEnter += button11_MouseEnter;
            //button8.MouseLeave += button11_MouseLeave;

            //button9.MouseEnter += button11_MouseEnter;
            //button9.MouseLeave += button11_MouseLeave;

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();


            this.MinimizeBox = false; // Disable minimize button
            this.MaximizeBox = false; // Disable maximize button
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            // Create a dummy invisible label and focus it
            Label dummyLabel = new Label { Width = 0, Height = 0 };
            this.Controls.Add(dummyLabel);
            dummyLabel.Focus();
            this.Controls.Remove(dummyLabel); // Remove immediately
        }


        #region Function:Prevent Control Flickering & Form lag when resize

        static void SetDoubleBuffered(Control ctl,bool DoubleBuffered)
        {
            try
            {
                typeof(Control).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, ctl, new object[] { DoubleBuffered });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // WS_EX_COMPOSITED
                return cp;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            if (!Panel_SignUP.Visible)
                Panel_SignUP.Visible = true;

            delayTimer.Stop();
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            panelToHide = Panel_SignUP;
            delayTimer.Start();
        }

        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            delayTimer.Stop();

            // Suspend layout and drawing before hiding
            if (panelToHide != null)
            {
                panelToHide.SuspendLayout();
                panelToHide.SendToBack();
                panelToHide.Visible = false;
                panelToHide.ResumeLayout(false);
                panelToHide = null;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Hide the sign-up panel
          //  form6.ShowDialog();

            if (form6.ShowDialog() == DialogResult.OK)
            {
                string role = form6.LoggedInUserRole;

                // Hide or close AppUser form
           //     this.Hide(); // or this.Close(); if this is not the actual Main Form

                // Open the role-specific form
                if (role == "Admin")
                {
                  form7.ShowDialog();
                }
                else if(role== "TourOperator")
                {
                    form8.ShowDialog();
                }
                else if(role== "ServiceProvider")
                {
                    form9.ShowDialog();
                }
                else if(role== "Traveler")
                {
                    // Open Traveler Interface
                    form10.ShowDialog();
                }
               
                // else open other forms based on role...
            }

        }

        //private void button11_MouseEnter(object sender, EventArgs e)
        //{
        //    if (!Panel_SignIN.Visible)
        //        Panel_SignIN.Visible = true;

        //    delayTimer.Stop();
        //}

        //private void button11_MouseLeave(object sender, EventArgs e)
        //{
        //    panelToHide = Panel_SignIN;
        //    delayTimer.Start();
        //}

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //foreach (Control control in this.Controls)
            //{
            //    if (control is Button button)
            //    {
            //        button.TabStop = false;
            //    }
            //}
        }

        //private void OpenNewForm(object obj)
        //{
        //    this.Opacity=0;
        //    Application.Run(new SignUp_Admin_form(this));
        //}

        private void button2_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM AppUsers WHERE UserRole = 'Admin'", conn);
                int adminCount = (int)cmd.ExecuteScalar();

                if (adminCount >= 4)
                {
                    MessageBox.Show("MAXIMUM OF 4 ADMIN ACCOUNTS ARE ALLOWED. CANNOT SIGN UP MORE ADMINS.", "LIMIT REACHED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Admin count is less than 3, proceed to open the signup form
                
            }
           
            form2.ShowDialog(); 
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            form3.ShowDialog();
        }

        private void AppUser_Form_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void AppUser_Form_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            form4.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form5.ShowDialog();
        }
    }


    public class BufferedPanel : Panel
    {
        public BufferedPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }
    }


}
