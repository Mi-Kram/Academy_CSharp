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

namespace WPF_OpacityMask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            RadialGradientBrush opacityMask = new RadialGradientBrush();
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(255, 0, 0, 0), 0.0));
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.55));
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(255, 0, 0, 0), 0.65));
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.75));
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(255, 0, 0, 0), 0.80));
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(0, 0, 0, 0), 0.90));
            opacityMask.GradientStops.Add(new GradientStop(Color.FromArgb(255, 0, 0, 0), 1.0));
            
            button3.OpacityMask = opacityMask;
            //button3.Background = opacityMask;
        }
    }
}
