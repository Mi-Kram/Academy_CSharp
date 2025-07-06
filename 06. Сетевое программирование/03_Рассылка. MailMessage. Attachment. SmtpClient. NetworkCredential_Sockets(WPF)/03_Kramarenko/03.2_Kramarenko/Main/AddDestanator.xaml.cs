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
    /// Логика взаимодействия для AddDestanator.xaml
    /// </summary>
    public partial class AddDestanator : Window
    {
        public string GMAIL { get; set; }
        public AddDestanator()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GMAIL = GMAILTXT.Text.Trim(' ').ToLower();
            if (GMAIL.Length == 0) MessageBox.Show("Введите адрес");
            else DialogResult = true;
        }
    }
}
