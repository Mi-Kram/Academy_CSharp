using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для ShowAllCars.xaml
    /// </summary>
    public partial class ShowAllCars : Window
    {
        public List<string[]> Owns { get; set; }
        public SqlConnection cn { get; set; }
        public ShowAllCars()
        {
            InitializeComponent();
            Owns = new List<string[]>();
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (cn == null) Close();

            foreach (string[] own in Owns)
            {
                Grid item = new Grid();
                item.Height = double.NaN;
                ColumnDefinition cdef = new ColumnDefinition();
                cdef.Width = new GridLength(250);
                item.ColumnDefinitions.Add(cdef);
                item.Margin = new Thickness(0, 5, 0, 0);
                item.ColumnDefinitions.Add(new ColumnDefinition());

                StackPanel panel = new StackPanel();
                panel.Margin = new Thickness(0);
                panel.Orientation = Orientation.Vertical;
                Grid.SetColumn(panel, 0);
                item.Children.Add(panel);

                TextBlock textBlock = new TextBlock();
                textBlock.Text = $"id: {own[0]}";
                textBlock.FontSize = 20;
                textBlock.Background = new SolidColorBrush(Color.FromRgb(145, 145, 145));
                textBlock.Padding = new Thickness(10, 0, 10, 0);
                panel.Children.Add(textBlock);

                textBlock = new TextBlock();
                textBlock.Text = $"name: {own[1]}";
                textBlock.FontSize = 20;
                textBlock.Background = new SolidColorBrush(Color.FromRgb(145, 145, 145));
                textBlock.Padding = new Thickness(10, 0, 10, 0);
                panel.Children.Add(textBlock);

                panel = new StackPanel();
                panel.Margin = new Thickness(0);
                panel.Orientation = Orientation.Vertical;
                Grid.SetColumn(panel, 1);
                item.Children.Add(panel);
                List<string> cars = GetCars(own[0].ToString());
                foreach (string car in cars)
                {
                    textBlock = new TextBlock();
                    textBlock.Text = car;
                    textBlock.FontSize = 20;
                    textBlock.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
                    textBlock.Padding = new Thickness(10, 0, 10, 0);
                    panel.Children.Add(textBlock);
                }

                stackPanel.Children.Add(item);
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

    }
}
