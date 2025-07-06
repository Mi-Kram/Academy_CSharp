using System;
using System.Collections.Generic;
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
using System.IO;
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;

namespace Main2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread thread1 = null, thread2 = null, thread3 = null;
        DateTime start1, end1;
        DateTime start2, end2;
        DateTime start3, end3;
        char[] delims = new char[] { ' ', '\r', '\n' };

        string pathToSource  = $@"{Environment.CurrentDirectory}\Resources\Source";
        string pathToResult1 = $@"{Environment.CurrentDirectory}\Resources\result01.txt";
        string pathToResult2 = $@"{Environment.CurrentDirectory}\Resources\result02.txt";
        string pathToResult3 = $@"{Environment.CurrentDirectory}\Resources\result03.txt";

        private class DirFile
        {
            public string PathToDir { get; set; }
            public string PathToFile { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            start1 = new DateTime(); end1 = new DateTime();
            start2 = new DateTime(); end2 = new DateTime();
            start3 = new DateTime(); end3= new DateTime();
        }



        #region Variant1

        private void Variant1_Click(object sender, RoutedEventArgs e)
        {
            if (thread1 == null)
            {
                ParameterizedThreadStart pr = new ParameterizedThreadStart(WriteDictionaryV1);
                thread1 = new Thread(pr);
                thread1.IsBackground = true;

                start1 = DateTime.Now;
                StartV1();
                thread1.Start(new DirFile { PathToDir = pathToSource, PathToFile = pathToResult1 });
            }
        }

        void WriteDictionaryV1(object obj)
        {
            DirFile dirFile = obj as DirFile;

            try
            {
                Dictionary<string, int> dic = GetWordsDictionaryV1(dirFile.PathToDir).OrderBy(x => x.Value)
                    .ThenBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

                WriteToFileV1(dic, dirFile.PathToFile);
                end1 = DateTime.Now;
                SetResult1((end1-start1).ToString(@"hh\:mm\:ss\.fff"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INFO", MessageBoxButton.OK);
                SetResult1("00:00:00.000");
            }
            finally
            {
                Thread tmp = thread1;

                FinalyV1();

                thread1 = null;
                tmp.Abort();
            }
        }

        Dictionary<string, int> GetWordsDictionaryV1(string path)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            DirectoryInfo dInfo = new DirectoryInfo(path);
            if (dInfo.Exists)
            {
                FileInfo[] files = dInfo.GetFiles("*.txt", SearchOption.AllDirectories);
                foreach (FileInfo file in files)
                {
                    string[] words = File.ReadAllText(file.FullName, Encoding.Default).ToLower()
                        .Split(delims, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string word in words)
                    {
                        if (result.ContainsKey(word)) result[word]++;
                        else result.Add(word, 1);
                    }

                    // вариант, который точно удаляет все лишнии символы, но работает долго
                    //MatchCollection matches = Regex.Matches(File.ReadAllText(file.FullName, Encoding.Default).ToLower(), @"[а-яa-z]+");
                    //foreach (Match item in matches)
                    //{
                    //    if (result.ContainsKey(item.Value)) result[item.Value]++;
                    //    else result.Add(item.Value, 1);
                    //}
                }
            }

            return result;
        }

        void WriteToFileV1(Dictionary<string, int> dic, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);

            int Vweight = dic.Values.Select(x => x.ToString()).Max(x => x.Length);

            foreach (KeyValuePair<string, int> item in dic)
            {
                sw.WriteLine($"{item.Value.ToString().PadLeft(Vweight, '0')}:   {item.Key}");
            }

            sw.Close();
            fs.Close();
        }


        #endregion


        #region Variant2

        private void Variant2_Click(object sender, RoutedEventArgs e)
        {
            if (thread2 == null)
            {
                ParameterizedThreadStart pr = new ParameterizedThreadStart(WriteDictionaryV2);
                thread2 = new Thread(pr);
                thread2.IsBackground = true;

                start2 = DateTime.Now;
                StartV2();
                thread2.Start(new DirFile { PathToDir = pathToSource, PathToFile = pathToResult2 });
            }
        }

        string[] GetPathToFilesV2(string path)
        {
            List<string> lst = new List<string>();

            DirectoryInfo dInfo = new DirectoryInfo(path);
            if (dInfo.Exists)
            {
                lst.AddRange(dInfo.GetFiles("*.txt", SearchOption.AllDirectories).Select(x => x.FullName));
            }

            return lst.ToArray();
        }

        void WriteDictionaryV2(object obj)
        {
            DirFile dirFile = obj as DirFile;

            try
            {
                string[] paths = GetPathToFilesV2(dirFile.PathToDir);

                Dictionary<string, int> dic1 = null;
                Dictionary<string, int> dic2 = null;

                Thread thread1 = new Thread(() => { dic1 = GetWordsDictionaryV2(paths.Take(paths.Length / 2).ToArray()); });
                Thread thread2 = new Thread(() => { dic2 = GetWordsDictionaryV2(paths.Skip(paths.Length / 2).ToArray()); });

                thread1.Start();
                thread2.Start();

                thread1.Join();
                thread2.Join();

                foreach (KeyValuePair<string, int> item in dic2)
                {
                    if (dic1.ContainsKey(item.Key)) dic1[item.Key] += item.Value;
                    else dic1.Add(item.Key, item.Value);
                }

                Dictionary<string, int> result = dic1.OrderBy(x => x.Value).ThenBy(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.Value);

                WriteToFileV2(result, dirFile.PathToFile);
                end2 = DateTime.Now;
                SetResult2((end2 - start2).ToString(@"hh\:mm\:ss\.fff"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SetResult2("00:00:00.000");
            }
            finally
            {
                Thread tmp = thread2;

                FinalyV2();

                thread2 = null;
                tmp.Abort();
            }
        }

        Dictionary<string, int> GetWordsDictionaryV2(string[] paths)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (string path in paths)
            {
                string[] words = File.ReadAllText(path, Encoding.Default).ToLower()
                    .Split(delims, StringSplitOptions.RemoveEmptyEntries);

                foreach (string word in words)
                {
                    if (result.ContainsKey(word)) result[word]++;
                    else result.Add(word, 1);
                }

                // вариант, который точно удаляет все лишнии символы, но работает долго
                //MatchCollection matches = Regex.Matches(File.ReadAllText(path, Encoding.Default).ToLower(), @"[а-яa-z]+");
                //foreach (Match item in matches)
                //{
                //    if (result.ContainsKey(item.Value)) result[item.Value]++;
                //    else result.Add(item.Value, 1);
                //}
            }

            return result;
        }

        void WriteToFileV2(Dictionary<string, int> dic, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);

            var Vweight = dic.Values.Select(x => x.ToString()).Max(x => x.Length);

            foreach (KeyValuePair<string, int> item in dic)
            {
                sw.WriteLine($"{item.Value.ToString().PadLeft(Vweight, '0')}:   {item.Key}");
            }

            sw.Close();
            fs.Close();
        }


        #endregion


        #region Variant3

        Dictionary<string, int> dicV3;
        private void Variant3_Click(object sender, RoutedEventArgs e)
        {
            if(thread3 == null)
            {
                ParameterizedThreadStart pr = new ParameterizedThreadStart(WriteDictionaryV3);
                thread3 = new Thread(pr);
                thread3.IsBackground = true;

                dicV3 = new Dictionary<string, int>();

                StartV3();
                start3 = DateTime.Now;
                thread3.Start(new DirFile() { PathToDir = pathToSource, PathToFile = pathToResult3 });
            }
        }

        string[] GetPathToFilesV3(string path)
        {
            List<string> lst = new List<string>();

            DirectoryInfo dInfo = new DirectoryInfo(path);
            if (dInfo.Exists)
            {
                lst.AddRange(dInfo.GetFiles("*.txt",SearchOption.AllDirectories).Select(x => x.FullName));
            }

            return lst.ToArray();
        }

        void WriteDictionaryV3(object obj)
        {
            DirFile dirFile = obj as DirFile;
            try
            {
                string[] paths = GetPathToFilesV3(dirFile.PathToDir);

                ParameterizedThreadStart pr1 = new ParameterizedThreadStart(FillDicV3);
                ParameterizedThreadStart pr2 = new ParameterizedThreadStart(FillDicV3);

                Thread thread1 = new Thread(pr1); thread1.IsBackground = true;
                Thread thread2 = new Thread(pr2); thread2.IsBackground = true;

                thread1.Start(paths.Take(paths.Length / 2).ToArray());
                thread2.Start(paths.Skip(paths.Length / 2).ToArray());

                thread1.Join();
                thread2.Join();

                Dictionary<string, int> result = dicV3.OrderBy(x => x.Value).ThenBy(x => x.Key)
                    .ToDictionary(x => x.Key, x => x.Value);

                WriteToFileV3(result, dirFile.PathToFile);
                end3 = DateTime.Now;
                SetResult3((end3 - start3).ToString(@"hh\:mm\:ss\.fff"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                SetResult2("00:00:00.000");
            }
            finally
            {
                Thread tmp = thread3;

                FinalyV3();

                thread3 = null;
                tmp.Abort();
            }
        }

        void FillDicV3(object obj)
        {
            string[] paths = obj as string[];

            foreach (string path in paths)
            {
                string[] words = File.ReadAllText(path, Encoding.Default).ToLower()
                    .Split(delims, StringSplitOptions.RemoveEmptyEntries);

                lock (dicV3)
                {
                    foreach (string word in words)
                    {
                        if (dicV3.ContainsKey(word)) dicV3[word]++;
                        else dicV3.Add(word, 1);
                    }
                }

                // вариант, который точно удаляет все лишнии символы, но работает долго
                //MatchCollection matches = Regex.Matches(File.ReadAllText(path, Encoding.Default).ToLower(), @"[а-яa-z]+");
                //lock (dicV3)
                //{
                //    foreach (Match item in matches)
                //    {
                //        if (dicV3.ContainsKey(item.Value)) dicV3[item.Value]++;
                //        else dicV3.Add(item.Value, 1);
                //    }
                //}
            }
        }

        void WriteToFileV3(Dictionary<string, int> dic, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);

            var Vweight = dic.Values.Select(x => x.ToString()).Max(x => x.Length);

            foreach (KeyValuePair<string, int> item in dic)
            {
                sw.WriteLine($"{item.Value.ToString().PadLeft(Vweight, '0')}:   {item.Key}");
            }

            sw.Close();
            fs.Close();
        }


        #endregion






        void VariantsClick(SolidColorBrush brush)
        {
            TextBlock msg = new TextBlock();
            msg.Text = "Time updated";
            msg.VerticalAlignment = VerticalAlignment.Top;
            msg.HorizontalAlignment = HorizontalAlignment.Center;
            msg.Foreground = brush;
            msg.FontSize = 12;
            msg.FontWeight = FontWeights.Bold;
            msg.Tag = "msgForDelete";
            msg.Margin = new Thickness(0, -20, 0, 0);
            mainGrid.Children.Add(msg);

            ThicknessAnimation ta = new ThicknessAnimation(new Thickness(0, 90, 0, 0), TimeSpan.FromSeconds(2));
            ta.Completed += Ta_Completed;
            DoubleAnimation da = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
            da.BeginTime = TimeSpan.FromSeconds(1.5);

            msg.BeginAnimation(ContentControl.MarginProperty, ta);
            msg.BeginAnimation(ContentControl.OpacityProperty, da);
        }
        private void Ta_Completed(object sender, EventArgs e)
        {
            for (int i = 0; i < mainGrid.Children.Count; i++)
            {
                if(mainGrid.Children[i] is TextBlock && ((mainGrid.Children[i] as TextBlock).Tag as string) == "msgForDelete")
                {
                    mainGrid.Children.Remove(mainGrid.Children[i]);
                    break;
                }
            }
        }




        delegate void finV();
        void StartV1()
        {
            BtnV1.Content = "Wait...";
            BtnV1.IsEnabled = false;
        }
        void FinalyV1()
        {
            if (!Dispatcher.CheckAccess())
            {
                finV finV = new finV(FinalyV1);
                Dispatcher.Invoke(finV);
            }
            else
            {
                BtnV1.Content = "Variant1";
                BtnV1.IsEnabled = true;

                VariantsClick(Brushes.Red);
            }
        }
        void StartV2()
        {
            BtnV2.Content = "Wait...";
            BtnV2.IsEnabled = false;
        }
        void FinalyV2()
        {
            if (!Dispatcher.CheckAccess())
            {
                finV finV = new finV(FinalyV2);
                Dispatcher.Invoke(finV);
            }
            else
            {
                BtnV2.Content = "Variant2";
                BtnV2.IsEnabled = true;

                VariantsClick(Brushes.Green);
            }
        }
        void StartV3()
        {
            BtnV3.Content = "Wait...";
            BtnV3.IsEnabled = false;
        }
        void FinalyV3()
        {
            if (!Dispatcher.CheckAccess())
            {
                finV finV = new finV(FinalyV3);
                Dispatcher.Invoke(finV);
            }
            else
            {
                BtnV3.Content = "Variant3";
                BtnV3.IsEnabled = true;

                VariantsClick(Brushes.Blue);
            }
        }



        delegate void SetResult(string value);
        void SetResult1(string value)
        {
            if (!Dispatcher.CheckAccess())
            {
                SetResult setResult = new SetResult(SetResult1);
                Dispatcher.Invoke(setResult, new object[] { value });
            }
            else
            {
                result1.Text = value;
                toolTipV1.Text = $"Best: {Min(toolTipV1.Text, value)}";
            }
        }
        void SetResult2(string value)
        {
            if (!Dispatcher.CheckAccess())
            {
                SetResult setResult = new SetResult(SetResult2);
                Dispatcher.Invoke(setResult, new object[] { value });
            }
            else
            {
                result2.Text = value;
                toolTipV2.Text = $"Best: {Min(toolTipV2.Text, value)}";
            }
        }
        void SetResult3(string value)
        {
            if (!Dispatcher.CheckAccess())
            {
                SetResult setResult = new SetResult(SetResult3);
                Dispatcher.Invoke(setResult, new object[] { value });
            }
            else
            {
                result3.Text = value;
                toolTipV3.Text = $"Best: {Min(toolTipV3.Text, value)}";
            }
        }


        string Min(string bestTime, string newTime)
        {
            try
            {
                if (bestTime == "Best: 00:00:00.000") return newTime;
                bestTime = bestTime.Remove(0, 6);

                for (int i = 0; i < bestTime.Length; i++)
                {
                    if (char.IsDigit(bestTime[i]))
                    {
                        int a = Convert.ToInt32(bestTime[i]), b = Convert.ToInt32(newTime[i]);
                        if (a == b) continue;
                        if (a < b) return bestTime;
                        else return newTime;
                    }
                }

                return bestTime;
            }
            catch
            {
                return bestTime;
            }
        }
    }
}
