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
    /// Логика взаимодействия для FindDialog.xaml
    /// </summary>
    public partial class FindDialog : Window
    {
        public List<string> ColumnNames { get; set; }
        public List<FindRow> Values { get; set; }
        public List<int> SelectedIndexes { get; set; }
        public string FindStr { get; set; }

        public bool Refresh { get; set; }
        
        public FindDialog()
        {
            InitializeComponent();

            ColumnNames = new List<string>();
            SelectedIndexes = new List<int>();
            Values = new List<FindRow>();
            FindStr = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Style textBlockTitleStyle = (Style)FindResource("TextBlockTitleStyle");
            Style checkBoxGridStyle = (Style)FindResource("CheckBoxGridStyle");
            Style checkBoxStyle = (Style)FindResource("CheckBoxStyle");
            for (int i = 0; i < ColumnNames.Count; i++)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Tag = i;
                checkBox.Style = checkBoxStyle;

                TextBlock title = new TextBlock();
                title.Style = textBlockTitleStyle;
                title.Text = ColumnNames[i];


                Values.Add(new FindRow() { CheckBox = checkBox, ColumnName = title.Text });


                Grid row = new Grid();
                row.Background = new SolidColorBrush(Colors.Black);
                row.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.15, GridUnitType.Star) });
                row.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                Grid gridRadioButton = new Grid();
                gridRadioButton.Style = checkBoxGridStyle;
                gridRadioButton.Children.Add(checkBox);
                row.Children.Add(gridRadioButton);

                Grid gridTextBlock = new Grid();
                Grid.SetColumn(gridTextBlock, 1);
                gridTextBlock.Style = checkBoxGridStyle;
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
        bool IsCorrect() => textBox.Text != "" && Values.Where(x => x.CheckBox.IsChecked == true).Count() > 0;

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            Refresh = true;
            DialogResult = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DialogResult == true)
            {
                FindStr = textBox.Text;
                for (int i = 0; i < Values.Count; i++)
                {
                    if (Values[i].CheckBox.IsChecked == true)
                    {
                        SelectedIndexes.Add(i);
                    }
                }
            }
        }
    }

    public class FindRow
    {
        public CheckBox CheckBox { get; set; }
        public string ColumnName { get; set; }
    }
}
