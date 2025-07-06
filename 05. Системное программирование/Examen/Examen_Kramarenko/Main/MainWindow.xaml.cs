using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Diagnostics;
using FolderItemsSearcher;
using FolderItem;
using System.Reflection;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread thread = null;
        public MainWindow()
        {
            InitializeComponent();

            PlugInOfColorForWindow.Window = this;

            List<PlugInOfColorForWindow> plugs = PlugInOfColorForWindow.GetPlugins("ColorDLL").OrderBy(x => x.Title.ToLower()).ToList();
            foreach (PlugInOfColorForWindow item in plugs)
            {
                colorPlugsCmb.Items.Add(new TextBlock() { Text = item.Title, Tag = item });
            }



            colorPlugsCmb.SelectedIndex = 0;
        }

        void AddFolderItem(DirItemInfo item)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => AddFolderItem(item));
            else
            {
                dataGrid.Items.Add(item);
                AddProgressBarValue(1);
            }
        }


        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (thread != null)
            {
                if (thread.IsAlive)
                {
                    MessageBox.Show("Программа уже работает!", "INFO", MessageBoxButton.OK);
                    return;
                }
                thread = null;
            }
            string path = dirTextBox.Text;

            if (Directory.Exists(path))
            {
                dataGrid.Items.Clear();

                thread = new Thread(() => FindFolderItems(path));
                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                MessageBox.Show("Folder is not found!", "INFO", MessageBoxButton.OK);
                dirTextBox.Focus();
            }
        }

        void FindFolderItems(string path)
        {
            try
            {
                string folderSearch = GetFolderSearch();
                string fileSearch = GetFileSearch();
                if (folderSearch == "") folderSearch = "*";
                if (fileSearch == "") folderSearch = "*.*";

                FolderItemsSearcher.Files searcher = new FolderItemsSearcher.Files();

                searcher.OnNewItem += AddFolderItem;
                searcher.OnSetProgressBarMaximum += SetProgressBarMaximum;
                searcher.OnTakeAwayProgressBarMaximum += TakeAwayProgressBarMaximum;

                SetProgressBarValue(0);

                searcher.Search(path, folderSearch, fileSearch);
            }
            catch (Exception ex)
            {
                if (!(ex is ThreadAbortException))
                    MessageBox.Show(ex.Message, "INFO", MessageBoxButton.OK);
            }
        }


        string GetFolderSearch()
        {
            if (!Dispatcher.CheckAccess())
                return Dispatcher.Invoke(() => GetFolderSearch());
            else
                return (maskaStackPanel.Children[0] as TextBlock).DataContext as string;
        }
        string GetFileSearch()
        {
            if (!Dispatcher.CheckAccess())
                return Dispatcher.Invoke(() => GetFileSearch());
            else
                return (maskaStackPanel.Children[1] as TextBlock).DataContext as string;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (thread != null)
            {
                if (thread.IsAlive)
                {
                    if (thread.ThreadState == (System.Threading.ThreadState.Background | System.Threading.ThreadState.Suspended)) thread.Resume();
                    thread.Abort();
                }
                thread = null;
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (thread != null && thread.IsAlive)
            {
                thread.Suspend();
            }
        }

        private void Resume_Click(object sender, RoutedEventArgs e)
        {
            if (thread != null && thread.IsAlive &&
                thread.ThreadState == (System.Threading.ThreadState.Background | System.Threading.ThreadState.Suspended))
            {
                thread.Resume();
            }
        }

        private void Maska_Click(object sender, RoutedEventArgs e)
        {
            MaskaDialog dialog = new MaskaDialog();
            dialog.folderMaska = (maskaStackPanel.Children[0] as TextBlock).DataContext as string;
            dialog.fileMaska = (maskaStackPanel.Children[1] as TextBlock).DataContext as string;
            dialog.ShowDialog();

            if (dialog.DialogResult == true)
            {
                (maskaStackPanel.Children[0] as TextBlock).DataContext = dialog.folderMaska;
                (maskaStackPanel.Children[1] as TextBlock).DataContext = dialog.fileMaska;
            }
        }

        void SetProgressBarMaximum(double max)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => SetProgressBarMaximum(max));
            else progressBar.Maximum = max;
        }
        void AddProgressBarValue(double val)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => AddProgressBarValue(val));
            else progressBar.Value += val;
        }
        void SetProgressBarValue(double val)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => SetProgressBarValue(val));
            else progressBar.Value = val;
        }
        void TakeAwayProgressBarMaximum(double val)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => TakeAwayProgressBarMaximum(val));
            else progressBar.Maximum -= val;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DirItemInfo item = (sender as DataGridRow).Item as DirItemInfo;
            if (item.Ext != null && item.Ext.ToLower() == ".exe")
            {
                try
                {
                    Process.Start(item.Path);
                }
                catch { }
            }
        }

        private void colorPlugsCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(colorPlugsCmb.SelectedItem != null)
            {
                FrameworkElement info = colorPlugsCmb.SelectedItem as FrameworkElement;
                PlugInOfColorForWindow.ActivatePlugIn((PlugInOfColorForWindow)info.Tag);
            }
        }
    }


    public class PlugInOfColorForWindow
    {
        public static Window Window { get; set; } = null;
        public string Title { get; set; }
        public Type Type { get; set; }
        public MethodInfo MethodInfo { get; set; }

        public PlugInOfColorForWindow()
        {
            Title = "";
            Type = null;
            MethodInfo = null;
        }




        public static List<PlugInOfColorForWindow> GetPlugins(string path)
        {
            List<PlugInOfColorForWindow> result = new List<PlugInOfColorForWindow>();

            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(path);
                FileInfo[] files = dinfo.GetFiles("*.dll");

                foreach (FileInfo file in files)
                {
                    if (CheckPlugIn(file.FullName, out Type plugInType, out MethodInfo plugInMethod))
                    {
                        result.Add(new PlugInOfColorForWindow()
                        {
                            Title = System.IO.Path.GetFileNameWithoutExtension(file.Name),
                            Type = plugInType,
                            MethodInfo = plugInMethod
                        });
                    }
                }
            }
            catch { }

            return result;
        }
        static bool CheckPlugIn(string path, out Type type, out MethodInfo method)
        {
            type = null;
            method = null;

            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(path);
            }
            catch
            {
                return false;
            }
            if (assembly == null)
                return false;

            foreach (Type currentType in assembly.GetTypes())
            {
                if (currentType.Name == "ChangeWindowColors")
                {
                    type = currentType;
                    break;
                }
            }
            if (type == null || !type.IsClass)
                return false;

            foreach (MethodInfo currentMethod in type.GetMethods())
            {
                if (currentMethod.Name == "ChangeColor")
                {
                    method = currentMethod;
                    break;
                }
            }
            if (method == null || !method.IsPublic)
                return false;

            return true;
        }
        public static void ActivatePlugIn(PlugInOfColorForWindow plugInInfo)
        {
            if(Window != null)
            {
                if (Window != null && plugInInfo != null)
                {
                    object obj = Activator.CreateInstance(plugInInfo.Type);
                    plugInInfo.MethodInfo.Invoke(obj, new object[] { Window });
                }
            }
            else
            {
                MessageBox.Show("Статическое поле Window == null", "Indo", MessageBoxButton.OK);
            }
        }
    }
}
