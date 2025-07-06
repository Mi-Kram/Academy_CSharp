using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.ServiceModel;
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
using Main.ServiceReference1;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IService1Callback
    {
        Service1Client server;

        Dictionary<string, (string fullPath, MemoryStream stream)> tempDicForFiles = new Dictionary<string, (string, MemoryStream)>();
        ObservableCollection<ItemInfo> collection = new ObservableCollection<ItemInfo>();
        string currentPath = "";
        bool isPressDataGridRow = false;

        public MainWindow()
        {
            InitializeComponent();
            Directory.CreateDirectory($"{Environment.CurrentDirectory}/Temp");
            server = new Service1Client(new InstanceContext(this as IService1Callback));
            dataGrid.ItemsSource = collection;
        }

        async void UpdateList()
        {
            if (!Dispatcher.CheckAccess())
                Dispatcher.Invoke(() => UpdateList());
            else
            {
                try
                {
                    collection.Clear();

                    FolderData list = await server.GetDataAsync(currentPath);
                    foreach (string folder in list.Folders)
                        collection.Add(new ItemInfo() { Type = ItemInfo.ItemType.Folder, Name = folder });

                    foreach (string file in list.Files)
                        collection.Add(new ItemInfo() { Type = ItemInfo.ItemType.File, Name = file });
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show("Нет подключения к серверу");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (await server.LogInAsync(LogInTextBox.Text, PasswordBox.Password))
                {
                    ShowMainGrid();
                }
                else
                {
                    MessageBox.Show("Ошибка");
                    LogInTextBox.Clear();
                    PasswordBox.Clear();
                }
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Нет подключения к серверу");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (await server.RegisterAsync(LogInTextBox.Text, PasswordBox.Password))
                {
                    ShowMainGrid();
                }
                else
                {
                    MessageBox.Show("Ошибка");
                    LogInTextBox.Clear();
                    PasswordBox.Clear();
                }
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Нет подключения к серверу");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ShowMainGrid()
        {
            MainGrid.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Collapsed;

            this.Height = 600;
            this.Width = 1000;

            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (500);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (300);
            UpdateList();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            int lastIndex = currentPath.LastIndexOf('/');
            if (lastIndex != -1)
            {
                currentPath = currentPath.Remove(lastIndex);
                serverPath_TextBox.Text = currentPath;
                UpdateList();

                if (currentPath == "")
                    ChangeStyleGoBackBtn(false);
            }
        }

        void ChangeStyleGoBackBtn(bool isOn)
        {
            GoBackBtn.Style = (Style)FindResource($"backBtnStyle{(isOn ? "On" : "Off")}");
        }


        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                ItemInfo itemInfo = Dispatcher.Invoke(() => (sender as DataGridRow).Item as ItemInfo); ;

                if (itemInfo.Type == ItemInfo.ItemType.Folder)
                {
                    bool changestyle = currentPath == "";

                    currentPath += $"/{itemInfo.Name}";
                    serverPath_TextBox.Text = currentPath;
                    UpdateList();

                    if (changestyle)
                        ChangeStyleGoBackBtn(true);
                }
            }
        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddFolderDialog dialog = new AddFolderDialog();
                dialog.Owner = this;

                if (dialog.ShowDialog() == true)
                {
                    if (collection.Count(x => x.Name.ToLower() == dialog.FolderName.ToLower()) != 0)
                    {
                        MessageBox.Show("Папка уже существует!");
                    }
                    else
                    {
                        if (dialog.FolderName != "" && server.CreateFolder($"{currentPath}/{dialog.FolderName}"))
                        {
                            UpdateList();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось создать папку!");
                        }
                    }
                }
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Нет подключения к серверу");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedItem != null)
                {
                    ItemInfo itemInfo = Dispatcher.Invoke(() => dataGrid.SelectedItem as ItemInfo);

                    bool isFolder = itemInfo.Type == ItemInfo.ItemType.Folder;

                    if (MessageBox.Show($"Удалить {(isFolder ? "папку" : "файл")} \"{itemInfo.Name}\"?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        bool result = isFolder ? server.RemoveFolder($"{currentPath}/{itemInfo.Name}") :
                            server.RemoveFile($"{currentPath}/{itemInfo.Name}");
                        if (result == true)
                        {
                            UpdateList();
                        }
                        else
                        {
                            MessageBox.Show($"Не удалось удалить {(isFolder ? "папку" : "файл")}!");
                        }
                    }
                }
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Нет подключения к серверу");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void dataGrid_Drop(object sender, DragEventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
                        (e.AllowedEffects & DragDropEffects.Copy) != 0 &&
                        !e.Data.GetDataPresent("DataGridItem"))
                    {
                        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                        byte[] buf = new byte[10000];

                        foreach (string path in files)
                        {
                            if (File.Exists(path))
                            {
                                using (FileStream sourceStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                                {
                                    using (MemoryStream memoryStream = new MemoryStream())
                                    {
                                        using (GZipStream compressionStream = new GZipStream(memoryStream, CompressionMode.Compress))
                                        {
                                            sourceStream.CopyTo(compressionStream);
                                        }

                                        byte[] array = memoryStream.ToArray();
                                        int tiles = array.Length / buf.Length;

                                        for (int i = 0; i < tiles; i++)
                                        {
                                            Array.Copy(array, i * buf.Length, buf, 0, buf.Length);
                                            server.CopyFile($"{currentPath}/{System.IO.Path.GetFileName(path)}", buf, false);
                                            Array.Clear(buf, 0, buf.Length);
                                        }

                                        Array.Copy(array, tiles * buf.Length, buf, 0, array.Length - (tiles * buf.Length));
                                        server.CopyFile($"{currentPath}/{System.IO.Path.GetFileName(path)}", buf, true);
                                    }
                                }
                            }
                            else if (Directory.Exists(path))
                            {
                                CopyDirToServer(path, currentPath);
                            }
                        }
                        UpdateList();
                    }
                }
                catch (CommunicationException ex)
                {
                    MessageBox.Show("Нет подключения к серверу");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        void CopyDirToServer(string path, string serverPath)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(path);

                serverPath += $"/{System.IO.Path.GetFileName(path)}";
                server.CreateFolder(serverPath);

                FileInfo[] files = dInfo.GetFiles();
                byte[] buf = new byte[10000];
                foreach (FileInfo file in files)
                {
                    using (FileStream sourceStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (GZipStream compressionStream = new GZipStream(memoryStream, CompressionMode.Compress))
                            {
                                sourceStream.CopyTo(compressionStream);
                            }

                            byte[] array = memoryStream.ToArray();
                            int tiles = array.Length / buf.Length;

                            for (int i = 0; i < tiles; i++)
                            {
                                Array.Copy(array, i * buf.Length, buf, 0, buf.Length);
                                server.CopyFile($"{serverPath}/{file.Name}", buf, false);
                                Array.Clear(buf, 0, buf.Length);
                            }

                            Array.Copy(array, tiles * buf.Length, buf, 0, array.Length - (tiles * buf.Length));
                            server.CopyFile($"{serverPath}/{file.Name}", buf, true);
                        }
                    }
                }

                DirectoryInfo[] dirs = dInfo.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    CopyDirToServer(dir.FullName, serverPath);
                }
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Нет подключения к серверу");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void DataGridRow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            isPressDataGridRow = true;
        }

        private void DataGridRow_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            isPressDataGridRow = false;
        }

        private async void DataGridRow_MouseLeave(object sender, MouseEventArgs e)
        {
            await Task.Run(() =>
            {
                if (isPressDataGridRow && sender != null)
                {
                    isPressDataGridRow = false;
                    ItemInfo itemInfo = Dispatcher.Invoke(() => (sender as DataGridRow).Item as ItemInfo);
                    DataObject dataObject = new DataObject();

                    string pathToTempDir = CopyToTempDir($"{currentPath}/{itemInfo.Name}", itemInfo);

                    StringCollection col = new StringCollection();
                    col.Add(pathToTempDir);
                    dataObject.SetFileDropList(col);

                    dataObject.SetData("DataGridItem", 0);
                    DragDropEffects effect = Dispatcher.Invoke(() => DragDrop.DoDragDrop(this, dataObject, DragDropEffects.Copy));
                }
            });
        }


        string CopyToTempDir(string serverPath, ItemInfo itemType)
        {
            try
            {
                string result = $"{Environment.CurrentDirectory}/Temp/{itemType.Name}";

                if (itemType.Type == ItemInfo.ItemType.File)
                {
                    if (File.Exists(result)) File.Delete(result);
                    byte[] buf = new byte[10000];

                    tempDicForFiles.Add(serverPath, (result, new MemoryStream()));
                    server.GetFile(serverPath); // ---------------------------------------------------------------------------------------------------------------------
                }
                else
                {
                    if (Directory.Exists(result)) Directory.Delete(result, true);
                    CopyDirToTempDir(result, serverPath);
                }

                return result;
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Нет подключения к серверу");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return string.Empty;
        }

        void CopyDirToTempDir(string path, string serverPath)
        {
            try
            {
                Directory.CreateDirectory(path);
                FolderData folderData = server.GetData(serverPath);

                foreach (string file in folderData.Files)
                {
                    byte[] buf = new byte[10000];
                    string fullServerPath = $"{serverPath}/{file}";

                    tempDicForFiles.Add(fullServerPath, ($"{path}/{file}", new MemoryStream()));
                    server.GetFile(fullServerPath); // ----------------------------------------------------------------------------------------------------------
                }

                foreach (string dir in folderData.Folders)
                {
                    CopyDirToTempDir($"{path}/{dir}", $"{serverPath}/{dir}");
                }
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show("Нет подключения к серверу");
            }
            catch { }
        }

        public void GetFilePart(string key, byte[] arr)
        {
            if (tempDicForFiles.ContainsKey(key))
            {
                tempDicForFiles[key].stream.Write(arr, 0, arr.Length);
            }
        }

        public void SetCloseStream(string key)
        {
            if (tempDicForFiles.ContainsKey(key))
            {
                try
                {
                    byte[] array = tempDicForFiles[key].stream.ToArray();
                    using (FileStream compressedStream = new FileStream($"{tempDicForFiles[key].fullPath}.gzip", FileMode.Create, FileAccess.ReadWrite))
                    {
                        compressedStream.Write(array, 0, array.Length);
                        compressedStream.Seek(0, SeekOrigin.Begin);

                        using (FileStream resultStream = File.Create(tempDicForFiles[key].fullPath))
                        {
                            using (GZipStream decompressionStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                            {
                                decompressionStream.CopyTo(resultStream);
                            }
                        }
                    }
                }
                catch { }
                finally
                {
                    if (File.Exists($"{tempDicForFiles[key].fullPath}.gzip"))
                        File.Delete($"{tempDicForFiles[key].fullPath}.gzip");

                    tempDicForFiles[key].stream.Close();
                    tempDicForFiles.Remove(key);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Directory.Delete($"{Environment.CurrentDirectory}/Temp", true);
        }
    }

    public class ItemInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public enum ItemType
        {
            Folder,
            File
        }

        private ItemType _type;
        private string _name;

        public ItemType Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));
        }
    }
}
