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

namespace WPF_Menu_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("New clicked!");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Добавление пункта меню в меню File
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Close";
            menuItem.Click += Close_Click;

            //menu1.Items.Add(menuItem);
            ((MenuItem)menu1.Items[0]).Items.Add(menuItem);

            //MessageBox.Show(menu1.Parent.ToString());

            // Добавление пункта меню в контекстное меню для кнопки
            MenuItem contextItem = new MenuItem();
            contextItem.Header = "Close!";
            contextItem.Click += Close_Click;
            addButton.ContextMenu.Items.Add(contextItem);

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
