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

namespace WPF_Pages
{
    /// <summary>
    /// Interaction logic for PageFunction1.xaml
    /// </summary>
    public partial class PageFunction1 : PageFunction<String>
    {
        public PageFunction1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Возврат 1 результата типа string
            OnReturn(new ReturnEventArgs<string>(textBox1.Text));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Возврат в 1 страницу без результата
            OnReturn(null);
        }
    }
}
