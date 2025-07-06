using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace SP_CriticalSections
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool res;

            // Попытаться занять mutex
            Mutex mutex = new Mutex(true, "mutex1", out res);
            if (res == false)
            {
                MessageBox.Show("Одна копия уже запущена. Закрываюсь...");
                mutex.WaitOne();
            }
            else
                Application.Run(new Form1());

            // Освободить mutex
            if (res)
                mutex.ReleaseMutex();
        }
    }
}