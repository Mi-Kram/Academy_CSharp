using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace NP_WF_Simple_WCF_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [ServiceContract]
        public interface IService1
        {
            [OperationContract]
            string GetData(int value);

            [OperationContract]
            List<string> GetList(string s);

            [OperationContract]
            int Add(int a, int b);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Фабрика, генерирующая каналы для связи с сервисами
            ChannelFactory<IService1> factory1 =
                new ChannelFactory<IService1>(new BasicHttpBinding(),
                    new EndpointAddress("http://localhost:1212/ServTest"));

            // Сгенерировать канал для связи с сервисом
            IService1 client1 = factory1.CreateChannel();

            // Удалённое исполнение методов
            MessageBox.Show(client1.GetData(23));
            List <string> lst = client1.GetList("test");

            MessageBox.Show(lst[0]);

            int res = client1.Add(3, 5);
            MessageBox.Show(res.ToString());
        }
    }
}

