using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        Mutex mutex = new Mutex();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            mutex = new Mutex(true, "threadHW2", out bool res);
            //if (!mutex.WaitOne(3000))
            //{
            //    MessageBox.Show("Приложение уже запущено!", "Отмена запуска", MessageBoxButton.OK);
            //    Environment.Exit(0);
            //}
            if (!res)
            {
                MessageBox.Show("Приложение уже запущено!", "Отмена запуска", MessageBoxButton.OK);
                Environment.Exit(0);
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            mutex.ReleaseMutex();
        }
    }
}
