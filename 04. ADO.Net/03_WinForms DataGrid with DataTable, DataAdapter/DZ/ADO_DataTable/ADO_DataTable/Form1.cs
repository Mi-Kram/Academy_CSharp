using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ADO_DataTable
{
    public partial class Form1 : Form
    {
        DataTable dt;

        SqlConnection cn = new SqlConnection();

        public Form1()
        {
            InitializeComponent();

            // Построитель строки соединения с сервером
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = "Pubs";
            builder.DataSource = ".\\SQLExpress";
            //builder.Password = "123";
            builder.ConnectTimeout = 30;
            //builder.UserID = "test";
            builder.IntegratedSecurity = true;  // - заходить с правами текущего пользователя Windows

            cn.ConnectionString = builder.ConnectionString;

            // Создание локальной таблицы с данными
            dt = new DataTable("My table");

            // Задать имя таблицы
            dt.TableName = "Авторы";

            // Настройка поля с увеличивающимся номером
            DataColumn col1 = new DataColumn("au_id", typeof(int));
            col1.AutoIncrement = true;
            col1.AutoIncrementSeed = 1;
            col1.AutoIncrementStep = 1;
            col1.Unique = true;
            dt.Columns.Add(col1);

            // Обычные столбцы
            DataColumn col2 = new DataColumn("au_fname", typeof(string));
            dt.Columns.Add(col2);
            DataColumn col3 = dt.Columns.Add("au_lname", typeof(string));
            DataColumn col4 = dt.Columns.Add("city", typeof(string));
            col4.DefaultValue = "Донецк";
            DataColumn col5 = dt.Columns.Add("state", typeof(string));
            col5.DefaultValue = "Донецкая";

            // Поле с вычисляемым значением
            DataColumn col6 = new DataColumn("res", typeof(string));
            //col6.Expression = "substring(au_lname, 2, 3)";
            //col6.Expression = "len(au_lname)+len(au_fname)";
            //col6.Expression = "iif(len(au_lname)>5,'big','small')";
            //col6.Expression = "len(au_lname)>len(au_fname)";
            //col6.Expression = "iif(substring(au_lname, 1, 1) in ('А', 'О', 'У'), iif(len(au_lname)>5,'big','small'),'No')";
            //col6.Expression = "iif(substring(au_lname,1,1) in ('А', 'О', 'У') AND len(au_lname)>5, 'big', 'small')";
            col6.Expression = "iif(au_lname LIKE '*в', 'yes', 'no')";
            dt.Columns.Add(col6);

            // Задать источнык данных для dataGridView1
            dataGridView1.DataSource = dt;

            // Настроить вид dataGridView1
            dataGridView1.Columns[0].HeaderText = "Id";
            dataGridView1.Columns[1].HeaderText = "Имя";
            dataGridView1.Columns[2].HeaderText = "Фамилия";
            dataGridView1.Columns[3].HeaderText = "Город";
            dataGridView1.Columns[4].HeaderText = "Штат";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Заполнение таблицы исходными данными
            DataRow dr = dt.NewRow();
            dr["au_fname"] = "Иван";
            dr["au_lname"] = "Петров";
            dr["city"] = "Киев";
            dr["state"] = "Киевская";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["au_fname"] = "Николай";
            dr["au_lname"] = "Матвеев";
            dr["city"] = "Донецк";
            dr["state"] = "Донецкая";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["au_fname"] = "Петр";
            dr["au_lname"] = "Орлов";
            dr["city"] = "Киев";
            dr["state"] = "Киевская";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["au_fname"] = "Владимир";
            dr["au_lname"] = "Ушаков";
            dr["city"] = "Харьков";
            dr["state"] = "Харьковская";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["au_fname"] = "Анна";
            dr["au_lname"] = "Борисова";
            dr["city"] = "Киев";
            dr["state"] = "Киевская";
            dt.Rows.Add(dr);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt.WriteXml("test.xml");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dt.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dt.ReadXml("test.xml");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Удаление из DataTable
            if(dataGridView1.SelectedRows.Count>0)
                dt.Rows[dataGridView1.SelectedRows[0].Index].Delete();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Добавление одной строки
            DataRow dr = dt.NewRow();
            dr["au_fname"] = textBox2.Text;
            dr["au_lname"] = textBox3.Text;
            dr["city"] = textBox4.Text;
            dr["state"] = textBox5.Text;
            dt.Rows.Add(dr);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Фиксация внутренней транзакции
            dt.AcceptChanges();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Откат внутренней транзакции
            dt.RejectChanges();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            cn.Open();

            DataTable dt = new DataTable("app_test");
            DataColumn col1 = new DataColumn("id", typeof(int));
            col1.AutoIncrement = true;
            col1.AutoIncrementSeed = 1;
            col1.AutoIncrementStep = 1;
            dt.Columns.Add(col1);
            DataColumn col2 = new DataColumn("str", typeof(string));
            dt.Columns.Add(col2);
            DataColumn col3 = new DataColumn("number1", typeof(int));
            dt.Columns.Add(col3);
            DataColumn col4 = dt.Columns.Add("number2", typeof(int));

            DateTime startTime = DateTime.Now;

            for (int i = 0; i < 10000; i++)
            {
                DataRow dr = dt.NewRow();
                dr["str"] = Guid.NewGuid().ToString("n");
                dr["number1"] = random.Next(10000);
                dr["number2"] = random.Next(10000);
                dt.Rows.Add(dr);
            }

            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(cn);
            sqlBulkCopy.DestinationTableName = dt.TableName;
            sqlBulkCopy.WriteToServer(dt);

            cn.Close();

            DateTime endTime = DateTime.Now;

            MessageBox.Show(String.Format("Done: {0}", (endTime - startTime).TotalMilliseconds.ToString()));
        }
    }
}