using System;
using System.Threading;
using System.Windows;

namespace Threading
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread t1 = null;
        int prevprime = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        void thread_func(object obj)
        {
            for (; ; )
            {
                NextPrime();
                Thread.Sleep(10);
            }
        }

        delegate void PrimeDel(int param);

        void PrintPrime(int param)
        {
            if (!Dispatcher.CheckAccess())  // запущена ли эта функция не в основном потоке?
            {
                PrimeDel primeEv = new PrimeDel(PrintPrime);
                Dispatcher.Invoke(primeEv, new object[] { param });
            }
            else
            {
                listbox1.Items.Add(param.ToString());
            }
        }

        void NextPrime()
        {
            int newprime = prevprime + 1;
            bool ctrl = true;

            for (int k = 2; k <= prevprime; k++)
            {
                if (newprime % k == 0)
                {
                    ctrl = false;

                    break;
                }
            }

            if (ctrl)
            {
                //listbox1.Items.Add(newprime.ToString());
                PrintPrime(newprime);
            }

            prevprime = newprime;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (t1 == null)
            {
                ParameterizedThreadStart pts = new ParameterizedThreadStart(thread_func);
                t1 = new Thread(pts);
                t1.IsBackground = true;
                t1.Start();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            t1.Abort();
            t1 = null;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            t1.Suspend();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            t1.Resume();
        }
    }
}
