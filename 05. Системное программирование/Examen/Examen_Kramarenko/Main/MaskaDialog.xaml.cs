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
    /// Логика взаимодействия для MaskaDialog.xaml
    /// </summary>
    public partial class MaskaDialog : Window
    {
        public string folderMaska { get; set; }
        public string fileMaska { get; set; }

        public MaskaDialog()
        {
            InitializeComponent();

            folderMaska = "*";
            fileMaska = "*";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (folderMaska == null || folderMaska == "") folderMaska = "*";
            if (fileMaska == null || fileMaska == "") fileMaska = "*.*";
            
            folderTextBox.Text = folderMaska;
            fileTextBox.Text = fileMaska;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DialogResult == true)
            {
                folderMaska = folderTextBox.Text.Trim(' ');
                fileMaska = fileTextBox.Text.Trim(' ');

                if (folderMaska == "") folderMaska = "*";
                if (fileMaska == "") fileMaska = "*.*";
            }
        }
    }
}
