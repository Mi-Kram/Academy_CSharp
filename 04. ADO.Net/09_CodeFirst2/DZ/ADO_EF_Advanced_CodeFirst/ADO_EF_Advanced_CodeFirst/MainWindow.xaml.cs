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

        MyDbContext db = new MyDbContext();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*var results = from employee in db.Employees
                          select employee;*/

            var results = from t in db.ProjectEmployees
                          select new
                          {
                              t.Employee.EmployeeName,
                              t.Employee.Address,
                              t.Project.ProjectName,
                              t.Project.StartDate,
                              t.Project.EndDate
                          };

            dataGrid1.ItemsSource = results.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var results = from t in db.Products
                          select new
                          {
                              t.ProductID,
                              t.ProductName,
                              t.Description,
                              t.Price,
                              t.Vendor.VendorName
                          };

            dataGrid1.ItemsSource = results.ToList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Product product = new Product
            {
                ProductName = "Trinitron 3D",
                Description = "TV",
                Price = 12314.23,
                Vendor = (from t in db.Vendors
                         where t.VendorID == 1
                         select t).First()
            };

            db.Products.Add(product);

            db.SaveChanges();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var results = from t in db.Vendors
                          select new
                          {
                              t.VendorID,
                              t.VendorName,
                              t.Address,
                              t.Phone
                          };

            dataGrid1.ItemsSource = results.ToList();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Product del_product = (from t in db.Products
                                 where t.ProductID == 1
                                 select t).FirstOrDefault();

            if (del_product == null)
                return;

            db.Products.Remove(del_product);

            db.SaveChanges();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ProjectEmployees pro = new ProjectEmployees
            {
                Employee = (from t in db.Employees
                           where t.EmployeeId == 3
                           select t).First(),

                Project = (from t in db.Projects
                           where t.ProjectId == 6
                           select t).First()
            };

            db.ProjectEmployees.Add(pro);

            db.SaveChanges();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ProjectEmployees del_product = (from t in db.ProjectEmployees
                                            where t.ProjectId == 6 && t.EmployeeId == 3
                                            select t).FirstOrDefault();

            if (del_product == null)
                return;

            db.ProjectEmployees.Remove(del_product);

            db.SaveChanges();
        }
    }
}
