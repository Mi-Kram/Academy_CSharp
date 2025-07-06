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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // программный переход на другую страницу
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Page2(textBox1.Text));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Переход на PageFunction1: создание и настройка точки возврата
            PageFunction1 pf1 = new PageFunction1();
            pf1.Return += new ReturnEventHandler<string>(pf1_Return);

            // Переход
            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            nav.Navigate(pf1);
        }

        // Обработчик возврата из PageFunction1
        void pf1_Return(object sender, ReturnEventArgs<string> e)
        {
            if (e != null) textBox1.Text = e.Result;
        }
    }
}
