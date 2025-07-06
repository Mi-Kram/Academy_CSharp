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

namespace NP_WebView2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // установка пакетов WebView2
        // Install-Package Microsoft.Web.WebView2 -Version 1.0.674-prerelease
        // https://docs.microsoft.com/en-us/microsoft-edge/webview2/gettingstarted/wpf

        // ссылка на WebView Runtime
        // https://developer.microsoft.com/en-us/microsoft-edge/webview2/

        public MainWindow()
        {
            InitializeComponent();

            // начата загрузка страницы
            webView.NavigationStarting += WebView_NavigationStarting;

            // загрузка страницы окончена
            webView.NavigationCompleted += WebView_NavigationCompleted;

            InitializeAsync();
        }

        async void InitializeAsync()
        {
            // инициализация webView2
            await webView.EnsureCoreWebView2Async(null);
        }

        private void WebView_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            //MessageBox.Show("Navigation starting");
        }

        private void WebView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            //MessageBox.Show("Navigation completed");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                addressBar.Items.Add(addressBar.Text);

                // загрузка страницы
                webView.CoreWebView2.Navigate($"http://{addressBar.Text}");
            }
        }

        // метод срабатывает на нажатие внутри Combobox
        private void addressBar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "\r" && webView != null && webView.CoreWebView2 != null)
            {
                addressBar.Items.Add(addressBar.Text);
                webView.CoreWebView2.Navigate($"http://{addressBar.Text}");
            }
        }

        private void addressBar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (addressBar.SelectedItem != null && webView != null && webView.CoreWebView2 != null)
            {
                webView.CoreWebView2.Navigate($"http://{addressBar.SelectedItem}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.GoBack();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.GoForward();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // управление масштабом страницы
            webView.ZoomFactor += 0.25;
            Title = $"WebView test app. Zoom factor: {webView.ZoomFactor}";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // управление масштабом страницы
            webView.ZoomFactor -= 0.25;
            Title = $"WebView test app. Zoom factor: {webView.ZoomFactor}";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.Stop();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.Reload();
        }

        private async void Button_Click_7(object sender, RoutedEventArgs e)
        {
            string scriptResult = await webView.ExecuteScriptAsync(tbScript.Text);
            MessageBox.Show(this, scriptResult, "Script Result");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            webView.NavigateToString(tbScript.Text);
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string path = $"file:///" + $"{Environment.CurrentDirectory}/local_page.html";
            webView.CoreWebView2.Navigate(path);
        }
    }
}
