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

namespace SP_WPF_Tasks
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

        delegate void MessageDel(string message);

        void PrintMessage(string message)
        {
            if (!Dispatcher.CheckAccess())  // запущена ли эта функция не в основном потоке?
            {
                MessageDel messageEvent = new MessageDel(PrintMessage);
                //Dispatcher.Invoke(messageEvent, new object[] { message });
                Dispatcher.BeginInvoke(messageEvent, new object[] { message });
            }
            else
            {
                messagesListBox.Items.Add(message);
            }
        }

        void testTask()
        {
            PrintMessage("test start...");
            Thread.Sleep(2000);
            PrintMessage("test end.");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task task1 = new Task(() =>
            {
                PrintMessage("Task 1 started...");
            });
            task1.Start();

            Task task2 = Task.Factory.StartNew(() => PrintMessage("Task 2 started..."));

            Task task = new Task(testTask);
            task.Start();

            // мёртвая блокировка
            //task.Wait();

            Task task3 = Task.Run(() =>
            {
                PrintMessage("Task 3 started...");
                PrintMessage("Task 3 ended.");
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var outerTask = Task.Factory.StartNew(() =>
            {
                PrintMessage("Outer task start...");
                var innerTask = Task.Factory.StartNew(() =>
                {
                    PrintMessage("Inner task start...");
                    Thread.Sleep(2000);
                    PrintMessage("Inner task end.");
                //}, TaskCreationOptions.AttachedToParent);
                });
                Thread.Sleep(1000);
                PrintMessage("Outer task end.");
            });

            //outerTask.Wait();
        }

        static int GetPrimeCount(int x)
        {
            int result = 0;
            for (int i = 2; i <= x; i++)
            {
                result++;
                for (int k = 2; k < i; k++)
                {
                    if (i % k == 0)
                    {
                        result--;
                        break;
                    }
                }
            }
            return result;
        }

        async void PrimeNumbersAsync()
        {
            PrintMessage("Start GetPrimeCount");
            int res = await Task.Run(() => GetPrimeCount(100000));
            PrintMessage($"Prime numbers = {res}");
            PrintMessage("End GetPrimeCount");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PrimeNumbersAsync();
        }
    }
}
