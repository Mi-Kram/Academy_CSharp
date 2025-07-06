using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SP_WPF_Mutex
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Mutex mutex;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool res = false;

            // попытка занять Mutex
            mutex = new Mutex(true, "mutex1", out res);

            // проверить занятость Mutex (если занят, то одна копия программы уже запущена)
            if (res == false)
            {
                    MessageBox.Show("Одна копия уже запущена. Закрываюсь...");
                    Environment.Exit(0);
            }

            // попытка занять Mutex
            /*mutex = new Mutex(false, "mutex1", out res);

            // подождать занятия Mutex в течение определённого времени
            if (mutex.WaitOne(5000) == false)
            {
                MessageBox.Show("Одна копия уже запущена. Закрываюсь...");
                Environment.Exit(0);
            }*/
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // при закрытии программы освободить занятый Mutex
            mutex.ReleaseMutex();
        }
    }
}
