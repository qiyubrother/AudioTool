using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace AudioTool
{
    public partial class ColorPickerDialog : Window
    {
        public Color SelectedColor { get; private set; }

        private readonly List<Color> _predefinedColors = new List<Color>
        {
            Colors.Red, Colors.Orange, Colors.Yellow, Colors.Green,
            Colors.Cyan, Colors.Blue, Colors.Purple, Colors.Pink,
            Colors.DarkRed, Colors.DarkOrange, Colors.DarkGreen, Colors.DarkBlue,
            Colors.IndianRed, Colors.Coral, Colors.LimeGreen, Colors.SkyBlue,
            Colors.Crimson, Colors.Tomato, Colors.MediumSeaGreen, Colors.DodgerBlue,
            Colors.Firebrick, Colors.OrangeRed, Colors.ForestGreen, Colors.RoyalBlue,
            Colors.Maroon, Colors.Chocolate, Colors.SeaGreen, Colors.Navy,
            Colors.Salmon, Colors.Gold, Colors.LightGreen, Colors.LightBlue,
            Colors.HotPink, Colors.Khaki, Colors.SpringGreen, Colors.DeepSkyBlue,
            Colors.Magenta, Colors.YellowGreen, Colors.Turquoise, Colors.SteelBlue,
            Colors.Violet, Colors.LawnGreen, Colors.Aqua, Colors.MediumBlue,
            Colors.Orchid, Colors.GreenYellow, Colors.PaleTurquoise, Colors.MidnightBlue,
            Colors.Plum, Colors.Chartreuse, Colors.CadetBlue, Colors.Indigo,
            Colors.Thistle, Colors.Lime, Colors.DarkCyan, Colors.DarkSlateBlue
        };

        public ColorPickerDialog(Color currentColor)
        {
            InitializeComponent();
            SelectedColor = currentColor;
            InitializeColorPalette();
            UpdatePreview();
        }

        private void InitializeColorPalette()
        {
            foreach (var color in _predefinedColors)
            {
                var rectangle = new Rectangle
                {
                    Width = 30,
                    Height = 30,
                    Fill = new SolidColorBrush(color),
                    Stroke = Brushes.White,
                    StrokeThickness = 1,
                    Margin = new Thickness(2),
                    Cursor = System.Windows.Input.Cursors.Hand
                };
                rectangle.MouseLeftButtonDown += (s, e) =>
                {
                    SelectedColor = color;
                    UpdatePreview();
                };
                ColorPalette.Items.Add(rectangle);
            }
        }

        private void UpdatePreview()
        {
            SelectedColorPreview.Fill = new SolidColorBrush(SelectedColor);
            ColorText.Text = $"RGB({SelectedColor.R}, {SelectedColor.G}, {SelectedColor.B})";
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}

