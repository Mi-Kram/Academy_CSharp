using System;
using System.Collections.Generic;
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
        List<string> lstView_columnNames;
        (bool save, bool current) isSave;
        public MainWindow()
        {
            InitializeComponent();

            cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["Main.Properties.Settings.connectStr"].ConnectionString;
            isSave = (true, false);

            lstView_columnNames = new List<string>();
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
                List<Table> tableCopy = GetItemsTables(curTable, GetColumnNames(curTable, "pubs"));
                try
                {
                    cn.Open();

                    string command = $"delete from {curTable}";
                    SqlCommand cmd = new SqlCommand(command, cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    foreach (Table item in lstView.Items)
                    {
                        command = $"insert into {curTable} (";
                        command += $"{lstView_columnNames[0]}";
                        for (int i = 1; i < lstView_columnNames.Count; i++)
                        {
                            command += $", {lstView_columnNames[i]}";
                        }
                        command += $") values (";
                        command += $"@Param0";
                        for (int i = 1; i < lstView_columnNames.Count; i++)
                        {
                            command += $", @Param{i}";
                        }
                        command += $")";

                        cmd = new SqlCommand(command, cn);

                        for (int i = 0; i < lstView_columnNames.Count; i++)
                        {
                            SqlParameter param = new SqlParameter();
                            param.ParameterName = $"@Param{i}";
                            param.Value = item.Items[i];
                            param.SqlDbType = SqlDbType.VarChar;
                            param.Size = int.MaxValue;
                            cmd.Parameters.Add(param);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    string command = $"delete from {curTable}";
                    SqlCommand cmd = new SqlCommand(command, cn);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    foreach (Table item in tableCopy)
                    {
                        command = $"insert into {curTable} (";
                        command += $"{lstView_columnNames[0]}";
                        for (int i = 1; i < lstView_columnNames.Count; i++)
                        {
                            command += $", {lstView_columnNames[i]}";
                        }
                        command += $") values (";
                        command += $"@Param0";
                        for (int i = 1; i < lstView_columnNames.Count; i++)
                        {
                            command += $", @Param{i}";
                        }
                        command += $")";

                        cmd = new SqlCommand(command, cn);

                        for (int i = 0; i < lstView_columnNames.Count; i++)
                        {
                            SqlParameter param = new SqlParameter();
                            param.ParameterName = $"@Param{i}";
                            param.Value = item.Items[i];
                            param.SqlDbType = SqlDbType.VarChar;
                            param.Size = int.MaxValue;
                            cmd.Parameters.Add(param);
                        }

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show(ex.Message, "Info", MessageBoxButton.OK);
                }
                cn.Close();
                isSave.save = true;
            }
            else if (result == MessageBoxResult.No)
            {
                isSave.save = true;
                AppdateListView((tables_cmbBox.SelectedItem as TextBlock).Text);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить изменения?", "Info", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                isSave.save = true;
                AppdateListView((tables_cmbBox.SelectedItem as TextBlock).Text);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //cn.Open();

            //SqlCommand cmd = new SqlCommand("GetTables", cn);
            //cmd.CommandType = CommandType.StoredProcedure;

            //SqlParameter param = new SqlParameter();
            //param.ParameterName = "@table_catalog";
            //param.Value = "pubs";
            //param.SqlDbType = SqlDbType.VarChar;
            //param.Size = int.MaxValue;
            //param.Direction = ParameterDirection.Input;
            //cmd.Parameters.Add(param);

            //SqlDataReader tableCols = cmd.ExecuteReader();
            //tables_cmbBox.Items.Clear();
            //while (tableCols.Read())
            //{
            //    tables_cmbBox.Items.Add(new TextBlock() { Text = tableCols[0].ToString() });
            //}
            //tableCols.Close();
            //cn.Close();
            //tables_cmbBox.SelectedIndex = 0;

            AppdateListView((tables_cmbBox.SelectedItem as TextBlock).Text);
        }

        void AppdateListView(string table_name)
        {
            try
            {
                List<string> lstColsName = GetColumnNames(table_name, "pubs");
                List<Table> lstTables = GetItemsTables(table_name, lstColsName);

                lstView.Items.Clear();
                GridView view = new GridView();
                lstView.View = view;

                for (int i = 0; i < lstColsName.Count; i++)
                {
                    GridViewColumn col = new GridViewColumn();
                    col.Header = lstColsName[i];
                    col.Width = 100.0;
                    col.DisplayMemberBinding = new Binding($"Items[{i}]");
                    view.Columns.Add(col);
                }
                foreach (Table item in lstTables)
                {
                    lstView.Items.Add(item);
                }

                lstView_columnNames = lstColsName;
            }
            catch (Exception ex)
            {
                cn.Close();
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
        List<Table> GetItemsTables(string table_name, List<string> lstColsName)
        {
            List<Table> items = new List<Table>();
            cn.Open();

            SqlCommand cmd = new SqlCommand($"select * from {table_name}", cn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader table = cmd.ExecuteReader();

            int n = lstColsName.Count;
            while (table.Read())
            {
                Table row = new Table();

                foreach (string colName in lstColsName)
                {
                    row.Items.Add(table[colName].ToString());
                }

                items.Add(row);
            }
            table.Close();

            cn.Close();
            return items;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cn.State != System.Data.ConnectionState.Closed) cn.Close();
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
                    if (cn != null) AppdateListView((tables_cmbBox.SelectedItem as TextBlock).Text);

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
            dialog.Headers = lstView_columnNames;
            dialog.Owner = this;
            dialog.Title = "Add Row";
            if(dialog.ShowDialog() == true)
            {
                isSave.save = false; 
                lstView.Items.Add(new Table(dialog.Values.ToArray()));
            }
        }

        private void RemoveRows_Click(object sender, RoutedEventArgs e)
        {
            if(lstView.SelectedItems.Count > 0 && MessageBox.Show("Удалить выделенные записи?", "Info", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                isSave.save = false;
                while (lstView.SelectedItems.Count > 0)
                {
                    lstView.Items.Remove(lstView.SelectedItems[0]);
                }
            }
        }

        private void EditRow_Click(object sender, RoutedEventArgs e)
        {
            if (lstView.SelectedItems.Count > 0)
            {
                Table selected = lstView.SelectedItems[0] as Table;

                AddRow dialog = new AddRow();
                dialog.Headers = lstView_columnNames;
                dialog.Values = selected.Items;
                dialog.Owner = this;
                dialog.Title = "Edit Row";
                if (dialog.ShowDialog() == true)
                {
                    isSave.save = false;
                    
                    int index = lstView.Items.IndexOf(selected);
                    lstView.Items.RemoveAt(index);
                    lstView.Items.Insert(index, new Table(dialog.Values.ToArray()));
                }
            }
        }

        private void ShowCars_Click(object sender, RoutedEventArgs e)
        {
            if (lstView.SelectedItems.Count > 0)
            {
                ShowCars dialog = new ShowCars();
                Table own = lstView.SelectedItems[0] as Table;
                dialog.Cars = GetCars(own.Items[0]);
                dialog.Own = $"id: {own.Items[0]},   name: {own.Items[1]}";

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
            List<Table> owns = GetOwnsFromLstView();
            ShowAllCars dialog = new ShowAllCars();
            dialog.cn = cn;
            dialog.Owns = owns;
            dialog.ShowDialog();
        }

        List<Table> GetOwnsFromLstView()
        {
            List<Table> result = new List<Table>();

            foreach (Table item in lstView.Items)
            {
                result.Add(item);
            }

            return result;
        }
    }

    public class Table
    {
        public List<string> Items { get; set; }

        public Table()
        {
            Items = new List<string>();
        }

        public Table(params string[] hdrs)
        {
            Items = new List<string>();
            foreach (string item in hdrs)
                Items.Add(item);
        }
    }
}
