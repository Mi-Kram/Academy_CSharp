using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
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

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        pubsDB pubsDB;
        //List<string> owners_columnNames, cars_columnNames;
        //List<string> dataGrid_columnNames;
        //DataGrid dataGrid;

        public MainWindow()
        {
            InitializeComponent();

            pubsDB = new pubsDB();
        }

        /// <summary>
        /// Позакать магазины, которые продают книги в жанре 'Business' (поздапросы и JOIN)
        /// </summary>
        private void Task1_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.Columns.Clear();

            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Stor names", Binding = new Binding() });

            // под запросы

            var table = pubsDB.stores.Where(stor => pubsDB.sales.Where(sale => pubsDB.titles.
            Where(book => book.type == "Business").Select(book => book.title_id).Contains(sale.title_id)).
            Select(sale => sale.stor_id).Contains(stor.stor_id)).Select(stor => stor.stor_name);

            //var table = from stor in pubsDB.stores
            //            where (from sale in pubsDB.sales
            //            where (from book in pubsDB.titles
            //            where book.type == "Business" select book.title_id)
            //            .Contains(sale.title_id) select sale.stor_id)
            //            .Contains(stor.stor_id) select stor.stor_name;

            // Join

            //var table = pubsDB.stores.Join(pubsDB.sales.Join(pubsDB.titles.Where(book => book.type == "Business")
            //    .Select(book => book.title_id), sale => sale.title_id, title_id => title_id, 
            //    (sale, title_id) => sale.stor_id), stor => stor.stor_id, stor_id => stor_id, 
            //    (stor, stor_id) => stor.stor_name).Distinct();

            //var table = (from stor in pubsDB.stores
            //            join sale in pubsDB.sales on stor.stor_id equals sale.stor_id
            //            join book in pubsDB.titles.Where(book => book.type == "Business")
            //            on sale.title_id equals book.title_id
            //            select stor.stor_name).Distinct();

            dataGrid.ItemsSource = table;
        }

        /// <summary>
        /// Показать авторов самых дешёвых книг
        /// </summary>
        private void Task2_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.Columns.Clear();

            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "First name", Binding = new Binding("au_fname") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Last name", Binding = new Binding("au_lname") });

            // под запросы

            var table = pubsDB.authors.Where(au => (pubsDB.titleauthor
                        .Where(ta => pubsDB.titles.Where(book => book.price ==
                        pubsDB.titles.Select(price => price.price).Min()).Select(book => book.title_id)
                        .Contains(ta.title_id))
                        .Select(ta => ta.au_id)).Contains(au.au_id))
                        .Select(au => new { au.au_fname, au.au_lname });

            //var table = from au in pubsDB.authors
            //            where (from ta in pubsDB.titleauthor
            //                   where (from book in pubsDB.titles
            //                          where book.price == (from price in pubsDB.titles select price.price).Min() 
            //                          select book.title_id).Contains(ta.title_id)
            //                   select ta.au_id).Contains(au.au_id)
            //            select new { au.au_fname, au.au_lname };

            // Join

            //var table = pubsDB.authors.Join(pubsDB.titleauthor.Join(pubsDB.titles
            //    .Where(book => book.price == pubsDB.titles.Select(price => price.price).Min())
            //    .Select(book => book.title_id), ta => ta.title_id, book_id => book_id, 
            //    (ta, book_id) => ta.au_id), au => au.au_id, ta_id => ta_id, 
            //    (au, ta_id) => new { au.au_fname, au.au_lname });

            //var table = from au in pubsDB.authors
            //            join ta in pubsDB.titleauthor on au.au_id equals ta.au_id
            //            join book in pubsDB.titles.Where(book => book.price == pubsDB.titles
            //            .Select(price => price.price).Min()) on ta.title_id equals book.title_id
            //            select new { au.au_fname, au.au_lname };

            dataGrid.ItemsSource = table;
        }

        /// <summary>
        /// Показать издательства, которые НЕ публикуют книги в жанре 'Business'
        /// </summary>
        private void Task3_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.Columns.Clear();

            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Publisher names", Binding = new Binding() });


            // под запросы

            var table = pubsDB.publishers.Select(p => p.pub_name).Except(pubsDB.publishers
                .Where(p => pubsDB.titles.Where(book => book.type == "Business")
                .Select(book => book.pub_id).Contains(p.pub_id)).Select(p => p.pub_name));

            //var table = (from p in pubsDB.publishers select p.pub_name)
            //            .Except(from p in pubsDB.publishers
            //                    where (from book in pubsDB.titles
            //                        where book.type == "Business"
            //                        select book.pub_id).Contains(p.pub_id)
            //                    select p.pub_name);

            // Join

            //var table = pubsDB.publishers.Select(p => p.pub_name)
            //    .Except(pubsDB.publishers.Join(pubsDB.titles.Where(book => book.type == "Business")
            //    .Select(book => book.pub_id), p => p.pub_id, pub_id => pub_id, (p, pub_id) => p.pub_name));

            //var table = (from p in pubsDB.publishers select p.pub_name)
            //            .Except(from p in pubsDB.publishers
            //                    join pub_id in pubsDB.titles.Where(book => book.type == "Business")
            //                    .Select(book => book.pub_id) on p.pub_id equals pub_id select p.pub_name);

            dataGrid.ItemsSource = table;
        }

        /// <summary>
        /// Показать жанры книг, которые продаются в магазинах штата 'CA'
        /// </summary>
        private void Task4_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.Columns.Clear();

            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Type of books", Binding = new Binding() });

            // под запросы

            var table = pubsDB.titles.Where(book => pubsDB.sales
                        .Where(sale => pubsDB.stores.Where(stor => stor.state == "CA")
                        .Select(stor => stor.stor_id).Contains(sale.stor_id))
                        .Select(sale => sale.title_id).Contains(book.title_id))
                        .Select(book => book.type).Distinct();

            //var table = (from book in pubsDB.titles
            //             where (from sale in pubsDB.sales
            //                    where (from stor in pubsDB.stores 
            //                           where stor.state == "CA" 
            //                           select stor.stor_id).Contains(sale.stor_id) 
            //                    select sale.title_id).Contains(book.title_id)
            //             select book.type).Distinct();

            // Join

            //var table = pubsDB.titles.Join(pubsDB.sales.Join(pubsDB.stores.Where(stor => stor.state == "CA")
            //    .Select(stor => stor.stor_id), sale => sale.stor_id, stor_id => stor_id, 
            //    (sale, stor_id) => sale.title_id), book => book.title_id, title_id => title_id, 
            //    (book, title_id) => book.type).Distinct();

            //var table = (from book in pubsDB.titles
            //             join sale in pubsDB.sales on book.title_id equals sale.title_id
            //             join stor in from st in pubsDB.stores where st.state == "CA" select st
            //             on sale.stor_id equals stor.stor_id
            //             select book.type).Distinct();

            dataGrid.ItemsSource = table;
        }

        /// <summary>
        /// Показкать самую раннюю и самую позднюю опубликованные книги в жанре 'Business'
        /// </summary>
        private void Task5_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.Columns.Clear();

            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Titles", Binding = new Binding("title") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Date", Binding = new Binding("pubdate") });

            dataGrid.ItemsSource = pubsDB.titles.Where(book => book.type == "Business" &&
                     book.pubdate == pubsDB.titles.Where(b => b.type == "Business")
                     .Select(b => b.pubdate).Min()).Select(book => new { book.title, book.pubdate });

            test dialog = new test();
            dialog.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Titles", Binding = new Binding("title") });
            dialog.dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Date", Binding = new Binding("pubdate") });
            dialog.dataGrid.ItemsSource = pubsDB.titles.Where(book => book.type == "Business" &&
                                        book.pubdate == pubsDB.titles.Where(b => b.type == "Business")
                                        .Select(b => b.pubdate).Max()).Select(book => new { book.title, book.pubdate });

            Title = "Самые дешёвые книги";
            dialog.ShowDialog();
            Title = "MainWindow";
            dataGrid.ItemsSource = null;
        }

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
