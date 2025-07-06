using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using NP_WPF_Client_FaultContract.ServiceReference1;

namespace NP_WPF_Client_FaultContract
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client = new Service1Client();

            try
            {
                MessageBox.Show(client.GetData(23));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int n = await client?.GetTimerNumberAsync();

                MessageBox.Show($"Number from server's timer: {n}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                client.Divide(0, 10);
            }
            catch (FaultException<MyError> ex)
            {
                MessageBox.Show(ex.Detail.Msg);
            }
            /*catch (FaultException ex)
            {
                MessageBox.Show(ex.Reason.ToString());
            }*/
            catch (FaultException<string> ex)
            {
                MessageBox.Show(ex.Detail);
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Communication exception");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(client.GetData(23));
            //MessageBox.Show(client.InnerChannel.SessionId);
        }
    }
}
