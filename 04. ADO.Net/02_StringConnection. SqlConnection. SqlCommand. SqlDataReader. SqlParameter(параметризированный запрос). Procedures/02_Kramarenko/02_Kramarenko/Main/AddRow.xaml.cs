using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AddRow.xaml
    /// </summary>
    public partial class AddRow : Window
    {
        public List<string> Headers { get; set; }
        public List<string> Values { get; set; }
        public AddRow()
        {
            InitializeComponent();

            Headers = new List<string>();
            Values = new List<string>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Headers.Count; i++)
            {
                Grid item = new Grid();
                item.Height = double.NaN;
                ColumnDefinition cdef = new ColumnDefinition();
                cdef.Width = new GridLength(250);
                item.ColumnDefinitions.Add(cdef);
                item.ColumnDefinitions.Add(new ColumnDefinition());

                TextBlock textBlock = new TextBlock();
                textBlock.Text = Headers[i];
                textBlock.FontSize = 20;
                textBlock.Background = new SolidColorBrush(Color.FromRgb(145, 145, 145));
                textBlock.Padding = new Thickness(10, 0, 10, 0);
                Grid.SetColumn(textBlock, 0);
                item.Children.Add(textBlock);

                TextBox textBox = new TextBox();
                textBox.Text = Values.Count > i ? Values[i] : "";
                textBox.FontSize = 20;
                textBox.VerticalAlignment = VerticalAlignment.Center;
                textBox.Width = 350;
                textBox.TextWrapping = TextWrapping.Wrap;
                textBox.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
                textBox.Padding = new Thickness(10, 0, 10, 0);
                Grid.SetColumn(textBox, 1);
                item.Children.Add(textBox);

                stackPanel.Children.Add(item);
            }
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Save changes?","Info",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DialogResult = true;
                Close();
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Values.Clear();
            foreach (FrameworkElement STitem in stackPanel.Children)
            {
                if(STitem is Grid)
                {
                    foreach (FrameworkElement item in (STitem as Grid).Children)
                    {
                        if(item is TextBox) Values.Add((item as TextBox).Text);
                    }
                }
            }
        }
    }
}
