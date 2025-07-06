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
    /// Логика взаимодействия для ModifyValueDialog.xaml
    /// </summary>
    public partial class ModifyValueDialog : Window
    {
        public string ValParent { get; set; }
        public string ValName { get; set; }
        public string Value { get; set; }

        public ModifyValueDialog()
        {
            InitializeComponent();

            ValParent="";
            ValName  ="";
            Value = "";
            }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pathTxtBlock.Text = ValParent;
            ValueNameTextBlock.Text = ValName;
            valueTxtBox.Text = Value;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DialogResult == true)
            {
                valueTxtBox.Text = valueTxtBox.Text.Trim(' ');
                if (valueTxtBox.Text.Length == 0)
                {
                    MessageBox.Show("Введите значение!", "INFO", MessageBoxButton.OK);
                    valueTxtBox.Focus();
                    e.Cancel = true;
                    return;
                }
                Value = valueTxtBox.Text;
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
    }
}
