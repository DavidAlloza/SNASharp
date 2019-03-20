namespace SNASharp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LogOutputTextBox = new System.Windows.Forms.TextBox();
            this.CalibrationButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SweepEndFrequencyTextbox = new System.Windows.Forms.TextBox();
            this.SweepStartFrequencyTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SweepProgressBar = new System.Windows.Forms.ProgressBar();
            this.AutodetectButton = new System.Windows.Forms.Button();
            this.OperationsTabControl = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.DetectorCombobox = new System.Windows.Forms.ComboBox();
            this.SweepLoopStopButton = new System.Windows.Forms.Button();
            this.SweepLoopProcessButton = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.SweepProcessButton = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.StopVFOButton = new System.Windows.Forms.Button();
            this.StartVFOButton = new System.Windows.Forms.Button();
            this.VFOFrequencyTextBox = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.ParallelCheckBox = new System.Windows.Forms.CheckBox();
            this.MotionalDisplayTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.QSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.AnalyseButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CapacitorTextBox = new System.Windows.Forms.TextBox();
            this.ImpedancetextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.QERBandPassTextBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.SaveDeviceButton = new System.Windows.Forms.Button();
            this.DeviceDelButton = new System.Windows.Forms.Button();
            this.DeviceProperyGrid = new System.Windows.Forms.PropertyGrid();
            this.NewDeviceButton = new System.Windows.Forms.Button();
            this.DevicesComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SamplesTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TransformerComboBox = new System.Windows.Forms.ComboBox();
            this.ControlgroupBox = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.OutputModeComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.AttLevelcomboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectecDeviceComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SerialPortComboBox = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.FirmwareTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.AutodetectCOMcheckBox = new System.Windows.Forms.CheckBox();
            this.backgroundWorkerSerialCapture = new System.ComponentModel.BackgroundWorker();
            this.SpectrumPictureBox = new SNASharp.SpectrumPictureBoxClass();
            this.OperationsTabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.ControlgroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpectrumPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // LogOutputTextBox
            // 
            this.LogOutputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogOutputTextBox.BackColor = System.Drawing.Color.Blue;
            this.LogOutputTextBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogOutputTextBox.ForeColor = System.Drawing.Color.Yellow;
            this.LogOutputTextBox.Location = new System.Drawing.Point(758, 532);
            this.LogOutputTextBox.Multiline = true;
            this.LogOutputTextBox.Name = "LogOutputTextBox";
            this.LogOutputTextBox.ReadOnly = true;
            this.LogOutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogOutputTextBox.Size = new System.Drawing.Size(372, 132);
            this.LogOutputTextBox.TabIndex = 1;
            this.LogOutputTextBox.TextChanged += new System.EventHandler(this.LogOutputTextBox_TextChanged);
            // 
            // CalibrationButton
            // 
            this.CalibrationButton.Location = new System.Drawing.Point(116, 23);
            this.CalibrationButton.Name = "CalibrationButton";
            this.CalibrationButton.Size = new System.Drawing.Size(95, 46);
            this.CalibrationButton.TabIndex = 13;
            this.CalibrationButton.Text = "Run calibration";
            this.CalibrationButton.UseVisualStyleBackColor = true;
            this.CalibrationButton.Click += new System.EventHandler(this.CalibrationButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(138, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "End Frequency (Hz)";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // SweepEndFrequencyTextbox
            // 
            this.SweepEndFrequencyTextbox.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SweepEndFrequencyTextbox.Location = new System.Drawing.Point(141, 91);
            this.SweepEndFrequencyTextbox.MaxLength = 12;
            this.SweepEndFrequencyTextbox.Name = "SweepEndFrequencyTextbox";
            this.SweepEndFrequencyTextbox.Size = new System.Drawing.Size(123, 26);
            this.SweepEndFrequencyTextbox.TabIndex = 12;
            this.SweepEndFrequencyTextbox.Text = "30 000 000";
            this.SweepEndFrequencyTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SweepEndFrequencyTextbox.TextChanged += new System.EventHandler(this.SweepEndFrequencyTextBox_TextChanged);
            this.SweepEndFrequencyTextbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SweepEndFrequencyTextbox_KeyUp);
            // 
            // SweepStartFrequencyTextbox
            // 
            this.SweepStartFrequencyTextbox.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SweepStartFrequencyTextbox.Location = new System.Drawing.Point(12, 91);
            this.SweepStartFrequencyTextbox.MaxLength = 12;
            this.SweepStartFrequencyTextbox.Name = "SweepStartFrequencyTextbox";
            this.SweepStartFrequencyTextbox.Size = new System.Drawing.Size(123, 26);
            this.SweepStartFrequencyTextbox.TabIndex = 11;
            this.SweepStartFrequencyTextbox.Text = "50 000";
            this.SweepStartFrequencyTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SweepStartFrequencyTextbox.TextChanged += new System.EventHandler(this.SweepStartTextbox_TextChanged);
            this.SweepStartFrequencyTextbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SweepStartFrequencyTextbox_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Start Frequency (Hz)";
            // 
            // SweepProgressBar
            // 
            this.SweepProgressBar.Enabled = false;
            this.SweepProgressBar.Location = new System.Drawing.Point(217, 23);
            this.SweepProgressBar.MarqueeAnimationSpeed = 10;
            this.SweepProgressBar.Name = "SweepProgressBar";
            this.SweepProgressBar.Size = new System.Drawing.Size(115, 46);
            this.SweepProgressBar.Step = 100;
            this.SweepProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.SweepProgressBar.TabIndex = 4;
            // 
            // AutodetectButton
            // 
            this.AutodetectButton.Location = new System.Drawing.Point(6, 23);
            this.AutodetectButton.Name = "AutodetectButton";
            this.AutodetectButton.Size = new System.Drawing.Size(107, 46);
            this.AutodetectButton.TabIndex = 2;
            this.AutodetectButton.Text = "COM Port Autodetect";
            this.AutodetectButton.UseVisualStyleBackColor = true;
            this.AutodetectButton.Click += new System.EventHandler(this.AutodetectButton_Click);
            // 
            // OperationsTabControl
            // 
            this.OperationsTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OperationsTabControl.Controls.Add(this.tabPage2);
            this.OperationsTabControl.Controls.Add(this.tabPage4);
            this.OperationsTabControl.Controls.Add(this.tabPage1);
            this.OperationsTabControl.Controls.Add(this.tabPage3);
            this.OperationsTabControl.Location = new System.Drawing.Point(754, 199);
            this.OperationsTabControl.Name = "OperationsTabControl";
            this.OperationsTabControl.SelectedIndex = 0;
            this.OperationsTabControl.Size = new System.Drawing.Size(376, 327);
            this.OperationsTabControl.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.DetectorCombobox);
            this.tabPage2.Controls.Add(this.SweepLoopStopButton);
            this.tabPage2.Controls.Add(this.SweepLoopProcessButton);
            this.tabPage2.Controls.Add(this.checkedListBox1);
            this.tabPage2.Controls.Add(this.SweepProcessButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(368, 301);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sweepmode";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(122, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Detector";
            // 
            // DetectorCombobox
            // 
            this.DetectorCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DetectorCombobox.FormattingEnabled = true;
            this.DetectorCombobox.Location = new System.Drawing.Point(125, 99);
            this.DetectorCombobox.Name = "DetectorCombobox";
            this.DetectorCombobox.Size = new System.Drawing.Size(121, 21);
            this.DetectorCombobox.TabIndex = 4;
            // 
            // SweepLoopStopButton
            // 
            this.SweepLoopStopButton.Enabled = false;
            this.SweepLoopStopButton.Location = new System.Drawing.Point(6, 86);
            this.SweepLoopStopButton.Name = "SweepLoopStopButton";
            this.SweepLoopStopButton.Size = new System.Drawing.Size(107, 34);
            this.SweepLoopStopButton.TabIndex = 3;
            this.SweepLoopStopButton.Text = "Stop";
            this.SweepLoopStopButton.UseVisualStyleBackColor = true;
            this.SweepLoopStopButton.Click += new System.EventHandler(this.SweepLoopStopButton_Click);
            // 
            // SweepLoopProcessButton
            // 
            this.SweepLoopProcessButton.Location = new System.Drawing.Point(6, 46);
            this.SweepLoopProcessButton.Name = "SweepLoopProcessButton";
            this.SweepLoopProcessButton.Size = new System.Drawing.Size(107, 34);
            this.SweepLoopProcessButton.TabIndex = 2;
            this.SweepLoopProcessButton.Text = "Start loop";
            this.SweepLoopProcessButton.UseVisualStyleBackColor = true;
            this.SweepLoopProcessButton.Click += new System.EventHandler(this.SweepLoopProcessButton_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Enabled = false;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "3dB bandpass",
            "Quality factor",
            "Impedance at serie resonance",
            "-6dB/-60dB shape factor"});
            this.checkedListBox1.Location = new System.Drawing.Point(125, 6);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(172, 64);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // SweepProcessButton
            // 
            this.SweepProcessButton.BackColor = System.Drawing.Color.Chartreuse;
            this.SweepProcessButton.Location = new System.Drawing.Point(6, 6);
            this.SweepProcessButton.Name = "SweepProcessButton";
            this.SweepProcessButton.Size = new System.Drawing.Size(107, 34);
            this.SweepProcessButton.TabIndex = 0;
            this.SweepProcessButton.Text = "Start single";
            this.SweepProcessButton.UseVisualStyleBackColor = false;
            this.SweepProcessButton.Click += new System.EventHandler(this.SweepProcessButton_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage4.Controls.Add(this.StopVFOButton);
            this.tabPage4.Controls.Add(this.StartVFOButton);
            this.tabPage4.Controls.Add(this.VFOFrequencyTextBox);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(368, 301);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "VFO";
            // 
            // StopVFOButton
            // 
            this.StopVFOButton.BackColor = System.Drawing.Color.Salmon;
            this.StopVFOButton.Enabled = false;
            this.StopVFOButton.Location = new System.Drawing.Point(182, 82);
            this.StopVFOButton.Name = "StopVFOButton";
            this.StopVFOButton.Size = new System.Drawing.Size(146, 45);
            this.StopVFOButton.TabIndex = 2;
            this.StopVFOButton.Text = "VFO Stop";
            this.StopVFOButton.UseVisualStyleBackColor = false;
            this.StopVFOButton.Click += new System.EventHandler(this.StopVFOButton_Click);
            // 
            // StartVFOButton
            // 
            this.StartVFOButton.BackColor = System.Drawing.Color.LightGreen;
            this.StartVFOButton.Location = new System.Drawing.Point(25, 82);
            this.StartVFOButton.Name = "StartVFOButton";
            this.StartVFOButton.Size = new System.Drawing.Size(146, 45);
            this.StartVFOButton.TabIndex = 1;
            this.StartVFOButton.Text = "VFO Start";
            this.StartVFOButton.UseVisualStyleBackColor = false;
            this.StartVFOButton.Click += new System.EventHandler(this.StartVFOButton_Click);
            // 
            // VFOFrequencyTextBox
            // 
            this.VFOFrequencyTextBox.Font = new System.Drawing.Font("Verdana", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VFOFrequencyTextBox.Location = new System.Drawing.Point(22, 18);
            this.VFOFrequencyTextBox.MaxLength = 12;
            this.VFOFrequencyTextBox.Name = "VFOFrequencyTextBox";
            this.VFOFrequencyTextBox.Size = new System.Drawing.Size(306, 50);
            this.VFOFrequencyTextBox.TabIndex = 0;
            this.VFOFrequencyTextBox.Text = "4 400 000 000";
            this.VFOFrequencyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VFOFrequencyTextBox.TextChanged += new System.EventHandler(this.VFOFrequencyTextBox_TextChanged);
            this.VFOFrequencyTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VFOFrequencyTextBox_KeyUp);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.ParallelCheckBox);
            this.tabPage1.Controls.Add(this.MotionalDisplayTextBox);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.QSelectionComboBox);
            this.tabPage1.Controls.Add(this.AnalyseButton);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(368, 301);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dipole analyzer";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "DUT motional parameters";
            // 
            // ParallelCheckBox
            // 
            this.ParallelCheckBox.AutoSize = true;
            this.ParallelCheckBox.Checked = true;
            this.ParallelCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ParallelCheckBox.Location = new System.Drawing.Point(98, 6);
            this.ParallelCheckBox.Name = "ParallelCheckBox";
            this.ParallelCheckBox.Size = new System.Drawing.Size(113, 17);
            this.ParallelCheckBox.TabIndex = 19;
            this.ParallelCheckBox.Text = "Parallel resonance";
            this.ParallelCheckBox.UseVisualStyleBackColor = true;
            // 
            // MotionalDisplayTextBox
            // 
            this.MotionalDisplayTextBox.BackColor = System.Drawing.Color.White;
            this.MotionalDisplayTextBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MotionalDisplayTextBox.ForeColor = System.Drawing.Color.Black;
            this.MotionalDisplayTextBox.Location = new System.Drawing.Point(12, 188);
            this.MotionalDisplayTextBox.Multiline = true;
            this.MotionalDisplayTextBox.Name = "MotionalDisplayTextBox";
            this.MotionalDisplayTextBox.ReadOnly = true;
            this.MotionalDisplayTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MotionalDisplayTextBox.Size = new System.Drawing.Size(351, 110);
            this.MotionalDisplayTextBox.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(200, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Dipole type";
            // 
            // QSelectionComboBox
            // 
            this.QSelectionComboBox.FormattingEnabled = true;
            this.QSelectionComboBox.Items.AddRange(new object[] {
            "Crystal",
            "Ceramic resonator",
            "Serie RLC"});
            this.QSelectionComboBox.Location = new System.Drawing.Point(266, 32);
            this.QSelectionComboBox.Name = "QSelectionComboBox";
            this.QSelectionComboBox.Size = new System.Drawing.Size(90, 21);
            this.QSelectionComboBox.TabIndex = 16;
            this.QSelectionComboBox.SelectedIndexChanged += new System.EventHandler(this.QSelectionComboBox_SelectedIndexChanged);
            // 
            // AnalyseButton
            // 
            this.AnalyseButton.Location = new System.Drawing.Point(6, 7);
            this.AnalyseButton.Name = "AnalyseButton";
            this.AnalyseButton.Size = new System.Drawing.Size(86, 38);
            this.AnalyseButton.TabIndex = 0;
            this.AnalyseButton.Text = "Run analyze";
            this.AnalyseButton.UseVisualStyleBackColor = true;
            this.AnalyseButton.Click += new System.EventHandler(this.AnalyseButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CapacitorTextBox);
            this.groupBox1.Controls.Add(this.ImpedancetextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.QERBandPassTextBox);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(9, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 74);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "QER Filter estimation";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "capacitors   (pF)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "impedance (Ohms)";
            // 
            // CapacitorTextBox
            // 
            this.CapacitorTextBox.Location = new System.Drawing.Point(135, 38);
            this.CapacitorTextBox.Name = "CapacitorTextBox";
            this.CapacitorTextBox.ReadOnly = true;
            this.CapacitorTextBox.Size = new System.Drawing.Size(71, 20);
            this.CapacitorTextBox.TabIndex = 8;
            this.CapacitorTextBox.Text = "?";
            // 
            // ImpedancetextBox
            // 
            this.ImpedancetextBox.Location = new System.Drawing.Point(240, 38);
            this.ImpedancetextBox.Name = "ImpedancetextBox";
            this.ImpedancetextBox.ReadOnly = true;
            this.ImpedancetextBox.Size = new System.Drawing.Size(72, 20);
            this.ImpedancetextBox.TabIndex = 7;
            this.ImpedancetextBox.Text = "?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "BandPass (kHz)";
            // 
            // QERBandPassTextBox
            // 
            this.QERBandPassTextBox.Location = new System.Drawing.Point(18, 38);
            this.QERBandPassTextBox.Name = "QERBandPassTextBox";
            this.QERBandPassTextBox.Size = new System.Drawing.Size(77, 20);
            this.QERBandPassTextBox.TabIndex = 5;
            this.QERBandPassTextBox.Text = "2.5";
            this.QERBandPassTextBox.TextChanged += new System.EventHandler(this.QERBandPassTextBox_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Controls.Add(this.SaveDeviceButton);
            this.tabPage3.Controls.Add(this.DeviceDelButton);
            this.tabPage3.Controls.Add(this.DeviceProperyGrid);
            this.tabPage3.Controls.Add(this.NewDeviceButton);
            this.tabPage3.Controls.Add(this.DevicesComboBox);
            this.tabPage3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(368, 301);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Devices editor";
            // 
            // SaveDeviceButton
            // 
            this.SaveDeviceButton.Location = new System.Drawing.Point(277, 30);
            this.SaveDeviceButton.Name = "SaveDeviceButton";
            this.SaveDeviceButton.Size = new System.Drawing.Size(86, 28);
            this.SaveDeviceButton.TabIndex = 4;
            this.SaveDeviceButton.Text = "Save";
            this.SaveDeviceButton.UseVisualStyleBackColor = true;
            this.SaveDeviceButton.Click += new System.EventHandler(this.SaveDeviceButton_Click);
            // 
            // DeviceDelButton
            // 
            this.DeviceDelButton.Location = new System.Drawing.Point(144, 30);
            this.DeviceDelButton.Name = "DeviceDelButton";
            this.DeviceDelButton.Size = new System.Drawing.Size(83, 28);
            this.DeviceDelButton.TabIndex = 3;
            this.DeviceDelButton.Text = "Delete";
            this.DeviceDelButton.UseVisualStyleBackColor = true;
            this.DeviceDelButton.Click += new System.EventHandler(this.DeviceDelButton_Click);
            // 
            // DeviceProperyGrid
            // 
            this.DeviceProperyGrid.Location = new System.Drawing.Point(3, 64);
            this.DeviceProperyGrid.Name = "DeviceProperyGrid";
            this.DeviceProperyGrid.Size = new System.Drawing.Size(360, 234);
            this.DeviceProperyGrid.TabIndex = 2;
            this.DeviceProperyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.DeviceProperyGrid_PropertyValueChanged);
            // 
            // NewDeviceButton
            // 
            this.NewDeviceButton.Location = new System.Drawing.Point(6, 30);
            this.NewDeviceButton.Name = "NewDeviceButton";
            this.NewDeviceButton.Size = new System.Drawing.Size(86, 28);
            this.NewDeviceButton.TabIndex = 1;
            this.NewDeviceButton.Text = "New";
            this.NewDeviceButton.UseVisualStyleBackColor = true;
            this.NewDeviceButton.Click += new System.EventHandler(this.NewDevicebutton_Click);
            // 
            // DevicesComboBox
            // 
            this.DevicesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DevicesComboBox.FormattingEnabled = true;
            this.DevicesComboBox.Location = new System.Drawing.Point(6, 3);
            this.DevicesComboBox.Name = "DevicesComboBox";
            this.DevicesComboBox.Size = new System.Drawing.Size(357, 21);
            this.DevicesComboBox.TabIndex = 0;
            this.DevicesComboBox.SelectedIndexChanged += new System.EventHandler(this.DevicesComboBox_SelectedIndexChanged);
            this.DevicesComboBox.SelectedValueChanged += new System.EventHandler(this.DevicesComboBox_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(267, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Samples";
            // 
            // SamplesTextBox
            // 
            this.SamplesTextBox.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SamplesTextBox.Location = new System.Drawing.Point(270, 91);
            this.SamplesTextBox.MaxLength = 5;
            this.SamplesTextBox.Name = "SamplesTextBox";
            this.SamplesTextBox.Size = new System.Drawing.Size(55, 26);
            this.SamplesTextBox.TabIndex = 14;
            this.SamplesTextBox.Text = "2000";
            this.SamplesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SamplesTextBox.TextChanged += new System.EventHandler(this.SamplesTextBox_TextChanged);
            this.SamplesTextBox.Leave += new System.EventHandler(this.SamplesTextBox_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Transformer ratio";
            // 
            // TransformerComboBox
            // 
            this.TransformerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TransformerComboBox.FormattingEnabled = true;
            this.TransformerComboBox.Items.AddRange(new object[] {
            "1:1 ",
            "1:4 ",
            "1:9",
            "1:16"});
            this.TransformerComboBox.Location = new System.Drawing.Point(9, 136);
            this.TransformerComboBox.Name = "TransformerComboBox";
            this.TransformerComboBox.Size = new System.Drawing.Size(86, 21);
            this.TransformerComboBox.TabIndex = 14;
            this.TransformerComboBox.SelectedIndexChanged += new System.EventHandler(this.TransformerComboBox_SelectedIndexChanged);
            // 
            // ControlgroupBox
            // 
            this.ControlgroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlgroupBox.Controls.Add(this.label15);
            this.ControlgroupBox.Controls.Add(this.OutputModeComboBox);
            this.ControlgroupBox.Controls.Add(this.label10);
            this.ControlgroupBox.Controls.Add(this.AttLevelcomboBox);
            this.ControlgroupBox.Controls.Add(this.label6);
            this.ControlgroupBox.Controls.Add(this.label5);
            this.ControlgroupBox.Controls.Add(this.SweepEndFrequencyTextbox);
            this.ControlgroupBox.Controls.Add(this.SamplesTextBox);
            this.ControlgroupBox.Controls.Add(this.SweepStartFrequencyTextbox);
            this.ControlgroupBox.Controls.Add(this.label8);
            this.ControlgroupBox.Controls.Add(this.TransformerComboBox);
            this.ControlgroupBox.Controls.Add(this.label4);
            this.ControlgroupBox.Controls.Add(this.SweepProgressBar);
            this.ControlgroupBox.Controls.Add(this.AutodetectButton);
            this.ControlgroupBox.Controls.Add(this.CalibrationButton);
            this.ControlgroupBox.Location = new System.Drawing.Point(754, 27);
            this.ControlgroupBox.Name = "ControlgroupBox";
            this.ControlgroupBox.Size = new System.Drawing.Size(376, 166);
            this.ControlgroupBox.TabIndex = 14;
            this.ControlgroupBox.TabStop = false;
            this.ControlgroupBox.Text = "Controls";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(269, 120);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 13);
            this.label15.TabIndex = 19;
            this.label15.Text = "Output mode";
            // 
            // OutputModeComboBox
            // 
            this.OutputModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OutputModeComboBox.FormattingEnabled = true;
            this.OutputModeComboBox.Location = new System.Drawing.Point(270, 136);
            this.OutputModeComboBox.Name = "OutputModeComboBox";
            this.OutputModeComboBox.Size = new System.Drawing.Size(86, 21);
            this.OutputModeComboBox.TabIndex = 18;
            this.OutputModeComboBox.SelectedIndexChanged += new System.EventHandler(this.OutputModeComboBox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(138, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Attenuator";
            // 
            // AttLevelcomboBox
            // 
            this.AttLevelcomboBox.FormattingEnabled = true;
            this.AttLevelcomboBox.Location = new System.Drawing.Point(141, 136);
            this.AttLevelcomboBox.Name = "AttLevelcomboBox";
            this.AttLevelcomboBox.Size = new System.Drawing.Size(86, 21);
            this.AttLevelcomboBox.TabIndex = 16;
            this.AttLevelcomboBox.SelectedIndexChanged += new System.EventHandler(this.AttLevelcomboBox_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MaximumSize = new System.Drawing.Size(100, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(100, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsPNGToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAsPNGToolStripMenuItem
            // 
            this.saveAsPNGToolStripMenuItem.Name = "saveAsPNGToolStripMenuItem";
            this.saveAsPNGToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.saveAsPNGToolStripMenuItem.Text = "Save as PNG";
            this.saveAsPNGToolStripMenuItem.Click += new System.EventHandler(this.MenuSavePicture_Click);
            // 
            // SelectecDeviceComboBox
            // 
            this.SelectecDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectecDeviceComboBox.FormattingEnabled = true;
            this.SelectecDeviceComboBox.Location = new System.Drawing.Point(153, 4);
            this.SelectecDeviceComboBox.Name = "SelectecDeviceComboBox";
            this.SelectecDeviceComboBox.Size = new System.Drawing.Size(272, 21);
            this.SelectecDeviceComboBox.TabIndex = 16;
            this.SelectecDeviceComboBox.SelectedIndexChanged += new System.EventHandler(this.SelectecDeviceComboBox_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(103, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 15);
            this.label11.TabIndex = 17;
            this.label11.Text = "Device";
            // 
            // SerialPortComboBox
            // 
            this.SerialPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SerialPortComboBox.FormattingEnabled = true;
            this.SerialPortComboBox.Location = new System.Drawing.Point(476, 2);
            this.SerialPortComboBox.Name = "SerialPortComboBox";
            this.SerialPortComboBox.Size = new System.Drawing.Size(159, 21);
            this.SerialPortComboBox.TabIndex = 18;
            this.SerialPortComboBox.SelectionChangeCommitted += new System.EventHandler(this.SerialPortComboBox_SelectionChangeCommitted);
            this.SerialPortComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SerialPortComboBox_MouseClick);
            this.SerialPortComboBox.MouseEnter += new System.EventHandler(this.SerialPortComboBox_MouseEnter);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(444, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Port";
            // 
            // FirmwareTextBox
            // 
            this.FirmwareTextBox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirmwareTextBox.Location = new System.Drawing.Point(743, 2);
            this.FirmwareTextBox.Name = "FirmwareTextBox";
            this.FirmwareTextBox.ReadOnly = true;
            this.FirmwareTextBox.Size = new System.Drawing.Size(40, 22);
            this.FirmwareTextBox.TabIndex = 20;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(651, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 13);
            this.label14.TabIndex = 21;
            this.label14.Text = "Firmware version";
            // 
            // AutodetectCOMcheckBox
            // 
            this.AutodetectCOMcheckBox.AutoSize = true;
            this.AutodetectCOMcheckBox.Location = new System.Drawing.Point(789, 5);
            this.AutodetectCOMcheckBox.Name = "AutodetectCOMcheckBox";
            this.AutodetectCOMcheckBox.Size = new System.Drawing.Size(172, 17);
            this.AutodetectCOMcheckBox.TabIndex = 22;
            this.AutodetectCOMcheckBox.Text = "COM port autodetect at launch";
            this.AutodetectCOMcheckBox.UseVisualStyleBackColor = true;
            this.AutodetectCOMcheckBox.CheckedChanged += new System.EventHandler(this.AutodetectCOMcheckBox_CheckedChanged);
            // 
            // backgroundWorkerSerialCapture
            // 
            this.backgroundWorkerSerialCapture.WorkerReportsProgress = true;
            this.backgroundWorkerSerialCapture.WorkerSupportsCancellation = true;
            this.backgroundWorkerSerialCapture.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSerialCapture_DoWork);
            this.backgroundWorkerSerialCapture.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerSerialCapture_ProgressChanged);
            this.backgroundWorkerSerialCapture.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerSerialCapture_RunWorkerCompleted);
            // 
            // SpectrumPictureBox
            // 
            this.SpectrumPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SpectrumPictureBox.BackColor = System.Drawing.Color.White;
            this.SpectrumPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SpectrumPictureBox.Location = new System.Drawing.Point(-1, 27);
            this.SpectrumPictureBox.Name = "SpectrumPictureBox";
            this.SpectrumPictureBox.Size = new System.Drawing.Size(749, 637);
            this.SpectrumPictureBox.TabIndex = 0;
            this.SpectrumPictureBox.TabStop = false;
            this.SpectrumPictureBox.SizeChanged += new System.EventHandler(this.SpectrumPictureBox_SizeChanged);
            this.SpectrumPictureBox.Click += new System.EventHandler(this.SpectrumPictureBox_Click);
            this.SpectrumPictureBox.MouseEnter += new System.EventHandler(this.SpectrumPictureBox_MouseEnter);
            this.SpectrumPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SpectrumPictureBox_MouseMove);
            this.SpectrumPictureBox.Resize += new System.EventHandler(this.SpectrumPictureBox_Resize);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 670);
            this.Controls.Add(this.AutodetectCOMcheckBox);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.FirmwareTextBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.SerialPortComboBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.SelectecDeviceComboBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.ControlgroupBox);
            this.Controls.Add(this.LogOutputTextBox);
            this.Controls.Add(this.OperationsTabControl);
            this.Controls.Add(this.SpectrumPictureBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SNASharp  (F4HTQ v2019_01_18)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.OperationsTabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ControlgroupBox.ResumeLayout(false);
            this.ControlgroupBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpectrumPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SpectrumPictureBoxClass SpectrumPictureBox;
        private System.Windows.Forms.TextBox LogOutputTextBox;
        private System.Windows.Forms.Button AutodetectButton;
        private System.Windows.Forms.ProgressBar SweepProgressBar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SweepEndFrequencyTextbox;
        private System.Windows.Forms.TextBox SweepStartFrequencyTextbox;
        private System.Windows.Forms.TabControl OperationsTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button CalibrationButton;
        private System.Windows.Forms.Button SweepProcessButton;
        private System.Windows.Forms.GroupBox ControlgroupBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ParallelCheckBox;
        private System.Windows.Forms.TextBox MotionalDisplayTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox QSelectionComboBox;
        private System.Windows.Forms.Button AnalyseButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CapacitorTextBox;
        private System.Windows.Forms.TextBox ImpedancetextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox QERBandPassTextBox;
        private System.Windows.Forms.ComboBox TransformerComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox SamplesTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsPNGToolStripMenuItem;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox AttLevelcomboBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox DevicesComboBox;
        private System.Windows.Forms.PropertyGrid DeviceProperyGrid;
        private System.Windows.Forms.Button NewDeviceButton;
        private System.Windows.Forms.ComboBox SelectecDeviceComboBox;
        private System.Windows.Forms.Button SaveDeviceButton;
        private System.Windows.Forms.Button DeviceDelButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button SweepLoopStopButton;
        private System.Windows.Forms.Button SweepLoopProcessButton;
        private System.Windows.Forms.ComboBox DetectorCombobox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox SerialPortComboBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox FirmwareTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox AutodetectCOMcheckBox;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSerialCapture;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox VFOFrequencyTextBox;
        private System.Windows.Forms.Button StopVFOButton;
        private System.Windows.Forms.Button StartVFOButton;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox OutputModeComboBox;
    }
}

