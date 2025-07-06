using Microsoft.Win32;
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
    /// Логика взаимодействия для CreateValueDialog.xaml
    /// </summary>
    public partial class CreateValueDialog : Window
    {
        public string ValParent { get; set; }
        public string ValName { get; set; }
        public string Value { get; set; }
        public RegistryValueKind Type { get; set; }
        public List<string> DenniedValNames { get; set; }

        public CreateValueDialog()
        {
            InitializeComponent();

            ValName = "";
            Value = "";
            ValParent = "";
            Type = RegistryValueKind.String;
            DenniedValNames = new List<string>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pathTxtBlock.Text = ValParent;
            DenniedValNames = DenniedValNames.Select(x => x.ToLower()).ToList();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == true)
            {
                if (nameInputTxtBox.Text.Length == 0)
                {
                    MessageBox.Show("Введите название значения!", "INFO", MessageBoxButton.OK);
                    nameInputTxtBox.Focus();
                    e.Cancel = true;
                    return;
                }
                if (DenniedValNames.Contains(nameInputTxtBox.Text.ToLower()))
                {
                    MessageBox.Show("Такое значение уже существует!", "INFO", MessageBoxButton.OK);
                    nameInputTxtBox.Focus();
                    e.Cancel = true;
                    return;
                }
                if (valueInputTxtBox.Text.Length == 0)
                {
                    MessageBox.Show("Введите значение!", "INFO", MessageBoxButton.OK);
                    valueInputTxtBox.Focus();
                    e.Cancel = true;
                    return;
                }

                ValName = nameInputTxtBox.Text;
                Value = valueInputTxtBox.Text;
                switch (cmbValueType.Text)
                {
                    case "DWord":
                        Type = RegistryValueKind.DWord;
                        break;
                    case "Binary":
                        Type = RegistryValueKind.Binary;
                        break;
                    default:
                        Type = RegistryValueKind.String;
                        break;
                }
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
