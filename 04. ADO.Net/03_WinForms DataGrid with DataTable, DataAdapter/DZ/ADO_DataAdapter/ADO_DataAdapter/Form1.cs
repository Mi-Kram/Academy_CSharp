using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace ADO_DataAdapter
{
    public partial class Form1 : Form
    {
        // хранит данные одной таблицы локально
        DataTable table1;

        // класс для фильтрации и сортировки данных
        DataView view1;

        // соединение с БД
        SqlConnection cn;

        // класс, проводящий все операции с сервером
        SqlDataAdapter adapter;

        public Form1()
        {
            InitializeComponent();
            table1 = new DataTable();

            // view1 фильтрует table1
            view1 = new DataView(table1);
            cn = new SqlConnection();
        }

        /*
         
            select au_fname, au_lname, state, city into authors3 from authors
            delete from authors3

            alter table authors3 add au_id int identity

            insert into authors3(au_fname, au_lname, state, city)
            select au_fname, au_lname, state, city from authors

            select * from authors3

         */

        private void button1_Click(object sender, EventArgs e)
        {
            string connection = ConfigurationManager.ConnectionStrings["DBTest2.Properties.Settings.mssql"].ConnectionString;

            // установить строку соединения, взятую из Configuration manager
            cn.ConnectionString = connection;

            // создать команду sql
            SqlCommand command = cn.CreateCommand();
            command.CommandText = "select au_id, au_fname, au_lname, city, state from authors3";

            // создаём адаптер, инициализированный командой, запрашивающей данные с сервера
            adapter = new SqlDataAdapter(command);

            // очищаем локальную таблицу
            table1.Clear();

            // читаем данные с сервера в таблицу
            adapter.Fill(table1);

            // показать таблицу в datagridview
            dataGridView1.DataSource = table1;

            // столбец с id - не показывать
            dataGridView1.Columns[0].Visible = false;

            //view1.RowFilter = "city like '%e'";

            // настройка фильтрации
            view1.RowFilter = "state = 'CA' and city='Oakland'";
            view1.Sort = "au_fname desc";

            // показать фильтрованную таблицу в datagridview2
            dataGridView2.DataSource = view1;
            dataGridView2.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // добавление нового поля
            DataRow dr = table1.NewRow();
            dr["au_fname"] = textBox1.Text;
            dr["au_lname"] = textBox2.Text;
            dr["city"] = textBox3.Text;
            dr["state"] = textBox4.Text;
            table1.Rows.Add(dr);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // настройка синхронизации с сервером
            string com = "insert into authors3(au_fname, au_lname, city, state) values (@p1, @p2, @p3, @p4)";
            SqlCommand command = new SqlCommand(com, cn);
            command.Parameters.Add("@p1", SqlDbType.VarChar, 20, "au_fname");
            command.Parameters.Add("@p2", SqlDbType.VarChar, 40, "au_lname");
            command.Parameters.Add("@p3", SqlDbType.VarChar, 20, "city");
            command.Parameters.Add("@p4", SqlDbType.Char, 2, "state");
            adapter.InsertCommand = command;

            com = "delete from authors3 where au_id=@p1";
            command = new SqlCommand(com, cn);
            command.Parameters.Add("@p1", SqlDbType.Int, 4, "au_id");
            adapter.DeleteCommand = command;

            com = "update authors3 set au_fname=@p2, au_lname=@p3, city=@p4, state=@p5 where au_id=@p1";
            command = new SqlCommand(com, cn);
            command.Parameters.Add("@p1", SqlDbType.Int, 4, "au_id");
            command.Parameters.Add("@p2", SqlDbType.VarChar, 20, "au_fname");
            command.Parameters.Add("@p3", SqlDbType.VarChar, 40, "au_lname");
            command.Parameters.Add("@p4", SqlDbType.VarChar, 20, "city");
            command.Parameters.Add("@p5", SqlDbType.Char, 2, "state");
            adapter.UpdateCommand = command;

            // Получить таблицу с изменёнными строками (добавленными)
            //table1.GetChanges(DataRowState.Added);
            
            // синхронизация данных с сервером
            adapter.Update(table1);

            // очистка локальной таблицы
            table1.Clear();

            // заполнение таблицы с сервера
            adapter.Fill(table1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable table2 = new DataTable();

            string connection = ConfigurationManager.ConnectionStrings["DBTest2.Properties.Settings.mssql"].ConnectionString;

            // установить строку соединения, взятую из Configuration manager
            cn.ConnectionString = connection;

            // создать команду sql
            SqlCommand command = cn.CreateCommand();
            command.CommandText = "select * from app_test";

            // создаём адаптер, инициализированный командой, запрашивающей данные с сервера
            adapter = new SqlDataAdapter(command);

            // читаем данные с сервера в таблицу
            adapter.Fill(table2);

            // очищаем локальную таблицу
            table2.Clear();

            Random random = new Random();

            DateTime startTime = DateTime.Now;

            for (int i = 0; i < 10000; i++)
            {
                DataRow dr = table2.NewRow();
                dr["str"] = Guid.NewGuid().ToString("n");
                dr["number1"] = random.Next(10000);
                dr["number2"] = random.Next(10000);
                table2.Rows.Add(dr);
            }

            // настройка синхронизации с сервером
            string com = "insert into app_test(str, number1, number2) values (@p1, @p2, @p3)";
            SqlCommand command2 = new SqlCommand(com, cn);
            command2.Parameters.Add("@p1", SqlDbType.VarChar, 200, "str");
            command2.Parameters.Add("@p2", SqlDbType.Int, 4, "number1");
            command2.Parameters.Add("@p3", SqlDbType.Int, 4, "number2");
            adapter.InsertCommand = command2;

            // синхронизация данных с сервером
            adapter.Update(table2);

            DateTime endTime = DateTime.Now;

            MessageBox.Show(String.Format("Done: {0}", (endTime - startTime).TotalMilliseconds.ToString()));

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // настройка локальной фильтрации
            view1.RowFilter = "state = 'UT'";
            view1.Sort = "au_fname asc";
        }
    }
}