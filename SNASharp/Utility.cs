using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SNASharp
{
    class Utility
    {

        public static long UpdateSampleCountToMatchIntegerFrequencyStepConstraint(long FreqStart, long FreqEnd, long InitialSampleCount)
        {

            double FrequencyStep = ((double)(FreqEnd - FreqStart)) / InitialSampleCount;
            if (FrequencyStep < 0.51)
                FrequencyStep = 0.51;

            double correctionRatio = FrequencyStep / Math.Round(FrequencyStep);
            return (long)Math.Truncate(InitialSampleCount * correctionRatio);
        }

        public static String BuildZeroLeftString(Int64 nValue, int nNeededlength)
        {
            String sValue = nValue.ToString();

            while (sValue.Length < nNeededlength)
            {
                sValue = sValue.Insert(0, "0");
            }
            return sValue;
        }

        public static String RemoveSeparator(String StringWithSeparator)
        {
            StringWithSeparator = StringWithSeparator.Replace(" ", "");
            return StringWithSeparator;
        }


        public static String GetFrequencyStringAtBest(Int64 nValue)
        {
            if (nValue > 100000000)
            {
                return GetStringWithSeparators(nValue / 1000, " ") + "kHz";
            }
            else
            {
                return GetStringWithSeparators(nValue, " ") + "Hz";
            }
        }

        public static String GetStringWithSeparators(Int64 nValue, String Separator)
        {

            Int64 nUnits = nValue % 1000;
            nValue /= 1000;
            Int64 nMilliers = nValue % 1000;
            nValue /= 1000;
            Int64 nMillion = nValue % 1000;
            nValue /= 1000;
            Int64 nMilliard = nValue % 1000;

            String Result = nMilliard.ToString() + Separator + BuildZeroLeftString(nMillion, 3) + Separator + BuildZeroLeftString(nMilliers, 3) + Separator + BuildZeroLeftString(nUnits, 3);

            for (int i = 0; i < 13; i++)
            {
                if (Result[0] == '0' || Result[0] == Separator[0])
                {
                    Result = Result.Substring(1);
                }
            }

            return Result;
        }


        public static int RetrieveMaxValueIndex(float[] Data)
        {
            float fMaxValue = Data[0];
            int nFirstIndex = 0;
            //int nLastIndex = 0;

            for (int i = 1; i < Data.Length; i++)
            {
                if (Data[i] > fMaxValue)
                {
                    fMaxValue = Data[i];
                    nFirstIndex = i;
                }

                //               if (Data[i] == fMaxValue)
                //               {
                //                   nLastIndex = i;
                //               }

            }
            //           return (nFirstIndex + nLastIndex) >> 1;
            return nFirstIndex;

        }

        public static int RetrieveMinValueIndex(float[] Data)
        {
            float fMinValue = Data[0];
            int nFirstIndex = 0;
            //int nLastIndex = 0;

            for (int i = 1; i < Data.Length; i++)
            {
                if (Data[i] < fMinValue)
                {
                    fMinValue = Data[i];
                    nFirstIndex = i;
                }
                /*
                if (Data[i] == fMinValue)
                {
                    nLastIndex = i;
                }*/

            }
            //return (nFirstIndex + nLastIndex) >> 1;
            return nFirstIndex;
        }

        public static int FindLevelIndex(float[] Array, int nStartIndex, int nIncrement, float fLevelToFind)
        {
            int nMaxIndex = Array.Length - 1;

            int nSide = 0;
            int nSideToFind;

            if (Array[nStartIndex] >= fLevelToFind)
            {
                nSideToFind = 1;
            }
            else
            {
                nSideToFind = -1;
            }


            int nIndex;
            for (nIndex = nStartIndex + nIncrement; nIndex >= 0 && nIndex <= nMaxIndex; nIndex += nIncrement)
            {
                if (Array[nIndex] >= fLevelToFind)
                {
                    nSide = 1;
                }
                else
                {
                    nSide = -1;
                }

                if (nSideToFind != nSide || Array[nIndex] == fLevelToFind)
                {
                    // we are on the other side ( or exactly on the level needed)
                    return nIndex;
                }
            }

            return nIndex - nIncrement;
        }

        public static void FilterArray(short[] Array, int nPass = 1)
        {
            float[] FloatArray = new float[Array.Length];
            for (int i = 0; i < Array.Length; i++)
            {
                FloatArray[i] = Array[i];
            }

            FilterArray(FloatArray, nPass);

            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = (short)Math.Round(FloatArray[i]);
            }
        }


        public static void FilterArray(float[] Array, int nPass = 1)
        {

            for (int nCurrentPass = 0; nCurrentPass < nPass; nCurrentPass++)
            {
                // first parse in direct 
                for (int i = 1; i < Array.Length; i++)
                {
                    Array[i] = 0.5f * Array[i] + 0.5f * Array[i - 1];
                }

                // now in reverse
                for (int i = Array.Length - 2; i >= 0; i--)
                {
                    Array[i] = 0.5f * Array[i] + 0.5f * Array[i + 1];
                }
            }

        }
    }
}
