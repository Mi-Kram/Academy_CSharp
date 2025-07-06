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
using WCF_WPF_Client_First.ServiceReference1;

namespace WCF_WPF_Client_First
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Service1Client client = new Service1Client();

            /*
            var result = client.GetData(Int32.Parse(textBox1.Text));
            textBox1.Text = result.ToString();
            */

            var result = await client.GetDataAsync(Int32.Parse(textBox1.Text));
            textBox1.Text = result.ToString();
        }
    }
}
