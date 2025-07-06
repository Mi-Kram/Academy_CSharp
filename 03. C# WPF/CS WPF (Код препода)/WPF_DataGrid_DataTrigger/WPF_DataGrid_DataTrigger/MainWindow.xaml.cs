using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF_DataGrid_DataTrigger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Car> cars = new ObservableCollection<Car>();

        public MainWindow()
        {

            InitializeComponent();

            cars.Add(new Car(1, "BMW", "X5", 250, 45000));
            cars.Add(new Car(2, "Opel", "Insignia", 220, 54000));
            cars.Add(new Car(3, "Fiat", "Punto", 170, 12450));
            cars.Add(new Car(4, "Nissan", "GTR", 300, 123990));
            cars.Add(new Car(5, "Audi", "Quattro", 250, 34000));
            cars.Add(new Car(9, "Bugatti", "Shiron", 415, 2000000));
            cars.Add(new Car(6, "Aston Martin", "DB9", 285.5, 23500));
            cars.Add(new Car(7, "Lada", "Riva", 120, 8900));
            cars.Add(new Car(8, "Pagani", "Zonda F", 380, 1500000));

            DataContext = cars;
        }
    }
}
