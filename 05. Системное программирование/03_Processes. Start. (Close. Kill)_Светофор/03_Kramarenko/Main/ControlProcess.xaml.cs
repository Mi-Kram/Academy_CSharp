using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using Forms = System.Windows.Forms;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для ControlProcess.xaml
    /// </summary>
    public partial class ControlProcess : Window
    {
        ObservableCollection<ProcessInfo> processCollection;
        public DispatcherTimer timer = new DispatcherTimer();
        public ControlProcess()
        {
            InitializeComponent();

            this.ShowInTaskbar = true;

            List<string> deniedProcessNames = new List<string>();
            deniedProcessNames = File.ReadAllLines($@"{Environment.CurrentDirectory}\Resources\dennyedProcesses.txt", Encoding.UTF8).ToList();

            processCollection = new ObservableCollection<ProcessInfo>();
            foreach (string processName in deniedProcessNames)
            {
                processCollection.Add(new ProcessInfo() { ProcessName = processName, IsStart = false, IsWorking = false });
            }
            dataGrid.ItemsSource = processCollection;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processCollection.Count(); i++)
            {
                try
                {
                    Process[] arr = processes.Where(x => x.ProcessName.ToLower() == processCollection[i].ProcessName.ToLower()).ToArray();
                    if (processCollection[i].IsStart)
                    {
                        foreach (Process item in arr)
                        {
                            if (!item.CloseMainWindow())
                                item.Kill();
                        }
                        processCollection[i].IsWorking = false;
                    }
                    else
                    {
                        processCollection[i].IsWorking = arr.Count() > 0;
                    }
                }
                catch { }
            }

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = processCollection;
        }

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                //ProcessInfo selected = dataGrid.SelectedItem as ProcessInfo;
                processCollection[dataGrid.SelectedIndex].IsStart = !processCollection[dataGrid.SelectedIndex].IsStart;
                //selected.IsStart = !selected.IsStart;
            }
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                try
                {
                    string name = (dataGrid.SelectedItem as ProcessInfo).ProcessName;
                    Process.Start($"{name}.exe");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.W)
            {
                string[] arr = Process.GetProcesses().Select(x => x.ProcessName).OrderBy(x => x).ToArray();
                File.WriteAllLines("processes.txt", arr);
            }
        }
    }
}
