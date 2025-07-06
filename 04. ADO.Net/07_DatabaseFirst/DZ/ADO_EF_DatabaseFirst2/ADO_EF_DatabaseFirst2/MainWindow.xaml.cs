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

namespace ADO_EF_DatabaseFirst2
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

        pubsEntities pubs = new pubsEntities();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var results = from t in pubs.authors
                         select new MyAuthor
                         {
                             FirstName = t.au_fname,
                             LastName = t.au_lname,
                             City = t.city,
                             State = t.state,
                             Phone = t.phone,
                             Address = t.address,
                         };

            //ObservableCollection<author> observableCollection = new ObservableCollection<author>(results);

            mainDataGrid.ItemsSource = results.ToList();
        }
    }
}
