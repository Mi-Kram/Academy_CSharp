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
    /// Логика взаимодействия для SortDialog.xaml
    /// </summary>
    public partial class SortDialog : Window
    {
        public List<string> ColumnNames { get; set; }
        List<RadioButton> radioButtons;
        public int SelectedIndex { get; set; }
        public SortDialog()
        {
            InitializeComponent();
            ColumnNames = new List<string>();
            SelectedIndex = -1;
            radioButtons = new List<RadioButton>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Style textBlockTitleStyle = (Style)FindResource("TextBlockTitleStyle");
            Style radioButtonGridStyle = (Style)FindResource("RadioButtonGridStyle");
            Style radioButtonStyle = (Style)FindResource("RadioButtonStyle");
            for (int i = 0; i < ColumnNames.Count; i++)
            {
                Grid row = new Grid();
                row.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) });
                row.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                TextBlock title = new TextBlock();
                title.Style = textBlockTitleStyle;
                title.Text = ColumnNames[i];
                row.Children.Add(title);

                Grid gridRadioButton = new Grid();
                gridRadioButton.Style = radioButtonGridStyle;
                
                RadioButton radioButton = new RadioButton();
                radioButton.Tag = i;
                radioButton.Style = radioButtonStyle;
                radioButton.GroupName = "Columns";
                radioButtons.Add(radioButton);
                gridRadioButton.Children.Add(radioButton);

                row.Children.Add(gridRadioButton);
                stackPanel.Children.Add(row);
            }

            if (SelectedIndex != -1) radioButtons[SelectedIndex].IsChecked = true;
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < radioButtons.Count; i++)
            {
                if(radioButtons[i].IsChecked == true)
                {
                    SelectedIndex = i;
                    break;
                }
            }

            if (SelectedIndex >= 0) DialogResult = true;
            else MessageBox.Show("Выбирете столбец для сортировки!", "INFO", MessageBoxButton.OK);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
