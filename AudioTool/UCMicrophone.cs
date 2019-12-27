using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace AudioTool
{
    public partial class UCMicrophone : UserControl
    {
        MMDeviceEnumerator enumerator = null;
        public UCMicrophone()
        {
            InitializeComponent();

            UsageColor = Color.DarkRed;
            enumerator = new MMDeviceEnumerator();
        }

        float microphoneValue = 0;
        private void CreateImage()
        {
            Bitmap image = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(image);
            g.Clear(BackColor);
            SolidBrush usageBrush = new SolidBrush(UsageColor);
            g.FillRectangle(usageBrush, 0, Height * (1 - microphoneValue * 1.0f / 100), Width, Height * (microphoneValue * 1.0f / 100));
            SizeF _sz = new Size(0, 0);
            string _s = microphoneValue.ToString("0.00");
            if (_s == "100.00")
            {
                _s = "100";
            }
            _sz = g.MeasureString(_s, Font);
            g.DrawString(_s, Font, new SolidBrush(ForeColor), (Width - _sz.Width) / 2, (Height - _sz.Height) / 2);
            BackgroundImage = image;
        }

        public Color UsageColor { get; set; }
        private void timer1_Tick(object sender, EventArgs e)
        {
            var CaptureDevices = enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
            using (var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console))
            {
                var selectedDevice = CaptureDevices.FirstOrDefault(c => c.ID == defaultDevice.ID);
                microphoneValue = selectedDevice.AudioMeterInformation.MasterPeakValue * 100;
                CreateImage();
                Application.DoEvents();
            }
        }

        public void Start() => timer1.Start();

        public void Stop() => timer1.Stop();
    }
}
