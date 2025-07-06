using Main.Models;
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
    /// Логика взаимодействия для EditManufacture.xaml
    /// </summary>
    public partial class EditManufacture : Window
    {
        public (string BrandTitle, string Address, string Phone) Value { get; set; }

        public EditManufacture()
        {
            InitializeComponent();

            Value = ("","","");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == true)
                Value = (BrandTitle.Text, Address.Text, Phone.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BrandTitle.Text = Value.BrandTitle;
            Address.Text = Value.Address;
            Phone.Text = Value.Phone;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsCorrectData()) DialogResult = true;
            else MessageBox.Show("Заполните правильно все поля", "INFO", MessageBoxButton.OK);
        }

        bool IsCorrectData()
        {
            BrandTitle.Text = BrandTitle.Text.Trim(new char[] { ' ' });
            Address.Text = Address.Text.Trim(new char[] { ' ' });
            Phone.Text = Phone.Text.Trim(new char[] { ' ' });

            if (BrandTitle.Text.Length == 0 || Address.Text.Length == 0 || Phone.Text.Length == 0) return false;

            return true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
