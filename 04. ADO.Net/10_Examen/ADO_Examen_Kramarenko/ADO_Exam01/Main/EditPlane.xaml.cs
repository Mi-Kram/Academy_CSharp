using Main.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для EditPlane.xaml
    /// </summary>
    public partial class EditPlane : Window
    {
        public SqlConnection SqlConnection { get; set; }
        public (string Model, double Price, double Speed, string ManufId) Value { get; set; }

        public EditPlane()
        {
            InitializeComponent();

            Value = ("", -1, -1, "");
            SqlConnection = null;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DialogResult == true)
                Value = (Model.Text, double.Parse(Price.Text), double.Parse(Speed.Text), (manuf.SelectedItem as TextBlock).Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (SqlConnection == null) DialogResult = false;

            List<Manufacture> manufactures = new List<Manufacture>();
            SqlDataReader reader = null;
            try
            {
                SqlConnection.Open();
                SqlCommand command = new SqlCommand("GetManufactures", SqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string manufacturerId = reader["ManufacturerId"].ToString();
                    string brandTitle = reader["BrandTitle"].ToString();
                    string address = reader["Address"].ToString();
                    string phone = reader["Phone"].ToString();

                    manufactures.Add(new Manufacture { ManufacturerId = manufacturerId, BrandTitle = brandTitle, Address = address, Phone = phone });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = false;
            }
            finally
            {
                if (reader != null && !reader.IsClosed) reader.Close();
                SqlConnection.Close();
            }

            manufactures = manufactures.OrderBy(x => x.BrandTitle).ToList();

            foreach (Manufacture item in manufactures)
            {
                TextBlock cmbItem = new TextBlock();
                cmbItem.Text = item.BrandTitle;
                ToolTip toolTip = new ToolTip();
                toolTip.Content = new TextBlock() { Text = $"Address={item.Address}, Phone={item.Phone}" };
                cmbItem.ToolTip = toolTip;

                manuf.Items.Add(cmbItem);
            }

            Model.Text = Value.Model;
            Price.Text = Value.Price < 0 ? "" : Value.Price.ToString();
            Speed.Text = Value.Speed < 0 ? "" : Value.Speed.ToString();

            for (int i = 0; i < manuf.Items.Count; i++)
            {
                if (manuf.Items[i] is TextBlock)
                {
                    if ((manuf.Items[i] as TextBlock).Text == Value.ManufId)
                    {
                        manuf.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsCorrectData()) DialogResult = true;
            else MessageBox.Show("Заполните правильно все поля", "INFO", MessageBoxButton.OK);
        }

        bool IsCorrectData()
        {
            Model.Text = Model.Text.Trim(new char[] { ' ' });
            Price.Text = Price.Text.Trim(new char[] { ' ' });
            Speed.Text = Speed.Text.Trim(new char[] { ' ' });

            if (Price.Text.Length == 0 || !double.TryParse(Price.Text, out double price) || price < 0) Price.Text = "";
            if (Speed.Text.Length == 0 || !double.TryParse(Speed.Text, out double speed) || speed < 0) Speed.Text = "";

            if (Model.Text.Length == 0 || manuf.SelectedItem == null || 
                Price.Text.Length == 0 || Speed.Text.Length == 0) return false;

            return true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
