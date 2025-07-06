using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        public string PathToPeopleImage { get; set; }
        private string SelectedPath { get; set; }
        public (string FName, string LName, int Age, string Address, string PhotoPath) Value { get; set; }

        public EditEmployee()
        {
            InitializeComponent();

            SelectedPath = string.Empty;
            PathToPeopleImage = "";
            Value = ("","",-1,"","");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (PathToPeopleImage == "") PathToPeopleImage = $@"{Environment.CurrentDirectory}\Resources\peopleImages\";
            
            FName.Text = Value.FName;
            LName.Text = Value.LName;
            if(Value.Age >= 0) Age.Text = Value.Age.ToString();
            Address.Text = Value.Address;
            if (Value.PhotoPath == "")
            {
                picture.Source = new BitmapImage(new Uri(PathToPeopleImage + "unknown.jpg", UriKind.Absolute));
            }
            else
            {
                //Image.Text = Value.PhotoPath;
                picture.Source = new BitmapImage(new Uri(PathToPeopleImage + Value.PhotoPath, UriKind.Absolute));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == true)
            {
                Value = (FName.Text, LName.Text, int.Parse(Age.Text), Address.Text, Value.PhotoPath == "" ? "unknown.jpg" : Value.PhotoPath);
                if(SelectedPath != string.Empty) File.Copy(SelectedPath, PathToPeopleImage + Value.PhotoPath);
            }
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsCorrectData()) DialogResult = true;
            else MessageBox.Show("Заполните правильно все поля", "INFO", MessageBoxButton.OK);
        }

        bool IsCorrectData()
        {
            FName.Text = FName.Text.Trim(new char[] { ' ' });
            LName.Text = LName.Text.Trim(new char[] { ' ' });
            Address.Text = Address.Text.Trim(new char[] { ' ' });
            //Image.Text = Image.Text.Trim(new char[] { ' ' });

            if (Age.Text.Length == 0 || !double.TryParse(Age.Text, out double age) || age < 0 || age > 150) Age.Text = "";

            if (FName.Text.Length == 0 || LName.Text.Length == 0 || 
                Age.Text.Length == 0 || Address.Text.Length == 0) return false;

            return true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.AddExtension = true;
            dialog.Filter = "JPG, JPEG, PNG, BMP|*.jpg;*.jpeg;*.png;*.bmp";
            dialog.DefaultExt = ".jpg";
            dialog.Title = "Opening";
            dialog.CheckFileExists = true;
            dialog.FileName = PathToPeopleImage;
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == true)
            {
                SelectedPath = dialog.FileName;
                string safeFileName =  dialog.SafeFileName;
                int cnt = 1;
                while (File.Exists(PathToPeopleImage + safeFileName))
                {
                    safeFileName = System.IO.Path.GetFileNameWithoutExtension(dialog.SafeFileName) + cnt.ToString().PadLeft(3, '0') + System.IO.Path.GetExtension(dialog.SafeFileName);
                    cnt++;
                }
                Value = (Value.FName, Value.LName, Value.Age, Value.Address, safeFileName);

                //Image.Text = dialog.SafeFileName;
                picture.Source = new BitmapImage(new Uri(SelectedPath, UriKind.Absolute));
            }
        }

        private void ClearPathPhoto_Click(object sender, RoutedEventArgs e)
        {
            //Image.Text = "";
            picture.Source = new BitmapImage(new Uri(PathToPeopleImage + "unknown.jpg", UriKind.Absolute));
        }
    }
}
