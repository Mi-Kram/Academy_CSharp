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
    /// Логика взаимодействия для FilterTable.xaml
    /// </summary>
    public partial class FilterTable : Window
    {
        public List<string> ColumnNames { get; set; }
        public List<FilterRow> Values { get; set; }
        public int SelectedIndex { get; set; }

        public bool Refresh { get; set; }
        public FilterTable()
        {
            InitializeComponent();
            Values = new List<FilterRow>();
            ColumnNames = new List<string>();
            SelectedIndex = -1;
            Refresh = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Style textBlockTitleStyle = (Style)FindResource("TextBlockTitleStyle");
            Style radioButtonGridStyle = (Style)FindResource("RadioButtonGridStyle");
            Style radioButtonStyle = (Style)FindResource("RadioButtonStyle");
            Style textBoxValueStyle = (Style)FindResource("TextBoxValueStyle");
            for (int i = 0; i < ColumnNames.Count; i++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Unchecked += RadioButton_Unchecked;
                radioButton.Checked += RadioButton_Checked;
                radioButton.Tag = i;
                radioButton.Style = radioButtonStyle;
                radioButton.GroupName = "Columns";

                TextBlock title = new TextBlock();
                title.IsEnabled = false;
                title.Style = textBlockTitleStyle;
                title.Text = ColumnNames[i];

                TextBox textBox = new TextBox();
                textBox.IsEnabled = false;
                textBox.Style = textBoxValueStyle;


                Values.Add(new FilterRow() { RadioButton = radioButton, columnName = title, Value = textBox });

                
                Grid row = new Grid();
                row.Background = new SolidColorBrush(Colors.Black);
                row.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0.2, GridUnitType.Star) });
                row.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
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

                Grid gridTextBox = new Grid();
                Grid.SetColumn(gridTextBox, 2);
                gridTextBox.Style = radioButtonGridStyle;
                gridTextBox.Children.Add(textBox);
                row.Children.Add(gridTextBox);

                stackPanel.Children.Add(row);
            }

            if (SelectedIndex != -1) Values[SelectedIndex].RadioButton.IsChecked = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            foreach (FilterRow item in Values)
            {
                if (item.RadioButton == sender as RadioButton)
                {
                    item.columnName.IsEnabled = true;
                    item.Value.IsEnabled = true;
                    break;
                }
            }
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (FilterRow item in Values)
            {
                if(item.RadioButton == sender as RadioButton)
                {
                    item.columnName.IsEnabled = false;
                    item.Value.Text = "";
                    item.Value.IsEnabled = false;
                    break;
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

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

    public class FilterRow
    {
        public RadioButton RadioButton { get; set; }
        public TextBlock columnName { get; set; }
        public TextBox Value { get; set; }
    }
}
