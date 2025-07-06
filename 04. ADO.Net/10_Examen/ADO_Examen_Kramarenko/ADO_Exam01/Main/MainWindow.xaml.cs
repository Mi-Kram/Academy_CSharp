using Main.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection cn;
        DataGrid currentDataGrid;
        
        public MainWindow()
        {
            InitializeComponent();
            currentDataGrid = ManufsDataGrid;

            cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["Main.Properties.Settings.stringConection"].ConnectionString;

        }

        void ApdateDataGrid()
        {
            SqlDataReader reader = null;
            string table = (tables_cmbBox.SelectedItem as TextBlock).Text;

            if(table != "Manufactures" && table != "Planes")
            {
                MessageBox.Show($"Недопустимая таблица {table}\nРазрешены только таблицы Manufacture и Planes\nПриложение будет закрыто!");
                Close();
            }

            try
            {
                List<object> lst = new List<object>();
                
                cn.Open();
                SqlCommand command = new SqlCommand($"Get{table}", cn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                reader = command.ExecuteReader();

                if (table == "Manufactures")
                {
                    while (reader.Read())
                    {
                        string manufacturerId = reader["ManufacturerId"].ToString();
                        string brandTitle = reader["BrandTitle"].ToString();
                        string address = reader["Address"].ToString();
                        string phone = reader["Phone"].ToString();

                        lst.Add(new Manufacture { ManufacturerId = manufacturerId, BrandTitle = brandTitle, Address = address, Phone = phone });
                    }
                }
                else if(table == "Planes")
                {
                    while (reader.Read())
                    {
                        string id = reader["Id"].ToString();
                        string model = reader["Model"].ToString();
                        if (!double.TryParse(reader["Price"].ToString(), out double price)) price = 0;
                        if (!double.TryParse(reader["Speed"].ToString(), out double speed)) speed = 0;
                        string manufacturerId = reader["ManufacturerId"].ToString();

                        lst.Add(new Plane { Id = id, Model = model, Price = price, Speed = speed, ManufacturerId = manufacturerId });
                    }
                }

                if(table == "Manufactures")
                {
                    ManufsDataGrid.Visibility = Visibility.Visible;
                    PlanesDataGrid.Visibility = Visibility.Hidden;

                    currentDataGrid.ItemsSource = null;
                    currentDataGrid = ManufsDataGrid;
                    currentDataGrid.ItemsSource = lst.Select(x => x as Manufacture).ToList();
                }
                else
                {
                    PlanesDataGrid.Visibility = Visibility.Visible;
                    ManufsDataGrid.Visibility = Visibility.Hidden;

                    currentDataGrid.ItemsSource = null;
                    currentDataGrid = PlanesDataGrid;
                    currentDataGrid.ItemsSource = lst.Select(x => x as Plane).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                cn.Close();
            }
        }


        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            if((tables_cmbBox.SelectedItem as TextBlock).Text == "Manufactures")
            {
                EditManufacture editManufacture = new EditManufacture();
                if (editManufacture.ShowDialog() == true)
                {
                    string newId = Guid.NewGuid().ToString("N");
                    while (!IsPrimaryKey(newId))
                    {
                        newId = Guid.NewGuid().ToString("N");
                    }

                    Manufacture manufacture = new Manufacture();
                    manufacture.ManufacturerId = newId;
                    manufacture.BrandTitle = editManufacture.Value.BrandTitle;
                    manufacture.Address = editManufacture.Value.Address;
                    manufacture.Phone = editManufacture.Value.Phone;

                    Add(manufacture);
                    ApdateDataGrid();
                }
            }
            else
            {
                EditPlane editPlane = new EditPlane();
                editPlane.SqlConnection = cn;
                if (editPlane.ShowDialog() == true)
                {
                    string newId = Guid.NewGuid().ToString("N");
                    while (!IsPrimaryKey(newId))
                    {
                        newId = Guid.NewGuid().ToString("N");
                    }

                    Plane plane = new Plane();
                    plane.Id = newId;
                    plane.Model = editPlane.Value.Model;
                    plane.Price = editPlane.Value.Price;
                    plane.Speed = editPlane.Value.Speed;
                    plane.ManufacturerId = editPlane.Value.ManufId;

                    Add(plane);
                    ApdateDataGrid();
                }
            }
        }

        void Add(Manufacture manufacture)
        {
            try
            {
                cn.Open();
                SqlCommand command = new SqlCommand("AddManufacture", cn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = $"@ManufacturerId";
                parameter1.Value = manufacture.ManufacturerId;
                parameter1.SqlDbType = SqlDbType.VarChar;
                parameter1.Size = 32;
                command.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = $"@BrandTitle";
                parameter2.Value = manufacture.BrandTitle;
                parameter2.SqlDbType = SqlDbType.VarChar;
                parameter2.Size = 50;
                command.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = $"@Address";
                parameter3.Value = manufacture.Address;
                parameter3.SqlDbType = SqlDbType.VarChar;
                parameter3.Size = 50;
                command.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = $"@Phone";
                parameter4.Value = manufacture.Phone;
                parameter4.SqlDbType = SqlDbType.VarChar;
                parameter4.Size = 20;
                command.Parameters.Add(parameter4);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        void Add(Plane plane)
        {
            try
            {
                cn.Open();
                SqlCommand command = new SqlCommand("AddPlane", cn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = $"@Id";
                parameter1.Value = plane.Id;
                parameter1.SqlDbType = SqlDbType.VarChar;
                parameter1.Size = 32;
                command.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = $"@Model";
                parameter2.Value = plane.Model;
                parameter2.SqlDbType = SqlDbType.VarChar;
                parameter2.Size = 50;
                command.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = $"@Price";
                parameter3.Value = plane.Price;
                parameter3.SqlDbType = SqlDbType.Float;
                command.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = $"@Speed";
                parameter4.Value = plane.Speed;
                parameter4.SqlDbType = SqlDbType.Float;
                command.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = $"@ManufacturerId";
                parameter5.Value = plane.ManufacturerId;
                parameter5.SqlDbType = SqlDbType.VarChar;
                parameter5.Size = 32;
                command.Parameters.Add(parameter5);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        bool IsPrimaryKey(string key)
        {
            if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Manufactures")
            {
                foreach (Manufacture item in currentDataGrid.Items)
                {
                    if (item.ManufacturerId == key) return false;
                }
            }
            else
            {
                foreach (Plane item in currentDataGrid.Items)
                {
                    if (item.Id == key) return false;
                }
            }

            return true;
        }

        private void EditRow_Click(object sender, RoutedEventArgs e)
        {
            if (currentDataGrid.SelectedItems.Count > 0)
            {
                if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Manufactures")
                {
                    Manufacture manufacture = currentDataGrid.SelectedItem as Manufacture;
                    EditManufacture editManufacture = new EditManufacture();
                    editManufacture.Value = (manufacture.BrandTitle, manufacture.Address, manufacture.Phone);

                    if (editManufacture.ShowDialog() == true)
                    {
                        manufacture.BrandTitle = editManufacture.Value.BrandTitle;
                        manufacture.Address = editManufacture.Value.Address;
                        manufacture.Phone = editManufacture.Value.Phone;

                        Edit(manufacture);
                        ApdateDataGrid();
                    }
                }
                else
                {
                    Plane plane = currentDataGrid.SelectedItem as Plane;
                    EditPlane editPlane = new EditPlane();
                    editPlane.SqlConnection = cn;
                    editPlane.Value = (plane.Model, plane.Price, plane.Speed, plane.ManufacturerId);

                    if (editPlane.ShowDialog() == true)
                    {
                        plane.Model = editPlane.Value.Model;
                        plane.Price = editPlane.Value.Price;
                        plane.Speed = editPlane.Value.Speed;
                        plane.ManufacturerId = editPlane.Value.ManufId;

                        Edit(plane);
                        ApdateDataGrid();
                    }
                }
            }
        }
        void Edit(Manufacture manufacture)
        {
            try
            {
                cn.Open();
                SqlCommand command = new SqlCommand("UpdateManufacture", cn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = $"@ManufacturerId";
                parameter1.Value = manufacture.ManufacturerId;
                parameter1.SqlDbType = SqlDbType.VarChar;
                parameter1.Size = 32;
                command.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = $"@BrandTitle";
                parameter2.Value = manufacture.BrandTitle;
                parameter2.SqlDbType = SqlDbType.VarChar;
                parameter2.Size = 50;
                command.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = $"@Address";
                parameter3.Value = manufacture.Address;
                parameter3.SqlDbType = SqlDbType.VarChar;
                parameter3.Size = 50;
                command.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = $"@Phone";
                parameter4.Value = manufacture.Phone;
                parameter4.SqlDbType = SqlDbType.VarChar;
                parameter4.Size = 20;
                command.Parameters.Add(parameter4);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        void Edit(Plane plane)
        {
            try
            {
                cn.Open();
                SqlCommand command = new SqlCommand("UpdatePlane", cn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = $"@Id";
                parameter1.Value = plane.Id;
                parameter1.SqlDbType = SqlDbType.VarChar;
                parameter1.Size = 32;
                command.Parameters.Add(parameter1);

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = $"@Model";
                parameter2.Value = plane.Model;
                parameter2.SqlDbType = SqlDbType.VarChar;
                parameter2.Size = 50;
                command.Parameters.Add(parameter2);

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = $"@Price";
                parameter3.Value = plane.Price;
                parameter3.SqlDbType = SqlDbType.Float;
                command.Parameters.Add(parameter3);

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = $"@Speed";
                parameter4.Value = plane.Speed;
                parameter4.SqlDbType = SqlDbType.Float;
                command.Parameters.Add(parameter4);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = $"@ManufacturerId";
                parameter5.Value = plane.ManufacturerId;
                parameter5.SqlDbType = SqlDbType.VarChar;
                parameter5.Size = 32;
                command.Parameters.Add(parameter5);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void RemoveRows_Click(object sender, RoutedEventArgs e)
        {
            RemoveRows();
        }
        void RemoveRows()
        {
            if (currentDataGrid.SelectedItems.Count > 0)
            {
                if ((tables_cmbBox.SelectedItem as TextBlock).Text == "Manufactures")
                {
                    SqlDataReader reader = null;
                    List<string> prohibitedManufs = new List<string>();
                    try
                    {
                        cn.Open();
                        SqlCommand command = new SqlCommand($"GetPlanes", cn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            prohibitedManufs.Add(reader["ManufacturerId"].ToString());
                        }
                        if (reader != null && !reader.IsClosed) reader.Close();
                        cn.Close();

                        List<Manufacture> lst = new List<Manufacture>();
                        bool flag = false;
                        foreach (Manufacture item in currentDataGrid.SelectedItems)
                        {
                            if (!prohibitedManufs.Contains(item.ManufacturerId)) lst.Add(item);
                            else flag = true;
                        }

                        Remove(lst);
                        if (flag) MessageBox.Show("Некоторые записи не были удалены, т.к. эти записи используются в других таблицах", "INFO", MessageBoxButton.OK);
                        ApdateDataGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        if (reader != null && !reader.IsClosed) reader.Close();
                        cn.Close();
                    }
                }
                else
                {
                    List<Plane> lst = new List<Plane>();
                    foreach (Plane item in currentDataGrid.SelectedItems)
                    {
                        lst.Add(item);
                    }

                    Remove(lst);
                    ApdateDataGrid();
                }
            }
        }
        void Remove(List<Manufacture> lst)
        {
            try
            {
                cn.Open();
                SqlCommand command = new SqlCommand("RemoveManufacture", cn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = $"@ManufacturerId";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Size = 32;
                command.Parameters.Add(parameter);

                foreach (Manufacture item in lst)
                {
                    parameter.Value = item.ManufacturerId;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
        void Remove(List<Plane> lst)
        {
            try
            {
                cn.Open();
                SqlCommand command = new SqlCommand("RemovePlane", cn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = $"@Id";
                parameter.SqlDbType = SqlDbType.VarChar;
                parameter.Size = 32;
                command.Parameters.Add(parameter);

                foreach (Plane item in lst)
                {
                    parameter.Value = item.Id;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        private void DataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //e.Cancel = true;
        }

        private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if((tables_cmbBox.SelectedItem as TextBlock).Text == "Manufacture")
            {
                Manufacture manufacture = e.Row.DataContext as Manufacture;

                Edit(manufacture);
                ApdateDataGrid();
            }
            else
            {
                Plane plane = e.Row.DataContext as Plane;

                Edit(plane);
                ApdateDataGrid();
            }
        }

        private void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                RemoveRows();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApdateDataGrid();
        }

        private void tables_cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.IsLoaded) ApdateDataGrid();
        }

    }
}
