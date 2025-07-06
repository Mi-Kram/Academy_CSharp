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

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitializeAsync();
        }

        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                string address = string.Empty;
                if (!addressBar.Text.ToLower().StartsWith("http://")) address = "http://";
                address += addressBar.Text;
                webView.CoreWebView2.Navigate(address);
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
            webView.CoreWebView2.Reload();
        }

        private void addressBar_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (webView != null && webView.CoreWebView2 != null)
                {
                    string address = string.Empty;
                    if (!addressBar.Text.ToLower().StartsWith("http://")) address = "http://";
                    address += addressBar.Text;
                    webView.CoreWebView2.Navigate(address);
                }
            }
        }
    }
}
