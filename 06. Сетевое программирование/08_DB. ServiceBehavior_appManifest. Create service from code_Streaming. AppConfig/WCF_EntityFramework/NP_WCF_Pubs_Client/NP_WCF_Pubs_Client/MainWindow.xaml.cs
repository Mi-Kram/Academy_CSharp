using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
using NP_WCF_Pubs_Client.ServiceReference1;

namespace NP_WCF_Pubs_Client
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

        Service1Client client;

        public async void RefreshAuthors()
        {
            try
            {
                client = new Service1Client();

                var result = await client?.GetAuthorsAsync();

                ObservableCollection<MyAuthor> observableCollection = new ObservableCollection<MyAuthor>(result);

                CollectionViewSource collection = new CollectionViewSource() { Source = observableCollection };
                collection.GroupDescriptions.Add(new PropertyGroupDescription("State"));
                //collection.GroupDescriptions.Add(new PropertyGroupDescription("City"));
                //collection.SortDescriptions.Add(new SortDescription("City", ListSortDirection.Ascending));
                //collection.Filter += Collection_Filter;

                dataGrid1.ItemsSource = collection.View;

                StateColumn.ItemsSource = new List<string>() { "CA", "DN", "UT", "KS", "TN", "MS", "MI", "MD", "KI"};

                //dataGrid1.Columns[0].Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Collection_Filter(object sender, FilterEventArgs e)
        {
            MyAuthor author = e.Item as MyAuthor;

            if (author != null)
            {
                if (author.Contract.HasValue)
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            RefreshAuthors();
        }

        private void dataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            MyAuthor selectedAuthorRow = dataGrid1.SelectedItem as MyAuthor;

            try
            {
                client?.UpdateAuthor(selectedAuthorRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            RefreshAuthors();
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyAuthor author = new MyAuthor
                {
                    Au_id = "123-45-7555",
                    FirstName = "Alex",
                    LastName = "Pupkin",
                    State = "DN",
                    City = "Donetsk",
                    Address = "Lenina, 5",
                    Zip = "23424",
                    Contract = false,
                    Phone = "12312345"
                };

                client?.AddAuthor(author);

                RefreshAuthors();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                // Получить выделенного автора
                MyAuthor selectedAuthorRow = dataGrid1.SelectedItem as MyAuthor;

                // Получить ID выделенного автора
                string selectedId = selectedAuthorRow.Au_id;

                try
                {
                    client?.DeleteAuthor(selectedId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // Обновить таблицу
                RefreshAuthors();
            }
        }
    }
}
