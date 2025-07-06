using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_DataBinding3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Countries c = new Countries();

        public MainWindow()
        {
            InitializeComponent();

            // Привязка коллекции к XAML
            this.DataContext = c;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            c.Add("Russia", "admin.gif");  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(countryList1.SelectedItem.ToString());
        }
    }

    public class Country
    {
        public Country(string name, string imagePath)
        {
            this.name = name;
            this.imagePath = imagePath;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public override string ToString()
        {
            return name + "!";
        }
    }

    // ObservableCollection автоматически обновляет элементы пользовательского интерфейса, связанные с полем DataContext
    public class Countries : ObservableCollection<Country>
    //public class Countries : List<Country>
    {
        public Countries()
        {
            Add(new Country("Austria", "enotik.gif"));
            Add(new Country("Germany", "admin.gif"));
            Add(new Country("Norway", "enotik.gif"));
            Add(new Country("USA", "admin.gif"));
        }

        public void Add(string name, string path)
        {
            Add(new Country(name, path));
        }
    }
}
