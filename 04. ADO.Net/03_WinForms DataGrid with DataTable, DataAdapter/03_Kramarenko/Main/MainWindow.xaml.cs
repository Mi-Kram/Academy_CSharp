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
        SqlConnection cn;
        SqlDataAdapter adapter;
        List<string> lstDataGrid_columnNames;
        (bool save, bool current) isSave;
        DataTable dt;

        public MainWindow()
        {
            InitializeComponent();

            cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["Main.Properties.Settings.connectStr"].ConnectionString;
            isSave = (true, false);

            dt = new DataTable();
            dataGrid.ItemsSource = dt.DefaultView;

            lstDataGrid_columnNames = new List<string>();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveTable(MessageBox.Show("Сохранить изменения?", "Info", MessageBoxButton.YesNoCancel));
        }

        void SaveTable(MessageBoxResult result, string curTable = "")
        {
            if (result == MessageBoxResult.Yes)
            {
                if(curTable == "") curTable = (tables_cmbBox.SelectedItem as TextBlock).Text;

                DataTable tableCopy = new DataTable();
                adapter = new SqlDataAdapter($"select * from {curTable}", cn);
                adapter.Fill(tableCopy);

                string cmd;
                SqlCommand command;
                try
                {
                    cmd = $"insert into {curTable} (";
                    cmd += lstDataGrid_columnNames[0];
                    for (int i = 1; i < lstDataGrid_columnNames.Count; i++)
                    {
                        cmd += $", {lstDataGrid_columnNames[i]}";
                    }
                    cmd += ") values (@p0";
                    for (int i = 1; i < lstDataGrid_columnNames.Count; i++)
                    {
                        cmd += $", @p{i}";
                    }
                    cmd += ")";
                    command = new SqlCommand(cmd, cn);
                    for (int i = 0; i < lstDataGrid_columnNames.Count; i++)
                    {
                        command.Parameters.Add($"@p{i}", SqlDbType.VarChar, 100, lstDataGrid_columnNames[i]);
                    }
                    adapter.InsertCommand = command;
                    //MessageBox.Show(GetMsg(command));


                    cmd = $"delete from {curTable} where {lstDataGrid_columnNames[0]} = @p0";
                    command = new SqlCommand(cmd, cn);
                    command.Parameters.Add("@p0", SqlDbType.VarChar, 100, lstDataGrid_columnNames[0]);
                    adapter.DeleteCommand = command;
                    //MessageBox.Show(GetMsg(command));


                    cmd = $"update {curTable} set ";
                    if (lstDataGrid_columnNames.Count > 1) cmd += $"{lstDataGrid_columnNames[1]} = @p1";
                    for (int i = 2; i < lstDataGrid_columnNames.Count; i++)
                    {
                        cmd += $", {lstDataGrid_columnNames[i]} = @p{i}";
                    }
                    cmd += $" where {lstDataGrid_columnNames[0]} = @p0";
                    command = new SqlCommand(cmd, cn);
                    for (int i = 0; i < lstDataGrid_columnNames.Count; i++)
                    {
                        command.Parameters.Add($"@p{i}", SqlDbType.VarChar, 100, lstDataGrid_columnNames[i]);
                    }
                    adapter.UpdateCommand = command;
                    //MessageBox.Show(GetMsg(command));


                    adapter.Update(dt);
                    MessageBox.Show("Сохранено");
                    //MessageBox.Show($"Обновлено строк: {adapter.Update(dt)}");
                    dt.Clear();
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    dt = tableCopy;
                    adapter.Update(dt);
                    dataGrid.ItemsSource = dt.DefaultView;

                    MessageBox.Show(ex.Message, "Info", MessageBoxButton.OK);
                }
                isSave.save = true;
            }
            else if (result == MessageBoxResult.No)
            {
                isSave.save = true;
                AppdateDataGrid((tables_cmbBox.SelectedItem as TextBlock).Text);
            }
        }

        string GetMsg(SqlCommand command)
        {
            string msg = command.CommandText;
            for (int i = 0; i < lstDataGrid_columnNames.Count; i++)
            {
                msg = msg.Replace($"p{i}", lstDataGrid_columnNames[i]);
            }
            return msg;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить изменения?", "Info", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                isSave.save = true;
                AppdateDataGrid((tables_cmbBox.SelectedItem as TextBlock).Text);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppdateDataGrid((tables_cmbBox.SelectedItem as TextBlock).Text);
        }

        void AppdateDataGrid(string table_name)
        {
            try
            {
                adapter = new SqlDataAdapter($"select * from {table_name}", cn);

                dataGrid.ItemsSource = null;
                dt.Clear();
                dt.Columns.Clear();
                adapter.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
                lstDataGrid_columnNames = GetColumnNames(table_name, "pubs");

                for (int i = 0; i < lstDataGrid_columnNames.Count; i++)
                {
                    dataGrid.Columns[i].Header = lstDataGrid_columnNames[i];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("2");

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        List<string> GetColumnNames(string table, string dateBase)
        {
            List<string> names = new List<string>();
            cn.Open();

            SqlCommand cmd = new SqlCommand("GetColumns", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@table_catalog";
            param1.Value = dateBase;
            param1.SqlDbType = SqlDbType.VarChar;
            param1.Size = int.MaxValue;
            param1.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@table_name";
            param2.Value = table;
            param2.SqlDbType = SqlDbType.VarChar;
            param2.Size = int.MaxValue;
            param2.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param2);

            SqlDataReader tableCols = cmd.ExecuteReader();

            while (tableCols.Read())
            {
                names.Add(tableCols[0].ToString());
            }
            tableCols.Close();

            cn.Close();
            return names;
        }


        private void tables_cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(tables_cmbBox.SelectedItem != null)
            {
                if (e.RemovedItems.Count != 0 && !isSave.save && !isSave.current)
                {
                    MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Info", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Cancel)
                    {
                        isSave.current = true;
                        tables_cmbBox.SelectedItem = e.RemovedItems[0];
                        return;
                    }
                    else SaveTable(result, (e.RemovedItems[0] as TextBlock).Text);
                }

                if (!isSave.current)
                {
                    if (cn != null) AppdateDataGrid((tables_cmbBox.SelectedItem as TextBlock).Text);

                    if (ShowAllCarsBtn != null)
                    {
                        if ((tables_cmbBox.SelectedItem as TextBlock).Text == "owners")
                        {
                            if (ShowAllCarsBtn.Visibility == Visibility.Collapsed) ShowAllCarsBtn.Visibility = Visibility.Visible;
                            if (ShowCarsBtn.Visibility == Visibility.Collapsed) ShowCarsBtn.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            if (ShowAllCarsBtn.Visibility == Visibility.Visible) ShowAllCarsBtn.Visibility = Visibility.Collapsed;
                            if (ShowCarsBtn.Visibility == Visibility.Visible) ShowCarsBtn.Visibility = Visibility.Collapsed;
                        }
                    }
                }

            }

            isSave.current = false;
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            AddRow dialog = new AddRow();
            dialog.Headers = lstDataGrid_columnNames;
            dialog.Owner = this;
            dialog.Title = "Add Row";
            if(dialog.ShowDialog() == true)
            {
                isSave.save = false;
                DataRow row = dt.NewRow();
                for (int i = 0; i < lstDataGrid_columnNames.Count; i++)
                {
                    row[lstDataGrid_columnNames[i]] = dialog.Values[i];
                }
                dt.Rows.Add(row);
            }
        }

        private void RemoveRows_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0 && MessageBox.Show("Удалить выделенные записи?", "Info", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                isSave.save = false;

                int[] indexes = GetDataTableSelectedIndexes().OrderByDescending(x => x).ToArray();
                foreach (int index in indexes)
                {
                    dt.Rows[index].Delete();
                }
            }
        }


        private void EditRow_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                DataRow row = dt.Rows[GetDataTableSelectedIndexes().OrderBy(x => x).ToArray()[0]];

                AddRow dialog = new AddRow();
                dialog.Headers = lstDataGrid_columnNames;
                dialog.Values = GetRow(row).ToList();

                dialog.Owner = this;
                dialog.Title = "Edit Row";
                if (dialog.ShowDialog() == true)
                {
                    isSave.save = false;
                    row.ItemArray = dialog.Values.ToArray();
                }
            }
        }

        bool isEquals(string[] obj1, string[] obj2)
        {
            if (obj1.Length != obj2.Length) return false;

            for (int i = 0; i < obj1.Length; i++)
            {
                if (obj1[i] != obj2[i]) return false;
            }

            return true;
        }
        string[] GetSelectedRow(int selectedIndex = 0)
        {
            List<string> lst = new List<string>();

            if (dataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < lstDataGrid_columnNames.Count; i++)
                {
                    lst.Add((dataGrid.Columns[i].GetCellContent(dataGrid.SelectedItems[selectedIndex]) as TextBlock).Text);
                }
            }

            return lst.ToArray();
        }
        string[] GetDataGridRow(int index)
        {
            List<string> lst = new List<string>();

            if (index < dataGrid.Items.Count)
            {
                for (int i = 0; i < lstDataGrid_columnNames.Count; i++)
                {
                    lst.Add((dataGrid.Columns[i].GetCellContent(dataGrid.Items[index]) as TextBlock).Text);
                }
            }

            return lst.ToArray();
        }
        string[] GetRow(DataRow row)
        {
            List<string> lst = new List<string>();

            foreach (string item in row.ItemArray)
            {
                lst.Add(item);
            }

            return lst.ToArray();
        }
        int[] GetDataTableSelectedIndexes()
        {
            List<int> lst = new List<int>();

            int n = dataGrid.SelectedItems.Count;
            for (int i = 0; i < n; i++)
            {
                string[] selectedRow = GetSelectedRow(i);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (isEquals(selectedRow, GetRow(dt.Rows[j])))
                    {
                        lst.Add(j);
                        break;
                    }
                }
            }

            return lst.ToArray();
        }

        private void ShowCars_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                ShowCars dialog = new ShowCars();
                DataRow own = dt.Rows[dataGrid.SelectedIndex];
                dialog.Cars = GetCars(own[0].ToString());
                dialog.Own = $"id: {own[0]},   name: {own[1]}";

                dialog.ShowDialog();
            }

        }

        List<string> GetCars(string own_id)
        {
            List<string> result = new List<string>();

            cn.Open();

            SqlCommand cmd = new SqlCommand("GetCars", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@own_id";
            param.Value = own_id;
            param.SqlDbType = SqlDbType.VarChar;
            param.Size = int.MaxValue;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            SqlDataReader cars = cmd.ExecuteReader();

            while (cars.Read())
            {
                result.Add($"id: {cars[0]},   brand: {cars[1]},   model: {cars[2]}");
            }
            cars.Close();

            cn.Close();

            return result;
        }

        private void ShowAllCars_Click(object sender, RoutedEventArgs e)
        {
            ShowAllCars dialog = new ShowAllCars();
            dialog.cn = cn;
            dialog.Owns = GetOwnsFromDataGrid();
            dialog.ShowDialog();
        }

        List<string[]> GetOwnsFromDataGrid()
        {
            List<string[]> result = new List<string[]>();

            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                result.Add(GetDataGridRow(i));
            }
            
            return result;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.G && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
            }

        }
    }
}
