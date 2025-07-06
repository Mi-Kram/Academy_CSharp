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

namespace WPF_Controls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Expander expander;

        public MainWindow()
        {
            InitializeComponent();

            // задать случайное расположение тиков
            Random rand = new Random();
            DoubleCollection doubleCollection = new DoubleCollection();
            for (int i = 0; i < 10; i++)
            {
                doubleCollection.Add(i * 5 + i * rand.Next(10));
            }

            slider1.Ticks = doubleCollection;
        }

        private void slider1_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            progressBar1.Value = slider1.Value;
        }

        private void scrollBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            progressBar1.Value = scrollBar1.Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(new CheckBox { Content = "Mercedes" });
            stackPanel.Children.Add(new CheckBox { Content = "BMW" });
            stackPanel.Children.Add(new CheckBox { Content = "Opel" });
            stackPanel.Children.Add(new CheckBox { Content = "Nissan" });
            stackPanel.Children.Add(new CheckBox { Content = "Aston Martin" });
            stackPanel.Height = Double.NaN;

            expander = new Expander();
            expander.Header = "Choose your favorite car";
            expander.Content = stackPanel;
            expander.Height = 400;
            expander.Width = 200;
            expander.Margin = new Thickness(0, 200, 100, 50);
            Grid.SetRow(expander, 0);
            Grid.SetColumn(expander, 0);

            expander.Expanded += Expander_Expanded;
            expander.Collapsed += Expander_Collapsed;

            grid1.Children.Add(expander);
        }

        private void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Collapsed!");
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Expanded!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(expander!=null)
                expander.IsExpanded = !expander.IsExpanded;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            progressBar1.Value++;
        }
    }
}
