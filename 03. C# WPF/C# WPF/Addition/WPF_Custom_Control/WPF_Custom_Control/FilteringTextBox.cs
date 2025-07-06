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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Custom_Control
{
    public enum FilteringTextOptions {
        Digits, Letters, All
    }

    public class FilteringTextBox : TextBox
    {
        // свойства пользовательского элемента управления
        // текст приглашения
        public string WatermarkText { get; set; }

        // фильтр для ввода
        public FilteringTextOptions Filter { get; set; }

        static FilteringTextBox()
        {
            // подключение стиля с шаблоном из файла Generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilteringTextBox), new FrameworkPropertyMetadata(typeof(FilteringTextBox)));
        }

        public FilteringTextBox()
        {
            // задание начальных значений
            WatermarkText = "Enter text here...";
            Filter = FilteringTextOptions.All;

            // задание event для фильтрации ввода
            this.KeyDown += FilteringTextBox_KeyDown;
        }

        private void FilteringTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // выполнение фильтрации ввода
            switch(Filter)
            {
                case FilteringTextOptions.Letters:
                    if (!(e.Key >= Key.A && e.Key <= Key.Z))
                        e.Handled = true;
                    break;

                case FilteringTextOptions.Digits:
                    if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
                        e.Handled = true;
                    break;

                case FilteringTextOptions.All:
                    e.Handled = false;
                    break;
            }
        }
    }
}
