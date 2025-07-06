using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

namespace ADO_DataGrid_Linq
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

        MyDataContext context = new MyDataContext();

        public void RefreshAuthors()
        {
            var result = from t in context.authors
                      select new Author
                      {
                          Au_id = t.au_id,
                          FirstName = t.au_fname,
                          LastName = t.au_lname,
                          City = t.city,
                          State = t.state,
                          Phone = t.phone,
                          Address = t.address,
                          Zip = t.zip,
                          Contract = t.contract
                      };

            // Коллекция используется для поддержки фильтрации
            ObservableCollection<Author> observableCollection = new ObservableCollection<Author>(result);

            CollectionViewSource collection = new CollectionViewSource() { Source = observableCollection };
            collection.GroupDescriptions.Add(new PropertyGroupDescription("State"));
            //collection.GroupDescriptions.Add(new PropertyGroupDescription("City"));

            //collection.SortDescriptions.Add(new SortDescription("City", ListSortDirection.Ascending));
            
            //collection.Filter += Collection_Filter;

            dataGrid1.ItemsSource = collection.View;
            //dataGrid1.ItemsSource = result;

            // заполнить выпадающий список штатов
            var statesList = (from t in context.authors
                              select t.state).Distinct();
            StateColumn.ItemsSource = statesList;
        }

        private void Collection_Filter(object sender, FilterEventArgs e)
        {
            Author author = e.Item as Author;

            if(author != null)
            {
                if (author.State == "CA")
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            RefreshAuthors();
        }

        // запускается перед окончанием редактирования
        private void dataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Выделенная строка в таблице с НОВЫМИ данными (режим UpdateSourceTrigger=PropertyChanged)
            Author selectedAuthorRow = dataGrid1.SelectedItem as Author;

            // Получение id редактируемого объекта
            string selectedId = selectedAuthorRow.Au_id;

            // Получить из БД ссылку на редактируемый объект в базе
            author selectedAuthorDB = (from t in context.authors
                                       where t.au_id == selectedId
                                       select t).First();

            // Перенести данные из таблицы в объект в БД
            selectedAuthorDB.au_id = selectedAuthorRow.Au_id;
            selectedAuthorDB.au_fname = selectedAuthorRow.FirstName;
            selectedAuthorDB.au_lname = selectedAuthorRow.LastName;
            selectedAuthorDB.city = selectedAuthorRow.City;
            selectedAuthorDB.address = selectedAuthorRow.Address;
            selectedAuthorDB.state = selectedAuthorRow.State;
            selectedAuthorDB.phone = selectedAuthorRow.Phone;
            selectedAuthorDB.zip = selectedAuthorRow.Zip;
            selectedAuthorDB.contract = selectedAuthorRow.Contract;

            // Синхронизировать изменения с БД
            context.SubmitChanges();

            // Перечитать данные из БД
            RefreshAuthors();
        }
    }
}
