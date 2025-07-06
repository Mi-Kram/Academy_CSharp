using System;
using System.Collections.Generic;
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
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitializeAsync();
        }

        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                string address = string.Empty;
                if (!addressBar.Text.ToLower().StartsWith("http://")) address = "http://";
                address += addressBar.Text;
                webView.CoreWebView2.Navigate(address);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.GoBack();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.GoForward();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            webView.CoreWebView2.Reload();
        }

        private void addressBar_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (webView != null && webView.CoreWebView2 != null)
                {
                    string address = string.Empty;
                    if (!addressBar.Text.ToLower().StartsWith("http://")) address = "http://";
                    address += addressBar.Text;
                    webView.CoreWebView2.Navigate(address);
                }
            }
        }


        DirectoryInfo GetDirectory(string findUrl, string dirPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(dirPath);
            DirectoryInfo[] dirs = dInfo.GetDirectories();

            foreach (DirectoryInfo dir in dirs)
            {
                FileInfo[] files = dir.GetFiles("url.txt");
                if (files.Length == 1)
                {
                    string curUrl = File.ReadAllText(files[0].FullName, Encoding.UTF8);

                    if (curUrl == findUrl)
                        return dir;
                }
            }

            return null;
        }

        DirectoryInfo CreateDirectory(string directory, string dirName = "Site")
        {
            string path = CreateDirectoryName(directory, dirName);

            DirectoryInfo result = new DirectoryInfo($"{directory}/{path}");
            result.Create();

            return result;
        }
        string CreateDirectoryName(string directory, string dirName = "Site")
        {
            DirectoryInfo dInfo = new DirectoryInfo(directory);
            DirectoryInfo[] dirs = dInfo.GetDirectories();

            string[] dirNames = dirs.Select(x => x.Name.ToLower()).ToArray();
            if (!dirNames.Contains(dirName.ToLower())) return dirName;

            string path = string.Empty;
            int cnt = 1;
            do
            {
                path = $"{dirName}{cnt++}";

            } while (dirNames.Contains(path.ToLower()));

            return path;
        }

        void Clear(DirectoryInfo source)
        {
            DirectoryInfo[] dirs = source.GetDirectories();
            FileInfo[] files = source.GetFiles();

            foreach (DirectoryInfo dir in dirs)
            {
                dir.Delete(true);
            }

            foreach (FileInfo file in files)
            {
                file.Delete();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (webView.Source.Port != -1)
            {
                if (!int.TryParse(depthValue.Text, out int depth) || depth < 0) depth = 0;
                if (depth > 10) depth = 10;

                DateTime start = DateTime.Now;
                Save(webView.Source.ToString(), $"{Environment.CurrentDirectory}/Resources/Sites", depth);
                DateTime end = DateTime.Now;

                MessageBox.Show($"Saved!\n{(end-start).ToString(@"mm\:ss")} minutes");

            }
        }


        void Save(string url, string catalog, int depth, bool isFirst = true, string newFolder = "")
        {
            DirectoryInfo siteDir = null;
            if (newFolder == "")
            {
                siteDir = GetDirectory(url, catalog);

                if (siteDir == null) siteDir = CreateDirectory(catalog);
                else Clear(siteDir);
            }
            else
            {
                siteDir = CreateDirectory(catalog, newFolder);
            }
            string curDir = siteDir.Name;
            File.WriteAllText($"{siteDir.FullName}/url.txt", webView.Source.ToString(), Encoding.UTF8);

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string htmlText = client.DownloadString(new Uri(url));

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlText);

            HtmlNodeCollection imgNodes = htmlDocument.DocumentNode.SelectNodes("//img");
            if (imgNodes != null)
            {
                string imgPath = "Images";
                string imgFolder = $"{siteDir.FullName}/Images";
                if (!Directory.Exists(imgFolder)) Directory.CreateDirectory(imgFolder);
                int cnt = 1;

                foreach (HtmlNode node in imgNodes)
                {
                    if (node != null)
                    {
                        string imgUrl = node.Attributes["src"].Value;
                        string imgName = $"Img{cnt++}{System.IO.Path.GetExtension(imgUrl)}";

                        string imgDirPath = $"{imgFolder}/{imgName}";
                        string localImg = $"{imgPath}/{imgName}";
                        try
                        {
                            byte[] data = client.DownloadData(new Uri(imgUrl));
                            File.WriteAllBytes(imgDirPath, data);

                            //node.Attributes["src"].Value = localImg;
                            node.Attributes["src"].Value = $"{imgFolder}/{imgName}";
                        }
                        catch { }
                    }
                }
            }

            if (depth > 0)
            {
                HtmlNodeCollection aNodes = htmlDocument.DocumentNode.SelectNodes("//a");
                if (aNodes != null)
                {
                    string refPath = "Refs";
                    string refFodler = $"{siteDir.FullName}/Refs";
                    if (!Directory.Exists(refFodler)) Directory.CreateDirectory(refFodler);
                    int cnt = 1;

                    foreach (HtmlNode node in aNodes)
                    {
                        if (node != null && node.Attributes["href"] != null && node.Attributes["href"].Value != null)
                        {
                            string refUrl = node.Attributes["href"].Value;
                            string refName = $"Ref{cnt++}";

                            string refDirPath = $"{refFodler}/{refName}";
                            string localRef = $"{refPath}/{refName}/site.html";

                            try
                            {
                                //node.Attributes["href"].Value = localRef;
                                node.Attributes["href"].Value = $"{refDirPath}/site.html";
                                Save(refUrl, refFodler, depth - 1, false, refName);
                            }
                            catch { }
                        }
                    }
                }


                //HtmlNodeCollection lNodes = htmlDocument.DocumentNode.SelectNodes("//link");
                //if (lNodes != null)
                //{
                //    string linkPath = "Links";
                //    string linkFodler = $"{siteDir.FullName}/Links";
                //    if (!Directory.Exists(linkFodler)) Directory.CreateDirectory(linkFodler);
                //    int cnt = 1;

                //    foreach (HtmlNode node in lNodes)
                //    {
                //        if (node != null && node.Attributes["href"] != null && node.Attributes["href"].Value != null)
                //        {
                //            string refUrl = node.Attributes["href"].Value;
                //            string refName = $"Link{cnt++}";

                //            string refDirPath = $"{linkFodler}/{refName}";
                //            string localRef = $"{linkPath}/{refName}";

                //            try
                //            {
                //                node.Attributes["href"].Value = localRef;
                //                Save(refUrl, linkFodler, depth - 1, false, refName);
                //            }
                //            catch { }
                //        }
                //    }
                //}
            }

            htmlDocument.Save($"{siteDir.FullName}/site.html", Encoding.UTF8);

            if (isFirst && !offlineSites.Items.Cast<TextBlock>().Select(x => x.Text).Contains(url))
            {
                offlineSites.Items.Add(new TextBlock() { Text = url, ToolTip = url, Tag = $"{siteDir.FullName}/site.html" });
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (offlineSites.SelectedItem != null)
            {
                HtmlWeb htmlWeb = new HtmlWeb();
                htmlWeb.OverrideEncoding = Encoding.UTF8;
                HtmlDocument htmlDocument = htmlWeb.Load((offlineSites.SelectedItem as TextBlock).Tag as string);

                webView.CoreWebView2.NavigateToString(htmlDocument.Text);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo dInfo = new DirectoryInfo($"{Environment.CurrentDirectory}/Resources/Sites");
            DirectoryInfo[] dirs = dInfo.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                FileInfo[] files = dir.GetFiles("url.txt");
                if(files.Count() == 1)
                {
                    string url = File.ReadAllText(files[0].FullName, Encoding.UTF8);
                    offlineSites.Items.Add(new TextBlock() { Text = url, ToolTip = url, Tag = $"{dir.FullName}/site.html" });
                }
            }
            if (offlineSites.Items.Count > 0) offlineSites.SelectedIndex = 0;
        }
    }
}
