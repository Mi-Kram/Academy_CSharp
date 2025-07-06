using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace NP_WF_Sockets
{
    public partial class Form1 : Form
    {
        // Поток для прослушивания порта и ожидания клиентов
        Thread ListenerThread;

        // Поток для ожидания сообщений
        Thread SocketThread;

        // Класс, который слушает порт
        TcpListener listener;

        // Класс, который обеспечивает передачу информации по сети
        TcpClient client;

        // Буфер сообщений
        byte[] myReadBuffer = new byte[1024];

        public Form1()
        {
            InitializeComponent();
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)   // запущена ли эта функция в чужом потоке?
            {
                SetTextCallback d = new SetTextCallback(SetText);

                // Запустить функцию SetText в основном потоке
                this.listBox1.Invoke(d, new object[] { text });
            }
            else
            {
                listBox1.Items.Add(text);
            }
        }

        void ClientDoListen()
        {
            NetworkStream netStream = client.GetStream();
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
                    catch(Exception e){
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

        void DoListen()
        {
            SetText("Wait...");

            // Бесконечно ожидает соединения с новым клиентом и возвращает адрес клиента
            client = listener.AcceptTcpClient();

            SetText("Connection established");

            // Получить из соединившегося клиента сетевой поток для обмена информацией
            NetworkStream netStream = client.GetStream();

            // Цикл получения информации
            while(true)
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

                        // Преобразовать принятые байты в UTF8-текст
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

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Попытаться соединиться с сервером по указанному адресу и порту
            client = new TcpClient(textBox1.Text, Convert.ToInt32(textBox3.Text));

            // Запуск потока для чтения информации с сервера
            SocketThread = new Thread(ClientDoListen);
            SocketThread.IsBackground = true;
            SocketThread.Start();
        }

        NetworkStream netStream;

        private void button3_Click(object sender, EventArgs e)
        {
            // Посылка сообщений
            netStream = client.GetStream();
            StreamWriter wr = new StreamWriter(netStream);

            // Если можно писать в поток
            if (netStream.CanWrite)
            {
                // Конвертировать текст из текстового поля в массив байт
                /*byte[] buf = Encoding.UTF8.GetBytes(textBox2.Text);

                // и переслать его через сетевой поток
                netStream.Write(buf, 0, buf.Length);
                netStream.Flush();*/

                
                wr.WriteLine(textBox2.Text);
                wr.Flush();
                
            }
            else
            {
                MessageBox.Show("Error!!!");
            }
            //wr.Close();
        }

    }
}