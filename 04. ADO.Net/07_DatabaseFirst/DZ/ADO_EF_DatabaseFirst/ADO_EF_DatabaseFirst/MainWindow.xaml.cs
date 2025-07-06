using EF_DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace ADO_EF_DatabaseFirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        pubsEntities context = new pubsEntities();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void RefreshAuthors()
        {
            var result = from t in context.authors
                         select new MyAuthor
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

            ObservableCollection<MyAuthor> observableCollection = new ObservableCollection<MyAuthor>(result);

            CollectionViewSource collection = new CollectionViewSource() { Source = observableCollection };

            //collection.GroupDescriptions.Add(new PropertyGroupDescription("State"));
            //collection.GroupDescriptions.Add(new PropertyGroupDescription("City"));

            //collection.SortDescriptions.Add(new SortDescription("City", ListSortDirection.Ascending));

            //collection.Filter += Collection_Filter;

            dataGrid1.ItemsSource = null;
            dataGrid1.ItemsSource = collection.View;

            var statesList = (from t in context.authors
                              select t.state).Distinct();
            StateColumn.ItemsSource = statesList.ToList();

            //dataGrid1.Columns[0].Visibility = Visibility.Hidden;
        }

        private void Collection_Filter(object sender, FilterEventArgs e)
        {
            MyAuthor author = e.Item as MyAuthor;

            if (author != null)
            {
                if (author.Contract == true)
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

            string selectedId = selectedAuthorRow.Au_id;

            author selectedAuthorDB = (from t in context.authors
                                       where t.au_id == selectedId
                                       select t).First();

            selectedAuthorDB.au_id = selectedAuthorRow.Au_id;
            selectedAuthorDB.au_fname = selectedAuthorRow.FirstName;
            selectedAuthorDB.au_lname = selectedAuthorRow.LastName;
            selectedAuthorDB.city = selectedAuthorRow.City;
            selectedAuthorDB.address = selectedAuthorRow.Address;
            selectedAuthorDB.state = selectedAuthorRow.State;
            selectedAuthorDB.phone = selectedAuthorRow.Phone;
            selectedAuthorDB.zip = selectedAuthorRow.Zip;
            selectedAuthorDB.contract = selectedAuthorRow.Contract.Value;

            context.SaveChanges();

            RefreshAuthors();

            //dataGrid1.CanUserSortColumns = true;
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            author author = new author
            {
                au_id = "123-45-7654",
                au_fname = "Alex",
                au_lname = "Pupkin",
                state = "DN",
                city = "Donetsk",
                address = "Lenina, 5",
                zip = "23424",
                contract = false,
                phone = "12312345"
            };

            context.authors.Add(author);

            context.SaveChanges();

            RefreshAuthors();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                // Получить выделенного автора
                MyAuthor selectedAuthorRow = dataGrid1.SelectedItem as MyAuthor;

                // Получить ID выделенного автора
                string selectedId = selectedAuthorRow.Au_id;

                author del_author = (from t in context.authors
                                           where t.au_id == selectedId
                                           select t).First();

                // Удалить из БД автора с данным ID
                context.authors.Remove(del_author);

                // Синхронизировать изменения
                context.SaveChanges();

                // Обновить таблицу
                RefreshAuthors();
            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            DateTime startTime = DateTime.Now;

            for (int i = 0; i < 10000; i++)
            {
                // Создание одиночного объекта
                app_test row = new app_test
                {
                    str = Guid.NewGuid().ToString("n"),
                    number1 = random.Next(10000),
                    number2 = random.Next(10000)
                };

                // Добавление в локальную таблицу
                context.app_test.Add(row);
            }

            // Синхронизация с БД
            context.SaveChanges();

            DateTime endTime = DateTime.Now;

            MessageBox.Show(String.Format("Done: {0}", (endTime - startTime).TotalMilliseconds.ToString()));
        }

        /*
            create function GetAuthorsByState(@st varchar(2))
            returns table
            as
            return select* from authors where state = @st
        */

        // Запуск хранимой функции из SQL-запроса
        private void StoredFunction_Click(object sender, RoutedEventArgs e)
        {
            // Параметризированный запрос, состоящий из LINQ и SQL, вызывающий хранимую функцию
            SqlParameter parameter = new SqlParameter("@state", "CA");
            var result = from t in context.Database.SqlQuery<author>("select * from GetAuthorsByState(@state)", parameter)
                         select new MyAuthor
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
            dataGrid1.ItemsSource = result.ToList();

            var statesList = (from t in context.authors
                              select t.state).Distinct();
            StateColumn.ItemsSource = statesList.ToList();
        }

        private void StoredFunction2_Click(object sender, RoutedEventArgs e)
        {
            // Простой способ вызова хранимой функции без SQL
            var result = from t in context.GetAuthorsByState("CA")
                         select new MyAuthor
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
            dataGrid1.ItemsSource = result.ToList();

            var statesList = (from t in context.authors
                              select t.state).Distinct();
            StateColumn.ItemsSource = statesList.ToList();
        }

        /*
            create procedure GetAuthorsByType(@type varchar(20))
            as
            begin
	            select * from authors where au_id in
	            (select au_id from titleauthor where title_id in
	            (select title_id from titles where type = @type))
            end
        */

        private void StoredProcedure_Click(object sender, RoutedEventArgs e)
        {
            // Параметризированный запрос, состоящий из LINQ и SQL, вызывающий хранимую процедуру
            SqlParameter parameter = new SqlParameter("@type", "business");
            var result = from t in context.Database.SqlQuery<author>("GetAuthorsByType @type", parameter)
                         select new MyAuthor
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
            dataGrid1.ItemsSource = result.ToList();

            var statesList = (from t in context.authors
                              select t.state).Distinct();
            StateColumn.ItemsSource = statesList.ToList();
        }

        /*
            create proc AddAuthor
            (
	            @au_id varchar(20),
	            @au_fname varchar(20),
	            @au_lname varchar(20),
	            @phone varchar(20),
	            @state varchar(2),
                @contract bit
            )
            as
            begin
	            insert into authors(au_id, au_fname, au_lname, phone, state, contract)
	            values
	            (@au_id, @au_fname, @au_lname, @phone, @state, @contract)
            end
        */
        private void StoredProcedure2_Click(object sender, RoutedEventArgs e)
        {
            bool? contract = true;
            // Простейший способ запуска хранимой процедуры, добавляющей автора
            context.AddAuthor("987-12-3456", "Alex", "KARPOV", "12323", "DN", contract);

            RefreshAuthors();
        }

        private void dataGrid1_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //dataGrid1.CanUserSortColumns = false;
        }
    }
}
