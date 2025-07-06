using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace NP_WF_Simple_WCF_Service
{
    public partial class Form1 : Form
    {
        ServiceHost host;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Создать класс, который публикует сервис
            host = new ServiceHost(typeof(Service1), new Uri(@"http://localhost:1212/ServTest"));

            // Создание протокола HTTP для передачи информации между сервисом и клиентом
            BasicHttpBinding b1 = new BasicHttpBinding();

            // Задание конечной точки для публикации сервиса
            host.AddServiceEndpoint(typeof(IService1), b1, "");

            // Открыть порт и ждать соединений
            host.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            host.Close();
        }

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

    public class Service1 : IService1
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public string GetData(int value)
        {
            return string.Format("Number: {0}", value);
        }

        List<string> lst = new List<string>();

        public List<string> GetList(string s)
        {
            lst.Add("One, "+s);
            lst.Add("Hello");
            return lst;
        }
    }
}
