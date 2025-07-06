using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_User_Control
{
    public partial class ColorChooser : UserControl
    {
        // объявление свойств зависимости
        public static DependencyProperty ColorProperty;

        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;

        public static DependencyProperty BrightnessProperty;
        public static DependencyProperty HueProperty;
        public static DependencyProperty SaturationProperty;

        // изменяемое пользователем свойство
        private DependencyProperty ActiveProperty;

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }
        public byte Brightness
        {
            get { return (byte)GetValue(BrightnessProperty); }
            set { SetValue(BrightnessProperty, value); }
        }
        public ushort Hue
        {
            get { return (ushort)GetValue(HueProperty); }
            set { SetValue(HueProperty, value); }
        }
        public byte Saturation
        {
            get { return (byte)GetValue(SaturationProperty); }
            set { SetValue(SaturationProperty, value); }
        }

        static ColorChooser()
        {
            //Console.WriteLine("static ColorChooser()");

            ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(ColorChooser),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));

            RedProperty = DependencyProperty.Register("Red", typeof(byte), typeof(ColorChooser),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            GreenProperty = DependencyProperty.Register("Green", typeof(byte), typeof(ColorChooser),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            BlueProperty = DependencyProperty.Register("Blue", typeof(byte), typeof(ColorChooser),
                 new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            BrightnessProperty = DependencyProperty.Register("Brightness", typeof(byte), typeof(ColorChooser),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorHSBChanged)));
            HueProperty = DependencyProperty.Register("Hue", typeof(ushort), typeof(ColorChooser),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorHSBChanged)));
            SaturationProperty = DependencyProperty.Register("Saturation", typeof(byte), typeof(ColorChooser),
                 new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorHSBChanged)));
        }

        public ColorChooser()
        {
            InitializeComponent();
        }

        // запускается в ответ на изменение свойства Color
        private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //Console.WriteLine("OnColorChanged");

            Color newColor = (Color) e.NewValue;
            ColorChooser colorpicker = (ColorChooser)sender;

            if(colorpicker.ActiveProperty == null)
            {
                // установить изменяемое свойство
                colorpicker.ActiveProperty = ColorProperty;

                colorpicker.SetValue(RedProperty, newColor.R);
                colorpicker.SetValue(GreenProperty, newColor.G);
                colorpicker.SetValue(BlueProperty, newColor.B);

                double h, s, b;
                RGBtoHSB(newColor, out h, out s, out b);

                colorpicker.SetValue(HueProperty, (ushort)h);
                colorpicker.SetValue(SaturationProperty, (byte)(100 * s));
                colorpicker.SetValue(BrightnessProperty, (byte)(100 * b));

                colorpicker.ActiveProperty = null;
            }
        }

        // запускается в ответ на изменение свойств H, S, Br
        private static void OnColorHSBChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorChooser colorPicker = (ColorChooser)sender;

            // проверка на наличие рекурсивного вызова
            if(colorPicker.ActiveProperty == null)
            {
                Console.WriteLine("OnColorHSBChanged");
                Color color = colorPicker.Color;

                float h = colorPicker.Hue;
                float s = colorPicker.Saturation / 100f;
                float b = colorPicker.Brightness / 100f;

                if (e.Property == HueProperty)
                {
                    colorPicker.ActiveProperty = HueProperty;
                    color = ColorFromHSV((ushort)e.NewValue, s, b);
                }
                    
                if (e.Property == SaturationProperty)
                {
                    colorPicker.ActiveProperty = SaturationProperty;
                    color = ColorFromHSV(h, (byte)e.NewValue / 100f, b);
                }
                    
                if (e.Property == BrightnessProperty)
                {
                    colorPicker.ActiveProperty = BrightnessProperty;
                    color = ColorFromHSV(h, s, (byte)e.NewValue / 100f);
                }

                colorPicker.SetValue(RedProperty, color.R);
                colorPicker.SetValue(GreenProperty, color.G);
                colorPicker.SetValue(BlueProperty, color.B);
                colorPicker.SetValue(ColorProperty, color);

                colorPicker.ActiveProperty = null;
            }
        }

        // запускается в ответ на изменение свойств R, G, B
        private static void OnColorRGBChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorChooser colorPicker = (ColorChooser)sender;

            // проверка на наличие рекурсивного вызова
            if (colorPicker.ActiveProperty == null)
            {
                //Console.WriteLine("OnColorRGBChanged");

                Color color = colorPicker.Color;

                if (e.Property == RedProperty)
                {
                    colorPicker.ActiveProperty = RedProperty;
                    color.R = (byte)e.NewValue;
                }
                    
                if (e.Property == GreenProperty)
                {
                    colorPicker.ActiveProperty = GreenProperty;
                    color.G = (byte)e.NewValue;
                }
                    
                if (e.Property == BlueProperty)
                {
                    colorPicker.ActiveProperty = BlueProperty;
                    color.B = (byte)e.NewValue;
                }

                double h = 0, s = 0, b = 0;
                RGBtoHSB(color, out h, out s, out b);

                colorPicker.SetValue(HueProperty, (ushort)h);
                colorPicker.SetValue(SaturationProperty, (byte)(100 * s));
                colorPicker.SetValue(BrightnessProperty, (byte)(100 * b));
                colorPicker.SetValue(ColorProperty, color);

                colorPicker.ActiveProperty = null;
            }
        }

        // получение модели цвета RGB из HSB
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            byte v = Convert.ToByte(value);
            byte p = Convert.ToByte(value * (1 - saturation));
            byte q = Convert.ToByte(value * (1 - f * saturation));
            byte t = Convert.ToByte(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        // получение модели цвета HSB из RGB
        public static void RGBtoHSB(Color color, out double h, out double s, out double v)
        {
            double min, max, delta;
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            min = r < g ? r : g;
            min = min < b ? min : b;

            max = r > g ? r : g;
            max = max > b ? max : b;
            v = max;
            delta = max - min;

            if (delta < 0.00001)
            {
                s = 0;
                h = 0;
                return;
            }
            if (max > 0.0)
            {
                s = (delta / max);
            }
            else
            {
                s = 0.0;
                h = double.NaN;
                return;
            }
            if (r >= max)
                h = (g - b) / delta;
            else
                if (g >= max)
                h = 2.0 + (b - r) / delta;
            else
                h = 4.0 + (r - g) / delta;

            h *= 60.0;

            if (h < 0.0)
                h += 360.0;

            return;
        }
    }
}
