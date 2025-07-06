using Main.Models;
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
        List<string> owners_columnNames, cars_columnNames;
        List<string> dataGrid_columnNames;
        DataGrid dataGrid;
        bool isCellEditig;

        public MainWindow()
        {
            InitializeComponent();

            pubsDB = new pubsDB();
            dataGrid = ownersDataGrid;

            owners_columnNames = new List<string>();
            owners_columnNames.AddRange(new string[] { "own_id", "pathImg", "name", "address", "phone" });
            dataGrid_columnNames = owners_columnNames;
            cars_columnNames = new List<string>(new string[] { "car_id", "pathImg", "brand", "body_type", "registrDate", "own_id" });
            isCellEditig = false;
        }

        void SaveTable()
        {
            try
            {
                pubsDB.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Ошибка. Не удалось изменить таблицу.");
                pubsDB = new pubsDB();
            }

            AppdateDataGrid((tables_cmbBox.SelectedItem as TextBlock).Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppdateDataGrid((tables_cmbBox.SelectedItem as TextBlock).Text);
        }

        void AppdateDataGrid(string table_name)
        {
            try
            {
                dataGrid.ItemsSource = null;
                switch (table_name)
                {
                    case "owners":
                        ownersDataGrid.Visibility = Visibility.Visible;
                        carsDataGrid.Visibility = Visibility.Hidden;
                        dataGrid = ownersDataGrid;
                        dataGrid_columnNames = owners_columnNames;

                        dataGrid.ItemsSource = (from own in pubsDB.Owners
                                                select own).ToList();

                        break;
                    case "cars":
                        carsDataGrid.Visibility = Visibility.Visible;
                        ownersDataGrid.Visibility = Visibility.Hidden;
                        dataGrid = carsDataGrid;
                        dataGrid_columnNames = cars_columnNames;

                        dataGrid.ItemsSource = (from car in pubsDB.Cars
                                                select car).ToList();

                        break;
                    default:
                        MessageBox.Show("Программа читает исключительно таблицы owners и cars.\n" +
                            "Программа будет закрыта.", "Info", MessageBoxButton.OK);
                        Close();
                        break;
                }
                dataGrid.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void tables_cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded) AppdateDataGrid((tables_cmbBox.SelectedItem as TextBlock).Text);
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            AddNewRow();
        }

        void AddNewRow()
        {
            AddRow dialog = new AddRow();
            dialog.Headers = dataGrid_columnNames;
            dialog.Owner = this;
            dialog.Title = "Add Row";
            if (dialog.ShowDialog() == true)
            {
                switch ((tables_cmbBox.SelectedItem as TextBlock).Text)
                {
                    case "owners":
                        {
                            Owner own = new Owner();
                            own.own_id = dialog.Values[0];
                            own.pathImg = dialog.Values[1];
                            own.name = dialog.Values[2];
                            own.address = dialog.Values[3];
                            own.phone = dialog.Values[4];

                            pubsDB.Owners.Add(own);

                            break;
                        }
                    case "cars":
                        {
                            Car car = new Car();
                            car.car_id = dialog.Values[0];
                            car.pathImg = dialog.Values[1];
                            car.brand = dialog.Values[2];
                            car.body_type = dialog.Values[3];
                            car.registrDate = dialog.Values[4];
                            car.own = (pubsDB.Owners.Where(own => own.own_id == dialog.Values[5])).First();

                            pubsDB.Cars.Add(car);

                            break;
                        }
                    default: break;
                }

                SaveTable();
            }
        }

        private void RemoveRows_Click(object sender, RoutedEventArgs e)
        {
            RemoveSelectedRows();
        }

        void RemoveSelectedRows()
        {
            if (dataGrid.SelectedItems.Count > 0 && MessageBox.Show("Удалить выделенные записи?", "Info", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                switch ((tables_cmbBox.SelectedItem as TextBlock).Text)
                {
                    case "owners":
                        {
                            List<Owner> owns = new List<Owner>();
                            foreach (Owner item in dataGrid.SelectedItems)
                            {
                                owns.Add(item);
                            }

                            foreach (Owner own in owns)
                            {
                                pubsDB.Owners.Remove(own);
                            }

                            break;
                        }
                    case "cars":
                        {
                            List<Car> cars = new List<Car>();
                            foreach (Car item in dataGrid.SelectedItems)
                            {
                                cars.Add(item);
                            }

                            foreach (Car own in cars)
                            {
                                pubsDB.Cars.Remove(own);
                            }

                            break;
                        }
                    default: break;
                }

                SaveTable();
            }
        }

        private void EditRow_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                AddRow dialog = new AddRow();
                dialog.Headers = dataGrid_columnNames;
                dialog.Owner = this;
                dialog.Title = "Edit Row";

                switch ((tables_cmbBox.SelectedItem as TextBlock).Text)
                {
                    case "owners":
                        {
                            Owner own = dataGrid.SelectedItem as Owner;
                            dialog.Values = new List<string>(new string[] { own.own_id, own.pathImg, own.name, own.address, own.phone });

                            if (dialog.ShowDialog() == true)
                            {
                                own.own_id = dialog.Values[0];
                                own.pathImg = dialog.Values[1];
                                own.name = dialog.Values[2];
                                own.address = dialog.Values[3];
                                own.phone = dialog.Values[4];
                            }

                            break;
                        }
                    case "cars":
                        {
                            Car car = dataGrid.SelectedItem as Car;
                            dialog.Values = new List<string>(new string[] { car.car_id, car.pathImg, car.brand, car.body_type, car.registrDate, car.own.own_id });

                            if (dialog.ShowDialog() == true)
                            {
                                car.car_id = dialog.Values[0];
                                car.pathImg = dialog.Values[1];
                                car.brand = dialog.Values[2];
                                car.body_type = dialog.Values[3];
                                car.registrDate = dialog.Values[4];
                                car.own = (pubsDB.Owners.Where(own => own.own_id == dialog.Values[5])).First();
                            }

                            break;
                        }
                    default: break;
                }
                SaveTable();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                pubsDB.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Ошибка. Не удалось изменить таблицу.");
                pubsDB = new pubsDB();
                e.Cancel = true;
            }
        }

        private void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.N)
            {
                AddNewRow();
                e.Handled = true;
            }
            else if (!isCellEditig && e.Key == Key.Delete)
            {
                RemoveSelectedRows();
                e.Handled = true;
            }
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isCellEditig = true;
        }

        private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            isCellEditig = false;
            SaveTable();
        }
    }
}
