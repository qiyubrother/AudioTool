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
    public partial class UCSpeaker : UserControl
    {
        MMDeviceEnumerator enumerator = null;
        public UCSpeaker()
        {
            InitializeComponent();

            UsageColor = Color.DarkRed;
            enumerator = new MMDeviceEnumerator();
        }

        float speakerValue = 0;
        private void CreateImage()
        {
            Bitmap image = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(image);
            g.Clear(BackColor);
            SolidBrush usageBrush = new SolidBrush(UsageColor);
            g.FillRectangle(usageBrush, 0, Height * (1 - speakerValue * 1.0f / 100), Width, Height * (speakerValue * 1.0f / 100));
            SizeF _sz = new Size(0, 0);
            string _s = speakerValue.ToString("0.00");
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
            try
            {
                using (var defaultDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia))
                {
                    if (defaultDevice != null)
                    {
                        speakerValue = (int)(defaultDevice.AudioMeterInformation.MasterPeakValue * 100);
                        defaultDevice.Dispose();
                        CreateImage();
                        Application.DoEvents();
                    }
                }
            }
            catch { }
        }


        public void Start() => timer1.Start();

        public void Stop() => timer1.Stop();
    }
}
