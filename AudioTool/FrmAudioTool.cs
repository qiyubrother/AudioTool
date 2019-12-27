using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioTool
{
    public partial class FrmAudioTool : Form
    {
        public FrmAudioTool()
        {
            InitializeComponent();
        }

        private void FrmAudioTool_Load(object sender, EventArgs e)
        {
            ucMicrophone1.Start();
            ucSpeaker1.Start();
        }

        private void btnSound_Click(object sender, EventArgs e)
        {
            Process.Start("mmsys.cpl");
        }
    }
}
