using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SNASharp
{

    public enum OutputMode
    {
        dB,
        SWR_3,
        SWR_6,
        SWR_10
    };

    public enum FilterMode
    {
        OFF = 0,
        LEVEL_1 = 1,
        LEVEL_2 = 2,
        LEVEL_3 = 3,
        LEVEL_4 = 4,
        LEVEL_5 = 5,
        LEVEL_6 = 6,
        LEVEL_7 = 7,
        LEVEL_8 = 8,
        LEVEL_9 = 9,
        LEVEL_10= 10
    };


    public class SavePref
    {
        bool _bDisplayDisclaimer = true;
        bool _SerialPortAutodetectAtLaunch = false;
        bool _RawCapture = false;
        bool _AttCal = true;

        String _LastUsedDevice = null;
        String _LastUsedCOMPort = null;
        String _LastUsedAttLevel = null;
        OutputMode _Output = OutputMode.dB;
        FilterMode _Filter = FilterMode.OFF;
        Int64 _LowFrequencyRange = -1;
        Int64 _HighFrequencyRange = -1;
        int _SampleCount = 2000;
        Int64 _LastVFOFrequency = 50000000;

        public bool AttCal
        {
            get { return _AttCal; }
            set { _AttCal = value; }
        }


        public bool DisplayDisclaimer
        {
            get { return _bDisplayDisclaimer; }
            set { _bDisplayDisclaimer = value; }
        }

        public bool RawCapture
        {
            get { return _RawCapture; }
            set { _RawCapture = value; }
        }

        public bool SerialPortAutodetectAtLaunch
        {
            get { return _SerialPortAutodetectAtLaunch; }
            set { _SerialPortAutodetectAtLaunch = value; }
        }

        public String LastUsedDevice
        {
            get { return _LastUsedDevice; }
            set { _LastUsedDevice = value; }
        }

        public String LastUsedCOMPort
        {
            get { return _LastUsedCOMPort; }
            set { _LastUsedCOMPort = value; }
        }

        public String LastUsedAttLevel
        {
            get { return _LastUsedAttLevel; }
            set { _LastUsedAttLevel = value; }
        }

        public OutputMode Output
        {
            get { return _Output; }
            set { _Output = value; }
        }


        public Int64 LowFrequency
        {
            get { return _LowFrequencyRange; }
            set { _LowFrequencyRange = value; }
        }

        public Int64 HighFrequency
        {
            get { return _HighFrequencyRange; }
            set { _HighFrequencyRange = value; }
        }

        public int SampleCount
        {
            get { return _SampleCount; }
            set { _SampleCount = value; }

        }

        public FilterMode  Filter
        {
            get { return _Filter; }
            set { _Filter = value; }

        }


        public Int64 LastVFOFrequency 
        {
            get { return _LastVFOFrequency; }
            set { _LastVFOFrequency = value; }

        }

    }
}
