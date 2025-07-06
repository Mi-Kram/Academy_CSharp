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

namespace Main.Resources
{
    /// <summary>
    /// Логика взаимодействия для EditDepartament.xaml
    /// </summary>
    public partial class EditDepartament : Window
    {
        string pathTopeopleImages = $@"{Environment.CurrentDirectory}\Resources\peopleImages\";
        DepEmplDBDataContext db = new DepEmplDBDataContext();
        public (string Departament, string Address, string Phone, string HeadID) Value { get; set; }
        public EditDepartament()
        {
            InitializeComponent();

            Value = ("", "", "", "");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == true)
                Value = (Departament.Text, Address.Text, Phone.Text, (Boss.SelectedItem as TextBlock).Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Employees> employees = db.Employees.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();

            foreach (Employees item in employees)
            {
                TextBlock cmbItem = new TextBlock();
                cmbItem.Text = $"{item.FirstName} {item.LastName}";
                ToolTip toolTip = new ToolTip();
                StackPanel toolTipPanel = new StackPanel() { Orientation = Orientation.Horizontal, Height = 50 };
                toolTipPanel.Children.Add(new Image() { Source = new BitmapImage(new Uri(pathTopeopleImages + item.PhotoPath, UriKind.Absolute)) });
                toolTipPanel.Children.Add(new TextBlock() { Margin = new Thickness(10, 0, 0, 0), Text = $"Возраст: {item.Age}, Адрес: {item.Address}" });
                toolTip.Content = toolTipPanel;
                cmbItem.ToolTip = toolTip;

                Boss.Items.Add(cmbItem);
            }

            Departament.Text = Value.Departament;
            Address.Text = Value.Address;
            Phone.Text = Value.Phone;

            for (int i = 0; i < Boss.Items.Count; i++)
            {
                if (Boss.Items[i] is TextBlock)
                {
                    if ((Boss.Items[i] as TextBlock).Text == Value.HeadID)
                    {
                        Boss.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsCorrectData()) DialogResult = true;
            else MessageBox.Show("Заполните правильно все поля", "INFO", MessageBoxButton.OK);
        }

        bool IsCorrectData()
        {
            Departament.Text = Departament.Text.Trim(new char[] { ' ' });
            Address.Text = Address.Text.Trim(new char[] { ' ' });
            Phone.Text = Phone.Text.Trim(new char[] { ' ' });


            if (Departament.Text.Length == 0 || Address.Text.Length == 0 || 
                Phone.Text.Length == 0 || Boss.SelectedItem == null) return false;

            return true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
