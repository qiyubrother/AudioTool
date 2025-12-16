using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AudioTool
{
    public partial class MainWindow : Window
    {
        private readonly AppConfig _config;

        public MainWindow()
        {
            InitializeComponent();
            _config = AppConfig.Load();
            LoadConfig();
        }

        private void LoadConfig()
        {
            // 加载窗口位置和大小
            if (_config.WindowLeft.HasValue && _config.WindowTop.HasValue)
            {
                Left = _config.WindowLeft.Value;
                Top = _config.WindowTop.Value;
            }

            // 加载透明度
            Opacity = _config.Opacity;

            // 加载总是在前设置
            Topmost = _config.TopMost;

            // 加载颜色设置
            if (_config.MicrophoneColorString != null)
            {
                var micColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(_config.MicrophoneColorString);
                MicrophoneControl.UsageColor = micColor;
            }
            if (_config.SpeakerColorString != null)
            {
                var spkColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(_config.SpeakerColorString);
                SpeakerControl.UsageColor = spkColor;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MicrophoneControl.Start();
            SpeakerControl.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 保存配置
            _config.WindowLeft = Left;
            _config.WindowTop = Top;
            _config.Opacity = Opacity;
            _config.TopMost = Topmost;
            _config.MicrophoneColorString = MicrophoneControl.UsageColor.ToString();
            _config.SpeakerColorString = SpeakerControl.UsageColor.ToString();
            _config.Save();

            MicrophoneControl.Stop();
            SpeakerControl.Stop();
        }

        private void MainContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            UpdateOpacityMenuItems();
        }

        private void UpdateOpacityMenuItems()
        {
            // 清除所有选中状态
            Opacity10.IsChecked = false;
            Opacity20.IsChecked = false;
            Opacity30.IsChecked = false;
            Opacity40.IsChecked = false;
            Opacity50.IsChecked = false;
            Opacity60.IsChecked = false;
            Opacity70.IsChecked = false;
            Opacity80.IsChecked = false;
            Opacity90.IsChecked = false;
            Opacity100.IsChecked = false;

            // 根据当前不透明度值设置选中状态
            double currentOpacity = Opacity;
            if (Math.Abs(currentOpacity - 0.1) < 0.01) Opacity10.IsChecked = true;
            else if (Math.Abs(currentOpacity - 0.2) < 0.01) Opacity20.IsChecked = true;
            else if (Math.Abs(currentOpacity - 0.3) < 0.01) Opacity30.IsChecked = true;
            else if (Math.Abs(currentOpacity - 0.4) < 0.01) Opacity40.IsChecked = true;
            else if (Math.Abs(currentOpacity - 0.5) < 0.01) Opacity50.IsChecked = true;
            else if (Math.Abs(currentOpacity - 0.6) < 0.01) Opacity60.IsChecked = true;
            else if (Math.Abs(currentOpacity - 0.7) < 0.01) Opacity70.IsChecked = true;
            else if (Math.Abs(currentOpacity - 0.8) < 0.01) Opacity80.IsChecked = true;
            else if (Math.Abs(currentOpacity - 0.9) < 0.01) Opacity90.IsChecked = true;
            else if (Math.Abs(currentOpacity - 1.0) < 0.01) Opacity100.IsChecked = true;
        }

        private void OpacityMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string tagValue)
            {
                if (double.TryParse(tagValue, out double opacity))
                {
                    Opacity = opacity;
                    _config.Opacity = opacity;
                    _config.Save();
                    UpdateOpacityMenuItems();
                }
            }
        }

        private void BtnSound_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("mmsys.cpl");
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MicColorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ColorPickerDialog(MicrophoneControl.UsageColor)
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                MicrophoneControl.UsageColor = dialog.SelectedColor;
            }
        }

        private void SpeakerColorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ColorPickerDialog(SpeakerControl.UsageColor)
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                SpeakerControl.UsageColor = dialog.SelectedColor;
            }
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}

