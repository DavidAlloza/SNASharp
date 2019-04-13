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
            for ( int i = 0; i < UserMarkerArrayValues.Length;i++)
            {
                UserMarkerArrayValues[i] = new MarkersValues();
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

        private CurveBPMarker [] UserMarkerArray = new []{  CurveBPMarker.OFF,
                                                    CurveBPMarker.OFF,
                                                    CurveBPMarker.OFF,
                                                    CurveBPMarker.OFF,
                                                    CurveBPMarker.OFF,
                                                    CurveBPMarker.OFF };

        private MarkersValues[] UserMarkerArrayValues = new MarkersValues[6];
       

        // computed values to the curve
        private Int64 nMaxLevelFrequency = -1;
        private Int64 nMinLevelFrequency = -1;


        private MarkersValues Marker3dB = new MarkersValues();
        private MarkersValues Marker6dB = new MarkersValues();
        private MarkersValues Marker60dB = new MarkersValues();
        private float n6dB60dBfShapeFactor = -1.0f;
        private YesNo Is_Visible = YesNo.Yes;

        private DipoleDetected DipoleD = DipoleDetected.UNDERTERMINED;


        public Int64 nSpectrumLowFrequency = 0;
        public Int64 nSpectrumHighFrequency = 0;
        public Int64 nFrequencyStep = -1;


        public float fMaxLeveldB = 0.0f;
        public float fMinLeveldB = -90.0f;

        public byte R = 0;
        public byte G = 0;
        public byte B = 255;


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
            get { return UserMarkerArray[0]; }
            set { UserMarkerArray[0] = value; }
        }

        [Category("Markers")]
        public CurveBPMarker Marker_2
        {
            get { return UserMarkerArray[1]; }
            set { UserMarkerArray[1] = value; }
        }

        [Category("Markers")]
        public CurveBPMarker Marker_3
        {
            get { return UserMarkerArray[2]; }
            set { UserMarkerArray[2] = value; }
        }

        [Category("Markers")]
        public CurveBPMarker Marker_4
        {
            get { return UserMarkerArray[3]; }
            set { UserMarkerArray[3] = value; }
        }

        [Category("Markers")]
        public CurveBPMarker Marker_5
        {
            get { return UserMarkerArray[4]; }
            set { UserMarkerArray[4] = value; }
        }

        [Category("Markers")]
        public CurveBPMarker Marker_6
        {
            get { return UserMarkerArray[5]; }
            set { UserMarkerArray[5] = value; }
        }


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


        void DrawLevel(float dB, MarkersValues Marker, Graphics g, CGraph Graph)
        {
            Pen mypenArrow = new Pen(Color.Black, LineWidth);
            Pen mypenHline = new Pen(Color.Black, LineWidth);
            mypenHline.DashStyle = DashStyle.Dash;

            Pen mypenVline = new Pen(Color.Black, LineWidth);
            mypenVline.DashStyle = DashStyle.DashDot;

            System.Drawing.StringFormat VerticalTextDrawFormat = new System.Drawing.StringFormat();
            VerticalTextDrawFormat.FormatFlags = StringFormatFlags.DirectionVertical;


            System.Drawing.Font SampleFont = new Font("Verdana", 8.0f);
            String ndB =((int)dB).ToString()+"dB";

            bool bCliped = false;


            // bandpass
            if (Marker.HighFreq > 0 && Marker.LowFreq > 0)
            {


                // bandpass
                PointF HCoords1 = Graph.GetCoords(Marker.LowFreq, fMaxLeveldB + dB, ref bCliped);
                if (!bCliped) mypenHline.CustomStartCap = new AdjustableArrowCap(4.0f, 4.0f);
                if (!bCliped)
                {
                    PointF VCoords1 = new PointF(HCoords1.X, Graph.UpBorder);
                    PointF VCoords2 = new PointF(HCoords1.X,  Graph.BitmapWhereDraw.Height- Graph.LowBorder);
                    g.DrawLine(mypenVline, VCoords1, VCoords2);

                    PointF CoordVCenter = new PointF(VCoords1.X, 0.5f * (VCoords1.Y + VCoords2.Y));
                    g.DrawString(Utility.GetFrequencyStringAtBest(Marker.LowFreq), SampleFont, Brushes.Black, CoordVCenter, VerticalTextDrawFormat);
                }

                PointF HCoords2 = Graph.GetCoords(Marker.HighFreq, fMaxLeveldB + dB, ref bCliped);
                if (!bCliped) mypenHline.CustomEndCap = new AdjustableArrowCap(4.0f, 4.0f);
                if (!bCliped)
                {
                    PointF VCoords1 = new PointF(HCoords2.X, Graph.UpBorder);
                    PointF VCoords2 = new PointF(HCoords2.X, Graph.BitmapWhereDraw.Height - Graph.LowBorder);
                    g.DrawLine(mypenVline, VCoords1, VCoords2);

                    PointF CoordVCenter = new PointF(VCoords1.X, 0.5f * (VCoords1.Y + VCoords2.Y));
                    g.DrawString(Utility.GetFrequencyStringAtBest(Marker.HighFreq), SampleFont, Brushes.Black, CoordVCenter, VerticalTextDrawFormat);

                }

                g.DrawLine(mypenHline, HCoords1, HCoords2);

                PointF CoordCenter = new PointF(0.5f * (HCoords1.X + HCoords2.X), HCoords1.Y);
                g.DrawString(" " + ndB+ " ("+Utility.GetFrequencyStringAtBest(Marker.BandPass)+")", SampleFont, Brushes.Black, CoordCenter);

            }
            else
            {
                if (Marker.BandPass == 0)
                {
                    // Hline ( min or max)
                    PointF Coords0 = Graph.GetCoords(this.nSpectrumLowFrequency, fMaxLeveldB + dB, ref bCliped);
                    PointF Coords1 = Graph.GetCoords(this.nSpectrumHighFrequency, fMaxLeveldB + dB, ref bCliped);
                    g.DrawLine(mypenHline, Coords0, Coords1);
                }
                else
                {
                    if (Marker.HighFreq > 0)
                    {
                        // low pass
                        PointF Coords0 = Graph.GetCoords(this.nSpectrumLowFrequency, fMaxLeveldB + dB, ref bCliped);
                        PointF Coords1 = Graph.GetCoords(Marker.HighFreq, fMaxLeveldB + dB, ref bCliped);
                        PointF Coords2 = new PointF(Coords1.X, Graph.BitmapWhereDraw.Height - Graph.LowBorder);

                        g.DrawLine(mypenHline, Coords0, Coords1);
                        g.DrawLine(mypenVline, Coords1, Coords2);


                        PointF CoordHCenter = new PointF(0.5f * (Coords0.X + Coords1.X), Coords0.Y);
                        PointF CoordVCenter = new PointF(Coords1.X , 0.5f*(Coords1.Y+ Coords2.Y));

                        g.DrawString(Utility.GetFrequencyStringAtBest(Marker.HighFreq), SampleFont, Brushes.Black, CoordVCenter,VerticalTextDrawFormat);
                        g.DrawString(ndB , SampleFont, Brushes.Black, CoordHCenter);

                    }

                    if (Marker.LowFreq > 0)
                    {
                        // high pass
                        //PointF Coords = Graph.GetCoords(Marker.LowFreq, fMaxLeveldB + dB, ref bCliped);
                        //if (!bCliped) DrawMarker(ndB, Coords, g, SampleFont, mypenArrow, false);


                        PointF Coords0 = Graph.GetCoords(this.nSpectrumHighFrequency, fMaxLeveldB + dB, ref bCliped);
                        PointF Coords1 = Graph.GetCoords(Marker.LowFreq, fMaxLeveldB + dB, ref bCliped);
                        PointF Coords2 = new PointF(Coords1.X, Graph.BitmapWhereDraw.Height - Graph.LowBorder);


                        g.DrawLine(mypenHline, Coords0, Coords1);
                        g.DrawLine(mypenVline, Coords1, Coords2);


                        PointF CoordCenter = new PointF(0.5f * (Coords0.X + Coords1.X), Coords0.Y);
                        PointF CoordVCenter = new PointF(Coords1.X, 0.5f * (Coords1.Y + Coords2.Y));

                        g.DrawString(Utility.GetFrequencyStringAtBest(Marker.LowFreq), SampleFont, Brushes.Black, CoordVCenter, VerticalTextDrawFormat);

                        g.DrawString(ndB, SampleFont, Brushes.Black, CoordCenter);

                    }
                }
            }


        }

        void DrawAdditionnalsInfos(Graphics g, CGraph Graph)
        {
            System.Drawing.Font SampleFont = new Font("Verdana", 8.0f);
            
            //foreach  (CurveBPMarker Marker in MarkerArray)
            for ( int nIndex = 0; nIndex < UserMarkerArray.Length; nIndex++)
            {
                CurveBPMarker Marker = UserMarkerArray[nIndex];
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
                    DrawLevel(fdB, UserMarkerArrayValues[nIndex], g, Graph);


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

                if (Graph.outputMode == OutputMode.dB)
                {
                    DrawAdditionnalsInfos(g, Graph);
                }
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

        private DipoleDetected DetermineDipoleType(ref MarkersValues Marker)
        {
            if (Marker.LowFreq == -1 && Marker.HighFreq == - 1)
                return DipoleDetected.FLAT;

            if (Marker.LowFreq == -1)
                return DipoleDetected.LPF;

            if (Marker.HighFreq == - 1)
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
                    Result += "3dB cut off : " + Utility.GetStringWithSeparators(Marker3dB.HighFreq, " ")+"Hz";
                    if (Marker6dB.HighFreq != -1)
                    {
                        Result += NL+"6dB cut off : " + Utility.GetStringWithSeparators(Marker6dB.HighFreq, " ") + "Hz";
                    }

                    break;
                case DipoleDetected.HPF:
                    Result += "High pass "+ NL;
                    Result += "3dB cut off : " + Utility.GetStringWithSeparators(Marker3dB.LowFreq, " ") + "Hz";
                    if (Marker6dB.LowFreq != -1)
                    {
                        Result += NL + "6dB cut off : " + Utility.GetStringWithSeparators(Marker6dB.LowFreq, " ") + "Hz";
                    }

                    break;
                case DipoleDetected.BPF:
                    Result += "Band pass "+ NL;
                    Result += "3dB Low : " + Utility.GetStringWithSeparators(Marker3dB.LowFreq, " ") + "Hz"+ NL;
                    Result += "3dB high : " + Utility.GetStringWithSeparators(Marker3dB.HighFreq, " ") + "Hz"+ NL;
                    Result += "3dB BP : " + Utility.GetStringWithSeparators(Marker3dB.BandPass, " ") + "Hz";

                    if (Marker6dB.BandPass != -1)
                    {
                        Result += NL+"6dB Low : " + Utility.GetStringWithSeparators(Marker6dB.LowFreq, " ") + "Hz" + NL;
                        Result += "6dB high : " + Utility.GetStringWithSeparators(Marker6dB.HighFreq, " ") + "Hz" + NL;
                        Result += "6dB BP : " + Utility.GetStringWithSeparators(Marker6dB.BandPass, " ") + "Hz";
                    }

                    if (n6dB60dBfShapeFactor != -1)
                    {
                        Result += NL + "6/60dB Shape factor : " + n6dB60dBfShapeFactor.ToString();

                    }

                    break;
            }
            //Result += "USER MARKERS"+ NL;



            return Result;
        }

        public void ComputeMarkerFrequencies(ref MarkersValues dest,
                                             float fLevelToFind, 
                                             int nMaxLevelIndex)
        {
            int nLeftIndex = Utility.FindLevelIndex(SpectrumValues, nMaxLevelIndex, -1, fLevelToFind);

            if (nLeftIndex != 0)
                dest.LowFreq = nSpectrumLowFrequency + nLeftIndex * nFrequencyStep;
            else
                dest.LowFreq = -1;

            int nRightIndex = Utility.FindLevelIndex(SpectrumValues, nMaxLevelIndex, 1, fLevelToFind);

            if (nRightIndex < SpectrumValues.Length - 1)
                dest.HighFreq = nSpectrumLowFrequency + nRightIndex * nFrequencyStep;
            else
                dest.HighFreq = -1;


            if (dest.LowFreq != -1 && dest.HighFreq != -1 )
                dest.BandPass = dest.HighFreq - dest.LowFreq;
            else
                dest.BandPass = -1;

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

            for ( int nIndex = 0; nIndex < UserMarkerArray.Length; nIndex++)
            {
                CurveBPMarker Marker = UserMarkerArray[nIndex];

                if (Marker != CurveBPMarker.OFF)
                {
                    switch (Marker)
                    {
                        case CurveBPMarker.MAX_LEVEL:
                            UserMarkerArrayValues[nIndex].LowFreq = nMaxLevelFrequency;
                            UserMarkerArrayValues[nIndex].HighFreq = -1;
                            UserMarkerArrayValues[nIndex].BandPass = 0;
                            break;
                        case CurveBPMarker.MIN_LEVEL:
                            UserMarkerArrayValues[nIndex].LowFreq = nMinLevelFrequency;
                            UserMarkerArrayValues[nIndex].HighFreq = -1;
                            UserMarkerArrayValues[nIndex].BandPass = 0;
                            break;
                        case CurveBPMarker.BP_3dB_TO_MIN:
                            ComputeMarkerFrequencies(ref UserMarkerArrayValues[nIndex], (fMinLeveldB + 3.0f), nMaxLevelIndex);
                            break;
                        case CurveBPMarker.BP_6dB_TO_MIN:
                            ComputeMarkerFrequencies(ref UserMarkerArrayValues[nIndex], (fMinLeveldB + 6.0f), nMaxLevelIndex);
                            break;
                        default:
                            ComputeMarkerFrequencies(ref UserMarkerArrayValues[nIndex], (fMaxLeveldB - ((int)Marker)), nMaxLevelIndex);
                            break;
                    }
                }
            }

            ComputeMarkerFrequencies(ref Marker3dB, fMaxLeveldB - 3.0f, nMaxLevelIndex);
            DipoleD = DetermineDipoleType(ref Marker3dB);
            ComputeMarkerFrequencies(ref Marker6dB, fMaxLeveldB - 6.0f, nMaxLevelIndex);
            ComputeMarkerFrequencies(ref Marker60dB, fMaxLeveldB - 60.0f, nMaxLevelIndex);

            if (Marker60dB.BandPass  != -1)
            {
                n6dB60dBfShapeFactor = ((float)Marker60dB.BandPass / Marker6dB.BandPass);
            }
            else
            {
                n6dB60dBfShapeFactor = -1;
            }

        }
    }

}
