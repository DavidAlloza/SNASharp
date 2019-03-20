using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SNASharp
{
    public partial class DisclaimerForm : Form
    {
        public DisclaimerForm()
        {
            InitializeComponent();
            this.ControlBox = false;
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
        }

        private void CheckBoxRemoveDisclaimer_CheckedChanged(object sender, EventArgs e)
        {
            Program.Save.DisplayDisclaimer = !CheckBoxRemoveDisclaimer.Checked;
        }

        private void DisclaimerForm_Leave(object sender, EventArgs e)
        {

        }
    }
}
