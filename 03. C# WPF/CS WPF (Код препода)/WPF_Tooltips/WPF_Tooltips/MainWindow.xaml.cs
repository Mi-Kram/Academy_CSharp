using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_Tooltips
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Создание подсказки
            ToolTip tip = new ToolTip();

            // Создание содержимого подсказки
            StackPanel panel1 = new StackPanel();
            panel1.Orientation = Orientation.Vertical;
            Image im1 = new Image();
            im1.Source = new BitmapImage(new Uri(@"admin.gif", UriKind.RelativeOrAbsolute));
            Rectangle rect = new Rectangle();
            rect.Width = 50;
            rect.Height = 30;
            rect.Fill = Brushes.Blue;
            panel1.Children.Add(im1);
            panel1.Children.Add(rect);

            // Помещение содержимого в подсказку
            tip.Content = panel1;

            // Указание места показа подсказки
            tip.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;

            // Установить подсказку для окна
            button1.ToolTip = tip;
        }

        DispatcherTimer tm;

        private void button1_MouseEnter(object sender, MouseEventArgs e)
        {
            tm = new DispatcherTimer();
            tm.Tick += new EventHandler(tm_Tick);
            tm.Interval = new TimeSpan(0, 0, 1);
            tm.Start();
        }

        void tm_Tick(object sender, EventArgs e)
        {
            this.Background = Brushes.Aquamarine;
            tm.Stop();
        }
    }
}
