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

    public class SavePref
    {
        bool _bDisplayDisclaimer = true;
        bool _SerialPortAutodetectAtLaunch = false;
        String _LastUsedDevice = null;
        String _LastUsedCOMPort = null;
        String _LastUsedAttLevel = null;
        OutputMode _Output = OutputMode.dB;
        Int64 _LowFrequencyRange = -1;
        Int64 _HighFrequencyRange = -1;
        int _SampleCount = 2000;


        public bool DisplayDisclaimer
        {
            get { return _bDisplayDisclaimer; }
            set { _bDisplayDisclaimer = value; }
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

    }
}
