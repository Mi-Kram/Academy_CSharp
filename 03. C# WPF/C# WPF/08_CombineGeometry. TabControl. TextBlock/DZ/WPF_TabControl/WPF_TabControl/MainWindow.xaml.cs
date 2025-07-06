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

namespace WPF_TabControl
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

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            // создать кладку
            TabItem item = new TabItem();

            // добавить в коллекцию вкладок
            tabControl1.Items.Add(item);

            // загрузить картинку
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("admin.gif", UriKind.RelativeOrAbsolute));
            img.Width = 32;
            img.Height = 32;

            // добавить картинку в заголовок вкладки
            item.Header = img;
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(tabControl1.SelectedIndex.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tabControl1.Items.RemoveAt(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(tabControl1.Items.Count>1)
                tabControl1.SelectedIndex = 1;
        }
    }
}
