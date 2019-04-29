using System;
using System.Collections.Generic;
using System.Text;
using NWTInterface;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;


namespace SNASharp
{
    public partial class Form1
    {


        private void SweepProcessButton_Click(object sender, EventArgs e)
        {
            ProcessSweepModeStartAcquisition();
        }

        private void SweepLoopProcessButton_Click(object sender, EventArgs e)
        {
            bLoop = true;
            SweepLoopProcessButton.Enabled = false;
            SweepLoopStopButton.Enabled = true;

            ProcessSweepModeStartAcquisition();
        }

        private void SweepLoopStopButton_Click(object sender, EventArgs e)
        {
            bLoop = false;
            SweepLoopStopButton.Enabled = false;
            SweepLoopProcessButton.Enabled = true;
        }


        private void OutputModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OutputModeComboBox.SelectedIndex != -1)
            {
                SpectrumPictureBox.Redraw();
            }
        }


        public void ProcessSweepModeStartAcquisition()
        {

            if (!bDeviceConnected)
            {
                LOGError("No Device connected");
            }


            if (!CheckForCalibration())
            {
                LOGError("No calibration available");
            }

            if (!CheckForCalibration() || !bDeviceConnected)
            {
                return;
            }

            FillAcquisitionParams(CurrentAcquisitionParams);
            FillAcquisitionParams(NextAcquisitionParams);
            NextAcquisitionParams.ResultDatas = new float[NextAcquisitionParams.nCount];



            //MyNotifier.SetProgressBar(SweepProgressBar);
            LOGDraw("Run sweep mode.", true);

            LOGDraw("BW:" + Utility.GetStringWithSeparators(nFrequencyDetectionEnd - nFrequencyDetectionStart, " ") + "Hz", false);
            LOGDraw(" samples:" + CurrentAcquisitionParams.nCount.ToString(), true);
            LOGDraw(" Step:" + CurrentAcquisitionParams.nFrequencyStep.ToString() + "Hz", false);
            LOGDraw(" Attenuator:" + GetCurrentAttenuatorLevel().ToString());

            SweepProcessButton.Enabled = false;
            ControlgroupBox.Enabled = false;

            if (bLoop)
            {
                SweepProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            }

            if (DeviceInterface.IsPortOpen())
            {
                backgroundWorkerSerialCapture.RunWorkerAsync(NextAcquisitionParams);
            }
            else
            {
                LOGError("COM port not open, run device detection of manualy select available port");
            }

        }

        void FillAcquisitionParams(NWTDevice.RunSweepModeParam Param)
        {
            int nCaptureCount = Convert.ToInt32(SamplesTextBox.Text);

            int nFrequencyStep = (int)((nFrequencyDetectionEnd - nFrequencyDetectionStart) / nCaptureCount);
            if (nFrequencyStep == 0)
                nFrequencyStep = 1;

            Param.Detector = (NWTDevice.DetectorUsed)DetectorCombobox.SelectedItem;
            Param.nBaseFrequency = nFrequencyDetectionStart;
            Param.nFrequencyStep = nFrequencyStep;
            Param.nCount = nCaptureCount;
            Param.Notifier = null;
            Param.Worker = backgroundWorkerSerialCapture;
        }

        public void ProcessSweepModeDisplayAcquisition(NWTDevice.RunSweepModeParam AcquisitionParams)
        {

            CGraph Graph = SpectrumPictureBox.GetGraphConfig();
            CCurve CurveConfig = (CCurve)CurveConfigPropertyGrid.SelectedObject;
            SpectrumPictureBox.SetActiveCurve(CurveConfig);

            CurveConfig.nSpectrumLowFrequency = AcquisitionParams.nBaseFrequency;
            CurveConfig.nSpectrumHighFrequency = AcquisitionParams.nBaseFrequency + AcquisitionParams.nFrequencyStep * AcquisitionParams.nCount;
            CurveConfig.nFrequencyStep = AcquisitionParams.nFrequencyStep;

            if (AcquisitionParams.ResultDatas != null)
            {
                Utility.FilterArray(AcquisitionParams.ResultDatas, (int)((FilterMode)FilterComboBox.SelectedItem));
                CurveConfig.SpectrumValues = AcquisitionParams.ResultDatas;
                CurveConfig.DetermineMinMaxLevels();
                CurveConfig.ComputeCaracteristicsParams();
                if (bLoop == false)
                {
                    LOGDraw("*** ----- RESULTS----- ***");
                    LOGDraw(CurveConfig.GetCurveDescription());
                }
            }

            Graph.nLastDrawingLowFrequency = nFrequencyDetectionStart ;
            Graph.nLastDrawingHighFrequency = nFrequencyDetectionEnd ;
            SpectrumPictureBox.DrawCurveCollection(SweepModeCurvesList, bLoop);
        }

        private void backgroundWorkerSerialCapture_DoWork(object sender, DoWorkEventArgs e)
        {

            e.Result = DeviceInterface.RunSweepMode((NWTDevice.RunSweepModeParam)e.Argument);
            //bIsAcquire = false;
        }

        private void backgroundWorkerSerialCapture_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           // if ( !bLoop)
                SweepProgressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerSerialCapture_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SweepProgressBar.Value = 0;


            CurrentAcquisitionParams.ResultDatas = (float[])e.Result;
            CurrentAcquisitionParams.nBaseFrequency = NextAcquisitionParams.nBaseFrequency;
            CurrentAcquisitionParams.nFrequencyStep = NextAcquisitionParams.nFrequencyStep;
            CurrentAcquisitionParams.nCount = NextAcquisitionParams.nCount;


            if (bLoop)
            {
                //NextAcquisitionParams.
                FillAcquisitionParams(NextAcquisitionParams);
                NextAcquisitionParams.ResultDatas = new float[NextAcquisitionParams.nCount];
                backgroundWorkerSerialCapture.RunWorkerAsync(NextAcquisitionParams);
            }
            else
            {
                ControlgroupBox.Enabled = true;
                SweepProcessButton.Enabled = true;
                SweepProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;

            }

            ProcessSweepModeDisplayAcquisition(CurrentAcquisitionParams);

        }

        private void CurveConfigPropertyGrid_PropertyValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            CCurve Curve= (CCurve)CurveConfigPropertyGrid.SelectedObject;
            Curve.ComputeCaracteristicsParams();
            SpectrumPictureBox.Redraw();
            CurveListComboBox.DataSource = null;
            CurveListComboBox.DataSource = SweepModeCurvesList;
            CurveListComboBox.SelectedItem = CurveConfigPropertyGrid.SelectedObject;
        }

        private void AddNewCurveButton_Click(object sender, EventArgs e)
        {
            CCurve NewCurve = new CCurve();
            NewCurve.Name = "Curve_" + SweepModeCurvesList.Count;
            NewCurve.Color_ = GetDefaultCurveColor(SweepModeCurvesList.Count);
            SweepModeCurvesList.Add(NewCurve);
            CurveListComboBox.DataSource = null;
            CurveListComboBox.DataSource = SweepModeCurvesList;
            CurveListComboBox.SelectedItem = NewCurve;
            SpectrumPictureBox.DrawCurveCollection(SweepModeCurvesList);
            CurveConfigPropertyGrid.SelectedObject = NewCurve;
        }

        private void CurveListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurveListComboBox.SelectedIndex != -1)
            {
                CurveConfigPropertyGrid.SelectedObject = SweepModeCurvesList[CurveListComboBox.SelectedIndex];
                SpectrumPictureBox.SetActiveCurve((CCurve)SweepModeCurvesList[CurveListComboBox.SelectedIndex]);
                SpectrumPictureBox.Redraw();
                CCurve Curve = (CCurve)SweepModeCurvesList[CurveListComboBox.SelectedIndex];
                if (Curve.SpectrumValues != null && Curve.SpectrumValues.Length > 0)
                {
                    LOGDraw("***------ SELECTED CURVE ----------***");
                    LOGDraw(Curve.GetCurveDescription());
                }
            }
        }

        private void DeleteCurveButton_Click(object sender, EventArgs e)
        {
            if (SweepModeCurvesList.Count > 1)
            {
                int nIndexToDelete = CurveListComboBox.SelectedIndex;
                CurveListComboBox.DataSource = null;
                CurveConfigPropertyGrid.SelectedObject = null;
                SweepModeCurvesList.RemoveAt(nIndexToDelete);
                CurveListComboBox.DataSource = SweepModeCurvesList;
                SpectrumPictureBox.SetActiveCurve((CCurve)SweepModeCurvesList[0]);
                SpectrumPictureBox.DrawCurveCollection(SweepModeCurvesList);
                CurveListComboBox.SelectedIndex = 0;
            }
        }


        private void SaveCurveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Curves files (*.xml)|*.xml|all files (*.*)|*.*";
            CCurve CurveToSave = (CCurve)CurveConfigPropertyGrid.SelectedObject;
            dialog.InitialDirectory = Program.CurvesPath;
            dialog.FileName = CurveToSave.ToString()+".xml";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xs = new XmlSerializer(typeof(CCurve));
                using (StreamWriter wr = new StreamWriter(dialog.FileName))
                {
                    xs.Serialize(wr, CurveToSave);
                }
            }

        }

        private void LoadCurveButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Curves files (*.xml)|*.xml|all files (*.*)|*.*";
            dialog.InitialDirectory = Program.CurvesPath;


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                XmlSerializer xs = new XmlSerializer(typeof(CCurve));
                using (StreamReader wr = new StreamReader(dialog.FileName))
                {
                    try
                    {
                        CCurve CurveToLoad = xs.Deserialize(wr) as CCurve;
                        CurveToLoad.ComputeCaracteristicsParams();

                        SweepModeCurvesList.Add(CurveToLoad);
                        CurveListComboBox.DataSource = null;
                        CurveListComboBox.DataSource = SweepModeCurvesList;
                        SpectrumPictureBox.DrawCurveCollection(SweepModeCurvesList);
                        CurveListComboBox.SelectedIndex = SweepModeCurvesList.Count - 1;
                    }
                    catch(Exception)
                    {
                        LOGError("sorry, your curve file format is incompatible with this software version");
                    }
                }
            }

        }

        private void ForceRangeButton_Click(object sender, EventArgs e)
        {
            CCurve ActiveCurve = (CCurve)CurveConfigPropertyGrid.SelectedObject;
            if (ActiveCurve != null && ActiveCurve.SpectrumValues != null)
            {
                SetSweepFrequencies(ActiveCurve.nSpectrumLowFrequency, ActiveCurve.nSpectrumHighFrequency);

            }
        }

    }
}
