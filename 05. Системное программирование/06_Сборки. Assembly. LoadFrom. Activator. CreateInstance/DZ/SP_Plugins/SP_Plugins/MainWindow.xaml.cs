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

namespace SP_Plugins
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Объект для управления плагинами
        PlugInManager plugInManager;

        public MainWindow()
        {
            InitializeComponent();

            // Менеджер плагинов управляет внешним видом основного окна
            plugInManager = new PlugInManager(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Отобразить список обнаруженных плагинов
            plugInListbox.ItemsSource = plugInManager.GetPlugInsNames();
        }

        private void plugInListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Активировать выбранный пользователем плагин
            plugInManager.ActivatePlugIn(plugInListbox.SelectedItem.ToString());
        }
    }
}
