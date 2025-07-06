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
    /// Логика взаимодействия для CreateKeyDialog.xaml
    /// </summary>
    public partial class CreateKeyDialog : Window
    {
        public string ValParent { get; set; }
        public string KeyName { get; set; }
        public List<string> DenniedKeys { get; set; }
        public CreateKeyDialog()
        {
            InitializeComponent();
            ValParent = "";
            KeyName = "";
            DenniedKeys = new List<string>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pathTxtBlock.Text = ValParent;
            DenniedKeys = DenniedKeys.Select(x => x.ToLower()).ToList();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == true)
            {
                if (keyInputTxtBox.Text.Length == 0)
                {
                    MessageBox.Show("Введите название ключа!", "INFO", MessageBoxButton.OK);
                    keyInputTxtBox.Focus();
                    e.Cancel = true;
                    return;
                }
                if (DenniedKeys.Contains(keyInputTxtBox.Text.ToLower()))
                {
                    MessageBox.Show("Такой ключ уже существует!", "INFO", MessageBoxButton.OK);
                    keyInputTxtBox.Focus();
                    e.Cancel = true;
                    return;
                }
                KeyName = keyInputTxtBox.Text;
            }
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) e.Handled = true;
        }
    }
}
