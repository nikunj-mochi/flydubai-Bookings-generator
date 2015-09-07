namespace CreatePNR_AutomationApp
{
    partial class frmCreatePNRs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreatePNRs));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbIBMeal = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbIBBaggage = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtIBFlightNo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbOBMeal = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbOBBaggage = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.chkSeating = new System.Windows.Forms.CheckBox();
            this.chkIFE = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbIBCabin = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbOBCabin = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtVoucherNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOBFlightNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPNRs = new System.Windows.Forms.NumericUpDown();
            this.txtInfantCount = new System.Windows.Forms.NumericUpDown();
            this.txtChildCount = new System.Windows.Forms.NumericUpDown();
            this.txtAdtCount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.dtArrivalDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtDepartureDate = new System.Windows.Forms.DateTimePicker();
            this.lblInfCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRT = new System.Windows.Forms.CheckBox();
            this.lblOrigin = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.cmbDestination = new System.Windows.Forms.TextBox();
            this.txtOrigin = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lnkDownload = new System.Windows.Forms.LinkLabel();
            this.lblUploadInfo = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnProcessPNRs = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblInfo = new System.Windows.Forms.Label();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lnkOpenLog = new System.Windows.Forms.LinkLabel();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblApplicationVersion = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNRs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInfantCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChildCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdtCount)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbIBMeal);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.cmbIBBaggage);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtIBFlightNo);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.cmbOBMeal);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.cmbOBBaggage);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.chkSeating);
            this.groupBox1.Controls.Add(this.chkIFE);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.cmbIBCabin);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cmbOBCabin);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtVoucherNo);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtOBFlightNo);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPNRs);
            this.groupBox1.Controls.Add(this.txtInfantCount);
            this.groupBox1.Controls.Add(this.txtChildCount);
            this.groupBox1.Controls.Add(this.txtAdtCount);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtArrivalDate);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtDepartureDate);
            this.groupBox1.Controls.Add(this.lblInfCount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkRT);
            this.groupBox1.Controls.Add(this.lblOrigin);
            this.groupBox1.Controls.Add(this.lblDestination);
            this.groupBox1.Controls.Add(this.cmbDestination);
            this.groupBox1.Controls.Add(this.txtOrigin);
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Location = new System.Drawing.Point(12, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(699, 285);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option1";
            // 
            // cmbIBMeal
            // 
            this.cmbIBMeal.FormattingEnabled = true;
            this.cmbIBMeal.Location = new System.Drawing.Point(485, 174);
            this.cmbIBMeal.Name = "cmbIBMeal";
            this.cmbIBMeal.Size = new System.Drawing.Size(116, 21);
            this.cmbIBMeal.TabIndex = 16;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(392, 182);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 13);
            this.label15.TabIndex = 65;
            this.label15.Text = "Inbound Meal";
            // 
            // cmbIBBaggage
            // 
            this.cmbIBBaggage.FormattingEnabled = true;
            this.cmbIBBaggage.Items.AddRange(new object[] {
            "BAGB",
            "BAGL",
            "BAGX"});
            this.cmbIBBaggage.Location = new System.Drawing.Point(486, 147);
            this.cmbIBBaggage.Name = "cmbIBBaggage";
            this.cmbIBBaggage.Size = new System.Drawing.Size(116, 21);
            this.cmbIBBaggage.TabIndex = 14;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(376, 155);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 13);
            this.label14.TabIndex = 63;
            this.label14.Text = "Inbound Baggage";
            // 
            // txtIBFlightNo
            // 
            this.txtIBFlightNo.Enabled = false;
            this.txtIBFlightNo.Location = new System.Drawing.Point(485, 69);
            this.txtIBFlightNo.MaxLength = 11;
            this.txtIBFlightNo.Name = "txtIBFlightNo";
            this.txtIBFlightNo.Size = new System.Drawing.Size(100, 20);
            this.txtIBFlightNo.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(380, 72);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 61;
            this.label13.Text = "Inbound FlightNo";
            // 
            // cmbOBMeal
            // 
            this.cmbOBMeal.FormattingEnabled = true;
            this.cmbOBMeal.Location = new System.Drawing.Point(160, 174);
            this.cmbOBMeal.Name = "cmbOBMeal";
            this.cmbOBMeal.Size = new System.Drawing.Size(116, 21);
            this.cmbOBMeal.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(50, 182);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 13);
            this.label12.TabIndex = 59;
            this.label12.Text = "Outbound Meal";
            // 
            // cmbOBBaggage
            // 
            this.cmbOBBaggage.FormattingEnabled = true;
            this.cmbOBBaggage.Location = new System.Drawing.Point(160, 147);
            this.cmbOBBaggage.Name = "cmbOBBaggage";
            this.cmbOBBaggage.Size = new System.Drawing.Size(116, 21);
            this.cmbOBBaggage.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(31, 155);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 13);
            this.label11.TabIndex = 57;
            this.label11.Text = "Outbound Baggage";
            // 
            // chkSeating
            // 
            this.chkSeating.AutoSize = true;
            this.chkSeating.Location = new System.Drawing.Point(160, 233);
            this.chkSeating.Name = "chkSeating";
            this.chkSeating.Size = new System.Drawing.Size(68, 17);
            this.chkSeating.TabIndex = 20;
            this.chkSeating.Text = "Seating?";
            this.chkSeating.UseVisualStyleBackColor = true;
            // 
            // chkIFE
            // 
            this.chkIFE.AutoSize = true;
            this.chkIFE.Location = new System.Drawing.Point(96, 233);
            this.chkIFE.Name = "chkIFE";
            this.chkIFE.Size = new System.Drawing.Size(48, 17);
            this.chkIFE.TabIndex = 19;
            this.chkIFE.Text = "IFE?";
            this.chkIFE.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(359, 256);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbIBCabin
            // 
            this.cmbIBCabin.FormattingEnabled = true;
            this.cmbIBCabin.Items.AddRange(new object[] {
            "Economy",
            "Business"});
            this.cmbIBCabin.Location = new System.Drawing.Point(486, 120);
            this.cmbIBCabin.Name = "cmbIBCabin";
            this.cmbIBCabin.Size = new System.Drawing.Size(116, 21);
            this.cmbIBCabin.TabIndex = 12;
            this.cmbIBCabin.SelectedValueChanged += new System.EventHandler(this.cmbIBCabin_SelectedValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(392, 128);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 50;
            this.label10.Text = "Inbound Cabin";
            // 
            // cmbOBCabin
            // 
            this.cmbOBCabin.FormattingEnabled = true;
            this.cmbOBCabin.Items.AddRange(new object[] {
            "Economy",
            "Business"});
            this.cmbOBCabin.Location = new System.Drawing.Point(159, 120);
            this.cmbOBCabin.Name = "cmbOBCabin";
            this.cmbOBCabin.Size = new System.Drawing.Size(116, 21);
            this.cmbOBCabin.TabIndex = 11;
            this.cmbOBCabin.SelectedValueChanged += new System.EventHandler(this.cmbOBCabin_SelectedValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(53, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "Outbound Cabin";
            // 
            // txtVoucherNo
            // 
            this.txtVoucherNo.Location = new System.Drawing.Point(486, 202);
            this.txtVoucherNo.MaxLength = 7;
            this.txtVoucherNo.Name = "txtVoucherNo";
            this.txtVoucherNo.Size = new System.Drawing.Size(100, 20);
            this.txtVoucherNo.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(406, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "Voucher no";
            // 
            // txtOBFlightNo
            // 
            this.txtOBFlightNo.Location = new System.Drawing.Point(159, 72);
            this.txtOBFlightNo.MaxLength = 11;
            this.txtOBFlightNo.Name = "txtOBFlightNo";
            this.txtOBFlightNo.Size = new System.Drawing.Size(100, 20);
            this.txtOBFlightNo.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Outbound FlightNo";
            // 
            // txtPNRs
            // 
            this.txtPNRs.Location = new System.Drawing.Point(160, 202);
            this.txtPNRs.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txtPNRs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtPNRs.Name = "txtPNRs";
            this.txtPNRs.Size = new System.Drawing.Size(57, 20);
            this.txtPNRs.TabIndex = 17;
            this.txtPNRs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtInfantCount
            // 
            this.txtInfantCount.Location = new System.Drawing.Point(485, 95);
            this.txtInfantCount.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.txtInfantCount.Name = "txtInfantCount";
            this.txtInfantCount.Size = new System.Drawing.Size(57, 20);
            this.txtInfantCount.TabIndex = 10;
            // 
            // txtChildCount
            // 
            this.txtChildCount.Location = new System.Drawing.Point(303, 96);
            this.txtChildCount.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.txtChildCount.Name = "txtChildCount";
            this.txtChildCount.Size = new System.Drawing.Size(57, 20);
            this.txtChildCount.TabIndex = 9;
            // 
            // txtAdtCount
            // 
            this.txtAdtCount.Location = new System.Drawing.Point(160, 96);
            this.txtAdtCount.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.txtAdtCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtAdtCount.Name = "txtAdtCount";
            this.txtAdtCount.Size = new System.Drawing.Size(57, 20);
            this.txtAdtCount.TabIndex = 8;
            this.txtAdtCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(429, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "Return";
            // 
            // dtArrivalDate
            // 
            this.dtArrivalDate.Enabled = false;
            this.dtArrivalDate.Location = new System.Drawing.Point(485, 44);
            this.dtArrivalDate.Name = "dtArrivalDate";
            this.dtArrivalDate.Size = new System.Drawing.Size(200, 20);
            this.dtArrivalDate.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(82, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Departure";
            // 
            // dtDepartureDate
            // 
            this.dtDepartureDate.Location = new System.Drawing.Point(160, 46);
            this.dtDepartureDate.MinDate = new System.DateTime(2015, 8, 26, 0, 0, 0, 0);
            this.dtDepartureDate.Name = "dtDepartureDate";
            this.dtDepartureDate.Size = new System.Drawing.Size(200, 20);
            this.dtDepartureDate.TabIndex = 4;
            this.dtDepartureDate.ValueChanged += new System.EventHandler(this.dtDepartureDate_ValueChanged);
            // 
            // lblInfCount
            // 
            this.lblInfCount.AutoSize = true;
            this.lblInfCount.Location = new System.Drawing.Point(430, 102);
            this.lblInfCount.Name = "lblInfCount";
            this.lblInfCount.Size = new System.Drawing.Size(34, 13);
            this.lblInfCount.TabIndex = 35;
            this.lblInfCount.Text = "Infant";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Child";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "ADT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "No. Of PNRs";
            // 
            // chkRT
            // 
            this.chkRT.AutoSize = true;
            this.chkRT.Location = new System.Drawing.Point(486, 23);
            this.chkRT.Name = "chkRT";
            this.chkRT.Size = new System.Drawing.Size(47, 17);
            this.chkRT.TabIndex = 3;
            this.chkRT.Text = "RT?";
            this.chkRT.UseVisualStyleBackColor = true;
            this.chkRT.CheckedChanged += new System.EventHandler(this.chkRT_CheckedChanged);
            // 
            // lblOrigin
            // 
            this.lblOrigin.AutoSize = true;
            this.lblOrigin.Location = new System.Drawing.Point(102, 27);
            this.lblOrigin.Name = "lblOrigin";
            this.lblOrigin.Size = new System.Drawing.Size(34, 13);
            this.lblOrigin.TabIndex = 30;
            this.lblOrigin.Text = "Origin";
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(293, 24);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(60, 13);
            this.lblDestination.TabIndex = 29;
            this.lblDestination.Text = "Destination";
            // 
            // cmbDestination
            // 
            this.cmbDestination.Location = new System.Drawing.Point(359, 17);
            this.cmbDestination.MaxLength = 3;
            this.cmbDestination.Name = "cmbDestination";
            this.cmbDestination.Size = new System.Drawing.Size(100, 20);
            this.cmbDestination.TabIndex = 2;
            this.cmbDestination.Leave += new System.EventHandler(this.txtDestination_TextChanged);
            // 
            // txtOrigin
            // 
            this.txtOrigin.Location = new System.Drawing.Point(160, 19);
            this.txtOrigin.MaxLength = 3;
            this.txtOrigin.Name = "txtOrigin";
            this.txtOrigin.Size = new System.Drawing.Size(100, 20);
            this.txtOrigin.TabIndex = 1;
            this.txtOrigin.Text = "DXB";
            this.txtOrigin.Leave += new System.EventHandler(this.txtOrigin_Leave);
            // 
            // btnCreate
            // 
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Location = new System.Drawing.Point(264, 256);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 21;
            this.btnCreate.Text = "Create PNRs";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lnkDownload);
            this.groupBox2.Controls.Add(this.lblUploadInfo);
            this.groupBox2.Controls.Add(this.btnBrowse);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnProcessPNRs);
            this.groupBox2.Location = new System.Drawing.Point(12, 303);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(699, 79);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Option2 - Upload excel";
            // 
            // lnkDownload
            // 
            this.lnkDownload.AutoSize = true;
            this.lnkDownload.Location = new System.Drawing.Point(587, 16);
            this.lnkDownload.Name = "lnkDownload";
            this.lnkDownload.Size = new System.Drawing.Size(98, 13);
            this.lnkDownload.TabIndex = 53;
            this.lnkDownload.TabStop = true;
            this.lnkDownload.Text = "Download template";
            this.lnkDownload.Visible = false;
            this.lnkDownload.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDownload_LinkClicked);
            // 
            // lblUploadInfo
            // 
            this.lblUploadInfo.AutoSize = true;
            this.lblUploadInfo.Location = new System.Drawing.Point(433, 41);
            this.lblUploadInfo.Name = "lblUploadInfo";
            this.lblUploadInfo.Size = new System.Drawing.Size(35, 13);
            this.lblUploadInfo.TabIndex = 6;
            this.lblUploadInfo.Text = "label9";
            // 
            // btnBrowse
            // 
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(152, 32);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(110, 23);
            this.btnBrowse.TabIndex = 23;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Input PNR source file";
            // 
            // btnProcessPNRs
            // 
            this.btnProcessPNRs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessPNRs.Location = new System.Drawing.Point(298, 32);
            this.btnProcessPNRs.Name = "btnProcessPNRs";
            this.btnProcessPNRs.Size = new System.Drawing.Size(112, 23);
            this.btnProcessPNRs.TabIndex = 24;
            this.btnProcessPNRs.Text = "Process PNRs";
            this.btnProcessPNRs.UseVisualStyleBackColor = true;
            this.btnProcessPNRs.Click += new System.EventHandler(this.btnProcessPNRs_Click);
            // 
            // progressBar
            // 
            this.progressBar.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.progressBar.Location = new System.Drawing.Point(341, 392);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(370, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 28;
            this.progressBar.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 402);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(37, 13);
            this.lblInfo.TabIndex = 29;
            this.lblInfo.Text = "Status";
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            this.fileDialog.Filter = "Excel files|*.xlsx";
            // 
            // lnkOpenLog
            // 
            this.lnkOpenLog.AutoSize = true;
            this.lnkOpenLog.Location = new System.Drawing.Point(632, 5);
            this.lnkOpenLog.Name = "lnkOpenLog";
            this.lnkOpenLog.Size = new System.Drawing.Size(79, 13);
            this.lnkOpenLog.TabIndex = 52;
            this.lnkOpenLog.TabStop = true;
            this.lnkOpenLog.Text = "Open log folder";
            this.lnkOpenLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOpenLog_LinkClicked);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(15, 447);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(622, 81);
            this.txtStatus.TabIndex = 53;
            this.txtStatus.Visible = false;
            // 
            // lblApplicationVersion
            // 
            this.lblApplicationVersion.AutoSize = true;
            this.lblApplicationVersion.Location = new System.Drawing.Point(625, 423);
            this.lblApplicationVersion.Name = "lblApplicationVersion";
            this.lblApplicationVersion.Size = new System.Drawing.Size(81, 13);
            this.lblApplicationVersion.TabIndex = 54;
            this.lblApplicationVersion.Text = "Version: 1.0.0.6";
            // 
            // frmCreatePNRs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(723, 449);
            this.Controls.Add(this.lblApplicationVersion);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lnkOpenLog);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCreatePNRs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "flydubai- Booking Generator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPNRs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInfantCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChildCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdtCount)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOBFlightNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown txtPNRs;
        private System.Windows.Forms.NumericUpDown txtInfantCount;
        private System.Windows.Forms.NumericUpDown txtChildCount;
        private System.Windows.Forms.NumericUpDown txtAdtCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtArrivalDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtDepartureDate;
        private System.Windows.Forms.Label lblInfCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRT;
        private System.Windows.Forms.Label lblOrigin;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.TextBox cmbDestination;
        private System.Windows.Forms.TextBox txtOrigin;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnProcessPNRs;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtVoucherNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.Label lblUploadInfo;
        private System.Windows.Forms.ComboBox cmbOBCabin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbIBCabin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.LinkLabel lnkOpenLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.LinkLabel lnkDownload;
        private System.Windows.Forms.CheckBox chkIFE;
        private System.Windows.Forms.CheckBox chkSeating;
        private System.Windows.Forms.ComboBox cmbOBBaggage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbOBMeal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtIBFlightNo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbIBBaggage;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbIBMeal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblApplicationVersion;

    }
}

