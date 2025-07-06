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

namespace _03_Kramarenko
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ChatMessengerWindow chatMessenger = new ChatMessengerWindow(LoginTextBox.Text, GetPort());
            chatMessenger.Show();
            this.Close();
        }

        private void PortTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            if (key == Key.Back || key == Key.Delete || key == Key.Left || key == Key.Right || key == Key.Tab) return;

            string s = key.ToString();
            if (s.Length != 1)
            {
                if (s.StartsWith("D")) s = s.Remove(0, 1);
                if (s.StartsWith("NumPad")) s = s.Remove(0, 6);
            }
            if (s.Length != 1 || !char.TryParse(s, out char ch) || !char.IsDigit(ch)) e.Handled = true;
        }

        int GetPort()
        {
            if (!Dispatcher.CheckAccess()) return Dispatcher.Invoke(() => GetPort());
            else
            {
                if (Int32.TryParse(PortTextBox.Text, out int port)) return port;
                else return -1;
            }
        }

    }
}
