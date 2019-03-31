using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NWTInterface;
using System.Globalization;
using System.Drawing.Imaging;
using System.Collections;
using System.Xml.Serialization;


namespace SNASharp
{
    class SpectrumPictureBoxClass :  System.Windows.Forms.PictureBox
    {

        public SpectrumPictureBoxClass()
        {
            // constructor
            // FreqDisplayOnSpectrumLabel
            // 
            FreqDisplayLabel.AutoSize = true;
            FreqDisplayLabel.Location = new System.Drawing.Point(5, 5);
            FreqDisplayLabel.Name = "FreqDisplayOnSpectrumLabel";
            FreqDisplayLabel.Size = new System.Drawing.Size(60, 13);
            FreqDisplayLabel.TabIndex = 15;
            FreqDisplayLabel.Text = "Frequency:";

            this.Controls.Add(this.FreqDisplayLabel);


            LevelDisplayLabel.AutoSize = true;
            LevelDisplayLabel.Location = new System.Drawing.Point(220, 5);
            LevelDisplayLabel.Name = "LevelDisplayOnSpectrumLabel";
            LevelDisplayLabel.Size = new System.Drawing.Size(60, 13);
            LevelDisplayLabel.TabIndex = 16;
            LevelDisplayLabel.Text = "Level:";

            this.Controls.Add(this.LevelDisplayLabel);



       
            System.Drawing.Font Font = new Font("Verdana", 10.0f);
            FreqDisplayLabel.Font = Font;
            LevelDisplayLabel.Font = Font;

            GraphConfig.SetPictureBox(this);

        }

        public void SetOwnedForm(Form1 _Owner )
        {
            Owner = _Owner;
        }
        
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            DisplayFrequencyAndLevelOnCorners(e.X);
        }

        public void DisplayFrequencyAndLevelOnCorners(int nXMouse)
        {
            Int64 nFrequency = GraphConfig.GetFrequencyFromXDisplay(nXMouse);


            double dBLevel;

            String LevelText = null;
            if (CurvesList.Count > 0 && ActiveCurve != null)
            {
                dBLevel = Math.Round(ActiveCurve.GeDBLevelFromFrequency(nFrequency), 2);
                LevelDisplayLabel.ForeColor = ActiveCurve.Color_;
                LevelText = ActiveCurve.Name+" : ";
            }
            else
            {
                dBLevel = 0.0f;
                LevelDisplayLabel.ForeColor = Color.Black;
                LevelText = "Level : ";
            }


            double ImpedanceNorm = (100.0f / ((float)Math.Pow(10.0f, dBLevel / 20.0f)) - 100.0f) / 1.0f;

            FreqDisplayLabel.Text = "Frequency : " + Utility.GetStringWithSeparators(nFrequency," ")+"Hz";


            if (GraphConfig.outputMode == OutputMode.dB)
            {

                if (ImpedanceNorm >= 0)
                    LevelDisplayLabel.Text = LevelText + dBLevel.ToString() + "dB " + " |Z|=" + Math.Round(ImpedanceNorm, 0) + " Ohms";
                else
                    LevelDisplayLabel.Text = LevelText + dBLevel.ToString() + "dB ";
            }
            else
            {
                LevelDisplayLabel.Text = LevelText + dBLevel.ToString() ;
            }

        }

        public void MouseClicManagement(object sender, EventArgs e)
        {
            if (Owner.DeviceInterface.GetDevice() == null)
                return; // no device

            MouseEventArgs Event = (MouseEventArgs)e;
            //if (Curve.SpectrumValues != null)
            {
                Int64 nCentralFrequency;
                Int64 nPreviousBW ;
                Int64 nNewStartFrequency ;
                Int64 nNewEndFrequency ;


                nCentralFrequency = GraphConfig.GetFrequencyFromXDisplay(Event.X);
                nPreviousBW = GraphConfig.nLastDrawingHighFrequency - GraphConfig.nLastDrawingLowFrequency;


                if (Event.Button == MouseButtons.Left)
                {
                    if (nPreviousBW < 100)
                        return;

                    nNewStartFrequency = nCentralFrequency - nPreviousBW / 3;
                    nNewEndFrequency = nCentralFrequency + nPreviousBW / 3;
                }
                else
                {
                    nNewStartFrequency = nCentralFrequency - nPreviousBW;
                    nNewEndFrequency = nCentralFrequency + nPreviousBW;

                }

                // now we correct the zoom to take acount of discrete frequency step 
                int nSampleCount = Owner.GetSampleCount();


                Int64 nBW = (nNewEndFrequency - nNewStartFrequency);

                Int64 nRealStep;
                if (nSampleCount > nBW)
                {
                    nSampleCount = (int)nBW;
                    Owner.SetSampleCount(nSampleCount);
                    nRealStep = 1;
                }
                else
                {
                    nRealStep = ((nNewEndFrequency - nNewStartFrequency) + nSampleCount / 2) / nSampleCount;
                }

                nNewStartFrequency = nCentralFrequency - nRealStep * nSampleCount / 2;
                nNewEndFrequency = nNewStartFrequency + nRealStep * nSampleCount ;


                //int nFrequencyStep = 

                
                if (nNewEndFrequency > Owner.DeviceInterface.MaxFrequency )
                    nNewEndFrequency = Owner.DeviceInterface.MaxFrequency;

                if (nNewStartFrequency < Owner.DeviceInterface.MinFrequency)
                    nNewStartFrequency = Owner.DeviceInterface.MinFrequency;


                GraphConfig.nLastDrawingLowFrequency = nNewStartFrequency;
                GraphConfig.nLastDrawingHighFrequency = nNewEndFrequency;

                DrawCurveCollection(CurvesList);
                Owner.SetSweepStartFrequency(nNewStartFrequency);
                Owner.SetSweepEndFrequency(nNewEndFrequency);
            }

        }

        public void SavePicture()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Fichiers image (*.png)|*.png|Tous les fichiers (*.*)|*.*";
            dialog.FileName = "SnaSharpSpectrum.png";
         

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Image.Save(dialog.FileName, ImageFormat.Png);
            }
        }

        public void ResizeAndRedraw(object sender, EventArgs e)
        {
            DrawCurveCollection(CurvesList);
        }

        public void Redraw()
        {
            DrawCurveCollection(CurvesList);
        }


        public void DrawSingleCurve(CCurve _curve)
        {
            if (Size.Width == 0 || Size.Height == 0)
                return;

            if (_curve.SpectrumValues != null)
            {
                _curve.DetermineMinMaxLevels();
            }

            CurvesList.Clear();
            CurvesList.Add(_curve);
            ActiveCurve = _curve;
            DrawCurveCollection(CurvesList);

        }


        public void DrawCurveCollection(ArrayList Curves, bool bLoopMode = false)
        {
            CurvesList = Curves;
            int nUpperScale = 10;
            int nLowerScale = -90;
            GraphConfig.outputMode = OutputMode.dB;

            if (CurvesList.Count > 0 && Owner!= null && Owner.GetOutputMode() == OutputMode.dB)
            {
                float fMax = CurvesRetrieveMaximumScale();


                if (fMax != Single.MinValue)
                {
                    nUpperScale = (int)(fMax + 10.0f);
                    nUpperScale /= 10;
                    nUpperScale *= 10;
                }

                float fMin = CurvesRetrieveMinimumScale();

                if (fMin != Single.MaxValue)
                {
                    nLowerScale = (int)(fMin - 10.0f);
                    nLowerScale /= 10;
                    nLowerScale *= 10;
                }


                if (bLoopMode)
                {
                    if (GraphConfig.fLastDrawingLevelHigh - nUpperScale <= 20 
                        && GraphConfig.fLastDrawingLevelHigh - nUpperScale > 0)
                    {
                        nUpperScale = (int)GraphConfig.fLastDrawingLevelHigh;
                    }

                    if (nLowerScale - GraphConfig.fLastDrawingLevelLow  <= 20
                        && nLowerScale - GraphConfig.fLastDrawingLevelLow > 0)
                    {
                        nLowerScale = (int)GraphConfig.fLastDrawingLevelLow;
                    }

                }

            }
            else
            {
                if (Owner != null)
                {
                    GraphConfig.outputMode = Owner.GetOutputMode();

                    switch (GraphConfig.outputMode)
                    {
                        case OutputMode.SWR_3:
                            nUpperScale = 3;
                            break;
                        case OutputMode.SWR_6:
                            nUpperScale = 6;
                            break;

                        case OutputMode.SWR_10:
                            nUpperScale = 10;
                            break;

                    }

                    nLowerScale = 1;
                }
            }


            GraphConfig.fLastDrawingLevelLow = nLowerScale;
            GraphConfig.fLastDrawingLevelHigh = nUpperScale;
            GraphConfig.GraphicDisplay(Curves,ActiveCurve);
        }


        public void SetActiveCurve(CCurve Active)
        {
            ActiveCurve = Active;
        }

        public CGraph GetGraphConfig()
        {
            return GraphConfig;
        }

        public void GraphicUpdateScaleRefresh(Int64 nStartFrequency, Int64 nEndFrequency)
        {
            if (nStartFrequency > 0)
                GraphConfig.nLastDrawingLowFrequency = nStartFrequency;

            if (nEndFrequency > 0)
                GraphConfig.nLastDrawingHighFrequency = nEndFrequency;

            DrawCurveCollection(CurvesList);
        }

        public float CurvesRetrieveMinimumScale()
        {
            float fMin = Single.MaxValue;
            foreach ( CCurve Curve in CurvesList)
            {
                if (Curve.fMinLeveldB < fMin && Curve.SpectrumValues != null)
                    fMin = Curve.fMinLeveldB;
            }
            return fMin;
        }

        public float CurvesRetrieveMaximumScale()
        {
            float fMax = Single.MinValue;
            foreach (CCurve Curve in CurvesList)
            {
                if (Curve.fMaxLeveldB > fMax && Curve.SpectrumValues != null)
                    fMax = Curve.fMaxLeveldB;
            }
            return fMax;
        }

        private CGraph GraphConfig = new CGraph();
        private ArrayList CurvesList = new ArrayList();
        private CCurve ActiveCurve = null;

  
        private System.Windows.Forms.Label FreqDisplayLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label LevelDisplayLabel = new System.Windows.Forms.Label();


        private Form1 Owner = null;

    }
}
