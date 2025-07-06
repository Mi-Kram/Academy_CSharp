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
    /// Логика взаимодействия для GroupDialog.xaml
    /// </summary>
    public partial class GroupDialog : Window
    {
        public List<string> ColumnNames { get; set; }
        public List<GroupRow> Values { get; set; }
        public int SelectedIndex { get; set; }
        public bool Refresh { get; set; }

        public GroupDialog()
        {
            InitializeComponent();

            ColumnNames = new List<string>();
            SelectedIndex = -1;
            Values = new List<GroupRow>();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Style textBlockTitleStyle = (Style)FindResource("TextBlockTitleStyle");
            Style radioButtonGridStyle = (Style)FindResource("RadioButtonGridStyle");
            Style radioButtonStyle = (Style)FindResource("RadioButtonStyle");
            for (int i = 0; i < ColumnNames.Count; i++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.GroupName = "Columns";
                radioButton.Tag = i;
                radioButton.Style = radioButtonStyle;

                TextBlock title = new TextBlock();
                title.Style = textBlockTitleStyle;
                title.Text = ColumnNames[i];


                Values.Add(new GroupRow() { RadioButton = radioButton, ColumnName = title.Text });


                Grid row = new Grid();
                row.Background = new SolidColorBrush(Colors.Black);
                row.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.15, GridUnitType.Star) });
                row.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                Grid gridRadioButton = new Grid();
                gridRadioButton.Style = radioButtonGridStyle;
                gridRadioButton.Children.Add(radioButton);
                row.Children.Add(gridRadioButton);

                Grid gridTextBlock = new Grid();
                Grid.SetColumn(gridTextBlock, 1);
                gridTextBlock.Style = radioButtonGridStyle;
                gridTextBlock.Children.Add(title);
                row.Children.Add(gridTextBlock);

                stackPanel.Children.Add(row);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsCorrect()) DialogResult = true;
            else MessageBox.Show("Отметьте столбцы и введите часть искомой ячейки!", "INFO", MessageBoxButton.OK);
        }
        bool IsCorrect() => Values.Where(x => x.RadioButton.IsChecked == true).Count() > 0;

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh = true;
            DialogResult = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == true)
            {
                for (int i = 0; i < Values.Count; i++)
                {
                    if (Values[i].RadioButton.IsChecked == true)
                    {
                        SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }

    public class GroupRow
    {
        public RadioButton RadioButton { get; set; }
        public string ColumnName { get; set; }
    }
}
