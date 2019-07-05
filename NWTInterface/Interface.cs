using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using System.ComponentModel;


namespace AnalyzerInterface
{
    public enum AttLevel { _0dB, _10dB, _20dB, _30dB, _40dB, _50dB };



    public class DeviceDef
    {
        public enum AnalyserClass { TrackingNoSpectrum, TrackingSpectrum, SpectrumNoTracking};

        public enum SamplingDataFormat
        {
            _16Bits_2Channel,
            _16Bits_1Channel
        };


        protected String _ModelName = "Generic model";
        protected String _BuilderName = "homemade";
        protected AnalyserClass _Class = AnalyserClass.TrackingNoSpectrum;
        protected Int64 _MinFrequencyInHz = 50000;
        protected Int64 _MaxFrequencyInHz = 90000000;
        protected int _nFrequencyDivisor = 1;
        protected float _VerticalResolutiondB = 0.1923f;
        protected SamplingDataFormat _SweepDataFormat= SamplingDataFormat._16Bits_2Channel;

    }

    public class NWTCompatibleDeviceDef : DeviceDef
    {

        public NWTCompatibleDeviceDef()
        {
        }

        protected new String _ModelName = "NWTCompatible";
        protected new String _BuilderName = "Homemade";
        protected int[] _FirmwareVersions = new int[] { 110,119 };

        protected bool _HaveAttenuator = false;
        protected bool _HaveLogDetector = true;
        protected bool _HaveLinDetector = false;
        protected int _AcquisitionReadTimeout = 500;

        public string ModelName
        {
            get { return _ModelName; }
            set { _ModelName = value; }
        }

        public string BuilderName
        {
            get { return _BuilderName; }
            set
            {
                _BuilderName = value;
            }
        }

        public bool Attenuator
        {
            get { return _HaveAttenuator; }
            set { _HaveAttenuator = value; }
        }


        public bool HaveLogDetector
        {
            get { return _HaveLogDetector; }
            set { _HaveLogDetector = value; }
        }

        public bool HaveLinDetector
        {
            get { return _HaveLinDetector; }
            set { _HaveLinDetector = value; }
        }


        public int[] AllowedFirmwaresVersions
        {
            get { return _FirmwareVersions; }
            set { _FirmwareVersions = value; }
        }

        public Int64 MinFrequencyInHz
        {
            get { return _MinFrequencyInHz; }
            set { _MinFrequencyInHz = value; }
        }

        public Int64 MaxFrequencyInHz
        {
            get { return _MaxFrequencyInHz; }
            set { _MaxFrequencyInHz = value; }
        }

        public int FrequencyDivisor
        {
            get { return _nFrequencyDivisor; }
            set { _nFrequencyDivisor = value; }
        }

        public float VerticalResolutiondB
        {
            get { return _VerticalResolutiondB; }
            set { _VerticalResolutiondB = value; }
        }

        public AnalyserClass Operating_mode
        {
            get { return _Class; }
            set { _Class = value; }
        }

        public SamplingDataFormat SweepDataFormat
        {
            get { return _SweepDataFormat; }
            set { _SweepDataFormat = value; }
        }

        public int AcquisitionTimeout
        {
            get { return _AcquisitionReadTimeout; }
            set { _AcquisitionReadTimeout = value; }
        }

        public override String  ToString() 
        {
            return _BuilderName + "_" + _ModelName;
        }
    }


    public class CalibrationDatas
    {
        public CalibrationDatas() 
        {
            //Reset();
        }

        public void SetReferenceArray(AttLevel Level, Int16[] Array, bool bLinear)
        {
            if (!bLinear)
            {
                switch (Level)
                {
                    case AttLevel._0dB:  ZeroDbReferenceLevel_0dB = Array; break;
                    case AttLevel._10dB:  ZeroDbReferenceLevel_10dB = Array; break; 
                    case AttLevel._20dB:  ZeroDbReferenceLevel_20dB = Array; break; 
                    case AttLevel._30dB:  ZeroDbReferenceLevel_30dB = Array; break; 
                    case AttLevel._40dB:  ZeroDbReferenceLevel_40dB = Array; break; 
                    case AttLevel._50dB:  ZeroDbReferenceLevel_50dB = Array; break; 
                    default:break;
                }
            }
            else
            {
                switch (Level)
                {
                    case AttLevel._0dB:  ZeroDbReferenceLevel_linear_0dB = Array; break; 
                    case AttLevel._10dB:  ZeroDbReferenceLevel_linear_10dB = Array; break;
                    case AttLevel._20dB:  ZeroDbReferenceLevel_linear_20dB = Array; break;
                    case AttLevel._30dB:  ZeroDbReferenceLevel_linear_30dB = Array; break;
                    case AttLevel._40dB:  ZeroDbReferenceLevel_linear_40dB = Array; break;
                    case AttLevel._50dB:  ZeroDbReferenceLevel_linear_50dB = Array; break;
                    default:break;
                }
            }
        }

        public Int16[] GetReferenceArray(AttLevel Level, bool bLinear)
        {
            if (!bLinear)
            {
                switch (Level)
                {
                    case AttLevel._0dB: return ZeroDbReferenceLevel_0dB;
                    case AttLevel._10dB: return ZeroDbReferenceLevel_10dB;
                    case AttLevel._20dB: return ZeroDbReferenceLevel_20dB;
                    case AttLevel._30dB: return ZeroDbReferenceLevel_30dB;
                    case AttLevel._40dB: return ZeroDbReferenceLevel_40dB;
                    case AttLevel._50dB: return ZeroDbReferenceLevel_50dB;
                    default:
                        return ZeroDbReferenceLevel_0dB;
                }
            }
            else
            {
                switch (Level)
                {
                    case AttLevel._0dB: return ZeroDbReferenceLevel_linear_0dB;
                    case AttLevel._10dB: return ZeroDbReferenceLevel_linear_10dB;
                    case AttLevel._20dB: return ZeroDbReferenceLevel_linear_20dB;
                    case AttLevel._30dB: return ZeroDbReferenceLevel_linear_30dB;
                    case AttLevel._40dB: return ZeroDbReferenceLevel_linear_40dB;
                    case AttLevel._50dB: return ZeroDbReferenceLevel_linear_50dB;
                    default:
                        return ZeroDbReferenceLevel_linear_0dB;
                }

            }
        }

        public double DDSFrequencyCorrection = 1.0;
        public Int64 nFirstCalibrationFrequency = 0;
        public Int64 nLastCalibrationFrequency = 0;
        public Int16[] ZeroDbReferenceLevel_0dB = null;
        public Int16[] ZeroDbReferenceLevel_10dB = null;
        public Int16[] ZeroDbReferenceLevel_20dB = null;
        public Int16[] ZeroDbReferenceLevel_30dB = null;
        public Int16[] ZeroDbReferenceLevel_40dB = null;
        public Int16[] ZeroDbReferenceLevel_50dB = null;

        public Int16[] ZeroDbReferenceLevel_linear_0dB = null;
        public Int16[] ZeroDbReferenceLevel_linear_10dB = null;
        public Int16[] ZeroDbReferenceLevel_linear_20dB = null;
        public Int16[] ZeroDbReferenceLevel_linear_30dB = null;
        public Int16[] ZeroDbReferenceLevel_linear_40dB = null;
        public Int16[] ZeroDbReferenceLevel_linear_50dB = null;


        public Int16 GetZeroDbValue(Int64 nFrequencyInHz, AttLevel Level,bool bLinear)
        {
            if (GetReferenceArray(Level, bLinear) == null)
                return 455;

            if (nFrequencyInHz < nFirstCalibrationFrequency)
                return GetReferenceArray(Level, bLinear)[0];

            Int64 nStepInHz = (nLastCalibrationFrequency - nFirstCalibrationFrequency) / GetReferenceArray(Level, bLinear).Length;

            Int64 nIndex = (nFrequencyInHz - nFirstCalibrationFrequency) / nStepInHz;
            float fIndex = (nFrequencyInHz - nFirstCalibrationFrequency) / nStepInHz;

            if (nIndex > GetReferenceArray(Level, bLinear).Length-1)
                nIndex = GetReferenceArray(Level, bLinear).Length - 1;

            /*
            int IntPart = (int)Math.Truncate(fIndex);
            float fFRactionnal = fIndex - (float)Math.Truncate(fIndex);


            if (nIndex < GetReferenceArray(Level, bLinear).Length - 1)
                return (short)(((float)GetReferenceArray(Level, bLinear)[nIndex])*(1.0f- fFRactionnal) + (float)GetReferenceArray(Level, bLinear)[nIndex+1]* fFRactionnal);
            else
            */
                return GetReferenceArray(Level, bLinear)[nIndex];


        }


        public Int16 GetAverageReferenceLevel(AttLevel Level, bool bLinear = false)
        {
            int nSum = 0;
            foreach (Int16 i in GetReferenceArray(Level, bLinear))
            {
                nSum += i;
            }

            nSum /= GetReferenceArray(Level, bLinear).Length;

            return (Int16)nSum;

        }
    }

    public class CBackNotifier
    {
        public virtual void SendProgress(int nLastIndex, float fLastValue)
        {
        }

        public virtual void LogText(String Text)
        {

        }

        public virtual void LogTextError(String Text)
        {

        }

    }


    public class NWTDevice
    {
        public enum DetectorUsed
        {
            LOGARITHMIC,
            LINEAR,
            BOTH
        };


        public class RunSweepModeParam
        {
            public DetectorUsed Detector = DetectorUsed.LOGARITHMIC;
            public Int64 nBaseFrequency = 0;
            public Int64 nFrequencyStep = 0;
            public int nCount = 0;
            public CBackNotifier Notifier = null;
            public BackgroundWorker Worker = null;
            public Int16[] NativeDatas = null;
            public float[] ResultDatas = null;

        }




        SerialPort port = new SerialPort();
        static CalibrationDatas CalibrationValues = new CalibrationDatas();
        NWTCompatibleDeviceDef DeviceDef = null;
        AttLevel CurrentLevel = AttLevel._0dB;
        public int nFirmwareVersionNumber = -1;
        String CurrentSerialPort = null;
        public void SetDevice(NWTCompatibleDeviceDef _DeviceDef)
        {
            DeviceDef = _DeviceDef;
        }

        public NWTCompatibleDeviceDef GetDevice()
        {
            return DeviceDef;
        }

        public bool IsPortOpen()
        {
            if (port != null)
                if (port.IsOpen)
                    return true;

            return false;
        }

        public Int64 MinFrequency
        {
            get
            {
                return DeviceDef.MinFrequencyInHz;
            }
        }

        public Int64 MaxFrequency
        {
            get
            {
                return DeviceDef.MaxFrequencyInHz;
            }
        }


        public NWTDevice(SerialPort _port)
        {
            port = _port;
        }

        public NWTDevice()
        {
        }


        public bool IsCalibrationFileAvailable(String Path)
        {
            if (File.Exists(System.IO.Path.Combine(Path,DeviceDef.ToString() + "_Calibration.XML")))
                return true;
            else
                return false;
        }

        // Serialization
        public void SaveCalibration(String Path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(CalibrationDatas));
            using (StreamWriter wr = new StreamWriter(System.IO.Path.Combine(Path,DeviceDef.ToString()+"_Calibration.XML")))
            {
                xs.Serialize(wr, CalibrationValues);
            }
        }

        public void LoadCalibration(String Path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(CalibrationDatas));
            using (StreamReader wr = new StreamReader(System.IO.Path.Combine(Path,DeviceDef.ToString() + "_Calibration.XML")))
            {
                CalibrationValues = xs.Deserialize(wr) as CalibrationDatas;
            }
        }


        public void RunCalibration(CBackNotifier Notifier, int nCount, bool bLinear = false)
        {

        CalibrationValues.nFirstCalibrationFrequency = DeviceDef.MinFrequencyInHz;
        CalibrationValues.nLastCalibrationFrequency = DeviceDef.MaxFrequencyInHz;

        Int16[] Out = new Int16[nCount];

        float[] Result = RunSweepMode(DeviceDef.MinFrequencyInHz,
                                          ((DeviceDef.MaxFrequencyInHz - DeviceDef.MinFrequencyInHz)) / nCount,
                                          nCount,
                                          bLinear,
                                          Notifier,
                                          null,
                                          Out);
            SNASharp.Utility.FilterArray(Out, 2);

            CalibrationValues.SetReferenceArray(CurrentLevel, Out, bLinear);
            Notifier.SendProgress(0, 0.0f);
        }

        Byte [] BuildZeroLeftString(Int64 nValue, int nNeededlength)
        {
            String sValue = nValue.ToString();

            while (sValue.Length < nNeededlength)
            {
                sValue = sValue.Insert(0, "0");
            }

            return Encoding.ASCII.GetBytes(sValue);
        }

        public float[] RunSweepModeHybrid(Int64 nBaseFrequency, Int64 nFrequencyStep, int nCount, CBackNotifier Notifier = null, BackgroundWorker Worker = null, Int16[] NativeDatas = null)
        {
            Int16[] Native = new Int16[nCount];


            float[] Log = RunSweepMode(nBaseFrequency, nFrequencyStep, nCount, false, Notifier, Worker);
            float[] Lin = RunSweepMode(nBaseFrequency, nFrequencyStep, nCount, true, Notifier, Worker, Native);
            float[] Result  = new float[nCount];
            for (int i = 0; i < nCount; i++)
            {
                if (Native[i] == 0)
                    Native[i] = 1;

                double LinVerticalResolutiondB = 20.0f * Math.Log10((double)(Native[i] + 1) / (double)Native[i]);
                double LogVerticalResolutiondB = GetDevice().VerticalResolutiondB;
                double fLerpFactor = LinVerticalResolutiondB / LogVerticalResolutiondB;
                if (fLerpFactor > 1.0)
                    fLerpFactor = 1.0;

                Result[i] = (float)(Log[i] * fLerpFactor + Lin[i] * (1.0 - fLerpFactor));
            }

            return Result;
        }

        public float []  RunSweepMode(RunSweepModeParam Param)
        {
            float[] fResult = null;
            switch (Param.Detector)
            {
                case DetectorUsed.BOTH:  fResult =  RunSweepModeHybrid(Param.nBaseFrequency, Param.nFrequencyStep, Param.nCount, Param.Notifier, Param.Worker, Param.NativeDatas);
                    break;
                case DetectorUsed.LINEAR:  fResult = RunSweepMode(Param.nBaseFrequency, Param.nFrequencyStep, Param.nCount, true, Param.Notifier, Param.Worker, Param.NativeDatas);
                    break;
                case DetectorUsed.LOGARITHMIC:  fResult =  RunSweepMode(Param.nBaseFrequency, Param.nFrequencyStep, Param.nCount, false, Param.Notifier, Param.Worker, Param.NativeDatas);
                    break;
            }
            Param.ResultDatas = fResult;
            return fResult;
        }

        public float[] RunSweepMode(Int64 nBaseFrequency, Int64 nFrequencyStep, int nCount, bool bUseInear = false, CBackNotifier Notifier = null, BackgroundWorker Worker =null, Int16[] NativeDatas = null)
        {
            int nSubBlockSize = 9999;
            int nPass = nCount / nSubBlockSize;
            int nRest = nCount % nSubBlockSize;

            float[] fullBuffer = new float[nCount];
            port.DiscardInBuffer();
            port.DiscardOutBuffer();

            port.ReadTimeout = DeviceDef.AcquisitionTimeout;


            for ( int nFullBlock = 0; nFullBlock < nPass; nFullBlock++)
            {
                int nOffset = nFullBlock * nSubBlockSize;
                Int16[] SubNativeData = new Int16[nSubBlockSize];
                float []fSubBuffer = RunSweepModeBlock(nBaseFrequency+ nOffset * nFrequencyStep, nFrequencyStep, nSubBlockSize, nCount, nOffset, bUseInear, Notifier, Worker, SubNativeData);
                fSubBuffer.CopyTo(fullBuffer, nOffset);

                if (NativeDatas != null)
                    SubNativeData.CopyTo(NativeDatas, nOffset);
            }

            if (nRest>0)
            {
                int nOffset = nPass * nSubBlockSize;
                Int16[] SubNativeData = new Int16[nRest];
                float[] fSubBuffer = RunSweepModeBlock(nBaseFrequency + nOffset * nFrequencyStep, nFrequencyStep, nRest, nCount, nOffset, bUseInear, Notifier, Worker, SubNativeData);
                fSubBuffer.CopyTo(fullBuffer, nOffset);

                if (NativeDatas != null)
                    SubNativeData.CopyTo(NativeDatas, nOffset);
            }

            if (Notifier != null) Notifier.SendProgress(0, 0.0f);

            return fullBuffer;
        }


        float[] RunSweepModeBlock(  Int64 nBaseFrequency, 
                                    Int64 nFrequencyStep, 
                                    int nCount,
                                    int nFullCaptureSize, 
                                    int nFullCaptureOffset,
                                    bool bUseLinear = false, 
                                    CBackNotifier Notifier = null,
                                     BackgroundWorker Worker = null,
                                    Int16 [] NativeDatas = null)
        {
            float[] DataOut = new float[nCount];
            byte[] StreamFromSNA = new byte[4];
            byte[] OutMessage = new byte[2 + 9 + 8 + 4];

            OutMessage[0] = 0x8f;
            if ( !bUseLinear)
                OutMessage[1] = (byte)'x';
            else
                OutMessage[1] = (byte)'w';


            byte[] cBaseFrequency = BuildZeroLeftString(nBaseFrequency/DeviceDef.FrequencyDivisor, 9);
            byte[] cFrequencyStep = BuildZeroLeftString(nFrequencyStep / DeviceDef.FrequencyDivisor, 8);
            byte[] cCount = BuildZeroLeftString(nCount, 4);

            cBaseFrequency.CopyTo(OutMessage, 2);
            cFrequencyStep.CopyTo(OutMessage, 2 + 9);
            cCount.CopyTo(OutMessage, 2 + 9 + 8);

            port.Write(OutMessage, 0, OutMessage.Length);

            // new we can read all mesurements 
            int nLowByte = 0;
            int nHighByte = 0;

            bool aTimeoutAsOccured = false;
            int nPreviousProgress = -1;
            for (int i = 0; i < nCount; i++)
            {
                try
                {
                    if (!aTimeoutAsOccured)
                    {
                        StreamFromSNA[0] = (byte)port.ReadByte();
                        StreamFromSNA[1] = (byte)port.ReadByte();

                        if (DeviceDef.SweepDataFormat == AnalyzerInterface.DeviceDef.SamplingDataFormat._16Bits_2Channel)
                        {
                            StreamFromSNA[2] = (byte)port.ReadByte();
                            StreamFromSNA[3] = (byte)port.ReadByte();
                        }
                    }
                }
                catch (TimeoutException)
                {
                    aTimeoutAsOccured = true;
                    StreamFromSNA[0] = (byte)nLowByte;
                    StreamFromSNA[1] = (byte)nHighByte;
                }

                 nLowByte = StreamFromSNA[0];
                 nHighByte = StreamFromSNA[1];

                int nMesure = (nHighByte << 8) + nLowByte;

                if (nMesure < 1)
                    nMesure = 1;

                if (NativeDatas != null)
                {
                    NativeDatas[i] = (Int16)nMesure;
                }

                if (!bUseLinear)
                {
                    // sampling from logarithmic detector
                    DataOut[i] = (((float)nMesure) - CalibrationValues.GetZeroDbValue(nBaseFrequency + nFrequencyStep * i, CurrentLevel,false)) * DeviceDef.VerticalResolutiondB;
                }
                else
                {
                    // sampling from linear detector
                    //if (nMesure == 0)
                     //   nMesure = 1;

                    float fConvertedTodB = 20.0f * (float)Math.Log10((double)nMesure / ((double)CalibrationValues.GetZeroDbValue(nBaseFrequency + nFrequencyStep * i, CurrentLevel, true)));
                    DataOut[i] = fConvertedTodB;

                }

                int nProgress = ((i + nFullCaptureOffset) * 100) / nFullCaptureSize;

                if (nProgress - nPreviousProgress > 1)
                {

                    if (Notifier != null)
                    {
                        Notifier.SendProgress(nProgress, DataOut[i]);
                    }

                    if (Worker != null)
                        Worker.ReportProgress(nProgress);

                    nPreviousProgress = nProgress;

                }

            }

            return DataOut;
        }


        public AttLevel AcquireAttenuatorValue()
        {
            byte[] OutMessage = new byte[2];
            OutMessage[0] = 0x8f;
            OutMessage[1] = (byte)'s';

            if (port.IsOpen)
                port.Write(OutMessage, 0, OutMessage.Length);
            else
                return AttLevel._0dB;

            try
            {
                nFirmwareVersionNumber = port.ReadByte();
                int nLevel = port.ReadByte();
                int nFlush = port.ReadByte();
                nFlush = port.ReadByte();

                switch (nLevel)
                {
                    case 0:  return AttLevel._0dB;
                    case 1: return AttLevel._10dB;
                    case 2: return AttLevel._20dB; 
                    case 3: return AttLevel._30dB; 
                    case 6: return AttLevel._40dB;
                    case 7: return AttLevel._50dB; 
                    default: return AttLevel._0dB;
                }

            }
            catch (TimeoutException)
            {
                // s function not supported
                return AttLevel._0dB;
            }

        }

        public bool Initialize(String SerialPortName)
        {
            byte[] OutMessage = new byte[2];
            OutMessage[0] = 0x8f;
            OutMessage[1] = (byte)'v';

            if (port.IsOpen)
                port.Close();

            port = new SerialPort(SerialPortName, 57600);
            String COMChecked = SerialPortName;


            bool bOpenedByMe;

            port.DtrEnable = true;
            port.RtsEnable = true;


            if (!port.IsOpen)
            {
                try
                {
                    port.Open();
                    bOpenedByMe = true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
            else
            {
                bOpenedByMe = false;
            }


            port.DiscardInBuffer();
            port.DiscardOutBuffer();

            port.ReadTimeout = 2000;
            port.Write(OutMessage, 0, OutMessage.Length);

            try
            {
                nFirmwareVersionNumber = port.ReadByte();
                CurrentSerialPort = SerialPortName;
                return true;
            }
            catch (TimeoutException)
            {
                // we try with the function 's'
                OutMessage[1] = (byte)'s';
                port.Write(OutMessage, 0, OutMessage.Length);

                try
                {
                    nFirmwareVersionNumber = port.ReadByte();
                    int nFlush = port.ReadByte();
                    nFlush = port.ReadByte();
                    nFlush = port.ReadByte();

                    CurrentSerialPort = SerialPortName;
                    return true;
                }
                catch (TimeoutException)
                {
                    if (bOpenedByMe)
                        port.Close();
                    
                }
            }
            return false;
        }

        public void SetFrequency(Int64 nFrequency)
        {
            if (port.IsOpen)
            {
                String sFrequency = (nFrequency/DeviceDef.FrequencyDivisor).ToString();


                while (sFrequency.Length < 9)
                {
                    sFrequency = sFrequency.Insert(0, "0");
                }

                byte[] FrequencyAsbytes = Encoding.ASCII.GetBytes(sFrequency);
                byte[] OutMessage = new byte[9 + 2];
                OutMessage[0] =  0x8f;
                OutMessage[1] = (byte)'f';
                FrequencyAsbytes.CopyTo(OutMessage, 2);
                port.Write(OutMessage, 0, OutMessage.Length);

                // needed to wait completion
                int nVersion = GetVersion();
            }
        }

        public float GetValueAtFrequency(Int64 nFrequency, bool bLinear)
        {
            SetFrequency(nFrequency);
            Int16 Input = ReadInput();

            if (!bLinear)
            {
                return (((float)Input) - CalibrationValues.GetZeroDbValue(nFrequency, CurrentLevel, false)) * DeviceDef.VerticalResolutiondB;
            }
            else
            {
                return  20.0f * (float)Math.Log10((double)Input / ((double)CalibrationValues.GetZeroDbValue(nFrequency, CurrentLevel, true)));
            }
        }


        public float GetNoiseFloor()
        {
            SetFrequency(0);
            return GetLevel(10);
        }

        public float GetLevel(int nSamples = 10)
        {
            float fAverage = 0;
            Int16 AverageLevel = CalibrationValues.GetAverageReferenceLevel(CurrentLevel);

            for (int i = 0; i < nSamples; i++)
            {
                Int16 Input = ReadInput();
                fAverage += (((float)Input) - AverageLevel) * DeviceDef.VerticalResolutiondB;
            }
            return fAverage / nSamples;
        }

        public AttLevel GetAttenuatorLevel()
        {
            return CurrentLevel;
        }

        public int GetAttenuatorLeveldB()
        {
            return ((int)GetAttenuatorLevel())*10;
        }

        public void SetAttenuatorLevel(AttLevel Level)
        {
            if (port.IsOpen)
            {
                byte[] OutMessage = new byte[3];
                OutMessage[0] = 0x8f;
                OutMessage[1] = (byte)'r';

                switch (Level)
                {
                    case AttLevel._0dB: OutMessage[2] = 0; break;
                    case AttLevel._10dB: OutMessage[2] = 1; break;
                    case AttLevel._20dB: OutMessage[2] = 2; break;
                    case AttLevel._30dB: OutMessage[2] = 3; break;
                    case AttLevel._40dB: OutMessage[2] = 6; break;
                    case AttLevel._50dB: OutMessage[2] = 7; break;
                    default: OutMessage[2] = 0; break;
                }

                port.Write(OutMessage, 0, OutMessage.Length);
                // needed to wait completion
                int nVersion = GetVersion();
                CurrentLevel = Level;
            }
        }

        public Int16 ReadInput()
        {
            byte[] GetValueCommand = new byte[] { 0x8f, (byte)'m'};
            port.Write(GetValueCommand, 0, GetValueCommand.Length);

            byte[] ValueRead = new byte[4];

            for (int nRead = 0; nRead < 4; nRead++)
            {
                    ValueRead[nRead] = (byte)port.ReadByte();
            }

            int nTest = (ValueRead[1] << 8) + (ValueRead[0]);

            return (Int16)nTest;

        }

        public int GetVersion()
        {
            byte[] OutMessage = new byte[2];
            OutMessage[0] = 0x8f;
            OutMessage[1] = (byte)'v';
            port.DiscardInBuffer();
            port.Write(OutMessage, 0, OutMessage.Length);

            int nVersion;
            try
            {
                nVersion = port.ReadByte();
            }
            catch(Exception)
            {
                nVersion = 0;
            }
            return nVersion;
        }
    }
}
