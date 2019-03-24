using System;
using System.Collections.Generic;
using System.Text;
using NWTInterface;
using System.ComponentModel;

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

            //bSweepLoop = true;

        }

        private void SweepLoopStopButton_Click(object sender, EventArgs e)
        {
            bLoop = false;
            SweepLoopStopButton.Enabled = false;
            SweepLoopProcessButton.Enabled = true;
        }


        private void OutputModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessSweepModeDisplayAcquisition(CurrentAcquisitionParams);
        }

        private void ComputeCaracteristicsParams(float[] Spectrum, Int64 nFrequencyBase, Int64 nFrequencyStep, CurveDef DisplayConfig)
        {
            int nMaxLevelIndex = Utility.RetrieveMaxValueIndex(Spectrum);
            int nMinLevelIndex = Utility.RetrieveMinValueIndex(Spectrum);

            float nMaxLevel = Spectrum[nMaxLevelIndex];
            float nMinLevel = Spectrum[nMinLevelIndex];


            int nSpectrumLeftValidityIndex = Spectrum.Length / 20;
            int nSpectrumRightValidityIndex = (Spectrum.Length - Spectrum.Length / 20);

            Int64 nMaxLevelFrequency = nFrequencyBase + nMaxLevelIndex * nFrequencyStep;
            Int64 nMinLevelFrequency = nFrequencyBase + nMinLevelIndex * nFrequencyStep;


            LOGDraw("Min:"+ Math.Round(nMinLevel, 2).ToString() + "dB ("+ Utility.GetStringWithSeparators(nMinLevelFrequency, " ") + " Hz)");
            LOGDraw("Max:" + Math.Round(nMaxLevel, 2).ToString() + "dB (" + Utility.GetStringWithSeparators(nMaxLevelFrequency, " ") + " Hz)");

            DisplayConfig.nMaxLevelFrequency = nMaxLevelFrequency;
            DisplayConfig.fMaxLeveldB = nMaxLevel;
            DisplayConfig.fMinLeveldB = nMinLevel;

            if (nMaxLevelIndex > nSpectrumLeftValidityIndex && nMaxLevelIndex < nSpectrumRightValidityIndex)
            {
                int nLeft3dBIndex = Utility.FindLevelIndex(Spectrum, nMaxLevelIndex, -1, nMaxLevel - 3.0f);
                int nRight3dBIndex = Utility.FindLevelIndex(Spectrum, nMaxLevelIndex, 1, nMaxLevel - 3.0f);

                if (    nLeft3dBIndex > nSpectrumLeftValidityIndex && nLeft3dBIndex < nSpectrumRightValidityIndex
                    &&  nRight3dBIndex > nSpectrumLeftValidityIndex && nRight3dBIndex < nSpectrumRightValidityIndex
                    && nLeft3dBIndex!= nRight3dBIndex)
                {

                    DisplayConfig.n3dBBandpassLowFrequency = nFrequencyBase + nLeft3dBIndex * nFrequencyStep;
                    DisplayConfig.n3dBBandpassHighFrequency = nFrequencyBase + nRight3dBIndex * nFrequencyStep;


                    int n3dBBandPass = (int)((nRight3dBIndex - nLeft3dBIndex) * nFrequencyStep);
                    LOGDraw("Q factor:" + (nMaxLevelFrequency / n3dBBandPass).ToString());
                    LOGDraw("3dB bandpass:" + Utility.GetStringWithSeparators(n3dBBandPass, " ") + "Hz");

                    int nLeft6dBIndex = Utility.FindLevelIndex(Spectrum, nMaxLevelIndex, -1, nMaxLevel - 6.0f);
                    int nRight6dBIndex = Utility.FindLevelIndex(Spectrum, nMaxLevelIndex, 1, nMaxLevel - 6.0f);


                    if (nLeft6dBIndex > nSpectrumLeftValidityIndex && nLeft6dBIndex < nSpectrumRightValidityIndex
                        && nRight6dBIndex > nSpectrumLeftValidityIndex && nRight6dBIndex < nSpectrumRightValidityIndex)
                    {
                        DisplayConfig.n6dBBandpassLowFrequency = nFrequencyBase + nLeft6dBIndex * nFrequencyStep;
                        DisplayConfig.n6dBBandpassHighFrequency = nFrequencyBase + nRight6dBIndex * nFrequencyStep;


                        int n6dBBandPass = (int)((nRight6dBIndex - nLeft6dBIndex) * nFrequencyStep);
                        LOGDraw("6dB bandpass:" + Utility.GetStringWithSeparators(n6dBBandPass, " ") + "Hz");

                        int nLeft60dBIndex = Utility.FindLevelIndex(Spectrum, nMaxLevelIndex, -1, nMaxLevel - 60.0f);
                        int nRight60dBIndex = Utility.FindLevelIndex(Spectrum, nMaxLevelIndex, 1, nMaxLevel - 60.0f);

                        if (nLeft60dBIndex > nSpectrumLeftValidityIndex && nLeft60dBIndex < nSpectrumRightValidityIndex
                            && nRight60dBIndex > nSpectrumLeftValidityIndex && nRight60dBIndex < nSpectrumRightValidityIndex)
                        {
                            DisplayConfig.n60dBBandpassLowFrequency = nFrequencyBase + nLeft60dBIndex * nFrequencyStep;
                            DisplayConfig.n60dBBandpassHighFrequency = nFrequencyBase + nRight60dBIndex * nFrequencyStep;


                            int n60dBBandPass = (int)((nRight60dBIndex - nLeft60dBIndex) * nFrequencyStep);
                            LOGDraw("60dB bandpass:" + Utility.GetStringWithSeparators(n60dBBandPass, " ") + "Hz");

                            float fShapeFactor = ((float)n60dBBandPass / n6dBBandPass);
                            LOGDraw("-6dB/-60dB shape factor:" + Math.Round(fShapeFactor, 2).ToString());

                        }
                        else
                        {
                            LOGDraw("60dB bandpass: NA", true);
                        }

                    }
                    else
                    {
                        LOGDraw("6dB bandpass: NA", true);
                    }


                }
                else
                {
                    LOGDraw("3dB bandpass: NA", true);
                }




            }
            else
            {
                LOGDraw("Serie resonance: NA", true);
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

            int nUpperScale = 10;
            int nLowerScale = -90;


             GraphDef Graph = SpectrumPictureBox.GetGraphConfig();

            CurveDef CurveConfig = (CurveDef)CurveConfigPropertyGrid.SelectedObject;
            SpectrumPictureBox.SetActiveCurve(CurveConfig);


            switch (OutputModeComboBox.SelectedIndex)
            {
                case (int)OutputMode.dB:

                    if (AcquisitionParams.ResultDatas != null)
                    {
                        nUpperScale = (int)(AcquisitionParams.ResultDatas[Utility.RetrieveMaxValueIndex(AcquisitionParams.ResultDatas)] + 10.0f);
                        nUpperScale /= 10;
                        nUpperScale *= 10;

                        nLowerScale = (int)(AcquisitionParams.ResultDatas[Utility.RetrieveMinValueIndex(AcquisitionParams.ResultDatas)] - 10.0f);
                        nLowerScale /= 10;
                        nLowerScale *= 10;

                        CurveConfig.SpectrumValues = AcquisitionParams.ResultDatas;


                        if (!bLoop)
                        {
                            LOGDraw("============================");
                            ComputeCaracteristicsParams(AcquisitionParams.ResultDatas, AcquisitionParams.nBaseFrequency, AcquisitionParams.nFrequencyStep, CurveConfig);
                            LOGDraw("============================");
                        }

                        Graph.bSWRDisplay = false;
                    }
                    break;

                    case (int)OutputMode.SWR_3:
                    case (int)OutputMode.SWR_6:
                    case (int)OutputMode.SWR_10:
                    Graph.bSWRDisplay = true;
                    int nUpperBound = 10;

                    switch (OutputModeComboBox.SelectedIndex)
                    {
                        case (int)OutputMode.SWR_3:
                            nUpperBound = 3;
                            break;
                        case (int)OutputMode.SWR_6:
                            nUpperBound = 6;
                            break;

                        case (int)OutputMode.SWR_10:
                            nUpperBound = 10;
                            break;

                    }


                    if (AcquisitionParams.ResultDatas != null)
                    {
                        // we allocate a buffer for SWR  data
                        float[] SWRDatas = new float[AcquisitionParams.ResultDatas.Length];

                        for (int i = 0; i < SWRDatas.Length; i++)
                        {
                            float fdBValue = AcquisitionParams.ResultDatas[i];
                            if (fdBValue > 0.0f)
                                fdBValue = 0.0f;

                            double fReflective = Math.Pow(10, fdBValue / 20.0f);
                            double WSR = (1.0 + fReflective) / (1.0 - fReflective);
                            SWRDatas[i] = (float)Math.Max(Math.Min(WSR, nUpperBound), 1.0);

                        }
                        CurveConfig.SpectrumValues = SWRDatas;
                    }
                    nLowerScale = 1;
                    nUpperScale = nUpperBound;

                    break;
            }

            CurveConfig.nSpectrumLowFrequency = AcquisitionParams.nBaseFrequency;
            CurveConfig.nSpectrumHighFrequency = AcquisitionParams.nBaseFrequency + AcquisitionParams.nFrequencyStep * AcquisitionParams.nCount;

            Graph.nLastDrawingLowFrequency = nFrequencyDetectionStart /*CurveConfig.nSpectrumLowFrequency*/;
            Graph.nLastDrawingHighFrequency = nFrequencyDetectionEnd /*CurveConfig.nSpectrumHighFrequency*/;
            Graph.fLastDrawingLevelLow = nLowerScale;
            Graph.fLastDrawingLevelHigh = nUpperScale;
            SpectrumPictureBox.GetGraphConfig().DrawBackGround();

            //CurveConfigPropertyGrid.SelectedObject = CurveConfig;

            SpectrumPictureBox.DrawCurveCollection(SweepModeCurvesList);
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
            SpectrumPictureBox.Redraw();
            CurveListComboBox.DataSource = null;
            CurveListComboBox.DataSource = SweepModeCurvesList;
            CurveListComboBox.SelectedItem = CurveConfigPropertyGrid.SelectedObject;
        }

        private void AddNewCurveButton_Click(object sender, EventArgs e)
        {
            CurveDef NewCurve = new CurveDef();
            NewCurve.Name = "Curve_" + SweepModeCurvesList.Count;
            NewCurve.Color = GetDefaultCurveColor(SweepModeCurvesList.Count);
            SweepModeCurvesList.Add(NewCurve);
            CurveListComboBox.DataSource = null;
            CurveListComboBox.DataSource = SweepModeCurvesList;
            CurveListComboBox.SelectedItem = NewCurve;
            CurveConfigPropertyGrid.SelectedObject = NewCurve;
        }

        private void CurveListComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurveListComboBox.SelectedIndex != -1)
            {
                CurveConfigPropertyGrid.SelectedObject = SweepModeCurvesList[CurveListComboBox.SelectedIndex];
                SpectrumPictureBox.SetActiveCurve((CurveDef)SweepModeCurvesList[CurveListComboBox.SelectedIndex]);
                SpectrumPictureBox.Redraw();
            }
        }

        private void DeleteCurveButton_Click(object sender, EventArgs e)
        {
        }


    }
}
