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

namespace WPF_Data_Binding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyProcesses lst = new MyProcesses();

            //List<string> lst = new List<string>();
            //lst.Add("hello");
            //lst.Add("12412");

            this.DataContext = lst;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            rectangle1.Width = 500;
        }
    }

    public class MyProcesses : List<string>
    {
        public MyProcesses()
        {
            System.Diagnostics.Process[] procLst = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process p in procLst)
            {
                Add(p.ProcessName);
            }
        }
    }
}
