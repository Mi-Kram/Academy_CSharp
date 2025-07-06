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

namespace ADO_EF_CodeFirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        PhonesDbContext context = new PhonesDbContext();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var results = from t in context.SmartPhones
                          select new
                          {
                              t.SmartPhoneId,
                              t.Model,
                              t.Price, 
                              t.Description,
                              t.Manufacturer.ManufacturerName,
                              t.Manufacturer.Address
                          };

            mainDataGrid.ItemsSource = results.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SmartPhone phone = new SmartPhone
            {
                Model = "Galaxy Fold",
                Description = "New expensive thing",
                Price = 908,
                Manufacturer = (from t in context.Manufacturers
                          where t.ManufacturerId == 1
                          select t).FirstOrDefault()
            };

            context.SmartPhones.Add(phone);

            context.SaveChanges();

            Button_Click(null, null);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SmartPhone del_product = (from t in context.SmartPhones
                                      where t.Model == "Galaxy Fold"
                                   select t).FirstOrDefault();

            if (del_product == null)
                return;

            context.SmartPhones.Remove(del_product);

            context.SaveChanges();

            Button_Click(null, null);
        }
    }
}
