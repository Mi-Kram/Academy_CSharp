using System;
using System.Collections.Generic;
using System.IO;
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
using Main.ServiceReference1;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Service1Client server = new Service1Client();
        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void GetData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listBox.ItemsSource = null;
                textBox.Clear();

                string address = address_TextBox.Text;
                string[] list = await server.GetDirDataAsync(address);

                var txtFiles = list.Where(x => System.IO.Path.GetExtension(x) == ".txt");

                if (txtFiles.Count() > 0)
                {
                    FileStream destFile = File.Create($"{Environment.CurrentDirectory}/text.txt");

                    Stream st = await server.GetStreamDataAsync(txtFiles.ElementAt(rand.Next(0, txtFiles.Count())));

                    byte[] buffer = new byte[1024 * 1024];

                    while (true)
                    {
                        int res = await st.ReadAsync(buffer, 0, buffer.Length);
                        if (res == 0) break;
                        destFile.Write(buffer, 0, res);
                    }

                    destFile.Close();
                    st.Close();

                    textBox.Text = File.ReadAllText($"{Environment.CurrentDirectory}/text.txt", Encoding.Default);
                }
                else
                {
                    textBox.Text = "Нет текстовых файлов";
                }
                list = list.Select(x => System.IO.Path.GetFileName(x)).ToArray();
                listBox.ItemsSource = list;
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show(this, "Нет подключения к серверу");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

        }
    }
}
