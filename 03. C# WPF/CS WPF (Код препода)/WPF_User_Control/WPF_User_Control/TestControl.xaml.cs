using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WPF_User_Control
{
    /// <summary>
    /// Interaction logic for TestControl.xaml
    /// </summary>
    public partial class TestControl : UserControl, INotifyPropertyChanged
    {
        // необходимо для сообщения об изменении значений свойств
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        int age = 0;
        public int Age {
            get => age;
            set {
                age = value;
                Console.WriteLine($"Age = {age}");

                // сообщить всем привязанным к свойству Age элементам управления о том, что его значение изменилось
                OnPropertyChanged("Age");
            } 
        }

        public static DependencyProperty WeightProperty;

        public int Weight
        {
            get { return (int)GetValue(WeightProperty); }
            set { SetValue(WeightProperty, value); }
        }

        static TestControl()
        {
            WeightProperty = DependencyProperty.Register("Weight", typeof(int), typeof(TestControl),
                new FrameworkPropertyMetadata(0, new PropertyChangedCallback(OnWeightChanged)));
        }

        public TestControl()
        {
            InitializeComponent();

            // передать свойства из кода в XAML-разметку для привязки к элементам управления
            DataContext = this;
        }

        private static void OnWeightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TestControl testControl = (TestControl)sender;
            Console.WriteLine($"Weight = {testControl.Weight}");
        }
    }
}
