using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AnalyzerInterface;
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
            InitializeComponent();
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
        
        void ImageUpdate()
        {
            this.Invalidate();
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



            HUDGraphics.Clear(Color.Transparent);
            Brush brush = new SolidBrush(Color.FromArgb(10, 255, 0, 0));
            //Brush brushText = new SolidBrush(Color.FromArgb(10, 255, 0, 0));

            HUDGraphics.FillRectangle(brush, X1, GraphConfig.UpBorder, nWidh, Size.Height - GraphConfig.LowBorder - GraphConfig.UpBorder);
            Pen RecPen = new Pen(Color.FromArgb(100, 255, 0, 0),2);
            HUDGraphics.DrawRectangle(RecPen, X1, GraphConfig.UpBorder, nWidh, Size.Height - GraphConfig.LowBorder - GraphConfig.UpBorder);

            //HUDGraphics.DrawString("F1 & F2 to ajust size", new Font(FontFamily.GenericMonospace, 10), brushText, new PointF(X1, Size.Height - GraphConfig.LowBorder-20));
            //ForeGroundImageGraphics.
            this.Invalidate();
            //ImageUpdate();

        }

        public void SpectrumPictureBox_MouseLeave(object sender, EventArgs e)
        {
            GraphConfig.DrawTopBox(CurvesList); // to redraw only curve names
            HUDGraphics.Clear(Color.Transparent);
            ImageUpdate();
        }

        private void SpectrumPictureBox_Paint(object sender, PaintEventArgs e)
        {
            /*
            PictureBoxGraphics.DrawImageUnscaled(GraphicAndCurvesBitmap, 0, 0);
            PictureBoxGraphics.DrawImageUnscaled(HUDBitmap, 0, 0);
            */

            e.Graphics.DrawImageUnscaled(GraphicAndCurvesBitmap, 0, 0);
            e.Graphics.DrawImageUnscaled(HUDBitmap, 0, 0);

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
            if (Math.Abs(nLastMouseX - e.X) >= 1.0f) // to disable update when only Y change
            {
                nLastMouseX = e.X;
                DisplayFrequencyAndLevelOnCorners(e.X);
                DisplayZoomBox(e.X);
            }
            //public int GetXFromFrequency(Int64 nFrequency)

        }

        public void DisplayFrequencyAndLevelOnCornersNew(int nXMouse)
        {
            Int64 nFrequency = GraphConfig.GetFrequencyFromXDisplay(nXMouse);
            FreqDisplayLabel.Text = "Frequency : " + Utility.GetStringWithSeparators(nFrequency, " ") + "Hz";


            double dBLevel;

            String LevelText = null;
            if (CurvesList.Count > 0 && ActiveCurve != null)
            {
                dBLevel = Math.Round(ActiveCurve.GeDBLevelFromFrequency(nFrequency), 2);
                LevelDisplayLabel.ForeColor = ActiveCurve.Color_;
                LevelText = ActiveCurve.Name + " : ";
            }
            else
            {
                dBLevel = 0.0f;
                LevelDisplayLabel.ForeColor = Color.Black;
                LevelText = "Level : ";
            }


            double ImpedanceNorm = (100.0f / ((float)Math.Pow(10.0f, dBLevel / 20.0f)) - 100.0f) / 1.0f;



            if (GraphConfig.outputMode == OutputMode.dB)
            {

                if (ImpedanceNorm >= 0)
                    LevelDisplayLabel.Text = LevelText + dBLevel.ToString() + "dB " + " |Z|=" + Math.Round(ImpedanceNorm, 0) + " Ohms";
                else
                    LevelDisplayLabel.Text = LevelText + dBLevel.ToString() + "dB ";

            }
            else
            {
                LevelDisplayLabel.Text = LevelText + dBLevel.ToString();
            }

        }


        public void DisplayFrequencyAndLevelOnCorners(int nXMouse)
        {

            Int64 nFrequency = GraphConfig.GetFrequencyFromXDisplay(nXMouse);


            double dBLevel;

            String LevelText = null;
            if (CurvesList.Count > 0 && ActiveCurve != null && ActiveCurve.Visible == CCurve.YesNo.Yes)
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

            GraphConfig.DrawTopBox(CurvesList, nFrequency);

        }

        public void DetermineNewFrequencyRange(int x, ref Int64 Low, ref Int64 High, ref int SampleCount)
        {
            Int64 nCentralFrequency;
            Int64 nPreviousBW;
            Int64 nNewStartFrequency;
            Int64 nNewEndFrequency;


            nCentralFrequency = GraphConfig.GetFrequencyFromXDisplay(x);
            nPreviousBW = GraphConfig.nLastDrawingHighFrequency - GraphConfig.nLastDrawingLowFrequency;


            if (nPreviousBW < 10)
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

        private void SpectrumPictureBoxClass_MouseClick(object sender, MouseEventArgs e)
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
                    if (nPreviousBW < 10)
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

                GraphicAndCurvesBitmap.Save(dialog.FileName, ImageFormat.Png);
            }
        }

        public void CopyPictureToclipboard()
        {
            Clipboard.SetImage(GraphicAndCurvesBitmap);
        }



        void BitmapUpdate(Size CurrentSize)
        {

            // release graphics
            if (HUDGraphics != null)
                HUDGraphics.Dispose();

            if (GraphicAndCurvesGraphics != null)
                GraphicAndCurvesGraphics.Dispose();

            if (PictureBoxGraphics != null)
                PictureBoxGraphics.Dispose();

            // release bitmaps

            if (HUDBitmap != null)
                HUDBitmap.Dispose();



            if (GraphicAndCurvesBitmap != null)
                GraphicAndCurvesBitmap.Dispose();

            if (Image != null)
                Image.Dispose();


            // allocate new bitmap
            if (CurrentSize.Width > 0 && CurrentSize.Height > 0)
            {
                HUDBitmap = new Bitmap(CurrentSize.Width, CurrentSize.Height);
                GraphicAndCurvesBitmap = new Bitmap(CurrentSize.Width, CurrentSize.Height);
                Image = new Bitmap(CurrentSize.Width, CurrentSize.Height);
            }
            else
            {
                HUDBitmap = new Bitmap(1, 1);
                GraphicAndCurvesBitmap = new Bitmap(1,1);
                Image = new Bitmap(1, 1);
            }

            // create new graphics
            HUDGraphics = Graphics.FromImage(HUDBitmap);
            GraphicAndCurvesGraphics = Graphics.FromImage(GraphicAndCurvesBitmap);
            PictureBoxGraphics = Graphics.FromImage(Image);


        }

        private void SpectrumPictureBoxClass_SizeChanged(object sender, EventArgs e)
        {
            BitmapUpdate(Size);
            DrawCurveCollection(CurvesList);
        }

        public void Redraw()
        {
            ImageUpdate();
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


            if (HUDBitmap == null)
            {
                BitmapUpdate(this.Size);
                ImageUpdate();
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

            GraphConfig.GraphicDisplay(Curves,ActiveCurve, GraphicAndCurvesBitmap);
            ImageUpdate();
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
        Bitmap GraphicAndCurvesBitmap = null;
        Bitmap HUDBitmap = null;
        Graphics GraphicAndCurvesGraphics = null;
        Graphics HUDGraphics = null;
        Graphics PictureBoxGraphics = null;

        float fZoomInRatio = 1.0f / 3.0f;
        int nLastMouseX = 0;

        private System.Windows.Forms.Label FreqDisplayLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label LevelDisplayLabel = new System.Windows.Forms.Label();


        private Form1 Owner = null;

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // SpectrumPictureBoxClass
            // 
            this.SizeChanged += new System.EventHandler(this.SpectrumPictureBoxClass_SizeChanged);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SpectrumPictureBoxClass_MouseClick);
            this.MouseLeave += new System.EventHandler(this.SpectrumPictureBox_MouseLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SpectrumPictureBox_Paint);

            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

    }
}
