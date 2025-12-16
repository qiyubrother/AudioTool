using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using NAudio.CoreAudioApi;

namespace AudioTool
{
    public partial class MicrophoneControl : System.Windows.Controls.UserControl
    {
        private MMDeviceEnumerator _enumerator;
        private DispatcherTimer _timer;
        private float _microphoneValue = 0;
        private LinearGradientBrush _gradientBrush;

        public static readonly DependencyProperty UsageColorProperty =
            DependencyProperty.Register(nameof(UsageColor), typeof(Color), typeof(MicrophoneControl),
                new PropertyMetadata(Colors.Red, OnUsageColorChanged));

        public Color UsageColor
        {
            get => (Color)GetValue(UsageColorProperty);
            set => SetValue(UsageColorProperty, value);
        }

        public LinearGradientBrush UsageColorBrush => _gradientBrush;

        public MicrophoneControl()
        {
            InitializeComponent();
            UsageColor = Colors.Red;
            _enumerator = new MMDeviceEnumerator();
            InitializeTimer();
            CreateGradientBrush();
        }

        private static void OnUsageColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MicrophoneControl control)
            {
                control.CreateGradientBrush();
            }
        }

        private void CreateGradientBrush()
        {
            var color = UsageColor;
            _gradientBrush = new LinearGradientBrush
            {
                StartPoint = new System.Windows.Point(0, 1),
                EndPoint = new System.Windows.Point(0, 0),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Color.FromArgb(255, color.R, color.G, color.B), 0.0),
                    new GradientStop(Color.FromArgb(230, color.R, color.G, color.B), 0.2),
                    new GradientStop(Color.FromArgb(180, (byte)Math.Min(255, color.R + 30), color.G, color.B), 0.5),
                    new GradientStop(Color.FromArgb(120, (byte)Math.Min(255, color.R + 60), (byte)Math.Min(255, color.G + 30), color.B), 0.8),
                    new GradientStop(Color.FromArgb(80, (byte)Math.Min(255, color.R + 80), (byte)Math.Min(255, color.G + 50), color.B), 1.0)
                }
            };
            VolumeBar.Fill = _gradientBrush;
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                var captureDevices = _enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToArray();
                using (var defaultDevice = _enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Console))
                {
                    var selectedDevice = captureDevices.FirstOrDefault(c => c.ID == defaultDevice.ID);
                    if (selectedDevice != null)
                    {
                        _microphoneValue = selectedDevice.AudioMeterInformation.MasterPeakValue * 100;
                        UpdateDisplay();
                    }
                }
            }
            catch { }
        }

        private void UpdateDisplay()
        {
            var percentage = Math.Max(0, Math.Min(1, _microphoneValue / 100.0));
            var targetHeight = ActualHeight * percentage;
            
            // 检查Height是否为NaN或无效值
            var currentHeight = double.IsNaN(VolumeBar.Height) || VolumeBar.Height < 0 ? 0 : VolumeBar.Height;
            
            // 使用动画平滑过渡高度
            var animation = new DoubleAnimation(
                currentHeight,
                targetHeight,
                new Duration(TimeSpan.FromMilliseconds(150)))
            {
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            VolumeBar.BeginAnimation(System.Windows.Shapes.Rectangle.HeightProperty, animation);

            var displayValue = _microphoneValue.ToString("0.00");
            if (displayValue == "100.00")
            {
                displayValue = "100";
            }
            ValueText.Text = displayValue;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            UpdateDisplay();
        }

        public void Start()
        {
            _timer?.Start();
        }

        public void Stop()
        {
            _timer?.Stop();
        }
    }
}

