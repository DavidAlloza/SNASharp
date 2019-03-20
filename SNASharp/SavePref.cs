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

    }
}
