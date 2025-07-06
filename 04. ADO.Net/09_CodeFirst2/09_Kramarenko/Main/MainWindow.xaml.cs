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
        EmployeesProjectsDBContext myContext;
        List<string> employees_columnNames, projects_columnNames;
        List<string> dataGrid_columnNames;
        DataGrid dataGrid;
        bool isCellEditig;

        public MainWindow()
        {
            InitializeComponent();

            myContext = new EmployeesProjectsDBContext();
            dataGrid = employeesDataGrid;

            employees_columnNames = new List<string>();
            employees_columnNames.AddRange(new string[] { "name", "address", "phone" });
            dataGrid_columnNames = employees_columnNames;
            projects_columnNames = new List<string>(new string[] { "name", "startDate", "endDate", "description" });
            isCellEditig = false;
        }

        void SaveTable()
        {
            try
            {
                myContext.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Ошибка. Не удалось изменить таблицу.");
                myContext = new EmployeesProjectsDBContext();
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
                    case "employees":
                        employeesDataGrid.Visibility = Visibility.Visible;
                        projectsDataGrid.Visibility = Visibility.Hidden;
                        dataGrid = employeesDataGrid;
                        dataGrid_columnNames = employees_columnNames;

                        dataGrid.ItemsSource = (from own in myContext.Employees
                                                select own).ToList();

                        break;
                    case "projects":
                        projectsDataGrid.Visibility = Visibility.Visible;
                        employeesDataGrid.Visibility = Visibility.Hidden;
                        dataGrid = projectsDataGrid;
                        dataGrid_columnNames = projects_columnNames;

                        dataGrid.ItemsSource = (from car in myContext.Projects
                                                select car).ToList();

                        break;
                    default:
                        MessageBox.Show("Программа читает исключительно таблицы employees и projects.\n" +
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
                    case "employees":
                        {
                            Employee employee = new Employee();
                            employee.name = dialog.Values[0];
                            employee.address = dialog.Values[1];
                            employee.phone = dialog.Values[2];

                            myContext.Employees.Add(employee);

                            break;
                        }
                    case "projects":
                        {
                            Project project = new Project();
                            project.name = dialog.Values[0];
                            project.startDate = dialog.Values[1];
                            project.endDate = dialog.Values[2];
                            project.description = dialog.Values[3];

                            myContext.Projects.Add(project);

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
                    case "employees":
                        {
                            List<Employee> employees = new List<Employee>();
                            foreach (Employee item in dataGrid.SelectedItems)
                            {
                                employees.Add(item);
                            }

                            foreach (Employee own in employees)
                            {
                                myContext.Employees.Remove(own);
                            }

                            break;
                        }
                    case "projects":
                        {
                            List<Project> projects = new List<Project>();
                            foreach (Project item in dataGrid.SelectedItems)
                            {
                                projects.Add(item);
                            }

                            foreach (Project own in projects)
                            {
                                myContext.Projects.Remove(own);
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
                    case "employees":
                        {
                            Employee employee = dataGrid.SelectedItem as Employee;
                            dialog.Values = new List<string>(new string[] { employee.name, employee.address, employee.phone });

                            if (dialog.ShowDialog() == true)
                            {
                                employee.name = dialog.Values[0];
                                employee.address = dialog.Values[1];
                                employee.phone = dialog.Values[2];
                            }

                            break;
                        }
                    case "projects":
                        {
                            Project project = dataGrid.SelectedItem as Project;
                            dialog.Values = new List<string>(new string[] { project.name, project.startDate, project.endDate, project.description });

                            if (dialog.ShowDialog() == true)
                            {
                                project.name = dialog.Values[0];
                                project.startDate = dialog.Values[1];
                                project.endDate = dialog.Values[2];
                                project.description = dialog.Values[3];
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
                myContext.SaveChanges();
            }
            catch
            {
                MessageBox.Show("Ошибка. Не удалось изменить таблицу.");
                myContext = new EmployeesProjectsDBContext();
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
