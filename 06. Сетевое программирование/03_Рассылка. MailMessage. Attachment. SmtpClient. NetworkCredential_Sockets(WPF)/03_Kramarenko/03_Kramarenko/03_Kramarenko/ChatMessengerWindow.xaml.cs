using MyMessengerMessage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace _03_Kramarenko
{
    /// <summary>
    /// Логика взаимодействия для ChatMessengerWindow.xaml
    /// </summary>
    public partial class ChatMessengerWindow : Window
    {
        Style messagePanelStyle;
        Style myMsgStyle, clientMsgStyle;

        public string UserName { get; set; }
        public string ID { get; set; }
        TcpClient ownClient;

        List<MyClientInfo> myClients;

        StackPanel clearPanel;

        void Initial()
        {
            UserName = "";
            ownClient = null;
            myClients = new List<MyClientInfo>();

            clearPanel = new StackPanel();
            clearPanel.Style = messagePanelStyle;
            clearPanel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7F7F7F"));
            messagePanelScrlViewer.Content = clearPanel;

            messagePanelStyle = (Style)FindResource("MessagePanelStyle");
            myMsgStyle = (Style)FindResource("MyMsgStyle");
            clientMsgStyle = (Style)FindResource("ClientMsgStyle");
        }

        public ChatMessengerWindow(string userNAme, int port)
        {
            InitializeComponent();
            Initial();

            UserName = userNAme;

            try
            { 
                ownClient = new TcpClient(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), port);

                Thread SocketThread = new Thread(ClientDoListen);
                SocketThread.IsBackground = true;
                SocketThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INFO", MessageBoxButton.OK);
                Environment.Exit(0);
            }
        }

        void ClientDoListen()
        {
            NetworkStream netStream = ownClient.GetStream();
            while (true)
            {
                if (netStream.CanRead)
                {
                    try
                    {
                        byte[] lenBuf = new byte[54];
                        netStream.Read(lenBuf, 0, 54);
                        int len = (int)Deserialize(lenBuf);

                        byte[] buf = new byte[len];
                        netStream.Read(buf, 0, len);
                        object obj = Deserialize(buf);

                        if(obj is ClientMessage)
                        {
                            Dispatcher.Invoke(() => 
                            {
                                ClientMessage message = obj as ClientMessage;

                                Control control = new Control();
                                control.DataContext = message;
                                control.Style = clientMsgStyle;

                                MyClientInfo clientInfo = myClients.Where(x => x.ID == message.FromUserID).First();
                                bool isOnCurrentPanel = clientInfo.MessagePanel == messagePanelScrlViewer.Content as StackPanel;
                                bool isBottom = isOnCurrentPanel && (messagePanelScrlViewer.VerticalOffset == messagePanelScrlViewer.ScrollableHeight);
                                clientInfo.MessagePanel.Children.Add(control);
                                if (isBottom) messagePanelScrlViewer.ScrollToEnd();
                                if (!isOnCurrentPanel) clientInfo.NewMessageCnt++;
                            });
                        }
                        else if(obj is ClientListMessage)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                ClientListMessage message = obj as ClientListMessage;

                                for (int i = 0; i < myClients.Count; i++)
                                {
                                    if (!message.Clients.ContainsKey(myClients[i].ID)) RemoveClient(myClients[i].ID);
                                }

                                IEnumerable<string> IDs = myClients.Select(x => x.ID);
                                foreach (KeyValuePair<string, string> pair in message.Clients)
                                {
                                    if (pair.Key != ID && !IDs.Contains(pair.Key))
                                    {
                                        AddClient(pair.Key, pair.Value);
                                    }
                                }
                            });
                        }
                        else if(obj is ClientIDMessage)
                        {
                            ClientIDMessage message = obj as ClientIDMessage;
                            ID = message.ID;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Lost connection", "INFO", MessageBoxButton.OK);
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            netStream.Close();
            Dispatcher.Invoke(() => Close());
        }

        void AddClient(string clientID, string clientName)
        {
            StackPanel msgsPanel = new StackPanel();
            msgsPanel.Style = messagePanelStyle;

            MyClientInfo myClientInfo = new MyClientInfo(clientID, clientName, msgsPanel);
            myClients.Add(myClientInfo);

            ClientList.Items.Add(new ListBoxItem() { DataContext = myClientInfo });
        }
        void RemoveClient(string clientID)
        {
            lock (ClientList)
            {
                for (int i = 0; i < ClientList.Items.Count; i++)
                {
                    if (((ClientList.Items[i] as FrameworkElement).DataContext as MyClientInfo).ID == clientID)
                    {
                        ClientList.Items.RemoveAt(i--);
                    }
                }

                myClients.RemoveAll(x => x.ID == clientID);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MeData.DataContext = new { Name = UserName, RegisterDate = DateTime.Now.ToString(@"HH\:mm") };

            JoinExitMessage message = new JoinExitMessage(UserName, JoinExitMessage.Type.Join);
            SendMessage(message);
        }

        private void SendMessage(JoinExitMessage message)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => SendMessage(message));
            else
            {
                try
                {
                    NetworkStream netSream = ownClient.GetStream();
                    if (netSream.CanRead)
                    {
                        byte[] arr = Serialize(message);
                        byte[] arrLen = Serialize(arr.Length);

                        netSream.Write(arrLen, 0, 54);
                        netSream.Write(arr, 0, arr.Length);
                        netSream.Flush();
                    }
                }
                catch { }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            JoinExitMessage message = new JoinExitMessage(UserName, JoinExitMessage.Type.Exit);
            SendMessage(message);
            ownClient.Close();
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

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        void SendMessage()
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => SendMessage());
            else
            {
                StackPanel messagePanel = messagePanelScrlViewer.Content as StackPanel;
                if (clearPanel != messagePanel && messagePanel != null)
                {
                    sendMsg_TextBox.Text = sendMsg_TextBox.Text.TrimEnd(' ', '\n', '\t');

                    if (sendMsg_TextBox.Text.Length != 0)
                    {
                        ClientMessage message = new ClientMessage("", UserName, ((ClientList.SelectedItem as ListBoxItem).DataContext as MyClientInfo).ID, sendMsg_TextBox.Text, DateTime.Now);

                        Control control = new Control();
                        control.DataContext = message;
                        control.Style = myMsgStyle;
                        ((ClientList.SelectedItem as ListBoxItem).DataContext as MyClientInfo).MessagePanel.Children.Add(control);

                        NetworkStream netSteam = ownClient.GetStream();

                        if (netSteam.CanRead)
                        {
                            byte[] arr = Serialize(message);
                            byte[] arrLen = Serialize(arr.Length);

                            try
                            {
                                netSteam.Write(arrLen, 0, 54);
                                netSteam.Write(arr, 0, arr.Length);
                                netSteam.Flush();
                            }
                            catch { }
                        }

                        sendMsg_TextBox.Text = "";
                    }
                }
            }
        }

        private void sendMsg_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (e.KeyboardDevice.Modifiers == ModifierKeys.Control || e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                {
                    sendMsg_TextBox.Text += "\n";
                    sendMsg_TextBox.SelectionStart = sendMsg_TextBox.Text.Length;
                }
                else SendMessage();
                e.Handled = true;
            }
        }

        private void ClientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ClientList.SelectedItem != null)
            {
                MyClientInfo clientInfo = (ClientList.SelectedItem as ListBoxItem).DataContext as MyClientInfo;
                messagePanelScrlViewer.Content = clientInfo.MessagePanel;
                sendMsgBorder.Visibility = Visibility.Visible;
                messagePanelScrlViewer.ScrollToEnd();
                clientInfo.NewMessageCnt = 0;
            }
            else
            {
                StackPanel messagePanel = messagePanelScrlViewer.Content as StackPanel;
                if (clearPanel != null && messagePanel != clearPanel) messagePanelScrlViewer.Content = clearPanel;
                sendMsgBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void UnFocusListBoxItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClientList.SelectedItem = null;
        }

        private void ClientList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClientList.SelectedItem = null;
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

    class MyClientInfo : INotifyPropertyChanged
    {
        int _newMessageCnt;
        public string ID { get; set; }
        public string Name { get; set; }
        public int NewMessageCnt
        {
            get => _newMessageCnt;
            set
            {
                _newMessageCnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NewMessageCnt)));
            }
        }
        public StackPanel MessagePanel { get; set; }

        public MyClientInfo()
        {
            ID = "";
            Name = "";
            NewMessageCnt = 0;
            MessagePanel = null;
        }
        public MyClientInfo(string id, string name, StackPanel messagePanel, int newMessageCnt = 0)
        {
            ID = id;
            Name = name;
            NewMessageCnt = newMessageCnt;
            MessagePanel = messagePanel;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
