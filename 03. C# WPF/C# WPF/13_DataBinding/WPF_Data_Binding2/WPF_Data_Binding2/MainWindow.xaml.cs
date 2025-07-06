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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Data_Binding2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Привязка списка стран к DataContext окна
            Countries c = new Countries();

            // Поле в каждом элементе управления, принимающий коллекцию ICollection
            this.DataContext = c.GetCountries();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Создание привязки из кода
            Binding bnd = new Binding();
            bnd.Source = listBox1;                                  // Ссылка на объект-источник привязки
            bnd.Path = new PropertyPath("SelectedItem.Content");    // Путь к свойству объекта-источника (содержимое выделенного пункта listbox1)
            bnd.Mode = BindingMode.TwoWay;                          // Режим привязки картинки и списка
            image1.SetBinding(Image.SourceProperty, bnd);           // привязать свойство Source картинки к свойству SelectedItem.Content списка
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Очистить все привязки объекта image1
            //BindingOperations.ClearAllBindings(image1);

            // Очистить призку свойства Source объекта image1
            BindingOperations.ClearBinding(image1, Image.SourceProperty);
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
            return name;
        }
    }

    public class Countries
    {
        List<Country> countries = new List<Country>();

        public Country[] GetCountries()
        {
            countries.Add(new Country("Austria", @"Pictures\admin.gif"));
            countries.Add(new Country("Germany", @"Pictures\AMER02_I.JPG"));
            countries.Add(new Country("Norway", @"Pictures\enotik.gif"));
            countries.Add(new Country("USA", @"Pictures\eyes.gif"));

            return countries.ToArray();
        }
    }
}
