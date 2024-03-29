﻿namespace SNASharp
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
            this.ForceRangeButton = new System.Windows.Forms.Button();
            this.SaveCurveButton = new System.Windows.Forms.Button();
            this.LoadCurveButton = new System.Windows.Forms.Button();
            this.DeleteCurveButton = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.AddNewCurveButton = new System.Windows.Forms.Button();
            this.CurveListComboBox = new System.Windows.Forms.ComboBox();
            this.CurveConfigPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SweepLoopStopButton = new System.Windows.Forms.Button();
            this.SweepLoopProcessButton = new System.Windows.Forms.Button();
            this.SweepProcessButton = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.StopVFOButton = new System.Windows.Forms.Button();
            this.StartVFOButton = new System.Windows.Forms.Button();
            this.VFOFrequencyTextBox = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.FastAnalyzeButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.ParallelCheckBox = new System.Windows.Forms.CheckBox();
            this.MotionalDisplayTextBox = new System.Windows.Forms.TextBox();
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
            this.DeviceProperyGrid = new System.Windows.Forms.PropertyGrid();
            this.NewDeviceButton = new System.Windows.Forms.Button();
            this.DevicesComboBox = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.FilterComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.DetectorCombobox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SamplesTextBox = new System.Windows.Forms.TextBox();
            this.ControlgroupBox = new System.Windows.Forms.GroupBox();
            this.AttCalCheckBox = new System.Windows.Forms.CheckBox();
            this.RawCaptureCheckBox = new System.Windows.Forms.CheckBox();
            this.ControlResetButton = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.OutputModeComboBox = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.AttLevelcomboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.LogOutputTextBox.Location = new System.Drawing.Point(633, 532);
            this.LogOutputTextBox.Multiline = true;
            this.LogOutputTextBox.Name = "LogOutputTextBox";
            this.LogOutputTextBox.ReadOnly = true;
            this.LogOutputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogOutputTextBox.Size = new System.Drawing.Size(372, 191);
            this.LogOutputTextBox.TabIndex = 1;
            this.LogOutputTextBox.TextChanged += new System.EventHandler(this.LogOutputTextBox_TextChanged);
            // 
            // CalibrationButton
            // 
            this.CalibrationButton.Location = new System.Drawing.Point(159, 27);
            this.CalibrationButton.Name = "CalibrationButton";
            this.CalibrationButton.Size = new System.Drawing.Size(139, 41);
            this.CalibrationButton.TabIndex = 13;
            this.CalibrationButton.Text = "Run calibration";
            this.CalibrationButton.UseVisualStyleBackColor = true;
            this.CalibrationButton.Click += new System.EventHandler(this.CalibrationButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(159, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "End Frequency (Hz)";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // SweepEndFrequencyTextbox
            // 
            this.SweepEndFrequencyTextbox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SweepEndFrequencyTextbox.Location = new System.Drawing.Point(159, 93);
            this.SweepEndFrequencyTextbox.MaxLength = 12;
            this.SweepEndFrequencyTextbox.Name = "SweepEndFrequencyTextbox";
            this.SweepEndFrequencyTextbox.Size = new System.Drawing.Size(139, 27);
            this.SweepEndFrequencyTextbox.TabIndex = 12;
            this.SweepEndFrequencyTextbox.Text = "30 000 000";
            this.SweepEndFrequencyTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SweepEndFrequencyTextbox.TextChanged += new System.EventHandler(this.SweepEndFrequencyTextBox_TextChanged);
            this.SweepEndFrequencyTextbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SweepEndFrequencyTextbox_KeyUp);
            // 
            // SweepStartFrequencyTextbox
            // 
            this.SweepStartFrequencyTextbox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SweepStartFrequencyTextbox.Location = new System.Drawing.Point(12, 93);
            this.SweepStartFrequencyTextbox.MaxLength = 12;
            this.SweepStartFrequencyTextbox.Name = "SweepStartFrequencyTextbox";
            this.SweepStartFrequencyTextbox.Size = new System.Drawing.Size(141, 27);
            this.SweepStartFrequencyTextbox.TabIndex = 11;
            this.SweepStartFrequencyTextbox.Text = "50 000";
            this.SweepStartFrequencyTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SweepStartFrequencyTextbox.TextChanged += new System.EventHandler(this.SweepStartTextbox_TextChanged);
            this.SweepStartFrequencyTextbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SweepStartFrequencyTextbox_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Start Frequency (Hz)";
            // 
            // SweepProgressBar
            // 
            this.SweepProgressBar.Enabled = false;
            this.SweepProgressBar.Location = new System.Drawing.Point(13, 17);
            this.SweepProgressBar.MarqueeAnimationSpeed = 10;
            this.SweepProgressBar.Name = "SweepProgressBar";
            this.SweepProgressBar.Size = new System.Drawing.Size(354, 4);
            this.SweepProgressBar.Step = 100;
            this.SweepProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.SweepProgressBar.TabIndex = 4;
            // 
            // AutodetectButton
            // 
            this.AutodetectButton.Location = new System.Drawing.Point(12, 27);
            this.AutodetectButton.Name = "AutodetectButton";
            this.AutodetectButton.Size = new System.Drawing.Size(141, 41);
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
            this.OperationsTabControl.Location = new System.Drawing.Point(629, 226);
            this.OperationsTabControl.Name = "OperationsTabControl";
            this.OperationsTabControl.SelectedIndex = 0;
            this.OperationsTabControl.Size = new System.Drawing.Size(376, 300);
            this.OperationsTabControl.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.ForceRangeButton);
            this.tabPage2.Controls.Add(this.SaveCurveButton);
            this.tabPage2.Controls.Add(this.LoadCurveButton);
            this.tabPage2.Controls.Add(this.DeleteCurveButton);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.AddNewCurveButton);
            this.tabPage2.Controls.Add(this.CurveListComboBox);
            this.tabPage2.Controls.Add(this.CurveConfigPropertyGrid);
            this.tabPage2.Controls.Add(this.SweepLoopStopButton);
            this.tabPage2.Controls.Add(this.SweepLoopProcessButton);
            this.tabPage2.Controls.Add(this.SweepProcessButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(368, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sweepmode";
            // 
            // ForceRangeButton
            // 
            this.ForceRangeButton.Location = new System.Drawing.Point(6, 226);
            this.ForceRangeButton.Name = "ForceRangeButton";
            this.ForceRangeButton.Size = new System.Drawing.Size(68, 43);
            this.ForceRangeButton.TabIndex = 13;
            this.ForceRangeButton.Text = "Force\r\nRange";
            this.ForceRangeButton.UseVisualStyleBackColor = true;
            this.ForceRangeButton.Click += new System.EventHandler(this.ForceRangeButton_Click);
            // 
            // SaveCurveButton
            // 
            this.SaveCurveButton.Location = new System.Drawing.Point(81, 174);
            this.SaveCurveButton.Name = "SaveCurveButton";
            this.SaveCurveButton.Size = new System.Drawing.Size(68, 46);
            this.SaveCurveButton.TabIndex = 12;
            this.SaveCurveButton.Text = "Save \r\ncurve";
            this.SaveCurveButton.UseVisualStyleBackColor = true;
            this.SaveCurveButton.Click += new System.EventHandler(this.SaveCurveButton_Click);
            // 
            // LoadCurveButton
            // 
            this.LoadCurveButton.Location = new System.Drawing.Point(6, 174);
            this.LoadCurveButton.Name = "LoadCurveButton";
            this.LoadCurveButton.Size = new System.Drawing.Size(70, 46);
            this.LoadCurveButton.TabIndex = 11;
            this.LoadCurveButton.Text = "Load \r\ncurve";
            this.LoadCurveButton.UseVisualStyleBackColor = true;
            this.LoadCurveButton.Click += new System.EventHandler(this.LoadCurveButton_Click);
            // 
            // DeleteCurveButton
            // 
            this.DeleteCurveButton.BackColor = System.Drawing.Color.Salmon;
            this.DeleteCurveButton.Location = new System.Drawing.Point(82, 126);
            this.DeleteCurveButton.Name = "DeleteCurveButton";
            this.DeleteCurveButton.Size = new System.Drawing.Size(68, 42);
            this.DeleteCurveButton.TabIndex = 10;
            this.DeleteCurveButton.Text = "Delete \r\ncurve";
            this.DeleteCurveButton.UseVisualStyleBackColor = false;
            this.DeleteCurveButton.Click += new System.EventHandler(this.DeleteCurveButton_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(156, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(67, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Active curve";
            // 
            // AddNewCurveButton
            // 
            this.AddNewCurveButton.BackColor = System.Drawing.Color.PaleGreen;
            this.AddNewCurveButton.Location = new System.Drawing.Point(6, 126);
            this.AddNewCurveButton.Name = "AddNewCurveButton";
            this.AddNewCurveButton.Size = new System.Drawing.Size(72, 42);
            this.AddNewCurveButton.TabIndex = 8;
            this.AddNewCurveButton.Text = "Add new \r\ncurve";
            this.AddNewCurveButton.UseVisualStyleBackColor = false;
            this.AddNewCurveButton.Click += new System.EventHandler(this.AddNewCurveButton_Click);
            // 
            // CurveListComboBox
            // 
            this.CurveListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CurveListComboBox.FormattingEnabled = true;
            this.CurveListComboBox.Location = new System.Drawing.Point(155, 19);
            this.CurveListComboBox.Name = "CurveListComboBox";
            this.CurveListComboBox.Size = new System.Drawing.Size(207, 21);
            this.CurveListComboBox.TabIndex = 7;
            this.CurveListComboBox.SelectedIndexChanged += new System.EventHandler(this.CurveListComboBox_SelectedIndexChanged);
            // 
            // CurveConfigPropertyGrid
            // 
            this.CurveConfigPropertyGrid.HelpVisible = false;
            this.CurveConfigPropertyGrid.Location = new System.Drawing.Point(155, 46);
            this.CurveConfigPropertyGrid.Name = "CurveConfigPropertyGrid";
            this.CurveConfigPropertyGrid.Size = new System.Drawing.Size(207, 223);
            this.CurveConfigPropertyGrid.TabIndex = 6;
            this.CurveConfigPropertyGrid.ToolbarVisible = false;
            this.CurveConfigPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.CurveConfigPropertyGrid_PropertyValueChanged);
            // 
            // SweepLoopStopButton
            // 
            this.SweepLoopStopButton.Enabled = false;
            this.SweepLoopStopButton.Location = new System.Drawing.Point(6, 86);
            this.SweepLoopStopButton.Name = "SweepLoopStopButton";
            this.SweepLoopStopButton.Size = new System.Drawing.Size(143, 34);
            this.SweepLoopStopButton.TabIndex = 3;
            this.SweepLoopStopButton.Text = "Stop";
            this.SweepLoopStopButton.UseVisualStyleBackColor = true;
            this.SweepLoopStopButton.Click += new System.EventHandler(this.SweepLoopStopButton_Click);
            // 
            // SweepLoopProcessButton
            // 
            this.SweepLoopProcessButton.Location = new System.Drawing.Point(6, 46);
            this.SweepLoopProcessButton.Name = "SweepLoopProcessButton";
            this.SweepLoopProcessButton.Size = new System.Drawing.Size(143, 34);
            this.SweepLoopProcessButton.TabIndex = 2;
            this.SweepLoopProcessButton.Text = "Start loop";
            this.SweepLoopProcessButton.UseVisualStyleBackColor = true;
            this.SweepLoopProcessButton.Click += new System.EventHandler(this.SweepLoopProcessButton_Click);
            // 
            // SweepProcessButton
            // 
            this.SweepProcessButton.BackColor = System.Drawing.Color.PaleGreen;
            this.SweepProcessButton.Location = new System.Drawing.Point(6, 6);
            this.SweepProcessButton.Name = "SweepProcessButton";
            this.SweepProcessButton.Size = new System.Drawing.Size(143, 34);
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
            this.tabPage4.Size = new System.Drawing.Size(368, 274);
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
            this.VFOFrequencyTextBox.Text = "50 000 000";
            this.VFOFrequencyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VFOFrequencyTextBox.TextChanged += new System.EventHandler(this.VFOFrequencyTextBox_TextChanged);
            this.VFOFrequencyTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VFOFrequencyTextBox_KeyUp);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.FastAnalyzeButton);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.ParallelCheckBox);
            this.tabPage1.Controls.Add(this.MotionalDisplayTextBox);
            this.tabPage1.Controls.Add(this.AnalyseButton);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(368, 274);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Crystal analyzer";
            // 
            // FastAnalyzeButton
            // 
            this.FastAnalyzeButton.Location = new System.Drawing.Point(98, 7);
            this.FastAnalyzeButton.Name = "FastAnalyzeButton";
            this.FastAnalyzeButton.Size = new System.Drawing.Size(86, 38);
            this.FastAnalyzeButton.TabIndex = 20;
            this.FastAnalyzeButton.Text = "Analyse only";
            this.FastAnalyzeButton.UseVisualStyleBackColor = true;
            this.FastAnalyzeButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DipoleAnalyseFinalStepOnly);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 128);
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
            this.ParallelCheckBox.Location = new System.Drawing.Point(249, 7);
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
            this.MotionalDisplayTextBox.Location = new System.Drawing.Point(11, 147);
            this.MotionalDisplayTextBox.Multiline = true;
            this.MotionalDisplayTextBox.Name = "MotionalDisplayTextBox";
            this.MotionalDisplayTextBox.ReadOnly = true;
            this.MotionalDisplayTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MotionalDisplayTextBox.Size = new System.Drawing.Size(351, 121);
            this.MotionalDisplayTextBox.TabIndex = 3;
            // 
            // AnalyseButton
            // 
            this.AnalyseButton.Location = new System.Drawing.Point(6, 7);
            this.AnalyseButton.Name = "AnalyseButton";
            this.AnalyseButton.Size = new System.Drawing.Size(86, 38);
            this.AnalyseButton.TabIndex = 0;
            this.AnalyseButton.Text = "full auto";
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
            this.groupBox1.Location = new System.Drawing.Point(9, 51);
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
            this.tabPage3.Controls.Add(this.DeviceProperyGrid);
            this.tabPage3.Controls.Add(this.NewDeviceButton);
            this.tabPage3.Controls.Add(this.DevicesComboBox);
            this.tabPage3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(368, 274);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Devices editor";
            // 
            // SaveDeviceButton
            // 
            this.SaveDeviceButton.Location = new System.Drawing.Point(98, 30);
            this.SaveDeviceButton.Name = "SaveDeviceButton";
            this.SaveDeviceButton.Size = new System.Drawing.Size(86, 28);
            this.SaveDeviceButton.TabIndex = 4;
            this.SaveDeviceButton.Text = "Save to disk";
            this.SaveDeviceButton.UseVisualStyleBackColor = true;
            this.SaveDeviceButton.Click += new System.EventHandler(this.SaveDeviceButton_Click);
            // 
            // DeviceProperyGrid
            // 
            this.DeviceProperyGrid.HelpVisible = false;
            this.DeviceProperyGrid.Location = new System.Drawing.Point(3, 64);
            this.DeviceProperyGrid.Name = "DeviceProperyGrid";
            this.DeviceProperyGrid.Size = new System.Drawing.Size(360, 210);
            this.DeviceProperyGrid.TabIndex = 2;
            this.DeviceProperyGrid.ToolbarVisible = false;
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
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(13, 123);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 13);
            this.label17.TabIndex = 25;
            this.label17.Text = "Capture filter";
            // 
            // FilterComboBox
            // 
            this.FilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterComboBox.FormattingEnabled = true;
            this.FilterComboBox.Location = new System.Drawing.Point(13, 139);
            this.FilterComboBox.Name = "FilterComboBox";
            this.FilterComboBox.Size = new System.Drawing.Size(75, 21);
            this.FilterComboBox.TabIndex = 24;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(160, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Detector";
            // 
            // DetectorCombobox
            // 
            this.DetectorCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DetectorCombobox.FormattingEnabled = true;
            this.DetectorCombobox.Location = new System.Drawing.Point(163, 139);
            this.DetectorCombobox.Name = "DetectorCombobox";
            this.DetectorCombobox.Size = new System.Drawing.Size(135, 21);
            this.DetectorCombobox.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(301, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Samples";
            // 
            // SamplesTextBox
            // 
            this.SamplesTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SamplesTextBox.Location = new System.Drawing.Point(304, 93);
            this.SamplesTextBox.MaxLength = 5;
            this.SamplesTextBox.Name = "SamplesTextBox";
            this.SamplesTextBox.Size = new System.Drawing.Size(62, 27);
            this.SamplesTextBox.TabIndex = 14;
            this.SamplesTextBox.Text = "2000";
            this.SamplesTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SamplesTextBox.TextChanged += new System.EventHandler(this.SamplesTextBox_TextChanged);
            this.SamplesTextBox.Leave += new System.EventHandler(this.SamplesTextBox_Leave);
            // 
            // ControlgroupBox
            // 
            this.ControlgroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlgroupBox.Controls.Add(this.AttCalCheckBox);
            this.ControlgroupBox.Controls.Add(this.RawCaptureCheckBox);
            this.ControlgroupBox.Controls.Add(this.label17);
            this.ControlgroupBox.Controls.Add(this.ControlResetButton);
            this.ControlgroupBox.Controls.Add(this.label15);
            this.ControlgroupBox.Controls.Add(this.FilterComboBox);
            this.ControlgroupBox.Controls.Add(this.OutputModeComboBox);
            this.ControlgroupBox.Controls.Add(this.label10);
            this.ControlgroupBox.Controls.Add(this.AttLevelcomboBox);
            this.ControlgroupBox.Controls.Add(this.label6);
            this.ControlgroupBox.Controls.Add(this.label5);
            this.ControlgroupBox.Controls.Add(this.SweepEndFrequencyTextbox);
            this.ControlgroupBox.Controls.Add(this.SamplesTextBox);
            this.ControlgroupBox.Controls.Add(this.SweepStartFrequencyTextbox);
            this.ControlgroupBox.Controls.Add(this.label12);
            this.ControlgroupBox.Controls.Add(this.DetectorCombobox);
            this.ControlgroupBox.Controls.Add(this.label4);
            this.ControlgroupBox.Controls.Add(this.SweepProgressBar);
            this.ControlgroupBox.Controls.Add(this.AutodetectButton);
            this.ControlgroupBox.Controls.Add(this.CalibrationButton);
            this.ControlgroupBox.Location = new System.Drawing.Point(629, 27);
            this.ControlgroupBox.Name = "ControlgroupBox";
            this.ControlgroupBox.Size = new System.Drawing.Size(376, 193);
            this.ControlgroupBox.TabIndex = 14;
            this.ControlgroupBox.TabStop = false;
            this.ControlgroupBox.Text = "Controls";
            // 
            // AttCalCheckBox
            // 
            this.AttCalCheckBox.AutoSize = true;
            this.AttCalCheckBox.Location = new System.Drawing.Point(99, 170);
            this.AttCalCheckBox.Name = "AttCalCheckBox";
            this.AttCalCheckBox.Size = new System.Drawing.Size(79, 17);
            this.AttCalCheckBox.TabIndex = 27;
            this.AttCalCheckBox.Text = "Use Att Cal";
            this.AttCalCheckBox.UseVisualStyleBackColor = true;
            this.AttCalCheckBox.CheckedChanged += new System.EventHandler(this.AttCalCheckBox_CheckedChanged);
            // 
            // RawCaptureCheckBox
            // 
            this.RawCaptureCheckBox.AutoSize = true;
            this.RawCaptureCheckBox.Location = new System.Drawing.Point(12, 170);
            this.RawCaptureCheckBox.Name = "RawCaptureCheckBox";
            this.RawCaptureCheckBox.Size = new System.Drawing.Size(87, 17);
            this.RawCaptureCheckBox.TabIndex = 26;
            this.RawCaptureCheckBox.Text = "Raw capture";
            this.RawCaptureCheckBox.UseVisualStyleBackColor = true;
            this.RawCaptureCheckBox.CheckedChanged += new System.EventHandler(this.RawCaptureCheckBox_CheckedChanged);
            // 
            // ControlResetButton
            // 
            this.ControlResetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ControlResetButton.Location = new System.Drawing.Point(301, 27);
            this.ControlResetButton.Name = "ControlResetButton";
            this.ControlResetButton.Size = new System.Drawing.Size(66, 41);
            this.ControlResetButton.TabIndex = 23;
            this.ControlResetButton.Text = "Reset";
            this.ControlResetButton.UseVisualStyleBackColor = true;
            this.ControlResetButton.Click += new System.EventHandler(this.ControlResetButton_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(304, 123);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 13);
            this.label15.TabIndex = 19;
            this.label15.Text = "Output mode";
            // 
            // OutputModeComboBox
            // 
            this.OutputModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OutputModeComboBox.FormattingEnabled = true;
            this.OutputModeComboBox.Location = new System.Drawing.Point(304, 139);
            this.OutputModeComboBox.Name = "OutputModeComboBox";
            this.OutputModeComboBox.Size = new System.Drawing.Size(62, 21);
            this.OutputModeComboBox.TabIndex = 18;
            this.OutputModeComboBox.SelectedIndexChanged += new System.EventHandler(this.OutputModeComboBox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(98, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "Attenuator";
            // 
            // AttLevelcomboBox
            // 
            this.AttLevelcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AttLevelcomboBox.FormattingEnabled = true;
            this.AttLevelcomboBox.Location = new System.Drawing.Point(99, 139);
            this.AttLevelcomboBox.Name = "AttLevelcomboBox";
            this.AttLevelcomboBox.Size = new System.Drawing.Size(54, 21);
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
            this.saveAsPNGToolStripMenuItem,
            this.copyToClipboardToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAsPNGToolStripMenuItem
            // 
            this.saveAsPNGToolStripMenuItem.Name = "saveAsPNGToolStripMenuItem";
            this.saveAsPNGToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveAsPNGToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.saveAsPNGToolStripMenuItem.Text = "Save as PNG";
            this.saveAsPNGToolStripMenuItem.Click += new System.EventHandler(this.MenuSavePicture_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // SelectecDeviceComboBox
            // 
            this.SelectecDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SelectecDeviceComboBox.FormattingEnabled = true;
            this.SelectecDeviceComboBox.Location = new System.Drawing.Point(153, 4);
            this.SelectecDeviceComboBox.Name = "SelectecDeviceComboBox";
            this.SelectecDeviceComboBox.Size = new System.Drawing.Size(285, 21);
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
            this.SerialPortComboBox.Location = new System.Drawing.Point(486, 2);
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
            this.label13.Location = new System.Drawing.Point(454, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Port";
            // 
            // FirmwareTextBox
            // 
            this.FirmwareTextBox.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirmwareTextBox.Location = new System.Drawing.Point(754, 2);
            this.FirmwareTextBox.Name = "FirmwareTextBox";
            this.FirmwareTextBox.ReadOnly = true;
            this.FirmwareTextBox.Size = new System.Drawing.Size(40, 22);
            this.FirmwareTextBox.TabIndex = 20;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(662, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 13);
            this.label14.TabIndex = 21;
            this.label14.Text = "Firmware version";
            // 
            // AutodetectCOMcheckBox
            // 
            this.AutodetectCOMcheckBox.AutoSize = true;
            this.AutodetectCOMcheckBox.Location = new System.Drawing.Point(809, 5);
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
            this.SpectrumPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SpectrumPictureBox.BackgroundImage")));
            this.SpectrumPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SpectrumPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("SpectrumPictureBox.Image")));
            this.SpectrumPictureBox.Location = new System.Drawing.Point(-1, 27);
            this.SpectrumPictureBox.Name = "SpectrumPictureBox";
            this.SpectrumPictureBox.Size = new System.Drawing.Size(624, 696);
            this.SpectrumPictureBox.TabIndex = 0;
            this.SpectrumPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
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
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SNASharp  (F4HTQ v2019_01_18)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
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
        private System.Windows.Forms.Button AnalyseButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CapacitorTextBox;
        private System.Windows.Forms.TextBox ImpedancetextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox QERBandPassTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox SamplesTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsPNGToolStripMenuItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox AttLevelcomboBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox DevicesComboBox;
        private System.Windows.Forms.PropertyGrid DeviceProperyGrid;
        private System.Windows.Forms.Button NewDeviceButton;
        private System.Windows.Forms.ComboBox SelectecDeviceComboBox;
        private System.Windows.Forms.Button SaveDeviceButton;
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
        private System.Windows.Forms.ComboBox CurveListComboBox;
        private System.Windows.Forms.PropertyGrid CurveConfigPropertyGrid;
        private System.Windows.Forms.Button AddNewCurveButton;
        private System.Windows.Forms.Button DeleteCurveButton;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button SaveCurveButton;
        private System.Windows.Forms.Button LoadCurveButton;
        private System.Windows.Forms.Button ControlResetButton;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.Button ForceRangeButton;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox FilterComboBox;
        private System.Windows.Forms.Button FastAnalyzeButton;
        private System.Windows.Forms.CheckBox RawCaptureCheckBox;
        private System.Windows.Forms.CheckBox AttCalCheckBox;
    }
}

