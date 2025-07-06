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
using System.Threading;

namespace TrafficLight
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

        enum TrafficLightStates { Red, YellowAfterRed, YellowAfterGreen, Green };

        TrafficLightStates trafficLightState = TrafficLightStates.YellowAfterGreen;

        private void SetColor(Ellipse ellipse, Brush brush)
        {
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(() => SetColor(ellipse, brush));
            else
            {
                try
                {
                    ellipse.Fill = brush;
                }
                catch (OperationCanceledException) { }
            }
        }

        private void RunLight(Ellipse ellipse, Brush brush, int delay)
        {
            lock(this)
            {
                switch(trafficLightState)
                {
                    case TrafficLightStates.Red:
                        if(brush == Brushes.Yellow)
                        {
                            trafficLightState = TrafficLightStates.YellowAfterRed;
                            SetColor(redEllipse, Brushes.Transparent);
                            SetColor(ellipse, brush);
                            Thread.Sleep(delay);
                        }
                        break;
                    case TrafficLightStates.YellowAfterRed:
                        if (brush == Brushes.Green)
                        {
                            trafficLightState = TrafficLightStates.Green;
                            SetColor(YellowEllipse, Brushes.Transparent);
                            SetColor(ellipse, brush);
                            Thread.Sleep(delay);
                        }
                        break;
                    case TrafficLightStates.YellowAfterGreen:
                        if (brush == Brushes.Red)
                        {
                            trafficLightState = TrafficLightStates.Red;
                            SetColor(YellowEllipse, Brushes.Transparent);
                            SetColor(ellipse, brush);
                            Thread.Sleep(delay);
                        }
                        break;
                    case TrafficLightStates.Green:
                        if (brush == Brushes.Yellow)
                        {
                            trafficLightState = TrafficLightStates.YellowAfterGreen;
                            SetColor(GreenEllipse, Brushes.Transparent);
                            SetColor(ellipse, brush);
                            Thread.Sleep(delay);
                        }
                        break;
                }
            }
        }

        private void RunSemaphoreLight(Ellipse ellipse, Brush brush, int delay)
        {
            // запуск lambda-выражения в потоке с заданием свойств класса Thread
            // (new Thread(() => { while (true) RunLight(ellipse, brush, delay); }) { IsBackground = true }).Start();
            
            Thread t = new Thread(() => { while (true) RunLight(ellipse, brush, delay); }) { IsBackground = true };
            t.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RunSemaphoreLight(redEllipse, Brushes.Red, 1000);
            RunSemaphoreLight(YellowEllipse, Brushes.Yellow, 2000);
            RunSemaphoreLight(GreenEllipse, Brushes.Green, 3000);
        }

    }
}
