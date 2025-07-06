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

namespace WPF_Resources
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //this.Background = FindResource("Brush1") as Brush;

            Brush br = TryFindResource("Brush1") as Brush;
            if (br != null) 
                this.Background = br;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //this.Resources.Clear();
            SolidColorBrush br = new SolidColorBrush(Colors.Red);
            this.Resources.Remove("Brush1");
            this.Resources.Add("Brush1", br);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.Background = (Brush)this.Resources["Brush1"];
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            //button1.Style = FindResource("BStyle1") as Style;
            this.Style = FindResource("BStyle1") as Style;
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Style st = new Style(typeof(Window));
            Setter setter1 = new Setter(Window.BackgroundProperty, Brushes.Brown);
            Setter setter2 = new Setter(Window.CursorProperty, Cursors.Cross);
            st.Setters.Add(setter1);
            st.Setters.Add(setter2);
            this.Style = st;
        }

        private void buttonMouseEnter(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Background = Brushes.Black;
            ((Button)sender).Foreground = Brushes.Red;
        }

        private void buttonMouseLeave(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Background = Brushes.Beige;
            ((Button)sender).Foreground = Brushes.Green;
        }

        private void imageMouseDown(object sender, RoutedEventArgs e)
        {
            ((Image)sender).Opacity = 0.2;
        }

        private void imageMouseUp(object sender, RoutedEventArgs e)
        {
            ((Image)sender).Opacity = 1f;
        }
    }
}
