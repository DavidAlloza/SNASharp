using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SNASharp
{
    public partial class Form1
    {
        void DeviceManagerInit()
        {
            //DevicesComboBox.DataSource = DeviceArray;
            // DeviceProperyGrid.SelectedObject = MainDevice;
            // check if device directory exist
            LoadAvailablesDeviceDef();
        }

        private void LoadAvailablesDeviceDef()
        {
            DirectoryInfo d = new DirectoryInfo(Program.DeviceDefPath);
            FileInfo[] Files = d.GetFiles("*.def"); //Getting Text files

            // unserialize device definition
            for (int i = 0; i < Files.Length; i++)
            {
                XmlSerializer xs = new XmlSerializer(typeof(NWTInterface.NWTCompatibleDeviceDef));
                using (StreamReader wr = new StreamReader(System.IO.Path.Combine(Program.DeviceDefPath,Files[i].ToString())))
                {
                    DeviceArray.Add(xs.Deserialize(wr) as NWTInterface.NWTCompatibleDeviceDef);
                }
            }

        }

        private void SaveDeviceButton_Click(object sender, EventArgs e)
        {
            for (int nDevice = 0; nDevice < DeviceArray.Count; nDevice++)
            {
                NWTInterface.NWTCompatibleDeviceDef DeviceDef = (NWTInterface.NWTCompatibleDeviceDef)DeviceArray[nDevice];
                XmlSerializer xs = new XmlSerializer(typeof(NWTInterface.NWTCompatibleDeviceDef));
                using (StreamWriter wr = new StreamWriter(System.IO.Path.Combine(Program.DeviceDefPath,DeviceDef.ToString() + ".def")))
                {
                    xs.Serialize(wr, DeviceDef);
                }
            }
        }


        private void NewDevicebutton_Click(object sender, EventArgs e)
        {
            NWTInterface.DeviceDef NewDevice = new NWTInterface.NWTCompatibleDeviceDef();
            DeviceArray.Add(NewDevice);
            DeviceListMenuRefresh();
            DevicesComboBox.SelectedItem = NewDevice;
            DeviceProperyGrid.SelectedObject = NewDevice;
        }


        private void DevicesComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (!bMuteDeviceComboBoxEvent)
                DeviceProperyGrid.SelectedObject = DevicesComboBox.SelectedItem;
        }

        private void DevicesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void DeviceListMenuRefresh(bool _bMuteDeviceComboBoxEvent = true)
        {
            bMuteDeviceComboBoxEvent = _bMuteDeviceComboBoxEvent;

            DevicesComboBox.DataSource = null;
            DevicesComboBox.DataSource = DeviceArray;
            SelectecDeviceComboBox.DataSource = null;
            SelectecDeviceComboBox.DataSource = DeviceArray;
            bMuteDeviceComboBoxEvent = false;

            //deviceModelToolStripMenuItem.Text = DeviceArray[0].ToString();
        }

        NWTInterface.NWTCompatibleDeviceDef GetDevice(int nDeviceIndex)
        {
            return (NWTInterface.NWTCompatibleDeviceDef)DeviceArray[nDeviceIndex];
        }

        int GetDeviceIndex(String sDeviceName)
        {
            for ( int i = 0; i < DeviceArray.Count; i++ )
            {
                String Name = DeviceArray[i].ToString();
                if (Name == sDeviceName)
                    return i;
            }

            return -1;
        }


        //public DeviceDef MainDevice = new NWTCompatibleDeviceDef();
        ArrayList DeviceArray = new ArrayList();

    }
}
