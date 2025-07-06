using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string source = $@"{Environment.CurrentDirectory}\Resources\Source";
        string dest = $@"{Environment.CurrentDirectory}\Resources\Dest";
        Thread thread = null;

        public MainWindow()
        {
            InitializeComponent();

            MyFile.ClearFiles(dest);
            //Refresh();

            FromDir.Text = source;
            ToDir.Text = dest;
        }


        void Refresh()
        {
            MyFile.ClearFiles(source, dest);
            MyFile.CreateSource(source);
            Close();
        }

        // выбрать папку source
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FromDir.Text = folderBrowser.SelectedPath;
            }
        }

        // выбрать папуку dest
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ToDir.Text = folderBrowser.SelectedPath;
            }
        }


        // кнопка старт
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (FromDir.Text.Length == 0 || ToDir.Text.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("Заполните все поля!", "INFO", MessageBoxButtons.OK);
                return;
            }
            if (!Directory.Exists(FromDir.Text))
            {
                System.Windows.Forms.MessageBox.Show("Папка источника не найдена!", "INFO", MessageBoxButtons.OK);
                FromDir.Text = "";
                return;
            }
            if (!Directory.Exists(ToDir.Text))
            {
                System.Windows.Forms.MessageBox.Show("Папка приёмника не найдена!", "INFO", MessageBoxButtons.OK);
                ToDir.Text = "";
                return;
            }

            StartCopy(FromDir.Text, ToDir.Text);
        }

        // инициализировать Thread
        void StartCopy(string source, string dest)
        {
            if (thread == null)
            {
                MyFile.ClearFiles(dest);
                progress.Value = 0;
                
                ParameterizedThreadStart pts = new ParameterizedThreadStart(copy);
                thread = new Thread(pts);
                thread.IsBackground = true;
                thread.Start(new string[] { source, dest });
            }
        }

        // запуск потока
        void copy(object obj)
        {
            bool flag = true;
            string source = (obj as string[])[0];
            string dest = (obj as string[])[1];
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(source);
                FileInfo[] files = dInfo.GetFiles();
                
                double length = GetDirLength(source);
                SetMaxValue(length);

                CopyDir(source, dest);
                MaxValue();
            }
            catch (Exception ex)
            {
                flag = false;
                System.Windows.Forms.MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK);
            }
            finally
            {
                if (flag) System.Windows.Forms.MessageBox.Show("Готово!", "INFO", MessageBoxButtons.OK);
                //thread.Abort();

                Thread tmp = thread;
                thread = null;
                tmp.Abort();
            }
        }

        // копировать файлы из папки source в папку dest
        void CopyDir(string source, string dest)
        {
            if (!Directory.Exists(dest)) Directory.CreateDirectory(dest);

            DirectoryInfo dInfo = new DirectoryInfo(source);
            FileInfo[] files = dInfo.GetFiles();

            foreach (FileInfo file in files)
            {
                File.Copy(file.FullName, dest + "\\" + file.Name);
                AddValue(file.Length + 1);
            }

            DirectoryInfo[] dirs = dInfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                CopyDir(dir.FullName, dest + "\\" + dir.Name);
            }
        }



        // размер папки
        double GetDirLength(string path)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);

            FileInfo[] files = dInfo.GetFiles();

            double sum = files.Sum(x => x.Length) + files.Count();

            DirectoryInfo[] dirs = dInfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                sum += GetDirLength(dir.FullName);
            }

            return sum;
        }



        // увеличить progress bar
        delegate void AddVal(double param);
        void AddValue(double val)
        {
            if (!Dispatcher.CheckAccess())  // запущена ли эта функция не в основном потоке?
            {
                AddVal addVal = new AddVal(AddValue);
                Dispatcher.Invoke(addVal, new object[] { val });
            }
            else
            {
                progress.Value += val;
            }
        }


        // заполнить progress bar на максимум
        delegate void MaxVal();
        void MaxValue()
        {
            if (!Dispatcher.CheckAccess())  // запущена ли эта функция не в основном потоке?
            {
                MaxVal maxVal = new MaxVal(MaxValue);
                Dispatcher.Invoke(maxVal, new object[] { });
            }
            else
            {
                progress.Value = progress.Maximum;
            }
        }


        // установить новое максимальное значение для progress bar
        delegate void SetMaxVal(double param);
        void SetMaxValue(double val)
        {
            if (!Dispatcher.CheckAccess())  // запущена ли эта функция не в основном потоке?
            {
                SetMaxVal setMaxVal = new SetMaxVal(SetMaxValue);
                Dispatcher.Invoke(setMaxVal, new object[] { val });
            }
            else
            {
                progress.Maximum = val;
            }
        }
    }
}

