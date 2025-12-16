using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using NAudio.CoreAudioApi;

namespace AudioTool
{
    public partial class SpeakerControl : System.Windows.Controls.UserControl
    {
        private MMDeviceEnumerator _enumerator;
        private DispatcherTimer _timer;
        private float _speakerValue = 0;
        private LinearGradientBrush _gradientBrush;

        public static readonly DependencyProperty UsageColorProperty =
            DependencyProperty.Register(nameof(UsageColor), typeof(Color), typeof(SpeakerControl),
                new PropertyMetadata(Colors.Red, OnUsageColorChanged));

        public Color UsageColor
        {
            get => (Color)GetValue(UsageColorProperty);
            set => SetValue(UsageColorProperty, value);
        }

        public LinearGradientBrush UsageColorBrush => _gradientBrush;

        public SpeakerControl()
        {
            InitializeComponent();
            UsageColor = Colors.Blue; // 改为蓝色
            _enumerator = new MMDeviceEnumerator();
            InitializeTimer();
            CreateGradientBrush();
        }

        private static void OnUsageColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SpeakerControl control)
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
                // 尝试多种方式获取音频数据，优先使用Console角色，因为它通常更准确
                MMDevice device = null;
                
                // 首先尝试Console角色
                try
                {
                    device = _enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
                }
                catch
                {
                    // 如果失败，尝试Multimedia角色
                    try
                    {
                        device = _enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                    }
                    catch
                    {
                        // 最后尝试Communications角色
                        try
                        {
                            device = _enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Communications);
                        }
                        catch { }
                    }
                }

                if (device != null)
                {
                    try
                    {
                        var meterInfo = device.AudioMeterInformation;
                        
                        // 尝试获取MasterPeakValue，如果不可用则使用PeakValues的最大值
                        float peakValue = 0;
                        try
                        {
                            peakValue = meterInfo.MasterPeakValue;
                        }
                        catch
                        {
                            // 如果MasterPeakValue不可用，使用PeakValues数组的最大值
                            if (meterInfo.PeakValues != null && meterInfo.PeakValues.Count > 0)
                            {
                                // 遍历所有通道找到最大值
                                for (int i = 0; i < meterInfo.PeakValues.Count; i++)
                                {
                                    var channelValue = meterInfo.PeakValues[i];
                                    if (channelValue > peakValue)
                                    {
                                        peakValue = channelValue;
                                    }
                                }
                            }
                        }
                        
                        _speakerValue = peakValue * 100;
                        UpdateDisplay();
                    }
                    finally
                    {
                        device.Dispose();
                    }
                }
            }
            catch { }
        }

        private void UpdateDisplay()
        {
            var percentage = Math.Max(0, Math.Min(1, _speakerValue / 100.0));
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

            var displayValue = _speakerValue.ToString("0.00");
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

