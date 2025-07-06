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
using NP_WCF_Auth_Client.ServiceReference1;

namespace NP_WCF_Auth_Client
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

        Service1Client client;

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            client = new Service1Client();

            try
            {
                client.ClientCredentials.UserName.UserName = "Alex";
                client.ClientCredentials.UserName.Password = "pass";
                MessageBox.Show(await client.GetDataAsync(23));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }

            
        }
    }
}
