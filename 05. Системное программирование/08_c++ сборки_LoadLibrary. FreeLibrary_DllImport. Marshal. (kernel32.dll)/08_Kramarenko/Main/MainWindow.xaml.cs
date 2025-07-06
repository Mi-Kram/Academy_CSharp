using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CS_DLL CS_DLL;
        public MainWindow()
        {
            InitializeComponent();

            FolderPathTextBox.Text = $@"{Environment.CurrentDirectory}\Source";

            FindCS_DLL(out CS_DLL);
        }

        [DllImport("CPlus_CreateWordsDictionary", SetLastError = true, EntryPoint = "CreateWordsDictionary", CallingConvention = CallingConvention.Cdecl)]
        static extern unsafe void CPlus_CreateWordsDictionary([MarshalAs(UnmanagedType.LPStr)] string folderPath, [MarshalAs(UnmanagedType.LPStr)] string resultPath);
        
        



        void FindCS_DLL(out CS_DLL CS_DLL)
        {
            CS_DLL = null;

            Type plugInType = null;
            MethodInfo plugInMethod = null;

            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom($@"{Environment.CurrentDirectory}\CS_DLL\CS_CreateWordsDictionary.dll");
            }
            catch
            {
                return;
            }
            if (assembly == null) return;

            foreach (Type currentType in assembly.GetTypes())
            {
                if (currentType.Name == "Words")
                {
                    plugInType = currentType;
                    break;
                }
            }
            if (plugInType == null || !plugInType.IsClass) return;

            foreach (MethodInfo currentMethod in plugInType.GetMethods())
            {
                if (currentMethod.Name == "CreateWordsDictionary")
                {
                    plugInMethod = currentMethod;
                    break;
                }
            }
            if (plugInMethod == null) return;

            CS_DLL = new CS_DLL { Type = plugInType, MethodInfo = plugInMethod };
        }

        string Convert(TimeSpan ts) => ts.ToString(@"hh\:mm\:ss\.fff");

        private void CSharp_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(FolderPathTextBox.Text))
            {
                Task.Run(() =>
                {
                    Button btn = Dispatcher.Invoke(() => CS_Btn);
                    CS_DLL dll = Dispatcher.Invoke(() => CS_DLL);
                    TextBlock time = Dispatcher.Invoke(() => CSharpTime);

                    try
                    {
                        SetButtonEnable(btn, false);
                        DateTime start = DateTime.Now;

                        object obj = Activator.CreateInstance(dll.Type);
                        object[] arg = new object[2] { GetFolderPath(), $@"{Environment.CurrentDirectory}\CS_Result.txt" };
                        dll.MethodInfo.Invoke(obj, arg);

                        DateTime end = DateTime.Now;
                        SetTextBlockText(time, Convert(end - start));
                    }
                    catch { }
                    finally
                    {
                        SetButtonEnable(btn, true);
                    }
                });
            }
            else
            {
                MessageBox.Show("Папка не найдена!", "INFO", MessageBoxButton.OK);
                FolderPathTextBox.Focus();
            }
        }



        string GetFolderPath()
        {
            if (!Dispatcher.CheckAccess())
                return Dispatcher.Invoke(() => GetFolderPath());
            else
                return FolderPathTextBox.Text;
        }
        CS_DLL GetCS_DLL()
        {
            if (!Dispatcher.CheckAccess())
                return Dispatcher.Invoke(() => GetCS_DLL());
            else
                return CS_DLL;
        }

        void SetButtonEnable(Button btn, bool value)
        {
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(() => SetButtonEnable(btn, value));
            else
                btn.IsEnabled = value;
        }
        void SetTextBlockText(TextBlock txtBlock, string value)
        {
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(() => SetTextBlockText(txtBlock, value));
            else
                txtBlock.Text = value;
        }


        private void CPlusPlus_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(FolderPathTextBox.Text))
            {
                Task.Run(() =>
                {
                    Button btn = Dispatcher.Invoke(() => CPlus_Btn);
                    TextBlock time = Dispatcher.Invoke(() => CPlusTime);

                    try
                    {
                        SetButtonEnable(btn, false);
                        DateTime start = DateTime.Now;

                        CPlus_CreateWordsDictionary(GetFolderPath(), $@"{Environment.CurrentDirectory}\CPlus_Result.txt");
                        
                        DateTime end = DateTime.Now;
                        SetTextBlockText(time, Convert(end - start));
                    }
                    catch (Exception ex) { }
                    finally
                    {
                        SetButtonEnable(btn, true);
                    }
                });
            }
            else
            {
                MessageBox.Show("Папка не найдена!", "INFO", MessageBoxButton.OK);
                FolderPathTextBox.Focus();
            }
        }
    }

    class CS_DLL
    {
        public Type Type { get; set; }
        public MethodInfo MethodInfo { get; set; }
    }

}
