using System;
using System.Drawing;
using System.Globalization;
using System.Xml.Serialization;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections;

namespace SNASharp
{
    public class CCurve
    {
        public CCurve()
        {
            for ( int i = 0; i < MarkerArrayValues.Length;i++)
            {
                MarkerArrayValues[i] = new MarkersValues();
            }
        }
        /*
        public class MyArraylist : ArrayList
        {
            public override String ToString()
            {
                return null;
            }
        };*/
        /*
        public class AttEntry
        {
            public int Value;

            public int _Value
            {
                get { return Value; }
                set { Value = value; }
            }

            public override String ToString()
            {
                return Value.ToString();
            }
        }
        */

        public class MarkersValues
        {
            public Int64 LowFreq = -1;
            public Int64 HighFreq = -1;
            public Int64 BandPass = -1;
        };

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

        public enum CurveBPMarker
        {
            OFF = -1,
            MAX_LEVEL = 0,
            BP_3dB = 3,
            BP_6dB = 6,
            BP_10dB = 10,
            BP_20dB = 20,
            BP_30dB = 30,
            BP_40dB = 40,
            BP_50dB = 50,
            BP_60dB = 60,
            BP_70dB = 70,
            BP_80dB = 80,
            BP_90dB = 90,
            BP_100dB = 100,
            BP_6dB_TO_MIN,
            BP_3dB_TO_MIN,
            MIN_LEVEL    
        };

        private CurveBPMarker [] MarkerArray = new []{  CurveBPMarker.OFF,
                                                    CurveBPMarker.OFF,
                                                    CurveBPMarker.OFF,
                                                    CurveBPMarker.OFF,
                                                    CurveBPMarker.OFF,
                                                    CurveBPMarker.OFF };

        private MarkersValues[] MarkerArrayValues = new MarkersValues[6];

        //        MyArraylist MarkersList = new MyArraylist();


        //  private AttEntry[] BPW = new AttEntry[] { };

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
        public Int64 nFrequencyStep = -1;

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

        [Category("Markers")]
        public CurveBPMarker Marker_1
        {
            get { return MarkerArray[0]; }
            set { MarkerArray[0] = value; }
        }

        [Category("Markers")]
        public CurveBPMarker Marker_2
        {
            get { return MarkerArray[1]; }
            set { MarkerArray[1] = value; }
        }

        [Category("Markers")]
        public CurveBPMarker Marker_3
        {
            get { return MarkerArray[2]; }
            set { MarkerArray[2] = value; }
        }

        [Category("Markers")]
        public CurveBPMarker Marker_4
        {
            get { return MarkerArray[3]; }
            set { MarkerArray[3] = value; }
        }

        [Category("Markers")]
        public CurveBPMarker Marker_5
        {
            get { return MarkerArray[4]; }
            set { MarkerArray[4] = value; }
        }

        [Category("Markers")]
        public CurveBPMarker Marker_6
        {
            get { return MarkerArray[5]; }
            set { MarkerArray[5] = value; }
        }

        /*
        [DisplayName("Markers list")]
        public CurveBPMarker [] Markers
        {
            get { return MarkerArray; }
            set { MarkerArray = value; }
        }

        [DisplayName("Markers_")]
        public AttEntry[] MarkersList_
        {
            get { return BPW; }
            set { BPW = value; }
        }
        
    */

        public override String ToString()
        {
            return CurveName;
        }

        void DrawMarker(String Text, PointF Coords, Graphics g, System.Drawing.Font SampleFont, Pen MyPen, bool bAtRight, bool bDrawArrow = true)
        {
            const float ArrowSize = 5.0f;

            if (bDrawArrow)
            {
                PointF MarkerUpLeft = new PointF(Coords.X - ArrowSize, Coords.Y- ArrowSize);
                PointF MarkerLowRight = new PointF(Coords.X + ArrowSize, Coords.Y+ ArrowSize);
                PointF MarkerUpRight = new PointF(Coords.X+ ArrowSize, Coords.Y - ArrowSize);
                PointF MarkerDownLeft = new PointF(Coords.X- ArrowSize, Coords.Y + ArrowSize);

                g.DrawLine(MyPen, MarkerUpLeft, MarkerLowRight);
                g.DrawLine(MyPen, MarkerUpRight, MarkerDownLeft);
            }
            g.DrawString(" "+Text, SampleFont, Brushes.Black, Coords);
        }


        void DrawLevel(float dB,Int64 LowFrequency, Int64 HighFrequency, Graphics g, CGraph Graph)
        {
            Pen mypenArrow = new Pen(Color.Black, LineWidth);
            Pen mypenHline = new Pen(Color.Black, LineWidth);
            mypenHline.DashStyle = DashStyle.Dash;

            Pen mypenVline = new Pen(Color.Black, LineWidth);
            mypenVline.DashStyle = DashStyle.DashDot;


            System.Drawing.Font SampleFont = new Font("Verdana", 8.0f);
            String ndB =((int)dB).ToString()+"dB";

            bool bCliped = false;



            if (HighFrequency > 0 && LowFrequency > 0)
            {


                // bandpass
                PointF HCoords1 = Graph.GetCoords(LowFrequency, fMaxLeveldB + dB, ref bCliped);
                if (!bCliped) mypenHline.CustomStartCap = new AdjustableArrowCap(4.0f, 4.0f);
                if (!bCliped)
                {
                    PointF VCoords1 = new PointF(HCoords1.X, Graph.UpBorder);
                    PointF VCoords2 = new PointF(HCoords1.X,  Graph.BitmapWhereDraw.Height- Graph.LowBorder);
                    g.DrawLine(mypenVline, VCoords1, VCoords2);
                }


                //if (!bCliped) DrawMarker(ndB, Coords1, g, SampleFont, mypenArrow, true, false);


                PointF HCoords2 = Graph.GetCoords(HighFrequency, fMaxLeveldB + dB, ref bCliped);
                if (!bCliped) mypenHline.CustomEndCap = new AdjustableArrowCap(4.0f, 4.0f);
                if (!bCliped)
                {
                    PointF VCoords1 = new PointF(HCoords2.X, Graph.UpBorder);
                    PointF VCoords2 = new PointF(HCoords2.X, Graph.BitmapWhereDraw.Height - Graph.LowBorder);
                    g.DrawLine(mypenVline, VCoords1, VCoords2);
                }

                g.DrawLine(mypenHline, HCoords1, HCoords2);

                PointF CoordCenter = new PointF(0.5f * (HCoords1.X + HCoords2.X), HCoords1.Y);
                g.DrawString(" " + ndB, SampleFont, Brushes.Black, CoordCenter);

            }
            else
            {

                if (HighFrequency > 0)
                {
                    PointF Coords = Graph.GetCoords(HighFrequency, fMaxLeveldB + dB, ref bCliped);
                    if (!bCliped) DrawMarker(ndB, Coords, g, SampleFont, mypenArrow, true);
                }

                if (LowFrequency > 0)
                {
                    PointF Coords = Graph.GetCoords(LowFrequency, fMaxLeveldB + dB, ref bCliped);
                    if (!bCliped) DrawMarker(ndB, Coords, g, SampleFont, mypenArrow, false);
                }
            }


        }

        void DrawAdditionnalsInfos(Graphics g, CGraph Graph)
        {
            System.Drawing.Font SampleFont = new Font("Verdana", 8.0f);
            
            //foreach  (CurveBPMarker Marker in MarkerArray)
            for ( int nIndex = 0; nIndex < MarkerArray.Length; nIndex++)
            {
                CurveBPMarker Marker = MarkerArray[nIndex];
                float fdB = 0.0f;
                if (Marker != CurveBPMarker.OFF)
                {
                    switch (Marker)
                    {
                        case CurveBPMarker.MAX_LEVEL:
                            fdB = 0.0f;
                            break;
                        case CurveBPMarker.MIN_LEVEL:
                            fdB =  fMinLeveldB - fMaxLeveldB;
                            break;
                        case CurveBPMarker.BP_3dB_TO_MIN:
                            fdB = fMinLeveldB - fMaxLeveldB + 3.0f;
                            break;
                        case CurveBPMarker.BP_6dB_TO_MIN:
                            fdB = fMinLeveldB - fMaxLeveldB + 6.0f;
                            break;
                        default:
                            fdB =  - ((int)Marker);
                            break;
                    }
                    DrawLevel(fdB, MarkerArrayValues[nIndex].LowFreq, MarkerArrayValues[nIndex].HighFreq, g, Graph);


                }
            }

//            DrawLevel(-3.0f, n3dBBandpassLowFrequency, n3dBBandpassHighFrequency, g, Graph);
//            DrawLevel(-6.0f, n6dBBandpassLowFrequency, n6dBBandpassHighFrequency, g, Graph);
//            DrawLevel(-60.0f, n60dBBandpassLowFrequency, n60dBBandpassHighFrequency, g, Graph);

        }

        public void Draw(CGraph Graph, bool IsActive)
        {
            if (Graph.BitmapWhereDraw.Size.Width == 0 || Graph.BitmapWhereDraw.Size.Height == 0 || Visible == CCurve.YesNo.No)
                return;

            int nWidth = Graph.BitmapWhereDraw.Size.Width - Graph.LeftBorder - Graph.RightBorder;
            int nHeight = Graph.BitmapWhereDraw.Size.Height - Graph.UpBorder - Graph.LowBorder;

            Graphics g = Graphics.FromImage(Graph.BitmapWhereDraw);

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
                double fXStartOffset = 0;
                double fXEndOffset = 0;

                double fSpectrumHRes = (double)(Graph.nLastDrawingHighFrequency - Graph.nLastDrawingLowFrequency) / nWidth;
                Int64 nCurveBW = nSpectrumHighFrequency - nSpectrumLowFrequency;

                if (nSpectrumLowFrequency < Graph.nLastDrawingLowFrequency)
                {
                    nFirstSpectrumIndex = (Int32)(((double)(Graph.nLastDrawingLowFrequency - nSpectrumLowFrequency) * SpectrumValues.Length) / nCurveBW);
                }
                else
                {
                    nFirstSpectrumIndex = 0;
                    fXStartOffset = (nSpectrumLowFrequency - Graph.nLastDrawingLowFrequency) / fSpectrumHRes;
                }

                int nLastSpectrumIndex;

                if (nSpectrumHighFrequency > Graph.nLastDrawingHighFrequency)
                {
                    nLastSpectrumIndex = (Int32)(((double)(Graph.nLastDrawingHighFrequency - nSpectrumLowFrequency) * SpectrumValues.Length) / nCurveBW);
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
                    Mesures[i].X = (float)(((double)i * nPixelToDisplay) / nSpectrumCount + Graph.LeftBorder + fXStartOffset);
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

        public void ComputeMarkerFrequencies(int nIndex,
                                             float fLevelToFind, 
                                             bool BandPassToChek,
                                             int nMaxLevelIndex)
        {
            int nLeftIndex = Utility.FindLevelIndex(SpectrumValues, nMaxLevelIndex, -1, fLevelToFind);

            if (nLeftIndex != 0)
                MarkerArrayValues[nIndex].LowFreq = nSpectrumLowFrequency + nLeftIndex * nFrequencyStep;
            else
                MarkerArrayValues[nIndex].LowFreq = -1;

            int nRightIndex = Utility.FindLevelIndex(SpectrumValues, nMaxLevelIndex, 1, fLevelToFind);

            if (nRightIndex < SpectrumValues.Length - 1)
                MarkerArrayValues[nIndex].HighFreq = nSpectrumLowFrequency + nRightIndex * nFrequencyStep;
            else
                MarkerArrayValues[nIndex].HighFreq = -1;


            if (n6dBBandpassHighFrequency != -1 && n6dBBandpassLowFrequency != -1 && BandPassToChek)
                MarkerArrayValues[nIndex].BandPass = MarkerArrayValues[nIndex].HighFreq - MarkerArrayValues[nIndex].LowFreq;
            else
                n6dBBandpass = -1;

        }

        public void ComputeCaracteristicsParams()
        {
            if (SpectrumValues == null || SpectrumValues.Length == 0)
                return;


            int nMaxLevelIndex = Utility.RetrieveMaxValueIndex(SpectrumValues);
            int nMinLevelIndex = Utility.RetrieveMinValueIndex(SpectrumValues);

            fMaxLeveldB = SpectrumValues[nMaxLevelIndex];
            fMinLeveldB = SpectrumValues[nMinLevelIndex];

            nFrequencyStep = (nSpectrumHighFrequency - nSpectrumLowFrequency) / SpectrumValues.Length;
            nMaxLevelFrequency = nSpectrumLowFrequency + nMaxLevelIndex * nFrequencyStep;
            nMinLevelFrequency = nSpectrumLowFrequency + nMinLevelIndex * nFrequencyStep;

            //foreach  (CurveBPMarker Marker in MarkerArray)
            for ( int nIndex = 0; nIndex < MarkerArray.Length; nIndex++)
            {
                CurveBPMarker Marker = MarkerArray[nIndex];

                if (Marker != CurveBPMarker.OFF)
                {
                    switch (Marker)
                    {
                        case CurveBPMarker.MAX_LEVEL:
                            MarkerArrayValues[nIndex].LowFreq = nMaxLevelFrequency;
                            MarkerArrayValues[nIndex].HighFreq = -1;
                            MarkerArrayValues[nIndex].BandPass = -1;
                            break;
                        case CurveBPMarker.MIN_LEVEL:
                            MarkerArrayValues[nIndex].LowFreq = nMinLevelFrequency;
                            MarkerArrayValues[nIndex].HighFreq = -1;
                            MarkerArrayValues[nIndex].BandPass = -1;
                            break;
                        case CurveBPMarker.BP_3dB_TO_MIN:
                            ComputeMarkerFrequencies(nIndex, (fMinLeveldB + 3.0f), true, nMaxLevelIndex);
                            break;
                        case CurveBPMarker.BP_6dB_TO_MIN:
                            ComputeMarkerFrequencies(nIndex, (fMinLeveldB + 6.0f), true, nMaxLevelIndex);
                            break;
                        default:
                            ComputeMarkerFrequencies(nIndex, (fMaxLeveldB - ((int)Marker)), true, nMaxLevelIndex);
                            break;
                    }
                }
            }


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
