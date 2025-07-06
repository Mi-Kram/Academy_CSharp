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

namespace WPF_WrapPanel
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
            WrapPanel myWrapPanel = new WrapPanel();
            myWrapPanel.Background = Brushes.BlueViolet;
            myWrapPanel.Orientation = Orientation.Vertical;

            myWrapPanel.Width = double.NaN;
            myWrapPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
            myWrapPanel.VerticalAlignment = VerticalAlignment.Stretch;

            Button btn1 = new Button();
            btn1.Content = "Button 1";
            Button btn2 = new Button();
            btn2.Content = "Button 2";
            Button btn3 = new Button();
            btn3.Content = "Button 3";
            Button btn4 = new Button();
            btn4.Content = "Button 4";

            myWrapPanel.Children.Add(btn1);
            myWrapPanel.Children.Add(btn2);
            myWrapPanel.Children.Add(btn3);
            myWrapPanel.Children.Add(btn4);

            //this.Content = myWrapPanel;

            button7.Content = myWrapPanel;
        }
    }
}
