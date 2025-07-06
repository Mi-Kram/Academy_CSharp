using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            userNameTextBox.Text = userNameTextBox.Text.Trim(' ');
            string userName = userNameTextBox.Text;
            if (userName.Length == 0) userName = "User";
            string hostName = hostTextBox.Text;
            if (!Int32.TryParse(PortTextBox.Text, out int port))
            {
                MessageBox.Show("Неверный порт!", "INFO", MessageBoxButton.OK);
                return;
            }
            
            ChatWindow window;
            if (ServerBtn.IsEnabled) window = new ChatWindow(IPAddress.Any, port);
            else window = new ChatWindow(hostName, port);
            window.UserName = userName;
            this.Hide();
            window.Show();
            this.Close();
        }



        private void Server_Click(object sender, RoutedEventArgs e)
        {
            ServerBtn.IsEnabled = false;
            ClientBtn.IsEnabled = true;
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            ClientBtn.IsEnabled = false;
            ServerBtn.IsEnabled = true;
        }
    }
}
