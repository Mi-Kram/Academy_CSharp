using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread lights = null;

        SolidColorBrush redON, redOFF;
        SolidColorBrush yellowON, yellowOFF;
        SolidColorBrush greenON, greenOFF;

        public MainWindow()
        {
            InitializeComponent();

            redON = (SolidColorBrush)FindResource("redON");
            redOFF = (SolidColorBrush)FindResource("redOFF");
            yellowON = (SolidColorBrush)FindResource("yellowON");
            yellowOFF = (SolidColorBrush)FindResource("yellowOFF");
            greenON = (SolidColorBrush)FindResource("greenON");
            greenOFF = (SolidColorBrush)FindResource("greenOFF");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (lights == null)
            {
                lights = new Thread(TurnOnLights); lights.IsBackground = true;
                lights.Start();
            }
        }
        void TurnOnLights()
        {
            while (true)
            {
                Thread redThread = new Thread(new ParameterizedThreadStart(Red)); redThread.IsBackground = true;
                redThread.Start(new int[] { 5000, 500, 700, 500, 700, 500, 700, 200 });
                redThread.Join();

                Thread yellowThread = new Thread(new ParameterizedThreadStart(Yellow)); yellowThread.IsBackground = true;
                yellowThread.Start(new int[] { 3000 });
                yellowThread.Join();

                Thread greenThread = new Thread(new ParameterizedThreadStart(Green)); greenThread.IsBackground = true;
                greenThread.Start(new int[] { 5000, 500, 700, 500, 700, 500, 700, 200 });
                greenThread.Join();

                yellowThread = new Thread(new ParameterizedThreadStart(Yellow)); yellowThread.IsBackground = true;
                yellowThread.Start(new int[] { 3000 });
                yellowThread.Join();
            }
        }



        void Green(object obj)
        {
            int[] delays = obj as int[];

            for (int i = 0; i < delays.Length; i++)
            {
                SetGreenLight(i % 2 == 0 ? greenON : greenOFF);
                Thread.Sleep(delays[i]);
            }
            SetGreenLight(greenOFF);
        }
        void Yellow(object obj)
        {
            int[] delays = obj as int[];

            for (int i = 0; i < delays.Length; i++)
            {
                SetYellowLight(i % 2 == 0 ? yellowON : yellowOFF);
                Thread.Sleep(delays[i]);
            }
            SetYellowLight(yellowOFF);
        }
        void Red(object obj)
        {
            int[] delays = obj as int[];

            for (int i = 0; i < delays.Length; i++)
            {
                SetRedLight(i % 2 == 0 ? redON : redOFF);
                Thread.Sleep(delays[i]);
            }
            SetRedLight(redOFF);
        }




        delegate void NewColor(SolidColorBrush color);
        void SetGreenLight(SolidColorBrush color)
        {
            if (!Dispatcher.CheckAccess())
            {
                NewColor newColor = new NewColor(SetGreenLight);
                Dispatcher.Invoke(newColor, new object[] { color });
            }
            else
            {
                green.Fill = color;
            }
        }
        void SetYellowLight(SolidColorBrush color)
        {
            if (!Dispatcher.CheckAccess())
            {
                NewColor newColor = new NewColor(SetYellowLight);
                Dispatcher.Invoke(newColor, new object[] { color });
            }
            else
            {
                yellow.Fill = color;
            }
        }
        void SetRedLight(SolidColorBrush color)
        {
            if (!Dispatcher.CheckAccess())
            {
                NewColor newColor = new NewColor(SetRedLight);
                Dispatcher.Invoke(newColor, new object[] { color });
            }
            else
            {
                red.Fill = color;
            }
        }

    }
}
