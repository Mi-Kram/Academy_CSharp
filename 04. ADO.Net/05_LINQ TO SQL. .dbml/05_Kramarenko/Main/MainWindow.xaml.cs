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

        public MainWindow()
        {
            InitializeComponent();

            pubsDB = new pubsDB();
            dataGrid = ownersDataGrid;

            owners_columnNames = new List<string>();
            owners_columnNames.AddRange(new string[] { "own_id", "pathImg", "name", "address", "phone" });
            dataGrid_columnNames = owners_columnNames;
            cars_columnNames = new List<string>(new string[] { "car_id", "pathImg", "brand", "body_type", "registrDate", "own_id" });
        }

        void SaveTable()
        {
            try
            {
                pubsDB.SubmitChanges();
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
                switch (table_name)
                {
                    case "owners":
                        ownersDataGrid.Visibility = Visibility.Visible;
                        carsDataGrid.Visibility = Visibility.Hidden;
                        dataGrid = ownersDataGrid;
                        dataGrid_columnNames = owners_columnNames;

                        dataGrid.ItemsSource = from own in pubsDB.owners
                                               select own;

                        break;
                    case "cars":
                        carsDataGrid.Visibility = Visibility.Visible;
                        ownersDataGrid.Visibility = Visibility.Hidden;
                        dataGrid = carsDataGrid;
                        dataGrid_columnNames = cars_columnNames;

                        dataGrid.ItemsSource = from car in pubsDB.cars
                                               select car;

                        break;
                    default:
                        MessageBox.Show("Программа читает исключительно таблицы owners и cars.\n" +
                            "Программа будет закрыта.","Info",MessageBoxButton.OK);
                        Close();
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void tables_cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsLoaded) AppdateDataGrid((tables_cmbBox.SelectedItem as TextBlock).Text);
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            AddRow dialog = new AddRow();
            dialog.Headers = dataGrid_columnNames;
            dialog.Owner = this;
            dialog.Title = "Add Row";
            if(dialog.ShowDialog() == true)
            {
                switch ((tables_cmbBox.SelectedItem as TextBox).Text)
                {                    
                    case "owners":
                        {
                            owners own = new owners();
                            own.own_id = dialog.Values[0];
                            own.pathImg = dialog.Values[1];
                            own.name = dialog.Values[2];
                            own.address = dialog.Values[3];
                            own.phone = dialog.Values[4];

                            pubsDB.owners.InsertOnSubmit(own);

                            break;
                        }
                    case "cars":
                        {
                            cars car = new cars();
                            car.car_id = dialog.Values[0];
                            car.pathImg = dialog.Values[1];
                            car.brand = dialog.Values[2];
                            car.body_type = dialog.Values[3];
                            car.registrDate = dialog.Values[4];
                            car.own_id = dialog.Values[5];

                            pubsDB.cars.InsertOnSubmit(car);

                            break;
                        }
                    default: break;
                }

                SaveTable();
            }
        }

        private void RemoveRows_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0 && MessageBox.Show("Удалить выделенные записи?", "Info", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                switch ((tables_cmbBox.SelectedItem as TextBlock).Text)
                {
                    case "owners":
                        {
                            List<owners> owns = new List<owners>();
                            for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
                            {
                                string id = (dataGrid.SelectedItems[i] as owners).own_id;
                                owns = owns.Union((from x in pubsDB.owners select x).Where(x => x.own_id == id)).ToList();
                            }
                            owns = (owns as IEnumerable<owners>).Distinct().ToList();

                            foreach (owners own in owns)
                            {
                                pubsDB.owners.DeleteOnSubmit(own);
                            }

                            break;
                        }
                    case "cars":
                        {
                            List<cars> cars = new List<cars>();
                            for (int i = 0; i < dataGrid.SelectedItems.Count; i++)
                            {
                                string id = (dataGrid.SelectedItems[i] as cars).car_id;
                                cars = cars.Union((from x in pubsDB.cars select x).Where(x => x.car_id == id)).ToList();
                            }
                            cars = (cars as IEnumerable<cars>).Distinct().ToList();

                            foreach (cars car in cars)
                            {
                                pubsDB.cars.DeleteOnSubmit(car);
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
                            owners own = dataGrid.SelectedItem as owners;
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
                            cars car = dataGrid.SelectedItem as cars;
                            dialog.Values = new List<string>(new string[] { car.car_id, car.pathImg, car.brand, car.body_type, car.registrDate, car.own_id });

                            if (dialog.ShowDialog() == true)
                            {
                                car.car_id = dialog.Values[0];
                                car.pathImg = dialog.Values[1];
                                car.brand = dialog.Values[2];
                                car.body_type = dialog.Values[3];
                                car.registrDate = dialog.Values[4];
                                car.own_id = dialog.Values[5];
                            }

                            break;
                        }
                    default: break;
                }
                SaveTable();
            }
        }

        private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            SaveTable();
        }
    }
}
