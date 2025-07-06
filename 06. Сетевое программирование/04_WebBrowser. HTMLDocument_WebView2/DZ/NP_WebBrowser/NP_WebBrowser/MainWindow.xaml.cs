using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace NP_WebBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            mainBrowser.LoadCompleted += MainBrowser_LoadCompleted;
        }

        private void MainBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            MessageBox.Show("Load completed");
            HTMLDocument document = (HTMLDocument)mainBrowser.Document;

            List<string> urls = new List<string>();

            foreach (IHTMLElement link in document.links)
            {
                if (link.innerText != null)
                {
                    urls.Add($"{link.innerText} ( {link.getAttribute("href")} ) {link.offsetLeft.ToString()}");
                }
            }

            listBox1.ItemsSource = urls;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // получить ссылку на ActiveX-объект, на котором базируется WebBrowser
            dynamic activeX = this.mainBrowser.GetType().InvokeMember("ActiveXInstance",
                BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, this.mainBrowser, new object[] { });

            try
            {
                // включить режим блокирования ошибок JS
                activeX.Silent = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            mainBrowser.Navigate($"http://{addressComboBox.Text}");
        }
    }
}
