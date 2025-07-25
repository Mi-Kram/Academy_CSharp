﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DataGrid_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool CarsFilter(object obj)
        {
            return true;
        }

        public MainWindow()
        {
            InitializeComponent();

            List<Car> carsList = new List<Car>
            {
                new Car { ImagePath=@"Images/bmw_x6.jpg", Model="X6", Brand="BMW", Category="Crossover", BuyDate=DateTime.Now, Price=54990, IsForSale=true },
                new Car { ImagePath=@"Images/bmw_m6.jpg", Model="M6", Brand="BMW", Category="Sedan", BuyDate=DateTime.Today, Price=49990, IsForSale=false },
                new Car { ImagePath=@"Images/audi_a4.jpg", Model="A4", Brand="Audi", Category="Sedan", BuyDate=DateTime.Parse("10-12-1999"), Price=29990, IsForSale=true },
                new Car { ImagePath=@"Images/audi_q7.jpg", Model="Q7", Brand="Audi", Category="Crossover", BuyDate=DateTime.Parse("4-11-2001"), Price=58997, IsForSale=false  }
            };

            ListCollectionView collection = new ListCollectionView(carsList);

            //collection.GroupDescriptions.Add(new PropertyGroupDescription("Brand"));
            //collection.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
            
            collection.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));

            //collection.Filter = new Predicate<object>(item => ((Car)item).Price>30000);
            collection.Filter = new Predicate<object>(CarsFilter);

            dataGrid1.ItemsSource = collection;
            //dataGrid1.ItemsSource = carsList;

            // Фиксированный размер столбца
            //dataGrid1.Columns[0].Width = new DataGridLength(300);

            // Размер берётся из заголовка
            //dataGrid1.Columns[0].Width = DataGridLength.SizeToHeader;

            // Размер берётся из содержимого
            //dataGrid1.Columns[0].Width = DataGridLength.SizeToCells;

            // Размер берётся из содержимого или залоговка
            dataGrid1.Columns[0].Width = DataGridLength.Auto;

            dataGrid1.Columns[0].MaxWidth = 300;
            dataGrid1.Columns[0].MinWidth = 100;

            dataGrid1.CanUserAddRows = false;
            dataGrid1.CanUserDeleteRows = false;

            dataGrid1.SelectionMode = DataGridSelectionMode.Extended;
            dataGrid1.SelectionUnit = DataGridSelectionUnit.FullRow;
        }

        private void dataGrid1_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Console.WriteLine("BeginningEdit: {0}", ((DataGrid)sender).CurrentCell.Item);

            //e.Cancel = true;
        }

        private void dataGrid1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Console.WriteLine("CellEditEnding Old: {0}", ((DataGrid)sender).CurrentCell.Item);
            Object element = e.EditingElement;
            string newValue = null;

            if (element is TextBox)
                newValue = ((TextBox)element).Text;

            if (element is ComboBox)
                newValue = ((ComboBox)element).Text;

            Console.WriteLine("CellEditEnding New: {0}", newValue);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.CanUserAddRows = true;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            dataGrid1.CanUserDeleteRows = true;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> items = new ObservableCollection<string> { "One", "Two", "Three", "Crossover", "Sedan" };
            ComboBoxColumn1.ItemsSource = items;
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dataGrid1.SelectedItem != null)
                selectedItem.Content = dataGrid1.SelectedItem.ToString();
        }
    }
}
