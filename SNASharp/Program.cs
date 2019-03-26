using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NWTInterface;
using System.IO;
using System.Xml.Serialization;

namespace SNASharp
{
    public static class Program
    {
        public static string Version = "F4HTQ SNASharp v2019_03_26_0";
        public static string DeviceDefPath = null;
        public static string SavePath = null;
        public static string SaveFullPath = null;
        public static string CalibrationPath = null;
        public static string CurvesPath = null;



        public static SavePref Save;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                DirectoryInfo di;
                string appPath = Application.StartupPath;
                DeviceDefPath = System.IO.Path.Combine(appPath, "DeviceDef");
                SavePath = System.IO.Path.Combine(appPath, "Save");
                CalibrationPath = System.IO.Path.Combine(appPath, "Calibration");
                CurvesPath = System.IO.Path.Combine(SavePath, "Curves");
                SaveFullPath = System.IO.Path.Combine(SavePath, "SavePref.XML");

                if (!Directory.Exists(DeviceDefPath))
                    di = Directory.CreateDirectory(DeviceDefPath);

                if (!Directory.Exists(SavePath))
                    di = Directory.CreateDirectory(SavePath);

                if (!Directory.Exists(CalibrationPath))
                    di = Directory.CreateDirectory(CalibrationPath);

                if (!Directory.Exists(CurvesPath))
                    di = Directory.CreateDirectory(CurvesPath);

                if (File.Exists(SaveFullPath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(SavePref));
                    using (StreamReader wr = new StreamReader(SaveFullPath))
                    {
                        Save = xs.Deserialize(wr) as SavePref;
                    }
                }
                else
                {
                    Program.Save = new SavePref();
                }

                //int testExcep = 1;
                //testExcep /= 0;


                if (Program.Save.DisplayDisclaimer)
                {

                    DisclaimerForm Disclaimer = new DisclaimerForm();
                    DialogResult Result = Disclaimer.ShowDialog();
                    if (Result == DialogResult.OK)
                    {
                        Application.Run(new Form1());
                    }
                }
                else
                {
                    Application.Run(new Form1());
                }
            }
           // catch(Exception e)
            {
                //Console.WriteLine(Environment.StackTrace);
                //Console.WriteLine(e.StackTrace);
            }
            
        }
    }
}
