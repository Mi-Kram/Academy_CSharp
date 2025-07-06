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
using System.Windows.Shapes;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        Style thisMsgStyle, anotherMsgStyle, joinExitMsgStyle;

        public string UserName { get; set; }

        TcpListener listener;

        List<TcpClient> clients;
        TcpClient ownClient;

        Task listenerWaitTask;
        CancellationTokenSource cts;

        void Initial()
        {
            UserName = "";
            listener = null;
            ownClient = null;
            clients = new List<TcpClient>();
            listenerWaitTask = null;
            cts = null;

            thisMsgStyle = (Style)FindResource("MessageThisPCStyle");
            anotherMsgStyle = (Style)FindResource("MessageAnotherPCStyle");
            joinExitMsgStyle = (Style)FindResource("MessageJoinExitStyle");
        }
        public ChatWindow()
        {
            InitializeComponent();

            Initial();
        }
        public ChatWindow(IPAddress IPAdress, int port)
        {
            InitializeComponent();

            Initial();
            Title = "Chat (Server)";

            try
            {
                CreateListnerThread(IPAdress, port);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INFO", MessageBoxButton.OK);
                Environment.Exit(0);
            }
        }
        public ChatWindow(string hostName, int port)
        {
            InitializeComponent();

            Initial();
            Title = "Chat (Client)";

            try
            {
                CreateClientThread(hostName, port);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INFO", MessageBoxButton.OK);
                Environment.Exit(0);
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserName.Length == 0) UserName = "User";
            if (ownClient != null)
            {
                ChatMessage joinMessage = new ChatMessage() { Message = $"New user {UserName} connected", MsgType = ChatMessage.MessageType.JoinExitMessage };
                Control message = CreateMsgControl(joinMessage);
                SendMessage(joinMessage, false);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ownClient != null)
            {
                ChatMessage exitMessage = new ChatMessage() { Message = $"User {UserName} disconnected", MsgType = ChatMessage.MessageType.JoinExitMessage };
                Control message = CreateMsgControl(exitMessage);
                SendMessage(exitMessage, false);
            }
            else if (listener != null)
            {
                if (listenerWaitTask != null) cts.Cancel();
                listener.Stop();
            }
        }


        void CreateListnerThread(IPAddress IPAdress, int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            // ожидание подключения клиента
            WaitNextClient();
        }
        void CreateClientThread(string hostName, int port)
        {
            ownClient = new TcpClient(hostName, port);
            clients.Add(ownClient);

            Thread SocketThread = new Thread(ClientDoListen);
            SocketThread.IsBackground = true;
            SocketThread.Start();
        }
        void WaitNextClient()
        {
            cts = new CancellationTokenSource();
            listenerWaitTask = new Task(DoListen, cts.Token);
            listenerWaitTask.Start();
        }


        void DoListen()
        {
            TcpClient client = null;
            client = listener.AcceptTcpClient();
            clients.Add(client);

            // ожидать следующего слушателя
            WaitNextClient();

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
                        int len = (int)DeserializeMessage(lenBuf);

                        // чтение сериализованного класс ChatMessage
                        byte[] buf = new byte[len];
                        netStream.Read(buf, 0, len);
                        ChatMessage chatMessage = (ChatMessage)DeserializeMessage(buf);

                        // если сообщение личное - заменить на сообщение от клиента
                        if (chatMessage.MsgType == ChatMessage.MessageType.MyMessage) chatMessage.MsgType = ChatMessage.MessageType.ClientMessage;
                        // разослать всем сообщение, кроме себя (client)
                        SendMessage(chatMessage, true, client);
                    }
                    catch { break; }
                }
                else
                {
                    break;
                }

            }

            netStream.Close();
            client.Close();
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
                        // чтение сериализованного int-а (размер сериализованного класс ChatMessage)
                        byte[] lenBuf = new byte[54];
                        netStream.Read(lenBuf, 0, 54);
                        int len = (int)DeserializeMessage(lenBuf);

                        // чтение сериализованного класс ChatMessage
                        byte[] buf = new byte[len];
                        netStream.Read(buf, 0, len);
                        ChatMessage chatMessage = (ChatMessage)DeserializeMessage(buf);

                        // если сообщение личное - заменить на сообщение от клиента
                        if (chatMessage.MsgType == ChatMessage.MessageType.MyMessage) chatMessage.MsgType = ChatMessage.MessageType.ClientMessage;
                        // добавить сообщение себе на панель
                        Dispatcher.Invoke(() =>
                        {
                            Control message = CreateMsgControl(chatMessage);
                            AddMessageToPanel(message);
                        });
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


        void SendMessage()
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => SendMessage());
            else
            {
                // удалить лишние символы в конце
                sendMsg_TextBox.Text = sendMsg_TextBox.Text.TrimEnd(' ', '\n', '\t');

                // если в сообщение есть текст - отправить
                if (sendMsg_TextBox.Text.Length != 0)
                {
                    ChatMessage chatMessage = new ChatMessage(UserName, sendMsg_TextBox.Text,
                        DateTime.Now, ChatMessage.MessageType.MyMessage, sendMsg_TextBox.LineCount);

                    SendMessage(chatMessage, true);
                    sendMsg_TextBox.Text = "";
                }
            }
        }
        void SendMessage(ChatMessage chatMessage, bool addToPanel, params TcpClient[] senders)
        {
            if (!Dispatcher.CheckAccess()) Dispatcher.Invoke(() => SendMessage(chatMessage, addToPanel, senders));
            else
            {
                // если сообщение надо добавить себе на экран
                if (addToPanel)
                {
                    Control message = CreateMsgControl(chatMessage);
                    AddMessageToPanel(message);
                }

                byte[] arr = SerializeMessage(chatMessage);
                byte[] lenArr = SerializeMessage(arr.Length);

                // рассылка всем клкиентам
                for (int i = 0; i < clients.Count; i++)
                {
                    TcpClient client = clients[i];
                    // если нет связи с клиентом - удалить его
                    if (!client.Connected)
                    {
                        clients.RemoveAt(i--);
                        continue;
                    }
                    // если текущий клиент состоит в списке отправителей - пропустить, перейти на следующую итерацию
                    if (senders.Contains(client)) continue;

                    NetworkStream netStream = client.GetStream();
                    if (netStream.CanWrite)
                    {
                        try
                        {
                            // записать сериализованный размер длины ChatMessage (размер массива сериализоапнного int-а = 54)
                            netStream.Write(lenArr, 0, 54);
                            // записать сериализованный класс ChatMessage
                            netStream.Write(arr, 0, arr.Length);

                            netStream.Flush();
                        }
                        catch { }
                    }
                }
            }
        }


        Control CreateMsgControl(ChatMessage chatMessage)
        {
            // создание Control-сообщения для отображения его на экране
            Control message = new Control();
            message.DataContext = chatMessage;

            switch (chatMessage.MsgType)
            {
                case ChatMessage.MessageType.MyMessage:
                    message.Style = thisMsgStyle;
                    break;
                case ChatMessage.MessageType.ClientMessage:
                    message.Style = anotherMsgStyle;
                    break;
                case ChatMessage.MessageType.JoinExitMessage:
                    message.Style = joinExitMsgStyle;
                    break;
                default:
                    return null;
            }

            return message;
        }
        byte[] SerializeMessage(object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        object DeserializeMessage(byte[] arrBytes)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            using (var memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                object obj = binForm.Deserialize(memStream);
                return obj;
            }
        }


        private void Send_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void sendMsg_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // если нажат Enter
            if (e.Key == Key.Enter)
            {
                // если одновременно нажаты Ctrl или Shift - перейти на следующую строку
                if (e.KeyboardDevice.Modifiers == ModifierKeys.Control || e.KeyboardDevice.Modifiers == ModifierKeys.Shift)
                {
                    sendMsg_TextBox.Text += "\n";
                    sendMsg_TextBox.SelectionStart = sendMsg_TextBox.Text.Length;
                }
                // иначе отправить сообщение
                else SendMessage();
                e.Handled = true;
            }
        }

        void AddMessageToPanel(Control message)
        {
            ScrollViewer scrollViewer = messagePanel.Parent as ScrollViewer;
            // true - если пользователь читает текущие сообщения (scrollViewer промотан до самого низа), иначе - false
            bool isBottom = scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight;
            messagePanel.Children.Add(message);
            // промотать до конца
            if (isBottom) scrollViewer.ScrollToEnd();
        }
    }


    [Serializable]
    public class ChatMessage
    {
        public enum MessageType
        {
            MyMessage,
            ClientMessage,
            JoinExitMessage
        }

        public string UserName { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public DateTime FullTime { get; set; }
        public int MsgLineCount { get; set; }
        public MessageType MsgType { get; set; }

        public ChatMessage()
        {
            UserName = "";
            Message = "";
            Time = "";
            FullTime = new DateTime();
            MsgLineCount = 1;
            MsgType = MessageType.MyMessage;
        }

        public ChatMessage(string userName, string message, DateTime time, MessageType msgType, int msgLineCount = 1)
        {
            UserName = userName;
            Message = message;
            Time = time.ToString(@"HH\:mm");
            FullTime = time;
            MsgLineCount = msgLineCount;
            MsgType = msgType;
        }
    }
}
