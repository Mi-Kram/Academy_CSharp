using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace NP_WebRequest
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //WebRequest request = WebRequest.Create(@"https://google.com");
            WebRequest request = WebRequest.Create(@"https://zaycev.net/musicset/dl/bff00c5cf3ceaac70c4f2811bbdccd84/4336384.json?spa=false");

            // Настройки для входа на сайт (заход без пароля)
            request.Credentials = CredentialCache.DefaultCredentials;

            // Настройки для выхода в инет
            /*WebProxy proxy = new WebProxy("10.1.0.160", 8080);
            proxy.Credentials = new NetworkCredential("link", "inet");
            request.Proxy = proxy;*/

            // Получить ответ от веб-сервера
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Показать статус соединения
            MessageBox.Show(response.StatusDescription);

            if(response.StatusCode == HttpStatusCode.OK)
            {
                // Получить сетевой поток для скачивания информации
                Stream dataStream = response.GetResponseStream();

                // Получить длину информации
                MessageBox.Show(response.ContentLength.ToString());

                // Выкачка информации при неизвестном размере
                FileStream destFile = File.Create(@"c:\temp\song.mp3");
                int b = dataStream.ReadByte();
                while (b != -1)
                {
                    destFile.WriteByte((byte)b);
                    b = dataStream.ReadByte();
                }
                destFile.Close();

                // Вариант, когда размер известен
                /*byte[] buffer = new byte[response.ContentLength];
                int size = dataStream.Read(buffer, 0, (int) response.ContentLength);
                MessageBox.Show(size.ToString());

                FileStream destFile = File.Create(@"c:\temp\song.mp3");
                destFile.Write(buffer, 0, (int) response.ContentLength);
                destFile.Close();*/

                dataStream.Close();
                response.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Класс загружает из сети информацию
            WebClient client = new WebClient();

            // Синхронная загрузка файла
            //client.DownloadFile("https://zaycev.net/musicset/dl/bff00c5cf3ceaac70c4f2811bbdccd84/4336384.json?spa=false", @"c:\temp\mozart.mp3");

            // Асинхронная загрузка файла
            client.DownloadFileAsync(new Uri("https://zaycev.net/musicset/dl/bff00c5cf3ceaac70c4f2811bbdccd84/4336384.json?spa=false", UriKind.RelativeOrAbsolute), @"c:\temp\mozart.mp3");
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Downloading completed!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WebRequest request = WebRequest.Create(@"http://lib.ru/ADAMS/hitchlost_engl.txt");

            // Настройки для входа на сайт (заход без пароля)
            request.Credentials = CredentialCache.DefaultCredentials;

            // Получить ответ от веб-сервера
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Показать статус соединения
            MessageBox.Show(response.StatusDescription);

            // Получить сетевой поток для скачивания информации
            Stream dataStream = response.GetResponseStream();

            // Получить длину информации
            MessageBox.Show(response.ContentLength.ToString());

            // Вариант для текстовых файлов
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            MessageBox.Show(responseFromServer.Length.ToString());
            textBlock1.Text = responseFromServer;
            reader.Close();

            dataStream.Close();
            response.Close();
        }
    }
}
