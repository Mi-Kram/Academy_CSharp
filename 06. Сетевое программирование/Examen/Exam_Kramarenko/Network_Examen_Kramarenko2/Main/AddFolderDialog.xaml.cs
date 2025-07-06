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
    /// Логика взаимодействия для AddFolderDialog.xaml
    /// </summary>
    public partial class AddFolderDialog : Window
    {
        public string FolderName { get; set; }
        public AddFolderDialog()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            FolderName = textBox.Text;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBox.Text = textBox.Text.Replace("/",  "");
            textBox.Text = textBox.Text.Replace("\\", "");
        }
    }
}
