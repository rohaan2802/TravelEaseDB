namespace TravelEaseForm
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtBookingID;
        private System.Windows.Forms.DateTimePicker dtBookingDate;
        private System.Windows.Forms.TextBox txtTravelerID;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.NumericUpDown numGroupSize;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Button btnSaveBooking;
        private System.Windows.Forms.Button btnOpenPayment;
        private System.Windows.Forms.Button btnOpenCancellation;  // New Button for Cancellation Form
        private System.Windows.Forms.Label lblBookingID;
        private System.Windows.Forms.Label lblBookingDate;
        private System.Windows.Forms.Label lblTravelerID;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblGroupSize;
        private System.Windows.Forms.Label lblTotalAmount;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtBookingID = new System.Windows.Forms.TextBox();
            this.dtBookingDate = new System.Windows.Forms.DateTimePicker();
            this.txtTravelerID = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.numGroupSize = new System.Windows.Forms.NumericUpDown();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.btnSaveBooking = new System.Windows.Forms.Button();
            this.btnOpenPayment = new System.Windows.Forms.Button();
            this.btnOpenCancellation = new System.Windows.Forms.Button(); // New Button Initialization

            this.lblBookingID = new System.Windows.Forms.Label();
            this.lblBookingDate = new System.Windows.Forms.Label();
            this.lblTravelerID = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblGroupSize = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.numGroupSize)).BeginInit();
            this.SuspendLayout();

            // lblBookingID
            this.lblBookingID.AutoSize = true;
            this.lblBookingID.Location = new System.Drawing.Point(30, 20);
            this.lblBookingID.Name = "lblBookingID";
            this.lblBookingID.Size = new System.Drawing.Size(75, 17);
            this.lblBookingID.TabIndex = 0;
            this.lblBookingID.Text = "Booking ID";

            // lblBookingDate
            this.lblBookingDate.AutoSize = true;
            this.lblBookingDate.Location = new System.Drawing.Point(30, 60);
            this.lblBookingDate.Name = "lblBookingDate";
            this.lblBookingDate.Size = new System.Drawing.Size(92, 17);
            this.lblBookingDate.TabIndex = 1;
            this.lblBookingDate.Text = "Booking Date";

            // lblTravelerID
            this.lblTravelerID.AutoSize = true;
            this.lblTravelerID.Location = new System.Drawing.Point(30, 100);
            this.lblTravelerID.Name = "lblTravelerID";
            this.lblTravelerID.Size = new System.Drawing.Size(75, 17);
            this.lblTravelerID.TabIndex = 2;
            this.lblTravelerID.Text = "Traveler ID";

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(30, 140);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(49, 17);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status";

            // lblGroupSize
            this.lblGroupSize.AutoSize = true;
            this.lblGroupSize.Location = new System.Drawing.Point(30, 180);
            this.lblGroupSize.Name = "lblGroupSize";
            this.lblGroupSize.Size = new System.Drawing.Size(80, 17);
            this.lblGroupSize.TabIndex = 4;
            this.lblGroupSize.Text = "Group Size";

            // lblTotalAmount
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(30, 220);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(97, 17);
            this.lblTotalAmount.TabIndex = 5;
            this.lblTotalAmount.Text = "Total Amount";

            // txtBookingID
            this.txtBookingID.Location = new System.Drawing.Point(130, 20);
            this.txtBookingID.Name = "txtBookingID";
            this.txtBookingID.Size = new System.Drawing.Size(200, 22);
            this.txtBookingID.TabIndex = 6;

            // dtBookingDate
            this.dtBookingDate.Location = new System.Drawing.Point(130, 60);
            this.dtBookingDate.Name = "dtBookingDate";
            this.dtBookingDate.Size = new System.Drawing.Size(200, 22);
            this.dtBookingDate.TabIndex = 7;

            // txtTravelerID
            this.txtTravelerID.Location = new System.Drawing.Point(130, 100);
            this.txtTravelerID.Name = "txtTravelerID";
            this.txtTravelerID.Size = new System.Drawing.Size(200, 22);
            this.txtTravelerID.TabIndex = 8;

            // cmbStatus
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Location = new System.Drawing.Point(130, 140);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 24);
            this.cmbStatus.TabIndex = 9;

            // Add status options to ComboBox
            this.cmbStatus.Items.AddRange(new object[] {
                "Pending", "Confirmed", "Cancelled"
            });

            // numGroupSize
            this.numGroupSize.Location = new System.Drawing.Point(130, 180);
            this.numGroupSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numGroupSize.Name = "numGroupSize";
            this.numGroupSize.Size = new System.Drawing.Size(200, 22);
            this.numGroupSize.TabIndex = 10;
            this.numGroupSize.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // txtTotalAmount
            this.txtTotalAmount.Location = new System.Drawing.Point(130, 220);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(200, 22);
            this.txtTotalAmount.TabIndex = 11;

            // btnSaveBooking
            this.btnSaveBooking.Location = new System.Drawing.Point(130, 260);
            this.btnSaveBooking.Name = "btnSaveBooking";
            this.btnSaveBooking.Size = new System.Drawing.Size(100, 30);
            this.btnSaveBooking.TabIndex = 12;
            this.btnSaveBooking.Text = "Save Booking";
            this.btnSaveBooking.UseVisualStyleBackColor = true;
            this.btnSaveBooking.Click += new System.EventHandler(this.btnSaveBooking_Click);

            // btnOpenPayment
            this.btnOpenPayment.Location = new System.Drawing.Point(230, 260);
            this.btnOpenPayment.Name = "btnOpenPayment";
            this.btnOpenPayment.Size = new System.Drawing.Size(100, 30);
            this.btnOpenPayment.TabIndex = 13;
            this.btnOpenPayment.Text = "Make Payment";
            this.btnOpenPayment.UseVisualStyleBackColor = true;
            this.btnOpenPayment.Click += new System.EventHandler(this.btnOpenPayment_Click);

            // btnOpenCancellation
            this.btnOpenCancellation.Location = new System.Drawing.Point(130, 300);  // Position for Cancellation button
            this.btnOpenCancellation.Name = "btnOpenCancellation";
            this.btnOpenCancellation.Size = new System.Drawing.Size(200, 30);
            this.btnOpenCancellation.TabIndex = 14;
            this.btnOpenCancellation.Text = "Open Cancellations";
            this.btnOpenCancellation.UseVisualStyleBackColor = true;
            this.btnOpenCancellation.Click += new System.EventHandler(this.btnOpenCancellation_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(380, 350);
            this.Controls.Add(this.txtBookingID);
            this.Controls.Add(this.dtBookingDate);
            this.Controls.Add(this.txtTravelerID);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.numGroupSize);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.btnSaveBooking);
            this.Controls.Add(this.btnOpenPayment);
            this.Controls.Add(this.btnOpenCancellation);  // Adding the Cancellation button to form
            this.Controls.Add(this.lblBookingID);
            this.Controls.Add(this.lblBookingDate);
            this.Controls.Add(this.lblTravelerID);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblGroupSize);
            this.Controls.Add(this.lblTotalAmount);
            this.Name = "Form1";
            this.Text = "Travelease - Booking System";

            ((System.ComponentModel.ISupportInitialize)(this.numGroupSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
