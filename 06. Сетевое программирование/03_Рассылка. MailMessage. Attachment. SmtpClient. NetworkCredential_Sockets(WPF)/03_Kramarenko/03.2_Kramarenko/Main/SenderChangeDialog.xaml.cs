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
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для SenderChangeDialog.xaml
    /// </summary>
    public partial class SenderChangeDialog : Window
    {
        public string Sender { get; set; }
        public string Password { get; set; }
        public SenderChangeDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sender = SenderTXT.Text.Trim(' ').ToLower();
            Password = PasswordTXT.Password;

            if (Sender.Length == 0) MessageBox.Show("Введите адрес");
            else if (!Sender.EndsWith("gmail.com")) MessageBox.Show("Ввод толко gmail.com");
            else DialogResult = true;
        }

    }
}
