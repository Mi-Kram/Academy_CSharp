using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace SP_Process
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

        DispatcherTimer timer;

        ObservableCollection<ProcessInfo> processesList = new ObservableCollection<ProcessInfo>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Тормозим таймер
            timer.Stop();

            // запомнить номер текущего выделенного элемента
            int selectedIndex = processTable.SelectedIndex;

            processesList.Clear();

            // Получение списка запущенных процессов
            Process[] processes = Process.GetProcesses();

            foreach (var proc in processes)
            {
                try
                {
                    // создать инфо о процессе
                    ProcessInfo processInfo = new ProcessInfo()
                    {
                        // Вставка в строку идентификатора процесса
                        Id = proc.Id,

                        // Добавление имени главного модуля процесса
                        MainModuleName = proc.MainModule.ModuleName,

                        // Отвечает ли процесс на запросы
                        Responding = proc.Responding,

                        // Заголовок главного окна
                        MainWindowTitle = proc.MainWindowTitle,

                        // Количество потоков
                        ThreadsCount = proc.Threads.Count,

                        // Количество дескрипторов
                        HandleCount = proc.HandleCount,

                        // Объем занимаемой памяти в Кб
                        UsingMemory = proc.WorkingSet64 / 1024
                    };

                    processesList.Add(processInfo);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);

                    // Если нет доступа
                    continue;
                }
            }

            // показать список процессов
            processTable.ItemsSource = processesList;

            // вернуть выделение в таблицу 
            processTable.SelectedIndex = selectedIndex;
            processTable.Focus();

            // Запуск таймера
            timer.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe");

            Process process = new Process();
            process.StartInfo.FileName = "calc.exe";
            process.EnableRaisingEvents = true;
            process.Exited += Process_Exited;
            process.Start();
            
            // подождать, пока закроется какой-то процесс
            //process.WaitForExit();
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            MessageBox.Show("Calc.exe exited!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(processTable.SelectedItem != null)
            {
                // получить идентификатор выделенного процесса
                int id = ((ProcessInfo)processTable.SelectedItem).Id;

                // находим процесс по идентификатору
                Process pr = Process.GetProcessById(id);

                if (pr == null)
                    return;

                try
                {
                    // закрыть главное окно (если оно есть)
                    if (!pr.CloseMainWindow())

                        // остановка процесса 
                        pr.Kill();
                }
                catch (Exception ex)
                {
                    // не сложилось
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // получить идентификатор выделенного процесса
            int id = ((ProcessInfo)processTable.SelectedItem).Id;

            Process pr = Process.GetProcessById(id);

            ProcessThreadCollection threads = pr.Threads;
            string s = "";
            foreach (ProcessThread tr in threads)
            {
                s += tr.Id.ToString();
                s += "  ";
                s += tr.ThreadState.ToString();
                s += "  ";
                s += tr.StartTime.ToString();
                s += "\n";
            }
            MessageBox.Show(s);
        }
    }
}
