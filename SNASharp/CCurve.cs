using System;
using System.Drawing;
using System.Globalization;
using System.Xml.Serialization;


namespace SNASharp
{
    public class CCurve
    {
        public enum YesNo
        {
            Yes,
            No
        };

        public enum DipoleDetected
        {
            LPF,
            HPF,
            BPF,
            FLAT,
            UNDERTERMINED
        };


        // computed values to the curve
        public Int64 nMaxLevelFrequency = -1;
        public Int64 nMinLevelFrequency = -1;

        public float fMaxLeveldB = 0.0f;
        public float fMinLeveldB = -90.0f;

        public Int64 n3dBBandpass = -1;
        public Int64 n3dBBandpassLowFrequency = -1;
        public Int64 n3dBBandpassHighFrequency = -1;

        public Int64 n6dBBandpass = -1;
        public Int64 n6dBBandpassLowFrequency = -1;
        public Int64 n6dBBandpassHighFrequency = -1;

        public Int64 n60dBBandpass = -1;
        public Int64 n60dBBandpassLowFrequency = -1;
        public Int64 n60dBBandpassHighFrequency = -1;
        public int nQ = -1;
        public float n6dB60dBfShapeFactor = -1.0f;
        public Int64 nSpectrumLowFrequency = 0;
        public Int64 nSpectrumHighFrequency = 0;
        private YesNo Is_Visible = YesNo.Yes;
        public byte R = 0;
        public byte G = 0;
        public byte B = 255;

        DipoleDetected DipoleD = DipoleDetected.UNDERTERMINED;

        [XmlIgnore]
        public float LineWidth = 1.0f;


        // infos
        private String CurveName = "Curve_0";


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

        [XmlIgnore]
        public Color Color_
        {
            get { return Color.FromArgb(255, R, G, B); }

            set
            {
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

        public String Width
        {
            get { return LineWidth.ToString(); }
            set
            {
                value.Replace(',', '.');
                LineWidth = Convert.ToSingle(value, new CultureInfo("en-US"));
            }
        }

        public override String ToString()
        {
            return CurveName;
        }

        void DrawMarker(String Text, PointF Coords, Graphics g, System.Drawing.Font SampleFont, Pen MyPen, bool bAtRight)
        {
            const float ArrowSize = 5.0f;
            PointF MarkerLeft = new PointF(Coords.X - ArrowSize, Coords.Y);
            PointF MarkerRight = new PointF(Coords.X + ArrowSize, Coords.Y);
            PointF MarkerUp = new PointF(Coords.X, Coords.Y- ArrowSize);
            PointF MarkerDown = new PointF(Coords.X, Coords.Y + ArrowSize);

            g.DrawLine(MyPen, MarkerLeft, MarkerRight);
            g.DrawLine(MyPen, MarkerUp, MarkerDown);

            g.DrawString(" "+Text, SampleFont, Brushes.Black, Coords);
        }

        void DrawAdditionnalsInfos(Graphics g, CGraph Graph)
        {
            System.Drawing.Font SampleFont = new Font("Verdana", 8.0f);
            
            Pen mypen = new Pen(Color.Black, LineWidth);
            bool bCliped = false;

            if (n3dBBandpassHighFrequency > 0)
            {
                PointF Coords = Graph.GetCoords(n3dBBandpassHighFrequency, fMaxLeveldB-3.0f, ref bCliped);
                if (!bCliped) DrawMarker("-3dB",Coords, g, SampleFont, mypen,true);
            }

            if (n3dBBandpassLowFrequency > 0)
            {
                PointF Coords = Graph.GetCoords(n3dBBandpassLowFrequency, fMaxLeveldB - 3.0f, ref bCliped);
                if (!bCliped) DrawMarker("-3dB", Coords, g, SampleFont, mypen, false);
            }

            if (n6dBBandpassHighFrequency > 0)
            {
                PointF Coords = Graph.GetCoords(n6dBBandpassHighFrequency, fMaxLeveldB - 6.0f, ref bCliped);
                if (!bCliped) DrawMarker("-6dB", Coords, g, SampleFont, mypen, true);
            }

            if (n6dBBandpassLowFrequency > 0)
            {
                PointF Coords = Graph.GetCoords(n6dBBandpassLowFrequency, fMaxLeveldB - 6.0f, ref bCliped);
                if (!bCliped) DrawMarker("-6dB", Coords, g, SampleFont, mypen, false);
            }

            if (n60dBBandpassHighFrequency > 0)
            {
                PointF Coords = Graph.GetCoords(n60dBBandpassHighFrequency, fMaxLeveldB - 60.0f, ref bCliped);
                if (!bCliped) DrawMarker("-60dB", Coords, g, SampleFont, mypen, true);
            }

            if (n60dBBandpassLowFrequency > 0)
            {
                PointF Coords = Graph.GetCoords(n60dBBandpassLowFrequency, fMaxLeveldB - 60.0f, ref bCliped);
                if ( !bCliped) DrawMarker("-60dB", Coords, g, SampleFont, mypen, false);
            }

            if (n60dBBandpassHighFrequency > 0 && n60dBBandpassLowFrequency > 0)
            {
                PointF Coords1 = Graph.GetCoords(n60dBBandpassLowFrequency, fMaxLeveldB - 60.0f, ref bCliped);
                PointF Coords2 = Graph.GetCoords(n60dBBandpassHighFrequency, fMaxLeveldB - 60.0f, ref bCliped);
                g.DrawLine(mypen, Coords1, Coords2);


            }

        }

        public void Draw(CGraph Graph, bool IsActive)
        {
            if (Graph.Picture.Size.Width == 0 || Graph.Picture.Size.Height == 0 || Visible == CCurve.YesNo.No)
                return;

            int nWidth = Graph.Picture.Size.Width - Graph.LeftBorder - Graph.RightBorder;
            int nHeight = Graph.Picture.Size.Height - Graph.UpBorder - Graph.LowBorder;

            Graphics g = Graphics.FromImage(Graph.Picture.Image);

            if (Graph.AntiAlias)
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            else
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;


            if (SpectrumValues != null
                && nSpectrumLowFrequency < Graph.nLastDrawingHighFrequency
                && nSpectrumHighFrequency > Graph.nLastDrawingLowFrequency
                && Graph.nLastDrawingHighFrequency > Graph.nLastDrawingLowFrequency) // check if curve is visible
            {


                float fVerticalRangedB = Graph.fLastDrawingLevelHigh - Graph.fLastDrawingLevelLow;
                float fVerticalScale = nHeight / fVerticalRangedB;


                Pen mypen = new Pen(Color_, LineWidth);


                int nFirstSpectrumIndex;
                float fXStartOffset = 0;
                float fXEndOffset = 0;

                float fSpectrumHRes = (Graph.nLastDrawingHighFrequency - Graph.nLastDrawingLowFrequency) / nWidth;
                Int64 nCurveBW = nSpectrumHighFrequency - nSpectrumLowFrequency;

                if (nSpectrumLowFrequency < Graph.nLastDrawingLowFrequency)
                {
                    nFirstSpectrumIndex = (Int32)(((Int64)(Graph.nLastDrawingLowFrequency - nSpectrumLowFrequency) * SpectrumValues.Length) / nCurveBW);
                }
                else
                {
                    nFirstSpectrumIndex = 0;
                    fXStartOffset = (nSpectrumLowFrequency - Graph.nLastDrawingLowFrequency) / fSpectrumHRes;
                }

                int nLastSpectrumIndex;

                if (nSpectrumHighFrequency > Graph.nLastDrawingHighFrequency)
                {
                    nLastSpectrumIndex = (Int32)(((Int64)(Graph.nLastDrawingHighFrequency - nSpectrumLowFrequency) * SpectrumValues.Length) / nCurveBW);
                }
                else
                {
                    nLastSpectrumIndex = SpectrumValues.Length - 1;
                    fXEndOffset = (Graph.nLastDrawingHighFrequency - nSpectrumHighFrequency) / fSpectrumHRes;
                }

                int nSpectrumCount = nLastSpectrumIndex - nFirstSpectrumIndex;

                int nPixelToDisplay = nWidth - (int)(fXEndOffset + fXStartOffset);

                PointF[] Mesures = new PointF[nSpectrumCount];

                for (int i = 0; i < nSpectrumCount; i++)
                {
                    float fMesure = 0.0f;
                    if (Graph.outputMode == OutputMode.dB)
                    {
                        fMesure = SpectrumValues[i + nFirstSpectrumIndex];
                    }
                    else
                    {
                        float fdBValue = SpectrumValues[i + nFirstSpectrumIndex];
                        if (fdBValue > 0.0f)
                            fdBValue = 0.0f;

                        double fReflective = Math.Pow(10, fdBValue / 20.0f);
                        double WSR = (1.0 + fReflective) / (1.0 - fReflective);
                        fMesure = (float)Math.Max(Math.Min(WSR, Graph.fLastDrawingLevelHigh), 1.0);
                    }

                    Mesures[i].Y = ((Graph.fLastDrawingLevelHigh - fMesure) * fVerticalScale) + Graph.UpBorder;
                    Mesures[i].X = ((float)i * nPixelToDisplay) / nSpectrumCount + Graph.LeftBorder + fXStartOffset;
                }

                if (Mesures.Length > 1)
                {
                    g.DrawLines(mypen, Mesures);
                }

                if (IsActive)
                {
                    System.Drawing.Font SampleFont = new Font("Verdana", 8.0f);

                    float fPixelPerSample = (float)Math.Round((float)nWidth / nSpectrumCount, 1);
                    float fSamplePerPixel = (float)Math.Round((float)nSpectrumCount / nWidth, 1);


                    if (nSpectrumCount > nWidth)
                        g.DrawString("Samples: " + nSpectrumCount.ToString() + " (" + fSamplePerPixel.ToString() + " Samples per pixel)", SampleFont, Brushes.Black, new Point(nWidth - 180, 10));
                    else
                        g.DrawString("Samples: " + nSpectrumCount.ToString() + " (" + fPixelPerSample.ToString() + " Pixel per sample)", SampleFont, Brushes.Red, new Point(nWidth - 180, 10));
                }
                DrawAdditionnalsInfos(g, Graph);
            }

            g.Dispose();
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

        public void DetermineMinMaxLevels()
        {
            fMinLeveldB = SpectrumValues[Utility.RetrieveMinValueIndex(SpectrumValues)];
            fMaxLeveldB = SpectrumValues[Utility.RetrieveMaxValueIndex(SpectrumValues)];
        }

        private DipoleDetected DetermineDipoleType(int LowIndex, int nHighIndex, int nCount)
        {
            if (LowIndex == 0 && nHighIndex == SpectrumValues.Length - 1)
                return DipoleDetected.FLAT;

            if (LowIndex == 0)
                return DipoleDetected.LPF;

            if (nHighIndex == SpectrumValues.Length - 1)
                return DipoleDetected.HPF;

            return DipoleDetected.BPF;
        }

        public String GetCurveDescription()
        {

            String NL = Environment.NewLine;

            String Result = "Name : " + Name + NL;
            Result += "Max Level : " + fMaxLeveldB + "dB";
            Result += " at " + Utility.GetStringWithSeparators(nMaxLevelFrequency, " ") + "Hz"+ NL;
            Result += "Min Level : " + fMinLeveldB + "dB";
            Result += " at " + Utility.GetStringWithSeparators(nMinLevelFrequency, " ") + "Hz"+ NL;

            Result += "DUT detected as ";

            switch (DipoleD)
            {
                case DipoleDetected.UNDERTERMINED:                   
                    break;
                case DipoleDetected.FLAT:
                    Result += " Flat caracteristic";
                    break;
                case DipoleDetected.LPF:
                    Result += "Low pass "+ NL;
                    Result += "3dB cut off : " + Utility.GetStringWithSeparators(n3dBBandpassHighFrequency," ")+"Hz";
                    if (n6dBBandpassHighFrequency != -1)
                    {
                        Result += NL+"6dB cut off : " + Utility.GetStringWithSeparators(n6dBBandpassHighFrequency, " ") + "Hz";
                    }

                    break;
                case DipoleDetected.HPF:
                    Result += "High pass "+ NL;
                    Result += "3dB cut off : " + Utility.GetStringWithSeparators(n3dBBandpassLowFrequency, " ") + "Hz";
                    if (n6dBBandpassLowFrequency != -1)
                    {
                        Result += NL + "6dB cut off : " + Utility.GetStringWithSeparators(n6dBBandpassLowFrequency, " ") + "Hz";
                    }

                    break;
                case DipoleDetected.BPF:
                    Result += "Band pass "+ NL;
                    Result += "3dB Low : " + Utility.GetStringWithSeparators(n3dBBandpassLowFrequency, " ") + "Hz"+ NL;
                    Result += "3dB high : " + Utility.GetStringWithSeparators(n3dBBandpassHighFrequency, " ") + "Hz"+ NL;
                    Result += "3dB BP : " + Utility.GetStringWithSeparators(n3dBBandpass, " ") + "Hz";

                    if (n6dBBandpass != -1)
                    {
                        Result += NL+"6dB Low : " + Utility.GetStringWithSeparators(n6dBBandpassLowFrequency, " ") + "Hz" + NL;
                        Result += "6dB high : " + Utility.GetStringWithSeparators(n6dBBandpassHighFrequency, " ") + "Hz" + NL;
                        Result += "6dB BP : " + Utility.GetStringWithSeparators(n6dBBandpass, " ") + "Hz";
                    }

                    if (n6dB60dBfShapeFactor != -1)
                    {
                        Result += NL + "6/60dB Shape factor : " + n6dB60dBfShapeFactor.ToString();

                    }

                    break;
            }
            return Result;
        }

        public void ComputeCaracteristicsParams()
        {
            if (SpectrumValues == null || SpectrumValues.Length == 0)
                return;

            int nMaxLevelIndex = Utility.RetrieveMaxValueIndex(SpectrumValues);
            int nMinLevelIndex = Utility.RetrieveMinValueIndex(SpectrumValues);

            fMaxLeveldB = SpectrumValues[nMaxLevelIndex];
            fMinLeveldB = SpectrumValues[nMinLevelIndex];

            int nSpectrumLeftValidityIndex = SpectrumValues.Length / 20;
            int nSpectrumRightValidityIndex = (SpectrumValues.Length - SpectrumValues.Length / 20);

            Int64 nFrequencyStep = (nSpectrumHighFrequency - nSpectrumLowFrequency) / SpectrumValues.Length;
            nMaxLevelFrequency = nSpectrumLowFrequency + nMaxLevelIndex * nFrequencyStep;
            nMinLevelFrequency = nSpectrumLowFrequency + nMinLevelIndex * nFrequencyStep;


            int nLeft3dBIndex = Utility.FindLevelIndex(SpectrumValues, nMaxLevelIndex, -1, fMaxLeveldB - 3.0f);
            n3dBBandpassLowFrequency = nSpectrumLowFrequency + nLeft3dBIndex * nFrequencyStep;

            int nRight3dBIndex = Utility.FindLevelIndex(SpectrumValues, nMaxLevelIndex, 1, fMaxLeveldB - 3.0f);
            n3dBBandpassHighFrequency = nSpectrumLowFrequency + nRight3dBIndex * nFrequencyStep;

            DipoleD = DetermineDipoleType(nLeft3dBIndex, nRight3dBIndex, SpectrumValues.Length);


            if (DipoleD == DipoleDetected.BPF)
            {
                n3dBBandpass = (int)((nRight3dBIndex - nLeft3dBIndex) * nFrequencyStep);
            }
            else
            {
                n3dBBandpass = -1;
                if (nLeft3dBIndex == 0)
                    n3dBBandpassLowFrequency = -1;

                if (nRight3dBIndex >= SpectrumValues.Length - 2)
                    n3dBBandpassHighFrequency = -1;
            }

            int nLeft6dBIndex = Utility.FindLevelIndex(SpectrumValues, nMaxLevelIndex, -1, fMaxLeveldB - 6.0f);
            if (nLeft6dBIndex != 0)
            {
                n6dBBandpassLowFrequency = nSpectrumLowFrequency + nLeft6dBIndex * nFrequencyStep;
            }
            else
            {
                n6dBBandpassLowFrequency = -1;
            }

            int nRight6dBIndex = Utility.FindLevelIndex(SpectrumValues, nMaxLevelIndex, 1, fMaxLeveldB - 6.0f);
            if (nRight6dBIndex < SpectrumValues.Length - 1)
            {
                n6dBBandpassHighFrequency = nSpectrumLowFrequency + nRight6dBIndex * nFrequencyStep;
            }
            else
            {
                n6dBBandpassHighFrequency = -1;
            }

//            DipoleDetected DipoleAt6dB = DetermineDipoleType(nLeft6dBIndex, nRight6dBIndex, SpectrumValues.Length);


            if (n6dBBandpassHighFrequency != -1 && n6dBBandpassLowFrequency != -1)
            {
                n6dBBandpass = (int)((nRight6dBIndex - nLeft6dBIndex) * nFrequencyStep);
            }
            else
            {
                n6dBBandpass = -1;
            }

            int nLeft60dBIndex = Utility.FindLevelIndex(SpectrumValues, nMaxLevelIndex, -1, fMaxLeveldB - 60.0f);

            if (nLeft60dBIndex != 0)
                n60dBBandpassLowFrequency = nSpectrumLowFrequency + nLeft60dBIndex * nFrequencyStep;
            else
                n60dBBandpassLowFrequency = -1;



            int nRight60dBIndex = Utility.FindLevelIndex(SpectrumValues, nMaxLevelIndex, 1, fMaxLeveldB - 60.0f);
            if (nRight60dBIndex < SpectrumValues.Length - 1)
                n60dBBandpassHighFrequency = nSpectrumLowFrequency + nRight60dBIndex * nFrequencyStep;
            else
                n60dBBandpassHighFrequency = -1;


            //DipoleDetected DipoleAt60dB = DetermineDipoleType(nLeft60dBIndex, nRight60dBIndex, SpectrumValues.Length);



            if (n60dBBandpassHighFrequency != -1 && n60dBBandpassLowFrequency!=-1)
            {
                n60dBBandpass = (int)((nRight60dBIndex - nLeft60dBIndex) * nFrequencyStep);
                n6dB60dBfShapeFactor = ((float)n60dBBandpass / n6dBBandpass);
            }
            else
            {
                n60dBBandpass = -1;
                n6dB60dBfShapeFactor = -1;
            }

        }
    }

}
