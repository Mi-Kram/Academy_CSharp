using MyMessengerMessage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace Server
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush runServerBrush, stopServerBrush;

        TcpListener listener;
        List<ClientInfo> clients;
        List<TaskInfo> listenerTasks;

        public MainWindow()
        {
            InitializeComponent();

            clients = new List<ClientInfo>();
            listenerTasks = new List<TaskInfo>();
            listener = null;

            runServerBrush = (SolidColorBrush)FindResource("RunServer");
            stopServerBrush = (SolidColorBrush)FindResource("StopServer");
        }

        private void PortTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            if (key == Key.Back || key == Key.Delete || key == Key.Left || key == Key.Right || key == Key.Tab) return;

            string s = key.ToString();
            if (s.Length != 1)
            {
                if (s.StartsWith("D")) s = s.Remove(0, 1);
                if (s.StartsWith("NumPad")) s = s.Remove(0, 6);
            }
            if (s.Length != 1 || !char.TryParse(s, out char ch) || !char.IsDigit(ch)) e.Handled = true;
        }


        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (listener == null)
            {
                try
                {
                    CreateListnerThread(IPAddress.Any, GetPort());
                    IsServerWorking.Fill = runServerBrush;
                }
                catch (Exception ex)
                {
                    IsServerWorking.Fill = stopServerBrush;
                    MessageBox.Show(ex.Message, "INFO", MessageBoxButton.OK);
                    if (listener != null) listener.Stop();
                    listener = null;
                }
            }
            else
            {
                MessageBox.Show("Сервер уже запущен!", "INFO", MessageBoxButton.OK);
            }
        }
        //private void Stop_Click(object sender, RoutedEventArgs e)
        //{
        //    Stop();
        //}

        void Stop()
        {
            if (listener != null)
            {
                try
                {
                    lock (listenerTasks)
                    {
                        foreach (TaskInfo taskInfo in listenerTasks)
                        {
                            taskInfo.cts.Cancel(true);
                        }
                        listenerTasks.Clear();
                        listener.Stop();
                    }

                    clients.Clear();
                    listener = null;
                    IsServerWorking.Fill = stopServerBrush;
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Сервер уже остановлен!", "INFO", MessageBoxButton.OK);
            }
        }
        int GetPort()
        {
            if (!Dispatcher.CheckAccess()) return Dispatcher.Invoke(() => GetPort());
            else
            {
                if (Int32.TryParse(PortTextBox.Text, out int port)) return port;
                else return -1;
            }
        }
        string CreateNewKeyID()
        {
            if (!Dispatcher.CheckAccess()) return Dispatcher.Invoke(() => CreateNewKeyID());
            else
            {
                string id = string.Empty;
                string[] keys;
                lock (clients)
                {
                    keys = clients.Select(x => x.ID).ToArray();
                }
                do
                {
                    id = Guid.NewGuid().ToString(@"N").ToLower();
                } while (keys.Contains(id));

                return id;
            }
        }


        void CreateListnerThread(IPAddress IPAdress, int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            // ожидание подключения клиента
            WaitNextClient();
        }
        void WaitNextClient()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task listenerWaitTask = new Task(DoListen, cts.Token);
            listenerTasks.Add(new TaskInfo(listenerWaitTask, cts));

            listenerWaitTask.Start();
        }


        void DoListen()
        {
            TcpClient client = listener.AcceptTcpClient();
            // ожидать следующего слушателя
            WaitNextClient();

            string id = string.Empty;

            NetworkStream netStream = client.GetStream();
            while (true)
            {
                if (netStream.CanRead)
                {
                    try
                    {
                        // чтение сериализованного int-а (размер сериализованного класс ChatMessage)
                        byte[] lenBuf = new byte[54];
                        netStream.Read(lenBuf, 0, 54);
                        int len = (int)Deserialize(lenBuf);

                        // чтение сериализованного класса
                        byte[] buf = new byte[len];
                        netStream.Read(buf, 0, len);
                        object obj = Deserialize(buf);

                        if (obj is ClientMessage)
                        {
                            ClientMessage message = obj as ClientMessage;
                            message.FromUserID = id;
                            SendMessage(message);
                        }
                        else if (obj is JoinExitMessage)
                        {
                            JoinExitMessage message = obj as JoinExitMessage;

                            if (message.type == JoinExitMessage.Type.Join)
                            {
                                id = CreateNewKeyID();
                                SendTo(client, new ClientIDMessage(id));

                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.Client = client;
                                clientInfo.ID = id;
                                clientInfo.Name = message.UserName;

                                lock (clients)
                                {
                                    clients.Add(clientInfo);
                                }
                            }
                            else
                            {
                                lock (clients)
                                {
                                    clients.RemoveAll(x => x.ID == id);
                                }
                            }
                            SendClientList();
                        }
                    }
                    catch { break; }
                }
                else { break; }
            }

            netStream.Close();
            client.Close();
        }

        void SendClientList()
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => SendClientList());
            else
            {
                lock (clients)
                {
                    ClientListMessage message = new ClientListMessage(clients.ToDictionary(key => key.ID, val => val.Name));

                    byte[] arr = Serialize(message);
                    byte[] arrLen = Serialize(arr.Length);

                    foreach (ClientInfo item in clients)
                    {
                        NetworkStream netStream = item.Client.GetStream();

                        if (netStream.CanRead)
                        {
                            try
                            {
                                netStream.Write(arrLen, 0, 54);
                                netStream.Write(arr, 0, arr.Length);
                                netStream.Flush();
                            }
                            catch { }
                        }
                    }
                }
            }
        }
        void SendMessage(ClientMessage message)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => SendMessage(message));
            else
            {
                try
                {
                    TcpClient client;
                    lock (clients)
                    {
                        client = clients.Where(x => x.ID == message.ToUserID).First().Client;
                    }
                    byte[] arr = Serialize(message);
                    byte[] arrLen = Serialize(arr.Length);

                    NetworkStream netStream = client.GetStream();

                    if (netStream.CanRead)
                    {
                        netStream.Write(arrLen, 0, 54);
                        netStream.Write(arr, 0, arr.Length);
                        netStream.Flush();
                    }
                }
                catch { }
            }
        }

        void SendTo(TcpClient client, ClientIDMessage message)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => SendTo(client, message));
            else
            {
                try
                {
                    byte[] arr = Serialize(message);
                    byte[] arrLen = Serialize(arr.Length);

                    NetworkStream netStream = client.GetStream();

                    if (netStream.CanRead)
                    {
                        netStream.Write(arrLen, 0, 54);
                        netStream.Write(arr, 0, arr.Length);
                        netStream.Flush();
                    }
                }
                catch { }
            }
        }

        byte[] Serialize(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(listener != null) Stop();
        }

        object Deserialize(byte[] arrBytes)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                object obj = binForm.Deserialize(memStream);
                return obj;
            }
        }
    }

    class TaskInfo
    {
        public Task task { get; set; }
        public CancellationTokenSource cts { get; set; }

        public TaskInfo(Task t, CancellationTokenSource c)
        {
            task = t;
            cts = c;
        }
    }
}
