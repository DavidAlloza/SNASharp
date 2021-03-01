using System;
using System.Drawing;
using System.Collections;

namespace SNASharp
{
    public class CGraph
    {

        public CGraph()
        {

        }


        // AXES
        public Int64 nLastDrawingLowFrequency = 50000;
        public Int64 nLastDrawingHighFrequency = 90000000;
        public float fLastDrawingLevelLow = -90;
        public float fLastDrawingLevelHigh = 10;

        // drawing surfaces
        public int LeftBorder = 50;
        public int UpBorder = 80;
        public int RightBorder = 40;
        public int LowBorder = 50;

        // rendering
        public bool AntiAlias = true;
        public bool HighQualityCurves = true;

        // where to draw

        public Bitmap BitmapWhereDraw = null;

        public OutputMode outputMode = OutputMode.dB;

        public Int64 GetFrequencyFromXDisplay(int nX)
        {
            int nWidth = BitmapWhereDraw.Size.Width - LeftBorder - RightBorder;
            double fFrequencyFraction = ((double)(nX - LeftBorder)) / nWidth;

            if (fFrequencyFraction < 0.0f)
                fFrequencyFraction = 0.0f;

            if (fFrequencyFraction > 1.0f)
                fFrequencyFraction = 1.0f;

            return nLastDrawingLowFrequency + (Int64)((double)(nLastDrawingHighFrequency - nLastDrawingLowFrequency) * fFrequencyFraction);
        }


        public int GetXFromFrequency(Int64 nFrequency)
        {
            int nWidth = BitmapWhereDraw.Size.Width - LeftBorder - RightBorder;

            if (nLastDrawingHighFrequency == nLastDrawingLowFrequency)
                return LeftBorder;

            return LeftBorder + (Int32)(((double)(nFrequency - nLastDrawingLowFrequency) * nWidth) / (nLastDrawingHighFrequency - nLastDrawingLowFrequency));
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

        public void DrawTopBox(ArrayList curveList = null, Int64 CursorFrequency = -1)
        {
            Graphics g = Graphics.FromImage(BitmapWhereDraw);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int nWidth = BitmapWhereDraw.Size.Width - LeftBorder - RightBorder;
            int nHeight = BitmapWhereDraw.Size.Height - UpBorder - LowBorder;
            float nFontSize = 12.0f;
            float nFontVerticalPos = UpBorder / 2 - 8;

            SolidBrush ClearBrush = new SolidBrush(Color.White);
            g.FillRectangle(ClearBrush, new Rectangle(LeftBorder, 0, BitmapWhereDraw.Size.Width, UpBorder-15));
            // display curve name
            System.Drawing.Font CurveFont = new Font("Verdana", nFontSize);


            if (curveList != null )
            {
                int nVisibleCurveCount = 0;
                foreach (CCurve Curve in curveList)
                {
                    if (Curve.Visible == CCurve.YesNo.Yes)
                        nVisibleCurveCount++;
                }

                int HCurveGranularity = BitmapWhereDraw.Size.Width / (1 + nVisibleCurveCount);

                int nXStart = HCurveGranularity;

                for (int nCurve = 0; nCurve < curveList.Count; nCurve++)
                {
                    CCurve Curve = (CCurve)curveList[nCurve];

                    if (Curve.Visible == CCurve.YesNo.Yes)
                    {
                        SolidBrush CurveBrush = new SolidBrush(Curve.Color_);
                        Pen mypen = new Pen(Curve.Color_, Curve.LineWidth + 1);

                        String TextToDraw = Curve.Name;
                        if (CursorFrequency >= 0)
                        {
                            double dBLevel = Math.Round(Curve.GeDBLevelFromFrequency(CursorFrequency), 2);
                            TextToDraw += ":" + dBLevel.ToString() + "dB";
                        }

                        g.DrawLine(mypen, nXStart - 15, nFontVerticalPos + 10, nXStart, nFontVerticalPos + 10);
                        g.DrawString(TextToDraw, CurveFont, CurveBrush, new PointF(nXStart + 2, nFontVerticalPos));
                        nXStart += HCurveGranularity;

                    }
                }
            }
            g.Dispose();
        }


        void DrawBottomBox(ArrayList curveList = null)
        {
            Graphics g = Graphics.FromImage(BitmapWhereDraw);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen mypenHLines = new Pen(Brushes.Black);
            int nWidth = BitmapWhereDraw.Size.Width - LeftBorder - RightBorder;
            int nHeight = BitmapWhereDraw.Size.Height - UpBorder - LowBorder;
            int BoxMarging = 5;




            g.DrawLine(mypenHLines, LeftBorder, UpBorder + nHeight + BoxMarging, LeftBorder + nWidth, UpBorder + nHeight + BoxMarging);
            g.DrawLine(mypenHLines, LeftBorder, UpBorder + nHeight + LowBorder - BoxMarging, LeftBorder + nWidth, UpBorder + nHeight + LowBorder - BoxMarging);
            g.DrawLine(mypenHLines, LeftBorder, UpBorder + nHeight + BoxMarging, LeftBorder, UpBorder + nHeight + LowBorder - BoxMarging);
            g.DrawLine(mypenHLines, LeftBorder + nWidth, UpBorder + nHeight + BoxMarging, LeftBorder + nWidth, UpBorder + nHeight + LowBorder - BoxMarging);

            System.Drawing.Font MsgFont = new Font("Verdana", 8.0f);
            String Msg = "Generated with " + Program.Version;

            g.DrawString(Msg, MsgFont, Brushes.Brown, new Point(nWidth - 240, BitmapWhereDraw.Size.Height - 20));


            g.Dispose();
        }

        public void DrawBackGround(ArrayList curveList = null)
        {

            if (BitmapWhereDraw.Size.Width == 0 || BitmapWhereDraw.Size.Height == 0)
                return;

            int nWidth = BitmapWhereDraw.Size.Width - LeftBorder - RightBorder;
            int nHeight = BitmapWhereDraw.Size.Height - UpBorder - LowBorder;


            Graphics g = Graphics.FromImage(BitmapWhereDraw);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            System.Drawing.Font dBFont = null;

            System.Drawing.Font FreqFont = new Font("Times New Roman", 10.0f);


            if (outputMode == OutputMode.dB)
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
                if (nXTextDisplay + StringSize < BitmapWhereDraw.Size.Width)
                {
                    g.DrawString(sFrequency, FreqFont, Brushes.Black, new Point(nXTextDisplay, UpBorder - 15));
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
                g.DrawLine(mypenHLines, LeftBorder, nY, LeftBorder + nWidth, nY);

                if (outputMode == OutputMode.dB)
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

            DrawTopBox(curveList);
            DrawBottomBox(curveList);
        }

        int GetVerticalScaleIncrementFromDelta(int nDelta)
        {
            if (nDelta <= 20)
                return 1;

            if (nDelta <= 40)
                return 5;

            return 10;
        }

        public PointF GetCoords(Int64 Frequency, float fLevel, ref bool bCliped)
        {
            PointF Result = new PointF();
            int nWidth = BitmapWhereDraw.Size.Width - LeftBorder - RightBorder;
            int nHeight = BitmapWhereDraw.Size.Height - UpBorder - LowBorder;
            bCliped = false;

            if (Frequency > nLastDrawingHighFrequency)
            {
                Result.X = nWidth + LeftBorder ;
                bCliped = true;
            }
            else
            {
                if (Frequency < nLastDrawingLowFrequency)
                {
                    Result.X = LeftBorder;
                    bCliped = true;

                }
                else
                {
                    double fHRatio = ((double)(Frequency - nLastDrawingLowFrequency)) / (nLastDrawingHighFrequency - nLastDrawingLowFrequency);
                    Result.X = (float)(LeftBorder + nWidth * fHRatio);
                }

            }
                

            if ( fLevel > fLastDrawingLevelHigh )
            {
                Result.Y = UpBorder;
                bCliped = true;

            }
            else
            {
                if (fLevel < fLastDrawingLevelLow)
                {
                    Result.Y = UpBorder+ nHeight;
                    bCliped = true;

                }
                else
                {
                    double fVRatio = (fLevel - fLastDrawingLevelHigh) / (fLastDrawingLevelHigh - fLastDrawingLevelLow);
                    Result.Y = (float)(UpBorder - nHeight * fVRatio);
                }
            }
            return Result;
        }

        public void GraphicDisplay(ArrayList curveList, CCurve ActiveCurve, Bitmap PictureBoxBitmap)
        {
            BitmapWhereDraw = PictureBoxBitmap;
            DrawBackGround(curveList);
            if (curveList != null)
            {
                for (int i = 0; i < curveList.Count; i++)
                {
                    if (curveList[i] != null)
                    {
                        bool IsActive = (curveList[i] == ActiveCurve);
                        ((CCurve)curveList[i]).Draw(this, IsActive);
                    }
                }
            }
        }
    }
}
