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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void colorChooser_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            Console.WriteLine(e.NewValue.ToString());
            //txb.Text = e.NewValue.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            colorChooser.Red = 120;
        }

        Random rand = new Random();

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double[] arr = new double[rand.Next(10)+2];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rand.Next(1000);

            barDiagram.Values.Clear();
            barDiagram.Values.AddRange(arr);
            barDiagram.RefreshData();
        }

        private void barDiagram_OnBarMouseDown(object sender, int BarIndex)
        {
            MessageBox.Show($"You just clicked on bar with index: {BarIndex}. Value = {barDiagram.Values[BarIndex]}");
        }
    }
}
