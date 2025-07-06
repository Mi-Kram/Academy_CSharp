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
    /// Логика взаимодействия для AddLetterDialog.xaml
    /// </summary>
    public partial class AddLetterDialog : Window
    {
        public string Header { get; set; }
        public string Body { get; set; }

        public AddLetterDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Header = HeaderTXT.Text.Trim(' ', '\t', '\n');
            Body = BodyTXT.Text.Trim(' ', '\t', '\n');
            if (Header.Length == 0 || Body.Length == 0) MessageBox.Show("Заполните поля!");
            else DialogResult = true;
        }
    }
}
