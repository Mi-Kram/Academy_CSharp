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
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ChatApp
{
    public partial class MainWindow : Window
    {
        // Поток для прослушивания порта и ожидания клиентов
        Thread ListenerThread;

        // Поток для ожидания сообщений
        Thread SocketThread;

        // Класс, который слушает порт
        TcpListener listener;

        // Класс, который обеспечивает передачу информации по сети
        TcpClient server;

        // Список клиентов, присоединившихся к серверу
        List<ChatClient> clients = new List<ChatClient>();

        // Буфер сообщений
        byte[] myReadBuffer = new byte[1024];

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// метод клиента, который ожидает сообщений от сервера
        /// </summary>
        void ClientDoListen()
        {
            NetworkStream netStream = server.GetStream();
            while (true)
            {
                if (netStream.CanRead)
                {
                    int numberOfBytesRead = 0;
                    string str = "";

                    try
                    {
                        numberOfBytesRead = netStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                        str += Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead);
                        SetText(str);
                    }
                    catch (Exception e)
                    {
                        SetText("Connection closed");
                        break;
                    }
                }
                else
                {
                    MessageBox.Show("Can`t read!!!");
                }
            }
            netStream.Close();
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (!Dispatcher.CheckAccess())   // запущена ли эта функция в чужом потоке?
            {
                SetTextCallback d = new SetTextCallback(SetText);

                // Запустить функцию SetText в основном потоке
                Dispatcher.Invoke(d, new object[] { text });
            }
            else
            {
                this.listBox1.Items.Add(text);
            }
        }

        /// <summary>
        /// Ожидание сообщений от конкретного клиента
        /// </summary>
        /// <param name="client">Параметр, задающий клиента, соединившегося с сервером</param>
        void ClientMessagesListen(object client2)
        {
            ChatClient client = (ChatClient)client2;

            // Получить из соединившегося клиента сетевой поток для обмена информацией
            NetworkStream netStream = client.TcpClient.GetStream();

            // Цикл получения информации
            while (true)
            {
                // Если можно читать из потока
                if (netStream.CanRead)
                {
                    int numberOfBytesRead = 0;
                    string str = "";

                    try
                    {
                        // Чтение сообщений (читаются байты)
                        numberOfBytesRead = netStream.Read(myReadBuffer, 0, myReadBuffer.Length);

                        // Преобразовать принятые байты в UNICODE-текст
                        str += Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead);

                        // Вывести полученный текст
                        SetText(str);
                    }
                    catch (Exception e)
                    {
                        SetText("Connection closed");
                        break;
                    }

                }
                else
                {
                    MessageBox.Show("Can`t read!!!");
                    break;
                }
            }

            // Закрыть сетевой поток
            netStream.Close();
        }

        // Номер клиента
        uint clientNumber = 1;

        void DoListen()
        {
            SetText("Wait...");

            // Бесконечно слушать порт и добавлять новых клиентов в чат
            while(true)
            {
                // Бесконечно ожидает соединения с новым клиентом и возвращает адрес клиента
                TcpClient client = listener.AcceptTcpClient();

                SetText("Connection established");

                ChatClient chatClient = new ChatClient()
                {
                    Login = String.Format("Client{0}", clientNumber++),
                    LoginDate = DateTime.Now,
                    TcpClient = client
                };

                // Добавить соединившегося клиента в список клиентов
                clients.Add(chatClient);

                // Создать параметрический поток (параметр - потоковая функция)
                ParameterizedThreadStart p1 = new ParameterizedThreadStart(ClientMessagesListen);

                // Создать поток
                Thread t1 = new Thread(p1);

                // Закрыть ли поток при закрытии окна
                t1.IsBackground = true;

                // Старт потока для прослушки сообщений от текущего клиента
                t1.Start(chatClient);
            }
        }

        NetworkStream netStream;

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Попытаться соединиться с сервером по указанному адресу и порту
            server = new TcpClient(textBox1.Text, Convert.ToInt32(textBox3.Text));

            // Запуск потока для чтения информации с сервера
            SocketThread = new Thread(ClientDoListen);
            SocketThread.IsBackground = true;
            SocketThread.Start();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Создание и настройка слушателя портов (диапазон адресов клиентов и номер порта)
            listener = new TcpListener(System.Net.IPAddress.Any, Convert.ToInt32(textBox3.Text));

            // Старт слушателя
            listener.Start();

            // Старт потока, слушающего порт
            ListenerThread = new Thread(DoListen);
            ListenerThread.IsBackground = true;
            ListenerThread.Start();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if(clients.Count > 0)   // Программа работает в качестве сервера
            {
                // Перебрать всех клиентов, с которыми установлено соединение
                for(int i=0; i<clients.Count; i++)
                {
                    ChatClient currentClient = clients[i];

                    // если клиент отсоединился от сервера
                    if (!currentClient.TcpClient.Connected)
                    {
                        // удалить клиента из списка клиентов
                        clients.RemoveAt(i--);
                        continue;
                    }

                    // Посылка сообщений
                    netStream = currentClient.TcpClient.GetStream();

                    // Если можно писать в поток
                    if (netStream.CanWrite)
                    {
                        // Конвертировать текст из текстового поля в массив байт
                        byte[] buf = Encoding.UTF8.GetBytes(textBox2.Text);

                        // и переслать его через серевой поток
                        netStream.Write(buf, 0, buf.Length);
                        netStream.Flush();
                    }
                    else
                    {
                        MessageBox.Show("Error!!!");
                    }
                }
            }
            else // Программа работает в качестве клиента
            {
                // Посылка сообщений
                netStream = server.GetStream();

                // Если можно писать в поток
                if (netStream.CanWrite)
                {
                    // Конвертировать текст из текстового поля в массив байт
                    byte[] buf = Encoding.UTF8.GetBytes(textBox2.Text);

                    // и переслать его через серевой поток
                    netStream.Write(buf, 0, buf.Length);
                    netStream.Flush();
                }
                else
                {
                    MessageBox.Show("Error!!!");
                }
            }    
        }
    }
}
