using System;
using System.Drawing;
using System.Windows.Forms;
using NWTInterface;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using System.IO.Ports;
using System.Collections;

namespace SNASharp
{

    public partial class Form1 : Form
    {

        enum QualityFactor
        {
            Crystal,
            Ceramic_resonator,
            Serie_RLC
        };


        enum ProcessingNeeded
        {
            serie_and_parallel_resonance,
            serie_resonance_only,
            parallel_resonance_only
        };

       


        public Form1()
        {

            InitializeComponent();
            SweepModeCurvesList.Clear();
            SweepModeCurvesList.Add(new CurveDef());

            CurveListComboBox.DataSource = null;
            CurveListComboBox.DataSource = SweepModeCurvesList;
            CurveListComboBox.SelectedItem = SweepModeCurvesList[0];
            CurveConfigPropertyGrid.SelectedObject = SweepModeCurvesList[0];


            AutodetectCOMcheckBox.Checked = Program.Save.SerialPortAutodetectAtLaunch;

            //SerialPortComboBox.SelectedValueChanged -= DevicesComboBox_SelectedValueChanged;
            RefreshCOMPortList();
            //SerialPortComboBox.SelectedValueChanged += DevicesComboBox_SelectedValueChanged;


            Text = Program.Version;
            QSelectionComboBox.DataSource = Enum.GetValues(typeof(QualityFactor));
            OutputModeComboBox.DataSource = Enum.GetValues(typeof(OutputMode));
            OutputModeComboBox.SelectedItem = Program.Save.Output;

            AttLevelcomboBox.SelectedIndexChanged -= AttLevelcomboBox_SelectedIndexChanged;
            AttLevelcomboBox.DataSource = Enum.GetValues(typeof(AttLevel));
            AttLevelcomboBox.SelectedIndexChanged += AttLevelcomboBox_SelectedIndexChanged;

            DetectorCombobox.DataSource = Enum.GetValues(typeof(NWTDevice.DetectorUsed));
            MyNotifier.SetProgressBar(SweepProgressBar);
            MyNotifier.SetForm(this);

            DeviceManagerInit();
            //SerialPortComboBox.Items.AddRange(SerialPort.GetPortNames());
            //AutoDetectserialPort();
            DeviceListMenuRefresh();

            bMuteDeviceComboBoxEvent = true;

            int nDeviceIndex = GetDeviceIndex(Program.Save.LastUsedDevice);

            if (nDeviceIndex >= 0)
            {
                SelectecDeviceComboBox.SelectedIndex = nDeviceIndex;
            }
            else
            {
                SelectecDeviceComboBox.SelectedIndex = 0;
            }

            bMuteDeviceComboBoxEvent = false;

            SetAnalyzer(SelectecDeviceComboBox.SelectedIndex);

            if (Program.Save.LastUsedCOMPort != null)
            {
                // we try to initalize this com port
                bool bSuccess = SerialPortInitialize(Program.Save.LastUsedCOMPort);

                if (bSuccess)
                    SerialPortComboBox.SelectedItem = Program.Save.LastUsedCOMPort;


                if (!bSuccess && Program.Save.SerialPortAutodetectAtLaunch)
                {
                    AutoDetectSerialPort();
                }
            }
            else
            {
                if (Program.Save.SerialPortAutodetectAtLaunch)
                {
                    AutoDetectSerialPort();
                }
            }


            TransformerComboBox.SelectedIndex = 0;
            SpectrumPictureBox.SetOwnedForm(this);


        }

        void RefreshCOMPortList()
        {
            try
            {
                SerialPortList = SerialPort.GetPortNames();
                if (SerialPortList.Length > 0)
                {
                    SerialPortComboBox.Items.Clear();
                    SerialPortComboBox.Items.AddRange(SerialPortList);
                }
            }
            catch ( Exception)
            {
                SerialPortComboBox.Items.Clear();
            }
        }

     

        bool SerialPortInitialize(String PortName)
        {
            if (DeviceInterface.Initialize(PortName) == true)
            {
                LOGDraw("Firmware version : " + DeviceInterface.nFirmwareVersionNumber.ToString());
                FirmwareTextBox.Text = DeviceInterface.nFirmwareVersionNumber.ToString();

                if (DeviceInterface.nFirmwareVersionNumber == DeviceInterface.GetDevice().Firmware)
                {
                    FirmwareTextBox.BackColor = Color.Chartreuse;
                    LOGDraw("Compatible analyzer found on port " + PortName);

                }
                else
                {
                    if (DeviceInterface.nFirmwareVersionNumber > 100 && DeviceInterface.nFirmwareVersionNumber < 120)
                    {
                        FirmwareTextBox.BackColor = Color.Yellow;
                        LOGWarning(PortName+":In range firmware version number, but no match with selected analyzer");

                    }
                    else
                    {
                        FirmwareTextBox.BackColor = Color.OrangeRed;
                        LOGWarning(PortName+":The device respond to the version request, but the firmware version number is out of range ");

                    }
                }
                bDeviceConnected = true;
                return true;
            }
            else
            {
                FirmwareTextBox.BackColor = Color.Red;
                FirmwareTextBox.Text = "NA";
                bDeviceConnected = false;
                LOGError(PortName+":No device respond to version request");
                return false;
            }
        }

        void AutoDetectSerialPort()
        {
            RefreshCOMPortList();

            for ( int i = 0; i < SerialPortList.Length; i++)
            {
                if (SerialPortInitialize(SerialPortList[i]))
                {
                    SerialPortComboBox.SelectedIndex = i;
                    break;
                }
            }

        }

        void SetAnalyzer(int Index)
        {
            if (DeviceArray.Count > 0)
            {
                NWTCompatibleDeviceDef DeviceDef = (NWTCompatibleDeviceDef)DeviceArray[Index];
                CurrentDeviceDef = DeviceDef;
                DeviceInterface.SetDevice(DeviceDef);
                LoadCalibrationFile();
                SetSweepFrequencies(DeviceInterface.MinFrequency, DeviceInterface.MaxFrequency);
                GraphDef Graph = SpectrumPictureBox.GetGraphConfig();

                Graph.fLastDrawingLevelLow = -90;
                Graph.fLastDrawingLevelHigh = 10;
                Graph.nLastDrawingLowFrequency = DeviceInterface.MinFrequency;
                Graph.nLastDrawingHighFrequency = DeviceInterface.MaxFrequency;
                SpectrumPictureBox.GetGraphConfig().DrawBackGround();

                if (!DeviceDef.Attenuator)
                {
                    AttLevelcomboBox.SelectedIndex = 0;
                    AttLevelcomboBox.Enabled = false;
                }
                else
                {
                    AttLevelcomboBox.Enabled = true;
                    if (Program.Save.LastUsedAttLevel != null)
                    {
                        AttLevel Level;
                        try
                        {
                            Level = (AttLevel)System.Enum.Parse(typeof(AttLevel), Program.Save.LastUsedAttLevel);
                        }
                        catch (Exception)
                        {
                            Level = AttLevel._0dB;
                        }
                        AttLevelcomboBox.SelectedIndex = (int)Level;
                    }
                    else
                    {
                        AttLevelcomboBox.SelectedIndex = 0;
                    }

                }

                if (!DeviceDef.AD8361)
                {
                    DetectorCombobox.SelectedIndex = 0;
                    DetectorCombobox.Enabled = false;
                }
                else
                {
                    DetectorCombobox.Enabled = true;
                }
            }

            if (SerialPortComboBox.SelectedItem != null)
                SerialPortInitialize((String)SerialPortComboBox.SelectedItem);
        }

        public void LoadCalibrationFile()
        {
            if (DeviceInterface.IsCalibrationFileAvailable(Program.CalibrationPath))
            {
                LOGDraw("Load calibration file..", false);
                DeviceInterface.LoadCalibration(Program.CalibrationPath);
                LOGDraw("..Success!");
                bCalibrationAvailable = true;
            }
            else
            {
                LOGWarning("Calibration file not available, please run calibration.");
            }

        }

        public bool CheckForCalibration()
        {
            if (!bCalibrationAvailable)
            {
                LOGWarning("Calibration needed. run calibration");
                DialogResult Result = MessageBox.Show("Calibration file not found, connect SNA input to SNA output and clic OK", "Need calibration", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                if (Result == DialogResult.OK)
                {
                    return ProcessCalibration(true);
                }
                else
                {
                    LOGWarning("calibration canceled");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public void LOGPrint(String LineToDraw, bool NewLine = true)
        {
            LogOutputTextBox.Text += LineToDraw;

            if (NewLine)
                LogOutputTextBox.Text += Environment.NewLine;

            LogOutputTextBox.SelectionStart = LogOutputTextBox.Text.Length;
            LogOutputTextBox.ScrollToCaret();
        }


        public void LOGDraw(String LineToDraw, bool NewLine = true)
        {
            LogOutputTextBox.ForeColor = System.Drawing.Color.Yellow;
            LOGPrint(LineToDraw, NewLine);
        }

        public void LOGWarning(String LineToDraw, bool NewLine = true)
        {
            LogOutputTextBox.ForeColor = System.Drawing.Color.Yellow;
            LOGPrint("WARNING:"+LineToDraw, NewLine);
        }


        public void LOGError(String LineToDraw, bool NewLine = true)
        {
            LogOutputTextBox.ForeColor = System.Drawing.Color.Red;
            LOGPrint("ERROR:" + LineToDraw, NewLine);
        }



        private void AutodetectButton_Click(object sender, EventArgs e)
        {
            AutoDetectSerialPort();
        }
        

        void RefreshQERFilterEstimator()
        {
            float fImpedance = Lm * (float)Math.PI * fQEREstimationBandPass * 1000.0f * 1.6f;
            float fCapacitor = 1000000000000f/(2.0f*(float)Math.PI* fserieFrequency*fImpedance);
            ImpedancetextBox.Text = Math.Round(fImpedance,0).ToString()+" Ohms";
            CapacitorTextBox.Text = Math.Round(fCapacitor,1).ToString() + " pF";
        }


        bool ProcessCalibrationNoAttenuator()
        {
            if (DeviceInterface.GetDevice().AD8309)
            {
                LOGDraw("Calibration in progress using logarithmic detector..", true);
                DeviceInterface.RunCalibration(MyNotifier,9999,false);
            }

            if (DeviceInterface.GetDevice().AD8361)
            {
                LOGDraw("Calibration in progress using linear detector..", true);
                DeviceInterface.RunCalibration(MyNotifier,9999,true);
            }

            DeviceInterface.SaveCalibration(Program.CalibrationPath);
            bCalibrationAvailable = true;

            LOGDraw("done.");
            return true;
        }

        bool ProcessCalibration(bool AllAttenuatorforced = false)
        {

            if ( !CurrentDeviceDef.Attenuator )
            {
                return ProcessCalibrationNoAttenuator();
            }

            DialogResult Result;
            if (AllAttenuatorforced)
                 Result = MessageBox.Show("We will run calibration for all attenuators setup", "Need calibration", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            else
                 Result = MessageBox.Show("Run calibration for all attenuators setup ?", "Need calibration", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (Result == DialogResult.Cancel)
            {
                LOGDraw(" canceled", true);
                return false;
            }
            else
            {



                AttLevel Current = (AttLevel)AttLevelcomboBox.SelectedItem;

                if (Result == DialogResult.No)
                {
                    if (DeviceInterface.GetDevice().AD8309)
                    {
                        LOGDraw("Calibration for " + Current.ToString() + " attenuator, using logarithmic detector", true);
                        DeviceInterface.SetAttenuatorLevel(Current);
                        DeviceInterface.RunCalibration(MyNotifier,9999, false);
                    }
                    if (DeviceInterface.GetDevice().AD8361)
                    {
                        LOGDraw("Calibration for " + Current.ToString() + " attenuator, using linear detector..", true);
                        DeviceInterface.SetAttenuatorLevel(Current);
                        DeviceInterface.RunCalibration(MyNotifier,9999, true);
                    }

                    DeviceInterface.SaveCalibration(Program.CalibrationPath);
                    bCalibrationAvailable = true;
                    LOGDraw("done.");

                }
                else
                {
                    foreach (AttLevel Level in Enum.GetValues(typeof(AttLevel)))
                    {
                        if (DeviceInterface.GetDevice().AD8309)
                        {
                            LOGDraw("Calibration for " + Level.ToString() + " attenuator, using logarihtmic detector..", true);
                            DeviceInterface.SetAttenuatorLevel(Level);
                            DeviceInterface.RunCalibration(MyNotifier,4000,false);
                        }

                        if (DeviceInterface.GetDevice().AD8361)
                        {
                            LOGDraw("Calibration for " + Level.ToString() + " attenuator, using linear detector..", true);
                            DeviceInterface.SetAttenuatorLevel(Level);
                            DeviceInterface.RunCalibration(MyNotifier,4000, true);
                        }


                        LOGDraw("done.");
                    }
                    DeviceInterface.SaveCalibration(Program.CalibrationPath);
                    bCalibrationAvailable = true;
                    DeviceInterface.SetAttenuatorLevel(Current);

                }

                return true;
            }

        }


        float [] RunSweep(Int64 nFrequencyStart, int nStep, int nCount, FormNotifier Notifier)
        {
            LOGDraw("BW:" + (nStep* nCount).ToString() + "Hz", false);
            LOGDraw(" samples:" + nCount.ToString(), false);
            LOGDraw(" Step:" + nStep.ToString());
            return DeviceInterface.RunSweepMode(nFrequencyStart, nStep, nCount, false,Notifier);
        }

        void SingleCurveDisplay(Int64 nFrequencyBase, int nStep, float [] data)
        {
            int Count = data.Length;
            CurveDef Curve = new CurveDef();
            Curve.Color_ = Color.DeepPink;

            GraphDef Graph = SpectrumPictureBox.GetGraphConfig();
            Graph.nLastDrawingLowFrequency = nFrequencyBase;
            Graph.nLastDrawingHighFrequency = nFrequencyBase + nStep * Count;
            Graph.fLastDrawingLevelLow = -90;
            Graph.fLastDrawingLevelHigh = 0;

            SpectrumPictureBox.GetGraphConfig().DrawBackGround();
            Curve.SpectrumValues = data;
            Curve.nSpectrumLowFrequency = nFrequencyBase;
            Curve.nSpectrumHighFrequency = nFrequencyBase  + nStep * Count;
            SpectrumPictureBox.DrawSingleCurve(Curve);
        }
        void DipoleAnalyse()
        {
            if (!CheckForCalibration() || !bDeviceConnected)
            {
                return;
            }
            int nFrequencyStep;
            float[] fSweepResult = null;
            Int64 nFrequencyBase;
            int nCaptureCount;
            float nserieLeveldB;
            int nserieIndex;
            Int64 nserieFrequency;

            CurveDef Curve = new CurveDef();

            // first scan 
            MyNotifier.SetProgressBar(SweepProgressBar);
            LOGDraw("Start dipole detection...");
            nFrequencyBase = nFrequencyDetectionStart;
            nCaptureCount = 9999;
            nFrequencyStep = (int)(((nFrequencyDetectionEnd - nFrequencyDetectionStart) + (nCaptureCount - 1) )/ nCaptureCount);


            if (nFrequencyStep == 0)
            {
                nFrequencyStep = 1;
                nCaptureCount = (int)(nFrequencyDetectionEnd - nFrequencyDetectionStart);
                SamplesTextBox.Text = nCaptureCount.ToString();
            }

            fSweepResult = RunSweep(nFrequencyBase, nFrequencyStep, nCaptureCount, MyNotifier);

            SingleCurveDisplay(nFrequencyBase, nFrequencyStep, fSweepResult);


            if (DipoleQualityFactor != QualityFactor.Serie_RLC)
            {
                nserieIndex = Utility.RetrieveMaxValueIndex(fSweepResult);
                nserieFrequency = nFrequencyBase + nserieIndex * nFrequencyStep;
                LOGDraw("first estimation serie resonance :" + Utility.GetStringWithSeparators(nserieFrequency / 1000," ") + "kHz");

                int nStepFromFrequencyFactor =(int) ((nserieFrequency * 6) / 30000000);

                if (nStepFromFrequencyFactor > 5)
                    nStepFromFrequencyFactor = 5;

                if (DipoleQualityFactor == QualityFactor.Crystal)
                {
                    nFrequencyStep = (nStepFromFrequencyFactor) + 1;
                }
                else
                {
                    nFrequencyStep = 5 * ((nStepFromFrequencyFactor) + 1);
                }
                

                if (!ParallelCheckBox.Checked)
                    nFrequencyBase = nserieFrequency - nFrequencyStep * 5000;
                else
                    nFrequencyBase = nserieFrequency - nFrequencyStep * 3300;

                nCaptureCount = 9999;
                LOGDraw("dipole accurate analyse...");
                fSweepResult = RunSweep(nFrequencyBase, nFrequencyStep, nCaptureCount, MyNotifier);

                SingleCurveDisplay(nFrequencyBase, nFrequencyStep, fSweepResult);
            }

            float nParralelLeveldB = 0.0f;
            float fParrallelFrequency = 0.0f;

            if (ParallelCheckBox.Checked)
            {
                int nParrallelIndex = Utility.RetrieveMinValueIndex(fSweepResult);

                while (nParrallelIndex > 9500)
                {
                    nFrequencyStep += nFrequencyStep;
                    LOGDraw("parrallel mode resonance not found, we broaden the scan...");
                    fSweepResult = RunSweep(nFrequencyBase, nFrequencyStep, nCaptureCount, MyNotifier);

                    SingleCurveDisplay(nFrequencyBase, nFrequencyStep, fSweepResult);

                    nParrallelIndex = Utility.RetrieveMinValueIndex(fSweepResult);
                }
                nParralelLeveldB = fSweepResult[nParrallelIndex];
                fParrallelFrequency = nFrequencyBase + nParrallelIndex * nFrequencyStep;
            }

            nserieIndex = Utility.RetrieveMaxValueIndex(fSweepResult);
            nserieLeveldB = fSweepResult[nserieIndex];

            fserieFrequency = nFrequencyBase + nserieIndex * nFrequencyStep;


            int SerieBandPass3dBLeft = Utility.FindLevelIndex(fSweepResult, nserieIndex, -1, nserieLeveldB - 3.0f);
            int SerieBandPass3dBRight = Utility.FindLevelIndex(fSweepResult, nserieIndex, +1, nserieLeveldB - 3.0f);

            int nBandPass3dB = (SerieBandPass3dBRight - SerieBandPass3dBLeft) * nFrequencyStep;


            LOGDraw("--------------------------------");
            LOGDraw("serie frequency :" + Utility.GetStringWithSeparators((int)fserieFrequency, " ") + "Hz");
            LOGDraw("serie attenuation level :" + Math.Round(nserieLeveldB, 2).ToString() + "dB");

            if (ParallelCheckBox.Checked)
            {
                LOGDraw("Parallel Frequency :" + Utility.GetStringWithSeparators((int)fParrallelFrequency, " ") + "Hz");
                LOGDraw("Parallel attenuation level :" + Math.Round(nParralelLeveldB,2).ToString() + "dB");
            }
            
            LOGDraw("3dB Bandpass :" + nBandPass3dB.ToString() + "Hz");
            LOGDraw("--------------------------------");
           

            // motionnals parameters
            Rm = (100.0f / ((float)Math.Pow(10.0f, nserieLeveldB / 20.0f)) - 100.0f) / fImpedanceRatio;
            Lm = (100 + Rm) / (2.0f * (float)Math.PI * nBandPass3dB);
            Cm = 1.0f / (4.0f * (float)Math.PI * (float)Math.PI * fserieFrequency * fserieFrequency * Lm);
            Q = Lm * fserieFrequency * 2.0f * (float)Math.PI / Rm;

            // we compute RP equivalent to Rm
            float Zl = Lm * 2.0f * (float)Math.PI * fserieFrequency;
            Rp = Zl * Zl / Rm;

            if (ParallelCheckBox.Checked)
            {
                Co = 1.0f / (4.0f * (float)Math.PI * (float)Math.PI * Lm * fParrallelFrequency * fParrallelFrequency - 1.0f / Cm);
                //Rp = (100.0f / ((float)Math.Pow(10.0f, nParralelLeveldB / 20.0f)) - 100.0f) / fImpedanceRatio;

            }
            else
            {
                Co = 0.0f;
            }


            float RmDisplay_Ohms = (float)Math.Round(Rm, 1);
            float LmDisplay_mH = (float)Math.Round(Lm * 1000.0f, 2);
            float CmDisplay_fF = (float)Math.Round(Cm*1000000000000000.0f, 2);


            MotionalDisplayTextBox.Text += "Rm=" + RmDisplay_Ohms.ToString()+" Ohms";
            MotionalDisplayTextBox.Text += Environment.NewLine;

            MotionalDisplayTextBox.Text += "Lm=" + LmDisplay_mH.ToString() + " mH";
            MotionalDisplayTextBox.Text += Environment.NewLine;

            MotionalDisplayTextBox.Text += "Cm=" + CmDisplay_fF.ToString() + " fF";
            MotionalDisplayTextBox.Text += Environment.NewLine;

            
            if (ParallelCheckBox.Checked)
            {
                float CoDisplayPF = (float)Math.Round(Co * 1000000000000.0f, 1);
                MotionalDisplayTextBox.Text += "Co=" + CoDisplayPF.ToString() + " pF";
                MotionalDisplayTextBox.Text += Environment.NewLine;
            }

            float fQDisplay = (float)Math.Round(Q, 0);

            MotionalDisplayTextBox.Text += "Q=" + fQDisplay.ToString();
            MotionalDisplayTextBox.Text += Environment.NewLine;

            float fBPDisplay = (float)Math.Round(fserieFrequency * 0.001f / Q, 3);
            MotionalDisplayTextBox.Text += "BP-3dB=" + fBPDisplay.ToString() + " kHz";
            MotionalDisplayTextBox.Text += Environment.NewLine;
            
            
            MotionalDisplayTextBox.Text += "------------------------------";
            MotionalDisplayTextBox.Text += Environment.NewLine;

            MotionalDisplayTextBox.SelectionStart = MotionalDisplayTextBox.Text.Length;
            MotionalDisplayTextBox.ScrollToCaret();

            RefreshQERFilterEstimator();
        }

        public class FormNotifier : NWTInterface.CBackNotifier
        {
            System.Windows.Forms.ProgressBar ProgressBar = null;
            Form1 form = null;
            int nLastProgress = -1;

            public void SetProgressBar(System.Windows.Forms.ProgressBar _ProgressBar)
            {
                ProgressBar = _ProgressBar;

            }

            public void SetForm(Form1 _form)
            {
                form = _form;
            }

            public override void SendProgress(int nLastIndex, float fLastValue)
            {
                if (ProgressBar != null && nLastIndex != nLastProgress)
                {
                    ProgressBar.Value = nLastIndex;
                    nLastProgress = nLastIndex;
                }
            }

            public override void LogText(String Text)
            {
                if (form != null)
                    form.LOGDraw(Text);
            }

            public override void LogTextError(String Text)
            {
                if (form != null)
                    form.LOGError(Text);
            }


        }

        public NWTDevice DeviceInterface = new NWTDevice();
        FormNotifier MyNotifier = new FormNotifier();
        NWTDevice.RunSweepModeParam CurrentAcquisitionParams = new NWTDevice.RunSweepModeParam();
        NWTDevice.RunSweepModeParam NextAcquisitionParams = new NWTDevice.RunSweepModeParam();

        volatile bool bLoop = false;


        float fQEREstimationBandPass = 2.5f;
        float Rm ;
        float Lm ;
        float Cm ;
        float Co ;
        float Q;
        float Rp; // 
        float fserieFrequency;
        Int64 nFrequencyDetectionStart = 50000;
        Int64 nFrequencyDetectionEnd = 4400000000;
        bool bCalibrationAvailable = false;
        float fImpedanceRatio = 1.0f;
        bool bDeviceConnected = false;
        QualityFactor DipoleQualityFactor = QualityFactor.Crystal;
        public static String Version = "F4HTQ SNASharp 2019_01_07 ";
        bool bMuteDeviceComboBoxEvent = false;
        NWTCompatibleDeviceDef CurrentDeviceDef = null;
        String[] SerialPortList = null;
        ArrayList SweepModeCurvesList = new ArrayList();
        Color[] DefaultCurveColor = { Color.Blue, Color.Red, Color.Green, Color.Black,Color.Violet,Color.Turquoise };


        Color GetDefaultCurveColor(int nIndex)
        {
            if (nIndex < DefaultCurveColor.Length)
                return DefaultCurveColor[nIndex];
            else
                return Color.Pink;
        }

        private void AnalyseButton_Click(object sender, EventArgs e)
        {
            DipoleAnalyse();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void StartFreqTrackBar_Scroll(object sender, EventArgs e)
        {

        }

        private void QERBandPassTextBox_TextChanged(object sender, EventArgs e)
        {
            String Result = QERBandPassTextBox.Text.ToString();
            Result = Result.Replace(",", ".");

            if (Result.Length > 0)
            {
                fQEREstimationBandPass = Convert.ToSingle(Result, new CultureInfo("en-US"));
                RefreshQERFilterEstimator();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void FrequencyBoxUpdate(ref Int64 Storage, TextBox Box)
        {
            String Result = Utility.RemoveSeparator(Box.Text);

            Result = Result.Replace(",", ".");

            if (Result.Length > 0)
            {
                Storage = Convert.ToInt64(Result, new CultureInfo("en-US"));

                if (Storage > DeviceInterface.MaxFrequency || Storage < DeviceInterface.MinFrequency || nFrequencyDetectionEnd <= nFrequencyDetectionStart)
                {
                    Box.ForeColor = Color.Red;
                }
                else
                {
                    Box.ForeColor = Color.Black;

                    if (nFrequencyDetectionEnd > nFrequencyDetectionStart)
                        SpectrumPictureBox.GraphicUpdateScaleRefresh(nFrequencyDetectionStart, nFrequencyDetectionEnd);
                }
            }

        }


        private void SweepStartTextbox_TextChanged(object sender, EventArgs e)
        {
            FrequencyBoxUpdate(ref nFrequencyDetectionStart, SweepStartFrequencyTextbox);

        }


        private void SweepEndFrequencyTextBox_TextChanged(object sender, EventArgs e)
        {
            FrequencyBoxUpdate(ref nFrequencyDetectionEnd, SweepEndFrequencyTextbox);
        }

        public int GetSampleCount()
        {
            return Convert.ToInt32(SamplesTextBox.Text);
        }

        void UpdateSampleCount()
        {
            if (nFrequencyDetectionEnd - nFrequencyDetectionStart < Convert.ToInt32(SamplesTextBox.Text))
            {
                SamplesTextBox.Text = (nFrequencyDetectionEnd - nFrequencyDetectionStart).ToString();
                SamplesTextBox.ForeColor = Color.Red;
            }
            else
            {
                SamplesTextBox.ForeColor = Color.Black;
            }
        }


        public void SetSweepFrequencies(Int64 nFrequencyStartInhz, Int64 nFrequencyEndInhz)
        {
            SweepStartFrequencyTextbox.Text = Utility.GetStringWithSeparators(nFrequencyStartInhz, " ");
            SweepEndFrequencyTextbox.Text = Utility.GetStringWithSeparators(nFrequencyEndInhz, " ");

            UpdateSampleCount();
        }

        public void SetSweepStartFrequency(Int64 nFrequencyInhz)
        {
            SweepStartFrequencyTextbox.Text = Utility.GetStringWithSeparators(nFrequencyInhz, " "); 
            UpdateSampleCount();
        }

        public void SetSweepEndFrequency(Int64 nFrequencyInhz)
        {
            SweepEndFrequencyTextbox.Text = Utility.GetStringWithSeparators(nFrequencyInhz, " ");
            UpdateSampleCount();
        }

        public void SetSampleCount(int nSampleCount)
        {
            SamplesTextBox.Text = nSampleCount.ToString();
        }

        private void CalibrationButton_Click(object sender, EventArgs e)
        {

            DialogResult Result;

            if (CurrentDeviceDef.Operating_mode != DeviceDef.AnalyserClass.SpectrumNoTracking)
            {
                Result = MessageBox.Show("Please connect the SNA input to SNA Output", "Run calibration", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            else
            {
                Result = MessageBox.Show("Please inject to the SNA input the signal for 0 dB reference (Noise source,..) ", "Run calibration", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

            if (Result == DialogResult.OK && bDeviceConnected)
            {
                bool CalibrationOk = ProcessCalibration();
            }

        }

        private void TransformerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TransformerComboBox.SelectedIndex)
            {
                case 0: fImpedanceRatio = 1.0f; break;
                case 1:fImpedanceRatio = 4.0f; break;
                case 2:fImpedanceRatio = 9.0f; break;
                case 3:fImpedanceRatio = 16.0f; break;
                default:break;
            }

            if (TransformerComboBox.SelectedIndex != 0)
                LOGWarning("If you change the impedance transformer do not forget to recalibrate the SNA before any new mesure.");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void QSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DipoleQualityFactor = (QualityFactor)QSelectionComboBox.SelectedItem;

            if (DipoleQualityFactor == QualityFactor.Serie_RLC)
                ParallelCheckBox.Checked = false;
        }

        private void SpectrumPictureBox_Click(object sender, EventArgs e)
        {
            SpectrumPictureBox.MouseClicManagement(sender, e);
        }

        private void SpectrumPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            SpectrumPictureBox.OnMouseMove(sender, e);

         }   

    private void SpectrumPictureBox_MouseEnter(object sender, EventArgs e)
        {
        }

        private void SpectrumPictureBox_Resize(object sender, EventArgs e)
        {
            //SpectrumPictureBox.ResizeAndRedraw(sender, e);
        }

        private void SpectrumPictureBox_SizeChanged(object sender, EventArgs e)
        {
            SpectrumPictureBox.ResizeAndRedraw(sender, e);
        }


        private void MenuSavePicture_Click(object sender, EventArgs e)
        {
            SpectrumPictureBox.SavePicture();
        }

        private void LogOutputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SamplesTextBox_Leave(object sender, EventArgs e)
        {
            UpdateSampleCount();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //checkedListBox1.GetItemChecked()
        }

        private void AttLevelcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeviceInterface.SetAttenuatorLevel((AttLevel)AttLevelcomboBox.SelectedItem);
            Program.Save.LastUsedAttLevel = AttLevelcomboBox.SelectedItem.ToString();
        }

        public AttLevel GetCurrentAttenuatorLevel()
        {
            return (AttLevel)AttLevelcomboBox.SelectedItem;
        }

        private void KeyUpManagement(System.Windows.Forms.TextBox MyTextBox)
        {
            int nCursor = MyTextBox.SelectionStart;
            int nInitialTextLengh = MyTextBox.Text.Length;
            String InitialText = Utility.RemoveSeparator(MyTextBox.Text);
            if (InitialText.Length != 0)
            {
                Int64 StartFrequency = Convert.ToInt64(InitialText, new CultureInfo("en-US"));
                MyTextBox.Text = Utility.GetStringWithSeparators(StartFrequency, " ");
                MyTextBox.SelectionStart = nCursor;

                if (MyTextBox.Text.Length > nInitialTextLengh)
                    MyTextBox.SelectionStart = nCursor + 1;

                if (MyTextBox.Text.Length < nInitialTextLengh )
                {
                    if (nCursor > 0)
                        MyTextBox.SelectionStart = nCursor - 1;
                    else
                        MyTextBox.SelectionStart = 0;
                }

            }
        }

        private void SweepStartFrequencyTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUpManagement(SweepStartFrequencyTextbox);
        }

        private void SweepEndFrequencyTextbox_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUpManagement(SweepEndFrequencyTextbox);
        }

        private void DeviceProperyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            DeviceListMenuRefresh();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SweepLoopStopButton.Enabled)
            {
                // we are running a asynchronous continuous capture, we must stop it.
                SweepLoopStopButton_Click(null, null);
            }

            Program.Save.LastUsedDevice = GetDevice(SelectecDeviceComboBox.SelectedIndex).ToString();
            Program.Save.LastUsedCOMPort = (String)SerialPortComboBox.SelectedItem;
            Program.Save.Output = (OutputMode)OutputModeComboBox.SelectedItem;

            XmlSerializer xs = new XmlSerializer(typeof(SavePref));
            using (StreamWriter wr = new StreamWriter(Program.SaveFullPath))
            {
                xs.Serialize(wr, Program.Save);
            }
        }

        private void SelectecDeviceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bMuteDeviceComboBoxEvent)
                SetAnalyzer(SelectecDeviceComboBox.SelectedIndex);
        }

        private void DetectorCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SamplesTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SerialPortComboBox_MouseEnter(object sender, EventArgs e)
        {/*
            String[] SerialPortList = SerialPort.GetPortNames();
            SerialPortComboBox.Items.Clear();
            SerialPortComboBox.Items.AddRange(SerialPortList);
            */
        }



        private void SerialPortComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (SerialPortComboBox.SelectedItem != null)
                SerialPortInitialize((String)SerialPortComboBox.SelectedItem);

        }

        private void SerialPortComboBox_MouseClick(object sender, MouseEventArgs e)
        {
            Object Current = SerialPortComboBox.SelectedItem;
            String[] SerialPortList = SerialPort.GetPortNames();
            SerialPortComboBox.Items.Clear();
            SerialPortComboBox.Items.AddRange(SerialPortList);
            SerialPortComboBox.SelectedItem = Current;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void AutodetectCOMcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.Save.SerialPortAutodetectAtLaunch = AutodetectCOMcheckBox.Checked;
        }

        private void VFOFrequencyTextBox_TextChanged(object sender, EventArgs e)
        {
            Int64 FinalValue;
            String Result = Utility.RemoveSeparator(VFOFrequencyTextBox.Text);

            Result = Result.Replace(",", ".");

            if (Result.Length > 0)
            {
                FinalValue = Convert.ToInt64(Result, new CultureInfo("en-US"));

                if (FinalValue > DeviceInterface.MaxFrequency || FinalValue < DeviceInterface.MinFrequency)
                {
                    VFOFrequencyTextBox.ForeColor = Color.Red;
                }
                else
                {
                    VFOFrequencyTextBox.ForeColor = Color.Black;

                    if (StartVFOButton.Enabled == false)
                    {
                        // we are running
                        LOGDraw("Set VFO Frequency:" + VFOFrequencyTextBox.Text);
                        DeviceInterface.SetFrequency(FinalValue);
                    }
                }
            }
        }

        private void VFOFrequencyTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            KeyUpManagement(VFOFrequencyTextBox);
        }

        private void StartVFOButton_Click(object sender, EventArgs e)
        {
            ControlgroupBox.Enabled = false;
            SpectrumPictureBox.Enabled = false;
            StartVFOButton.Enabled = false;
            StopVFOButton.Enabled = true;

            VFOFrequencyTextBox_TextChanged(sender, e);

        }

        private void StopVFOButton_Click(object sender, EventArgs e)
        {
            ControlgroupBox.Enabled = true;
            SpectrumPictureBox.Enabled = true;
            StartVFOButton.Enabled = true;
            StopVFOButton.Enabled = false;
            int nVersion = DeviceInterface.GetVersion();
        }

        public OutputMode GetOutputMode()
        {
            return (OutputMode)OutputModeComboBox.SelectedItem;
        }
    }
}
