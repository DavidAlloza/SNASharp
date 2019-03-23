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

            if (CurvesList.Count > 0 && CurvesList[0] != null)
            {
                dBLevel = Math.Round( ((CurveDef)CurvesList[0]).GeDBLevelFromFrequency(nFrequency), 2);
            }
            else
            {
                dBLevel = 0.0f;
            }


            double ImpedanceNorm = (100.0f / ((float)Math.Pow(10.0f, dBLevel / 20.0f)) - 100.0f) / 1.0f;

            FreqDisplayLabel.Text = "Frequency : " + Utility.GetStringWithSeparators(nFrequency," ")+"Hz";

            if (!GraphConfig.bSWRDisplay)
            {

                if (ImpedanceNorm >= 0)
                    LevelDisplayLabel.Text = "Level : " + dBLevel.ToString() + "dB " + " |Z|=" + Math.Round(ImpedanceNorm, 0) + " Ohms";
                else
                    LevelDisplayLabel.Text = "Level : " + dBLevel.ToString() + "dB ";
            }
            else
            {
                LevelDisplayLabel.Text = "Level : " + dBLevel.ToString() ;
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


        public void DrawSingleCurve(CurveDef _curve)
        {
            if (Size.Width == 0 || Size.Height == 0)
                return;


            CurvesList.Clear();
            CurvesList.Add(_curve);

            GraphConfig.GraphicDisplay(CurvesList);

        }


        public void DrawCurveCollection(ArrayList Curves)
        {
            CurvesList = Curves;
            GraphConfig.GraphicDisplay(Curves);
        }


        public GraphDef GetGraphConfig()
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


       
        private GraphDef GraphConfig = new GraphDef();
        private ArrayList CurvesList = new ArrayList();

  
        private System.Windows.Forms.Label FreqDisplayLabel = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label LevelDisplayLabel = new System.Windows.Forms.Label();


        private Form1 Owner = null;

    }

    public class CurveDef
    {
        public enum YesNo
        {
            Yes,
            No
        };
        // computed values to the curve
        public Int64 nMaxLevelFrequency = -1;
        public float fMaxLeveldB = 0.0f;
        public float fMinLeveldB = -90.0f;

        public Int64 n3dBBandpassLowFrequency = -1;
        public Int64 n3dBBandpassHighFrequency = -1;
        public Int64 n6dBBandpassLowFrequency = -1;
        public Int64 n6dBBandpassHighFrequency = -1;
        public Int64 n60dBBandpassLowFrequency = -1;
        public Int64 n60dBBandpassHighFrequency = -1;
        public int nQ = -1;
        public float fShapeFactor = -1.0f;
        public Int64 nSpectrumLowFrequency = 0;
        public Int64 nSpectrumHighFrequency = 0;
        public YesNo Is_Visible = YesNo.Yes;

        // infos
        public String CurveName = "Curve_0";

        // rendering specific
        public Color DrawingColor = Color.Blue;

        // captured data
        public float[] SpectrumValues = null;

        public String Name
        {
            get { return CurveName; }
            set { CurveName = value; }
        }

        public YesNo Visible
        {
            get { return Is_Visible; }
            set { Is_Visible = value; }
        }

        public Color Color
        {
            get { return DrawingColor; }
            set { DrawingColor = value; }
        }


        public override String ToString()
        {
            return CurveName;
        }

        public float GeDBLevelFromFrequency(Int64 nFrequency)
        {
            if (SpectrumValues != null)
            {
                if (nFrequency > nSpectrumHighFrequency)
                    nFrequency = nSpectrumHighFrequency;

                if (nFrequency < nSpectrumLowFrequency)
                    nFrequency = nSpectrumLowFrequency;

                float fFrequencyFraction = ((float)(nFrequency - nSpectrumLowFrequency)) / (nSpectrumHighFrequency - nSpectrumLowFrequency);
                int nArrayIndex = (int)(fFrequencyFraction * (SpectrumValues.Length - 1));
                return SpectrumValues[nArrayIndex];
            }
            else
            {
                return 0.0f;
            }
        }
    }


    public class GraphDef
    {

        public GraphDef()
        {

        }

        public void SetPictureBox(System.Windows.Forms.PictureBox _PictureBox)
        {
            Picture = _PictureBox;
        }

        // AXES
        public Int64 nLastDrawingLowFrequency = 50000;
        public Int64 nLastDrawingHighFrequency = 90000000;
        public float fLastDrawingLevelLow = -90;
        public float fLastDrawingLevelHigh = 10;
        public bool bSWRDisplay = false;

        // drawing surfaces
        public  int LeftBorder = 50;
        public  int UpBorder = 50;
        public  int RightBorder = 40;
        public  int LowBorder = 50;

        // rendering
        public bool AntiAlias = true;
        public bool HighQualityCurves = true;

        // where to draw
        System.Windows.Forms.PictureBox Picture = null;

        public Int64 GetFrequencyFromXDisplay(int nX)
        {
            int nWidth = Picture.Size.Width - LeftBorder - RightBorder;
            float fFrequencyFraction = ((float)(nX - LeftBorder)) / nWidth;

            if (fFrequencyFraction < 0.0f)
                fFrequencyFraction = 0.0f;

            if (fFrequencyFraction > 1.0f)
                fFrequencyFraction = 1.0f;

            return nLastDrawingLowFrequency + (Int64)((nLastDrawingHighFrequency - nLastDrawingLowFrequency) * fFrequencyFraction);
        }


        public int GetXFromFrequency(Int64 nFrequency)
        {
            int nWidth = Picture.Size.Width - LeftBorder - RightBorder;

            if (nLastDrawingHighFrequency == nLastDrawingLowFrequency)
                return LeftBorder;

            return LeftBorder + (Int32)(((Int64)(nFrequency - nLastDrawingLowFrequency) * nWidth) / (nLastDrawingHighFrequency - nLastDrawingLowFrequency));
        }

        public Int64 GetFrequencyGranularityDisplay(Int64 fBW, int nDisplay = 13)
        {
            Int64[] List = new Int64[] { 100,200,250,500,1000,2000,2500,
                                        5000,10000,20000,25000,50000,
                                        100000,200000,250000,500000,
                                        1000000,2000000, 2500000,
                                        5000000,10000000,20000000,
                                        25000000,50000000,100000000,200000000,250000000,500000000 };

            for (int i = 0; i < List.Length; i++)
            {
                if ((fBW / List[i]) <= nDisplay) return List[i];
            }

            return 1000000000;
        }

        public void DrawCurve(CurveDef _curve)
        {
            if (Picture.Size.Width == 0 || Picture.Size.Height == 0 || _curve.Is_Visible == CurveDef.YesNo.No)
                return;

            int nWidth = Picture.Size.Width - LeftBorder - RightBorder;
            int nHeight = Picture.Size.Height - UpBorder - LowBorder;

            Graphics g = Graphics.FromImage(Picture.Image);

            if (AntiAlias)
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            else
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;


            if (_curve.SpectrumValues != null
                && _curve.nSpectrumLowFrequency < nLastDrawingHighFrequency
                && _curve.nSpectrumHighFrequency > nLastDrawingLowFrequency
                && nLastDrawingHighFrequency > nLastDrawingLowFrequency) // check if curve is visible
            {


                float fVerticalRangedB = fLastDrawingLevelHigh - fLastDrawingLevelLow;
                float fVerticalScale = nHeight / fVerticalRangedB;


                Pen mypen = new Pen(_curve.DrawingColor);


                int nFirstSpectrumIndex;
                float fXStartOffset = 0;
                float fXEndOffset = 0;

                float fSpectrumHRes = (nLastDrawingHighFrequency - nLastDrawingLowFrequency) / nWidth;
                Int64 nCurveBW = _curve.nSpectrumHighFrequency - _curve.nSpectrumLowFrequency;

                if (_curve.nSpectrumLowFrequency < nLastDrawingLowFrequency)
                {
                    nFirstSpectrumIndex = (Int32)(((Int64)(nLastDrawingLowFrequency - _curve.nSpectrumLowFrequency) * _curve.SpectrumValues.Length) / nCurveBW);
                }
                else
                {
                    nFirstSpectrumIndex = 0;
                    fXStartOffset = (_curve.nSpectrumLowFrequency - nLastDrawingLowFrequency) / fSpectrumHRes;
                }

                int nLastSpectrumIndex;

                if (_curve.nSpectrumHighFrequency > nLastDrawingHighFrequency)
                {
                    nLastSpectrumIndex = (Int32)(((Int64)(nLastDrawingHighFrequency - _curve.nSpectrumLowFrequency) * _curve.SpectrumValues.Length) / nCurveBW);
                }
                else
                {
                    nLastSpectrumIndex = _curve.SpectrumValues.Length - 1;
                    fXEndOffset = (nLastDrawingHighFrequency - _curve.nSpectrumHighFrequency) / fSpectrumHRes;
                }

                int nSpectrumCount = nLastSpectrumIndex - nFirstSpectrumIndex;

                int nPixelToDisplay = nWidth - (int)(fXEndOffset + fXStartOffset);

                PointF[] Mesures = new PointF[nSpectrumCount];

                for (int i = 0; i < nSpectrumCount; i++)
                {

                    Mesures[i].Y = ((fLastDrawingLevelHigh - _curve.SpectrumValues[i + nFirstSpectrumIndex]) * fVerticalScale) + UpBorder;
                    Mesures[i].X = ((float)i * nPixelToDisplay) / nSpectrumCount + LeftBorder + fXStartOffset;
                }

                g.DrawLines(mypen, Mesures);

                System.Drawing.Font SampleFont = new Font("Verdana", 8.0f);

                float fPixelPerSample = (float)Math.Round((float)nWidth / nSpectrumCount, 1);
                float fSamplePerPixel = (float)Math.Round((float)nSpectrumCount / nWidth, 1);


                if (nSpectrumCount > nWidth)
                    g.DrawString("Samples: " + nSpectrumCount.ToString() + " (" + fSamplePerPixel.ToString() + " Samples per pixel)", SampleFont, Brushes.Black, new Point(nWidth - 190, 10));
                else
                    g.DrawString("Samples: " + nSpectrumCount.ToString() + " (" + fPixelPerSample.ToString() + " Pixel per sample)", SampleFont, Brushes.Red, new Point(nWidth - 190, 10));


            }


            System.Drawing.Font MsgFont = new Font("Verdana", 8.0f);
            String Msg = "Generated with " + Program.Version;

            g.DrawString(Msg, MsgFont, Brushes.Brown, new Point(nWidth - 230, Picture.Size.Height - 33));

            /*
            if (_curve != null)
            {
                // now we can trace all additionals graphics
                if (_curve.n3dBBandpassLowFrequency > 0 || _curve.n3dBBandpassHighFrequency > 0)
                {
                    float fTopReferenceLevel = _curve.fMaxLeveldB;

                    //g.DrawLine(mypen,)
                }
            }
            */
            g.Dispose();

        }

        public void DrawBackGround()
        {
            if (Picture.Size.Width == 0 || Picture.Size.Height == 0)
                return;


            int nWidth = Picture.Size.Width - LeftBorder - RightBorder;
            int nHeight = Picture.Size.Height - UpBorder - LowBorder;


            Bitmap DrawArea = new Bitmap(Picture.Size.Width, Picture.Size.Height);
            Picture.Image = DrawArea;

            Graphics g = Graphics.FromImage(Picture.Image);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            System.Drawing.Font dBFont = null;

            System.Drawing.Font FreqFont = new Font("Times New Roman", 10.0f);


            if (!bSWRDisplay)
                dBFont = new Font("Times New Roman", 10.0f);
            else
                dBFont = new Font("Times New Roman", 15.0f);



            Pen mypenHLines = new Pen(Brushes.LightGray);
            //Pen mypenHLinesInter = new Pen(Brushes.WhiteSmoke);


            Int64 fBW = nLastDrawingHighFrequency - nLastDrawingLowFrequency;

            Int64 nGranularity;

            nGranularity = GetFrequencyGranularityDisplay(fBW, nWidth / 80);

            Int64 nExtra;
            Int64 nFirst;

            nExtra = nLastDrawingLowFrequency % nGranularity;

            if (nExtra != 0)
            {
                nFirst = nLastDrawingLowFrequency - nExtra + nGranularity;
            }
            else
            {
                nFirst = nLastDrawingLowFrequency;
            }

            for (Int64 nFreqDisplay = nFirst; nFreqDisplay <= nLastDrawingHighFrequency; nFreqDisplay += nGranularity)
            {
                int nXDisplay = GetXFromFrequency(nFreqDisplay);

                String sFrequency = null;
                if (nGranularity >= 1000000)
                {
                    Int64 nRest = nFreqDisplay / 100000;
                    nRest = nRest % 10;

                    sFrequency = (nFreqDisplay / 1000000).ToString() + "." + nRest.ToString() + "M";
                }
                else
                {
                    Int64 nRest = nFreqDisplay / 100;
                    nRest = nRest % 10;
                    sFrequency = (nFreqDisplay / 1000).ToString() + "." + nRest.ToString() + "k";
                }

                float StringSize = g.MeasureString(sFrequency, FreqFont).Width;
                int nXTextDisplay = nXDisplay - (int)(StringSize / 2);
                if (nXTextDisplay + StringSize < Picture.Size.Width)
                {
                    g.DrawString(sFrequency, FreqFont, Brushes.Black, new Point(nXTextDisplay, 30));
                }

                g.DrawLine(mypenHLines, nXDisplay, UpBorder, nXDisplay, UpBorder + nHeight);
            }

            float fVerticalRangedB = fLastDrawingLevelHigh - fLastDrawingLevelLow;
            float fVerticalScale = nHeight / fVerticalRangedB;

            int ndBDisplayStep = GetVerticalScaleIncrementFromDelta((int)fVerticalRangedB);

            // draw 10dB HLines
            for (int ndB = (int)fLastDrawingLevelHigh; ndB >= (int)fLastDrawingLevelLow; ndB -= ndBDisplayStep)
            {
                // int nY = (int)((GraphConfig.fLastDrawingdBHigh - ndB - ndBDisplayStep / 2) * fVerticalScale) + GraphConfig.UpBorder;
                // g.DrawLine(mypenHLinesInter, GraphConfig.LeftBorder, nY, GraphConfig.LeftBorder + nWidth, nY);

                int nY = (int)((fLastDrawingLevelHigh - ndB) * fVerticalScale) + UpBorder;
                g.DrawLine(mypenHLines, LeftBorder, nY,LeftBorder + nWidth, nY);

                if (!bSWRDisplay)
                {
                    float fX = 5.0f;

                    if (ndB < 0)
                        g.DrawString(ndB.ToString() + "dB", dBFont, Brushes.Black, new PointF(fX, nY - 7));

                    if (ndB == 0)
                        g.DrawString("   0" + "dB", dBFont, Brushes.Black, new PointF(fX, nY - 7));

                    if (ndB > 0)
                        g.DrawString("+" + ndB.ToString() + "dB", dBFont, Brushes.Black, new PointF(fX, nY - 7));
                }
                else
                {
                    float fX = 25.0f;

                    g.DrawString(ndB.ToString(), dBFont, Brushes.Black, new PointF(fX, nY - 10));
                }


            }

            // borders
            mypenHLines = new Pen(Brushes.Gray);
            g.DrawLine(mypenHLines, LeftBorder, UpBorder, LeftBorder + nWidth, UpBorder);
            g.DrawLine(mypenHLines, LeftBorder, UpBorder + nHeight, LeftBorder + nWidth, UpBorder + nHeight);
            g.DrawLine(mypenHLines, LeftBorder, UpBorder, LeftBorder, UpBorder + nHeight);
            g.DrawLine(mypenHLines, LeftBorder + nWidth, UpBorder, LeftBorder + nWidth, UpBorder + nHeight);

            g.Dispose();

        }

        int GetVerticalScaleIncrementFromDelta(int nDelta)
        {
            if (nDelta <= 20)
                return 1;

            if (nDelta <= 40)
                return 5;

            return 10;
        }

        public void GraphicDisplay(ArrayList curveList)
        {
            DrawBackGround();
            if (curveList != null)
            {
                for (int i = 0; i < curveList.Count; i++)
                {
                    if (curveList[i] != null)
                    {
                        DrawCurve((CurveDef)curveList[i]);
                    }
                }
            }
        }
   }
}
