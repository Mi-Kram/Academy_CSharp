using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Forms = System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly string PATH_TO_FOLDER = $@"{Environment.CurrentDirectory}\Resources\Source";
        readonly string PATH_TO_CONFIG = $@"{Environment.CurrentDirectory}\Resources\config.txt";
        readonly string PATH_TO_LOG = $@"{Environment.CurrentDirectory}\Resources\log.txt";
        public MainWindow()
        {
            InitializeComponent();

            MyIO.CreateFolder(PATH_TO_FOLDER);
            txtBox.Text = PATH_TO_FOLDER;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(txtBox.Text))
                Task.Run(() =>  RemoveFolders());
            else
            {
                MessageBox.Show("Папка не найдена!", "INFO", MessageBoxButton.OK);
                txtBox.Text = "";
                txtBox.Focus();
            }
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog dialog = new Forms.FolderBrowserDialog();
            if(dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                txtBox.Text = dialog.SelectedPath;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            MyIO.CreateFolder(PATH_TO_FOLDER);
        }
        void AddLog(string log, string path)
        {
            File.AppendAllText(log, $"{DateTime.Now.ToString(@"dd\.MM\.yyyy HH\:mm\:ss")}> {path}\n");
        }

        void RemoveFolders()
        {
            string config = GetConfig();
            string folder = GetFolder();
            string log = GetLog();

            SetButtonEbabled(startBtn, false);

            string[] delDirs = File.ReadAllLines(config);
            string[] removes = Directory.GetDirectories(folder, "*", SearchOption.AllDirectories)
                .Where(x => delDirs.Contains(System.IO.Path.GetFileName(x))).ToArray();

            double percent_of_file = removes.Count() > 0 ? (100 / removes.Count()) : 0;

            foreach (string dir in removes)
            {
                try
                {
                    if (Directory.Exists(dir))
                    {
                        Directory.Delete(dir, true);
                        AddLog(log, dir);
                    }
                }
                catch { }
                finally
                {
                    AddProgressValue(percent_of_file);
                }
            }

            SetProgressValue(100);
            MessageBox.Show("ГОТОВО!","INFO",MessageBoxButton.OK);
            SetButtonEbabled(startBtn, true);
            SetProgressValue(0);
        }

        string GetFolder()
        {
            if (!Dispatcher.CheckAccess())
            {
                return Dispatcher.Invoke(() => GetFolder());
            }
            else
            {
                return txtBox.Text;
            }
        }
        string GetConfig()
        {
            if (!Dispatcher.CheckAccess())
            {
                return Dispatcher.Invoke(() => GetConfig());
            }
            else
            {
                return PATH_TO_CONFIG;
            }
        }
        string GetLog()
        {
            if (!Dispatcher.CheckAccess())
            {
                return Dispatcher.Invoke(() => GetLog());
            }
            else
            {
                return PATH_TO_LOG;
            }
        }

        void SetProgressValue(double val)
        {
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(() => SetProgressValue(val));
            else
            {
                progress.Value = val;
                if (progress.Value > 100) progress.Value = 100;
            }
        }
        void AddProgressValue(double val)
        {
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(() => AddProgressValue(val));
            else
            {
                progress.Value += val;
                if (progress.Value > 100) progress.Value = 100;
            }
        }
        void SetButtonEbabled(Button btn, bool val)
        {
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(() => SetButtonEbabled(btn, val));
            else
            {
                btn.IsEnabled = val;
            }

        }
    }
}
