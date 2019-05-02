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
using System.Drawing.Drawing2D;

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

        }

        public void SetOwnedForm(Form1 _Owner )
        {
            Owner = _Owner;
        }
        
        void DisplayZoomBox(int nX)
        {
            Int64 Low = -1;
            Int64 High = -1;
            int SampleCount = -1;
            DetermineNewFrequencyRange(nX, ref Low, ref High, ref SampleCount);

            int X1 = GraphConfig.GetXFromFrequency(Low);
            int X2 = GraphConfig.GetXFromFrequency(High);

            if (X1 < GraphConfig.LeftBorder)
                X1 = GraphConfig.LeftBorder;

            if (X2 > Size.Width - GraphConfig.RightBorder)
                X2 = Size.Width - GraphConfig.RightBorder;


            int nWidh = X2 - X1;



            ForeGroundImageGraphics.Clear(Color.Transparent);
            Brush brush = new SolidBrush(Color.FromArgb(50, 255, 0, 0));
            //Brush brushText = new SolidBrush(Color.FromArgb(50, 255, 0, 0));

            ForeGroundImageGraphics.FillRectangle(brush, X1, GraphConfig.UpBorder, nWidh, Size.Height - GraphConfig.LowBorder - GraphConfig.UpBorder);
            //ForeGroundImageGraphics.DrawString("F1 & F2 to ajust size", new Font("Arial", 10), brushText, new PointF(X1, Size.Height - GraphConfig.LowBorder));
            //ForeGroundImageGraphics.
            Image = ForeGroundImageBitmap;

        }

        public void SpectrumPictureBox_MouseLeave(object sender, EventArgs e)
        {
            ForeGroundImageGraphics.Clear(Color.Transparent);
            Image = ForeGroundImageBitmap;
        }

        public void ZoomIncrease()
        {
            fZoomInRatio += 0.05f;
            if (fZoomInRatio > 1.0f)
                fZoomInRatio = 1.0f;

            DisplayZoomBox(nLastMouseX);
        }

        public void ZoomDecrease()
        {
            fZoomInRatio -= 0.05f;
            if (fZoomInRatio < 0.05f)
                fZoomInRatio = 0.05f;

            DisplayZoomBox(nLastMouseX);
        }
        public void OnMouseMove(object sender, MouseEventArgs e)
        {

            nLastMouseX = e.X;
            DisplayFrequencyAndLevelOnCorners(e.X);
            DisplayZoomBox(e.X);

            //public int GetXFromFrequency(Int64 nFrequency)

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

        public void DetermineNewFrequencyRange(int x, ref Int64 Low, ref Int64 High, ref int SampleCount)
        {
            Int64 nCentralFrequency;
            Int64 nPreviousBW;
            Int64 nNewStartFrequency;
            Int64 nNewEndFrequency;


            nCentralFrequency = GraphConfig.GetFrequencyFromXDisplay(x);
            nPreviousBW = GraphConfig.nLastDrawingHighFrequency - GraphConfig.nLastDrawingLowFrequency;


            if (nPreviousBW < 100)
                return;

            nNewStartFrequency = nCentralFrequency - (Int64)(nPreviousBW * fZoomInRatio * 0.5f);
            nNewEndFrequency = nCentralFrequency + (Int64)(nPreviousBW * fZoomInRatio * 0.5f);


            // now we correct the zoom to take acount of discrete frequency step 
            SampleCount = Owner.GetSampleCount();


            Int64 nBW = (nNewEndFrequency - nNewStartFrequency);

            Int64 nRealStep;
            if (SampleCount > nBW)
            {
                SampleCount = (int)nBW;
                nRealStep = 1;
            }
            else
            {
                nRealStep = ((nNewEndFrequency - nNewStartFrequency) + SampleCount / 2) / SampleCount;
            }

            nNewStartFrequency = nCentralFrequency - nRealStep * SampleCount / 2;
            nNewEndFrequency = nNewStartFrequency + nRealStep * SampleCount;


            if (nNewEndFrequency > Owner.DeviceInterface.MaxFrequency)
                nNewEndFrequency = Owner.DeviceInterface.MaxFrequency;

            if (nNewStartFrequency < Owner.DeviceInterface.MinFrequency)
                nNewStartFrequency = Owner.DeviceInterface.MinFrequency;

            Low = nNewStartFrequency;
            High = nNewEndFrequency;
        }

        public void MouseClicManagement(object sender, EventArgs e)
        {
            if (Owner.DeviceInterface.GetDevice() == null)
                return; // no device

            MouseEventArgs Event = (MouseEventArgs)e;
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

                    nNewStartFrequency = nCentralFrequency - (Int64)(nPreviousBW * fZoomInRatio*0.5f);
                    nNewEndFrequency = nCentralFrequency + (Int64)(nPreviousBW * fZoomInRatio*0.5f);
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
                /*
                PictureBoxBitmap.Dispose();
                PictureBoxBitmap = new Bitmap(2000,2000);
                DrawCurveCollection(CurvesList);
                PictureBoxBitmap.Save(dialog.FileName, ImageFormat.Png);
                PictureBoxBitmap.Dispose();
                PictureBoxBitmap = new Bitmap(Size.Width, Size.Height);
                Image = PictureBoxBitmap;
                */

                BackgroundImage.Save(dialog.FileName, ImageFormat.Png);
            }
        }

        public void CopyPictureToclipboard()
        {
            Clipboard.SetImage(BackgroundImage);
        }



        void BitmapUpdate(Size CurrentSize)
        {

            // release graphics
            if (ForeGroundImageGraphics != null)
                ForeGroundImageGraphics.Dispose();

            if (BackGroundImageGraphics != null)
                BackGroundImageGraphics.Dispose();

            // release bitmaps

            if (ForeGroundImageBitmap != null)
                ForeGroundImageBitmap.Dispose();

            if (BackGroundImageBitmap != null)
                BackGroundImageBitmap.Dispose();


            // allocate new bitmap
            if (CurrentSize.Width > 0 && CurrentSize.Height > 0)
            {
                ForeGroundImageBitmap = new Bitmap(CurrentSize.Width, CurrentSize.Height);
                BackGroundImageBitmap = new Bitmap(CurrentSize.Width, CurrentSize.Height);
            }
            else
            {
                ForeGroundImageBitmap = new Bitmap(1, 1);
                BackGroundImageBitmap = new Bitmap(1,1);
            }

            // create new graphics
            ForeGroundImageGraphics = Graphics.FromImage(ForeGroundImageBitmap);
            BackGroundImageGraphics = Graphics.FromImage(BackGroundImageBitmap);

        }

        public void ResizeAndRedraw(object sender, EventArgs e)
        {
            BitmapUpdate(Size);
            DrawCurveCollection(CurvesList);
        }

        public void Redraw()
        {
            Image = ForeGroundImageBitmap;
            BackgroundImage = BackGroundImageBitmap;
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


            if (ForeGroundImageBitmap == null)
            {
                BitmapUpdate(this.Size);
                Image = ForeGroundImageBitmap;
                BackgroundImage = BackGroundImageBitmap;
            }



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

            GraphConfig.GraphicDisplay(Curves,ActiveCurve, BackGroundImageBitmap);
            BackgroundImage = BackGroundImageBitmap;
            Image = ForeGroundImageBitmap;
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
        Bitmap BackGroundImageBitmap = null;
        Bitmap ForeGroundImageBitmap = null;
        Graphics BackGroundImageGraphics = null;
        Graphics ForeGroundImageGraphics = null;
        float fZoomInRatio = 1.0f / 3.0f;
        int nLastMouseX = 0;

        private System.Windows.Forms.Label FreqDisplayLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label LevelDisplayLabel = new System.Windows.Forms.Label();


        private Form1 Owner = null;

    }
}
