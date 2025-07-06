using NP_WCF_Stream_Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace NP_WCF_Stream_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // https://docs.microsoft.com/en-us/dotnet/api/system.servicemodel.nettcpbinding.readerquotas?view=dotnet-plat-ext-5.0
            // https://docs.microsoft.com/ru-ru/dotnet/api/system.servicemodel.basichttpbinding.maxbuffersize?view=netframework-4.0
        }

        delegate void MessageDel(string message);

        void SetText(string message)
        {
            if (!Dispatcher.CheckAccess())  // запущена ли эта функция не в основном потоке?
            {
                MessageDel messageEvent = new MessageDel(SetText);
                Dispatcher.Invoke(messageEvent, new object[] { message });
                //Dispatcher.BeginInvoke(messageEvent, new object[] { message });
            }
            else
            {
                textBox1.Text = message;
            }
        }

        Service1Client client;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client = new Service1Client();
            MessageBox.Show(client.GetData(23));
        }

        private async void GetFileAsync()
        {
            int sum = 0;
            FileStream destFile = File.Create(@"c:\temp\video2.mp4");

            Stream st = await client.GetFileAsync();

            byte[] buffer = new byte[1024*1024];

            while (true)
            {
                int res = await st.ReadAsync(buffer, 0, buffer.Length);
                if (res == 0) break;
                sum += res;
                SetText($"Recieved: {sum / 1024 / 1024}M");
                destFile.Write(buffer, 0, res);
            }

            destFile.Close();
            st.Close();

            MessageBox.Show("Done.");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetFileAsync();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            byte[] arr = await client.GetArrayAsync();
            File.WriteAllBytes(@"c:\temp\video2.mp4", arr);

            MessageBox.Show("Done.");
        }
    }
}
