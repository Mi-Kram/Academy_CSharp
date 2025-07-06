using Main.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
        DepEmplDBDataContext MyDBContext = new DepEmplDBDataContext();
        DataGrid currentDataGrid;
        string pathToPeopleImages;
        bool isCellEditing;
        public MainWindow()
        {
            InitializeComponent();
            
            currentDataGrid = DepssDataGrid;
            pathToPeopleImages = $@"{Environment.CurrentDirectory}\Resources\peopleImages\";
            isCellEditing = false;
        }

        void ApdateDataGrid()
        {
            if(MyDBContext.Employees.Count() > 0 && MyDBContext.Employees.First().PhotoPath.ToLower()
                .Replace('/','\\').StartsWith(pathToPeopleImages.ToLower().Replace('/', '\\')))
            {
                var employees = MyDBContext.Employees.Select(x => x);
                foreach (Employees item in employees)
                {
                    item.PhotoPath = item.PhotoPath.Remove(0, pathToPeopleImages.Length);
                }
            }

            string table = (tables_cmbBox.SelectedItem as TextBlock).Text;

            currentDataGrid.ItemsSource = null;
            if (table == "Departments")
            {
                ObservableCollection<Departments> observableCollection = new ObservableCollection<Departments>(MyDBContext.Departments.Select(x => x));
                CollectionViewSource collection = new CollectionViewSource() { Source = observableCollection };
                collection.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));

                currentDataGrid = DepssDataGrid;
                DepssDataGrid.Visibility = Visibility.Visible;
                EmplsDataGrid.Visibility = Visibility.Hidden;

                currentDataGrid.ItemsSource = collection.View;
            }
            else if (table == "Employees")
            {
                ObservableCollection<Employees> observableCollection = new ObservableCollection<Employees>(MyDBContext.Employees.Select(x => x));
                foreach (Employees item in observableCollection)
                {
                    item.PhotoPath = pathToPeopleImages + item.PhotoPath;
                }
                CollectionViewSource collection = new CollectionViewSource() { Source = observableCollection };
                collection.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));

                currentDataGrid = EmplsDataGrid;
                EmplsDataGrid.Visibility = Visibility.Visible;
                DepssDataGrid.Visibility = Visibility.Hidden;

                currentDataGrid.ItemsSource = collection.View;
            }
            else
            {
                MessageBox.Show($"Недопустимая таблица {table}\nРазрешены только таблицы Departments и Employees\nПриложение будет закрыто!");
                Close();
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApdateDataGrid();
        }

        private void tables_cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded) ApdateDataGrid();
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Departments")
            {
                EditDepartament editDepartament = new EditDepartament();
                if (editDepartament.ShowDialog() == true)
                {
                    string newId = Guid.NewGuid().ToString("N");
                    while (!IsPrimaryKey(newId))
                    {
                        newId = Guid.NewGuid().ToString("N");
                    }

                    Departments departments = new Departments();
                    departments.DepartmentId = newId;
                    departments.Title = editDepartament.Value.Departament;
                    departments.Address = editDepartament.Value.Address;
                    departments.PhoneNumber = editDepartament.Value.Phone;
                    departments.HeadId = editDepartament.Value.HeadID;

                    Add(departments);
                    ApdateDataGrid();
                }
            }
            else
            {
                EditEmployee editEmployee = new EditEmployee();
                if (editEmployee.ShowDialog() == true)
                {
                    string newId = Guid.NewGuid().ToString("N");
                    while (!IsPrimaryKey(newId))
                    {
                        newId = Guid.NewGuid().ToString("N");
                    }

                    Employees employees = new Employees();
                    employees.Employee_id = newId;
                    employees.FirstName = editEmployee.Value.FName;
                    employees.LastName = editEmployee.Value.LName;
                    employees.Age = editEmployee.Value.Age;
                    employees.Address = editEmployee.Value.Address;
                    employees.PhotoPath = editEmployee.Value.PhotoPath;

                    Add(employees);
                    ApdateDataGrid();
                }
            }
        }
        void Add(Departments departments)
        {
            try
            {
                MyDBContext.Departments.InsertOnSubmit(departments);
                SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MyDBContext = new DepEmplDBDataContext();
            }
        }
        void Add(Employees employees)
        {
            try
            {
                MyDBContext.Employees.InsertOnSubmit(employees);
                SubmitChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MyDBContext = new DepEmplDBDataContext();
            }
        }

        void SubmitChanges()
        {
            if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Departments")
            {
                MyDBContext.SubmitChanges();
            }
            else
            {
                foreach (Employees item in currentDataGrid.Items)
                {
                    if (item.PhotoPath.ToLower().Replace('/', '\\').StartsWith(pathToPeopleImages.ToLower().Replace('/', '\\')))
                        item.PhotoPath = item.PhotoPath.Remove(0, pathToPeopleImages.Length);
                }

                MyDBContext.SubmitChanges();
            }
        }
        bool IsPrimaryKey(string key)
        {
            if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Departments")
            {
                foreach (Departments item in currentDataGrid.Items)
                {
                    if (item.DepartmentId == key) return false;
                }
            }
            else
            {
                foreach (Employees item in currentDataGrid.Items)
                {
                    if (item.Employee_id == key) return false;
                }
            }

            return true;
        }

        private void EditRow_Click(object sender, RoutedEventArgs e)
        {
            if (currentDataGrid.SelectedItems.Count > 0)
            {
                if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Departments")
                {
                    EditDepartament editDepartament = new EditDepartament();
                    Departments departments = currentDataGrid.SelectedItem as Departments;
                    editDepartament.Value = (departments.Title, departments.Address, departments.PhoneNumber, departments.HeadId);
                    if (editDepartament.ShowDialog() == true)
                    {
                        try
                        {
                            departments.Title = editDepartament.Value.Departament;
                            departments.Address = editDepartament.Value.Address;
                            departments.PhoneNumber = editDepartament.Value.Phone;
                            departments.HeadId = editDepartament.Value.HeadID;

                            SubmitChanges();
                            ApdateDataGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            MyDBContext = new DepEmplDBDataContext();
                        }
                    }
                }
                else
                {
                    EditEmployee editEmployee = new EditEmployee();
                    Employees employee = currentDataGrid.SelectedItem as Employees;
                    string safeFileName = employee.PhotoPath;
                    if (safeFileName.ToLower().Replace('/', '\\').StartsWith(pathToPeopleImages.ToLower().Replace('/', '\\')))
                        safeFileName = safeFileName.Remove(0, pathToPeopleImages.Length);
                    editEmployee.Value = (employee.FirstName, employee.LastName, employee.Age.Value, employee.Address, safeFileName);

                    if (editEmployee.ShowDialog() == true)
                    {
                        try
                        {
                            employee.FirstName = editEmployee.Value.FName;
                            employee.LastName = editEmployee.Value.LName;
                            employee.Age = editEmployee.Value.Age;
                            employee.Address = editEmployee.Value.Address;
                            employee.PhotoPath = editEmployee.Value.PhotoPath;

                            SubmitChanges();
                            ApdateDataGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            MyDBContext = new DepEmplDBDataContext();
                        }
                    }
                }
            }
        }

        void RemoveSelectedRows()
        {
            if (currentDataGrid.SelectedItems.Count > 0)
            {
                if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Departments")
                {
                    bool flag = false;
                    foreach (Departments item in currentDataGrid.SelectedItems)
                    {
                        if (!MyDBContext.DepartmentsEmployees.Select(x => x.DepartmentId).Contains(item.DepartmentId))
                            MyDBContext.Departments.DeleteOnSubmit(item);
                        else flag = true;
                    }

                    try
                    {
                        SubmitChanges();
                        ApdateDataGrid();
                        if (flag) MessageBox.Show("Некоторые записи не были удалены, т.к. их используют другие таблицы!", "INFO", MessageBoxButton.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MyDBContext = new DepEmplDBDataContext();
                        ApdateDataGrid();
                    }
                }
                else
                {
                    bool flag = false;
                    string safeFileName = string.Empty;
                    string fileName = string.Empty;
                        
                    foreach(Employees item in currentDataGrid.SelectedItems)
                    {
                        if (!MyDBContext.DepartmentsEmployees.Select(x => x.EmployeeId).Contains(item.Employee_id))
                            MyDBContext.Employees.DeleteOnSubmit(item);
                        else flag = true;
                    }

                    try
                    {
                        SubmitChanges();
                        ApdateDataGrid();
                        if (flag) MessageBox.Show("Некоторые записи не были удалены, т.к. их используют другие таблицы!", "INFO", MessageBoxButton.OK);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MyDBContext = new DepEmplDBDataContext();
                        ApdateDataGrid();
                    }
                }
            }
        }

        private void RemoveRows_Click(object sender, RoutedEventArgs e)
        {
            RemoveSelectedRows();
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            isCellEditing = true;
            //e.Cancel = true;
        }

        private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            isCellEditing = false;
            SubmitChanges();
            ApdateDataGrid();
        }

        private void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(!isCellEditing && e.Key == Key.Delete)
            {
                RemoveSelectedRows();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (isCellEditing)
            {
                SubmitChanges();
            }
        }

        private void SortTable_Click(object sender, RoutedEventArgs e)
        {
            SortDialog dialog = new SortDialog();
            dialog.Owner = this;

            if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Departments")
            {
                dialog.ColumnNames = new List<string>(new string[] { "Название департамента", "Адрес департамента", "Номер телефона" });
                if(dialog.ShowDialog() == true)
                {
                    currentDataGrid.ItemsSource = null;

                    //ObservableCollection<Departments> observableCollection = new ObservableCollection<Departments>(MyDBContext.Departments.Select(x => x));
                    //CollectionViewSource collection = new CollectionViewSource() { Source = observableCollection };

                    List<Departments> lst = MyDBContext.Departments.Select(x => x).ToList();

                    switch (dialog.SelectedIndex)
                    {
                        case 0:
                            lst = lst.OrderBy(x => x.Title).ToList();
                            break;
                        case 1:
                            lst = lst.OrderBy(x => x.Address).ToList();
                            break;
                        case 2:
                            lst = lst.OrderBy(x => x.PhoneNumber).ToList();
                            break;
                        default:
                            MessageBox.Show("Ошибка. Не верный индекс!");
                            return;
                    }

                    //currentDataGrid.ItemsSource = collection.View;
                    currentDataGrid.ItemsSource = lst;
                }
            }
            else
            {
                dialog.ColumnNames = new List<string>(new string[] { "Имя", "Фамилия", "Возраст", "Адрес" });
                if (dialog.ShowDialog() == true)
                {
                    currentDataGrid.ItemsSource = null;

                    //ObservableCollection<Employees> observableCollection = new ObservableCollection<Employees>(MyDBContext.Employees.Select(x => x));
                    //CollectionViewSource collection = null;

                    List<Employees> lst = MyDBContext.Employees.Select(x => x).ToList();
                    
                    switch (dialog.SelectedIndex)
                    {
                        case 0:
                            lst = lst.OrderBy(x => x.FirstName).ToList();
                            break;
                        case 1:
                            lst = lst.OrderBy(x => x.LastName).ToList();
                            break;
                        case 2:
                            lst = lst.OrderBy(x => x.Age).ToList();
                            break;
                        case 3:
                            lst = lst.OrderBy(x => x.Address).ToList();
                            break;
                        default:
                            MessageBox.Show("Ошибка. Не верный индекс!");
                            return;
                    }

                    //currentDataGrid.ItemsSource = collection.View;
                    currentDataGrid.ItemsSource = lst;
                }
            }
        }

        private void FilterTable_Click(object sender, RoutedEventArgs e)
        {
            FilterTable dialog = new FilterTable();
            dialog.Owner = this;

            if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Departments")
            {
                dialog.ColumnNames = new List<string>(new string[] { "Название департамента", "Адрес департамента", "Номер телефона" });
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    currentDataGrid.ItemsSource = null;

                    //ObservableCollection<Departments> observableCollection = new ObservableCollection<Departments>(MyDBContext.Departments.Select(x => x));
                    //CollectionViewSource collection = null;

                    List<Departments> lst = MyDBContext.Departments.Select(x => x).ToList();

                    switch (dialog.SelectedIndex)
                    {
                        case 0:
                            lst = lst.Where(x => x.Title == dialog.Values[dialog.SelectedIndex].Value.Text).ToList();
                            break;
                        case 1:
                            lst = lst.Where(x => x.Address == dialog.Values[dialog.SelectedIndex].Value.Text).ToList();
                            break;
                        case 2:
                            lst = lst.Where(x => x.PhoneNumber == dialog.Values[dialog.SelectedIndex].Value.Text).ToList();
                            break;
                        default:
                            MessageBox.Show("Ошибка. Не верный индекс!");
                            return;
                    }

                    //currentDataGrid.ItemsSource = collection.View;
                    currentDataGrid.ItemsSource = lst;
                }
                else if(dialogResult == false)
                {
                    if (dialog.Refresh) ApdateDataGrid();
                }
            }
            else
            {
                dialog.ColumnNames = new List<string>(new string[] { "Имя", "Фамилия", "Возраст", "Адрес" });
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    currentDataGrid.ItemsSource = null;

                    //ObservableCollection<Employees> observableCollection = new ObservableCollection<Employees>(MyDBContext.Employees.Select(x => x));
                    //CollectionViewSource collection = null;

                    List<Employees> lst = MyDBContext.Employees.Select(x => x).ToList();

                    switch (dialog.SelectedIndex)
                    {
                        case 0:
                            lst = lst.Where(x => x.FirstName == dialog.Values[dialog.SelectedIndex].Value.Text).ToList();
                            break;
                        case 1:
                            lst = lst.Where(x => x.LastName == dialog.Values[dialog.SelectedIndex].Value.Text).ToList();
                            break;
                        case 2:
                            lst = lst.Where(x => x.Age.ToString() == dialog.Values[dialog.SelectedIndex].Value.Text).ToList();
                            break;
                        case 3:
                            lst = lst.Where(x => x.Address == dialog.Values[dialog.SelectedIndex].Value.Text).ToList();
                            break;
                        default:
                            MessageBox.Show("Ошибка. Не верный индекс!");
                            return;
                    }

                    //currentDataGrid.ItemsSource = collection.View;
                    currentDataGrid.ItemsSource = lst;
                }
                else if (dialogResult == false)
                {
                    if (dialog.Refresh) ApdateDataGrid();
                }

            }
        }

        private void GroupTable_Click(object sender, RoutedEventArgs e)
        {
            GroupDialog dialog = new GroupDialog();
            dialog.Owner = this;

            if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Departments")
            {
                dialog.ColumnNames = new List<string>(new string[] { "Название департамента", "Адрес департамента", "Номер телефона" });
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    currentDataGrid.ItemsSource = null;

                    ObservableCollection<Departments> observableCollection = new ObservableCollection<Departments>(MyDBContext.Departments.Select(x => x));
                    CollectionViewSource collection = new CollectionViewSource() { Source = observableCollection };

                    switch (dialog.SelectedIndex)
                    {
                        case 0:
                            collection.GroupDescriptions.Add(new PropertyGroupDescription("Title"));
                            break;
                        case 1:
                            collection.GroupDescriptions.Add(new PropertyGroupDescription("Address"));
                            break;
                        case 2:
                            collection.GroupDescriptions.Add(new PropertyGroupDescription("PhoneNumber"));
                            break;
                        default:
                            MessageBox.Show("Ошибка. Не верный индекс!");
                            return;
                    }

                    currentDataGrid.ItemsSource = collection.View;
                }
                else if (dialogResult == false)
                {
                    if (dialog.Refresh) ApdateDataGrid();
                }
            }
            else
            {
                dialog.ColumnNames = new List<string>(new string[] { "Имя", "Фамилия", "Возраст", "Адрес" });
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    currentDataGrid.ItemsSource = null;

                    ObservableCollection<Employees> observableCollection = new ObservableCollection<Employees>(MyDBContext.Employees.Select(x => x));
                    CollectionViewSource collection = new CollectionViewSource() { Source = observableCollection };

                    switch (dialog.SelectedIndex)
                    {
                        case 0:
                            collection.GroupDescriptions.Add(new PropertyGroupDescription("FirstName"));
                            break;
                        case 1:
                            collection.GroupDescriptions.Add(new PropertyGroupDescription("LastName"));
                            break;
                        case 2:
                            collection.GroupDescriptions.Add(new PropertyGroupDescription("Age"));
                            break;
                        case 3:
                            collection.GroupDescriptions.Add(new PropertyGroupDescription("Address"));
                            break;
                        default:
                            MessageBox.Show("Ошибка. Не верный индекс!");
                            return;
                    }

                    currentDataGrid.ItemsSource = collection.View;
                }
                else if (dialogResult == false)
                {
                    if (dialog.Refresh) ApdateDataGrid();
                }

            }
        }

        private void FindCell_Click(object sender, RoutedEventArgs e)
        {
            FindDialog dialog = new FindDialog();
            dialog.Owner = this;

            if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Departments")
            {
                dialog.ColumnNames = new List<string>(new string[] { "Название департамента", "Адрес департамента", "Номер телефона" });
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    currentDataGrid.ItemsSource = null;

                    List<Departments> lst = new List<Departments>();

                    foreach (int index in dialog.SelectedIndexes)
                    {
                        switch (index)
                        {
                            case 0:
                                lst = lst.Union(MyDBContext.Departments.Where(x => x.Title.ToLower().Contains(dialog.FindStr.ToLower()))).ToList();
                                break;
                            case 1:
                                lst = lst.Union(MyDBContext.Departments.Where(x => x.Address.ToLower().Contains(dialog.FindStr.ToLower()))).ToList();
                                break;
                            case 2:
                                lst = lst.Union(MyDBContext.Departments.Where(x => x.PhoneNumber.ToLower().Contains(dialog.FindStr.ToLower()))).ToList();
                                break;
                            default:
                                MessageBox.Show("Ошибка. Не верный индекс!");
                                return;
                        }
                    }

                    currentDataGrid.ItemsSource = lst;
                }
                else if (dialogResult == false)
                {
                    if (dialog.Refresh) ApdateDataGrid();
                }
            }
            else
            {
                dialog.ColumnNames = new List<string>(new string[] { "Имя", "Фамилия", "Возраст", "Адрес" });
                bool? dialogResult = dialog.ShowDialog();
                if (dialogResult == true)
                {
                    currentDataGrid.ItemsSource = null;

                    List<Employees> lst = new List<Employees>();

                    foreach (int index in dialog.SelectedIndexes)
                    {
                        switch (index)
                        {
                            case 0:
                                lst = lst.Union(MyDBContext.Employees.Where(x => x.FirstName.ToLower().Contains(dialog.FindStr.ToLower()))).ToList();
                                break;
                            case 1:
                                lst = lst.Union(MyDBContext.Employees.Where(x => x.LastName.ToLower().Contains(dialog.FindStr.ToLower()))).ToList();
                                break;
                            case 2:
                                lst = lst.Union(MyDBContext.Employees.Where(x => x.Age.ToString().ToLower().Contains(dialog.FindStr.ToLower()))).ToList();
                                break;
                            case 3:
                                lst = lst.Union(MyDBContext.Employees.Where(x => x.Address.ToLower().Contains(dialog.FindStr.ToLower()))).ToList();
                                break;
                            default:
                                MessageBox.Show("Ошибка. Не верный индекс!");
                                return;
                        }
                    }

                    currentDataGrid.ItemsSource = lst;
                }
                else if (dialogResult == false)
                {
                    if (dialog.Refresh) ApdateDataGrid();
                }

            }
        }

        private void UnionTables_Click(object sender, RoutedEventArgs e)
        {
            if(UnionDataGrid.Visibility == Visibility.Hidden)
            {
                UnionDataGrid.Visibility = Visibility.Visible;
                currentDataGrid.Visibility = Visibility.Hidden;

                tables_cmbBox.IsEnabled = false;
                editMenuItem.IsEnabled = false;
                sortingMenuItem.IsEnabled = false;
                findMeniItem.IsEnabled = false;

                UnionMenuItem.Header = "Close";

                UnionDataGrid.ItemsSource = MyDBContext.GetEmployeesWithDepartmentTitle().ToList(); ;
            }
            else
            {
                currentDataGrid.Visibility = Visibility.Visible;
                UnionDataGrid.Visibility = Visibility.Hidden;

                UnionDataGrid.ItemsSource = null;

                tables_cmbBox.IsEnabled = true;
                editMenuItem.IsEnabled = true;
                sortingMenuItem.IsEnabled = true;
                findMeniItem.IsEnabled = true;

                UnionMenuItem.Header = "Union tables";
            }
        }

        private void UnionDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
