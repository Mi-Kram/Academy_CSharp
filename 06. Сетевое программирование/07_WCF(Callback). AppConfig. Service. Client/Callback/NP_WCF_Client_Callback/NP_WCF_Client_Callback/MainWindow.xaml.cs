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
using NP_WCF_Client_Callback.ServiceReference1;
using System.ServiceModel;

namespace NP_WCF_Client_Callback
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IService1Callback
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Service1Client client;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // получить ссылку на реализацию Callback-контракт на клиенте
            IService1Callback callback = this as IService1Callback;
            InstanceContext context = new InstanceContext(callback);

            // ссылка на сервис
            client = new Service1Client(context);
            MessageBox.Show(client.GetData(23));
        }

        delegate void MessageDel(string message);

        // вывод принятых сообщений в messagesListBox из не основных потоков
        void PrintMessage(string message)
        {
            if (!Dispatcher.CheckAccess())  // запущена ли эта функция не в основном потоке?
            {
                MessageDel messageEvent = new MessageDel(PrintMessage);
                //Dispatcher.Invoke(messageEvent, new object[] { message });
                Dispatcher.BeginInvoke(messageEvent, new object[] { message });
            }
            else
            {
                messagesListBox.Items.Add(message);
            }
        }

        // простой тест Callback-вызовов
        public void OnCallback()
        {
            MessageBox.Show("Callback!!!");
        }

        /// <summary>
        /// печать сообщений с сервиса
        /// </summary>
        /// <param name="mes"></param>
        public void OnSendMessage(string mes)
        {
            PrintMessage(mes);
        }

        // вызов метода на сервисе для тестирования Callback
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await client.SetCallbackAsync();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(client.GetData(23));
            MessageBox.Show(client.InnerChannel.SessionId);
        }

        // регистрация пользователя в чате
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            IService1Callback callback = this as IService1Callback;
            InstanceContext context = new InstanceContext(callback);
            client = new Service1Client(context);
            client.AddUser(loginTextBox.Text);
        }

        // посылка сообщения другому пользователю через сервис
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            client.SendMessage(toTextBox.Text, messageTextBox.Text);
        }
    }
}
