using HtmlAgilityPack;
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

namespace NP_HtmlAgilityPack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HtmlAgilityPack.HtmlDocument htmlDocument = null;

        public MainWindow()
        {
            InitializeComponent();

            //webView.NavigationCompleted += WebView_NavigationCompleted;

            InitializeAsync();
        }

        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // загрузка HTML-документа при помощи WebClient
            WebClientLoad($"http://{addressBar.Text}");
        }

        private void WebClientLoad(string url)
        {
            progressBar1.Value = 0;

            // загрузка из интернет средствами WebClient
            WebClient client = new WebClient();

            // Загрузка текста интернет страницы
            client.DownloadStringAsync(new Uri(url, UriKind.RelativeOrAbsolute));

            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadStringCompleted += Client_DownloadStringCompleted;

            // указание кодировки скачиваемой страницы
            client.Encoding = Encoding.UTF8;
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            // загрузить сайт в WebView
            if (webView != null && webView.CoreWebView2 != null)
            {
                if(!addressBar.Items.Contains(addressBar.Text))
                    addressBar.Items.Add(addressBar.Text);
                webView.CoreWebView2.Navigate($"http://{addressBar.Text}");
            }

            // Весь HTML-документ (пустой)
            htmlDocument = new HtmlAgilityPack.HtmlDocument();

            // Загрузка документа из строки, полученной с сайта при помощи WebClient
            htmlDocument.LoadHtml(e.Result);

            // показать текст загруженной страницы
            htmlTextBox.Text = e.Result;

            // анализ картинок на странице
            imagesListBox.Items.Clear();

            // запрос на языке XPath
            var imgNodes = htmlDocument.DocumentNode.SelectNodes("//img");
            if (imgNodes != null)
            {
                foreach (HtmlNode node in imgNodes)
                {
                    imagesListBox.Items.Add(node.Attributes["src"].Value);
                }
            }

            // нахождение всех ссылок в документе
            hrefListBox.Items.Clear();
            var aNodes = htmlDocument.DocumentNode.SelectNodes("//a");
            if (aNodes != null)
            {
                foreach (HtmlNode node in aNodes)
                {
                    if (node.Attributes != null && node.Attributes["href"] != null && node.Attributes["href"].Value != null)
                        hrefListBox.Items.Add(node.Attributes["href"].Value);
                }
            }

            // нахождение одиночного тега
            HtmlNode singleNode = htmlDocument.DocumentNode.SelectSingleNode("//h1");
            h1ListBox.Items.Clear();
            if (singleNode != null)
                h1ListBox.Items.Add(singleNode.InnerText);

            // нахождение списка всех опций
            optionsListBox.Items.Clear();
            var liNodes = htmlDocument.DocumentNode.SelectNodes("//li");
            if (liNodes != null)
            {
                foreach (HtmlNode node in liNodes)
                {
                    optionsListBox.Items.Add(node.InnerText);
                }
            }
        }

        private void addressBar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "\r")
            {
                WebClientLoad($"http://{addressBar.Text}");
            }
        }

        private void addressBar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (addressBar.SelectedItem != null)
            {
                WebClientLoad($"http://{addressBar.Text}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // нахождение всех строк таблицы
            var trNodes = htmlDocument?.DocumentNode.SelectNodes("//ul");
            if (trNodes != null)
            {
                foreach (HtmlNode node in trNodes)
                    node.AppendChild(HtmlNode.CreateNode("<td><b>hello</b></td>"));
            }

            htmlDocument.Save("result.html");

            //htmlTextBox.Text = htmlDocument.DocumentNode.OuterHtml;

            webView.CoreWebView2.NavigateToString(htmlDocument.DocumentNode.OuterHtml);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var liNodes = htmlDocument?.DocumentNode.SelectNodes("//li");
            if (liNodes != null)
            {
                // обойти все опции и поменять их содержимое
                foreach (HtmlNode node in liNodes)
                    node.InnerHtml = "<b>Replaced option!!!</b>";
            }

            htmlDocument.Save("result.html");

            webView.CoreWebView2.NavigateToString(htmlDocument.DocumentNode.OuterHtml);
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            // загрузка из интернет средствами HtmlAgilityPack
            HtmlWeb htmlWeb = new HtmlWeb();

            // паралельная загрузка из интернет
            htmlDocument = await htmlWeb.LoadFromWebAsync($"http://{addressBar.Text}");

            // загрузить сайт в WebView
            if (webView != null && webView.CoreWebView2 != null)
            {
                if (!addressBar.Items.Contains(addressBar.Text))
                    addressBar.Items.Add(addressBar.Text);
                webView.CoreWebView2.Navigate($"http://{addressBar.Text}");
            }

            htmlTextBox.Text = htmlDocument.Text;

            imagesListBox.Items.Clear();
            var imgNodes = htmlDocument.DocumentNode.SelectNodes("//img");
            if (imgNodes != null)
            {
                foreach (HtmlNode node in imgNodes)
                {
                    imagesListBox.Items.Add(node.Attributes["src"].Value);
                }
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.UTF8;
            htmlDocument = htmlWeb.Load($"{Environment.CurrentDirectory}/local_page.html");

            // нахождение всех ссылок в документе
            hrefListBox.Items.Clear();
            var aNodes = htmlDocument.DocumentNode.SelectNodes("//a");
            if (aNodes != null)
            {
                foreach (HtmlNode node in aNodes)
                {
                    if (node.Attributes != null && node.Attributes["href"] != null && node.Attributes["href"].Value != null)
                        hrefListBox.Items.Add(node.Attributes["href"].Value);
                }
            }

            webView.CoreWebView2.NavigateToString(htmlDocument.Text);
        }
    }
}
