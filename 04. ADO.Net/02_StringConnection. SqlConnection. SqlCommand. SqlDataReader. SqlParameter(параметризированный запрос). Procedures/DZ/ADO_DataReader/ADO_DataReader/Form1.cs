using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;

//using System.Data.OracleClient;
//using Oracle.ManagedDataAccess.Client;

using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

// create table t1(id1 int, id2 varchar(20), primary key(id1, id2))

namespace ADO_DataReader
{
    public partial class Form1 : Form
    {

        SqlConnection cn;

        // Install-Package Oracle.ManagedDataAccess.Core -Version 2.18.3
        //OracleConnection ocn;

        public Form1()
        {
            InitializeComponent();

            cn = new SqlConnection();
            
            //string connection = ConfigurationManager.ConnectionStrings["ADO_DataReader.Properties.Settings.pubsConnectionString"].ConnectionString;

            //int portNumber = Properties.Settings.Default.PortNumber;
            //string userName = Properties.Settings.Default.UserName;

            // Построитель строки соединения с сервером
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = "Pubs";
            builder.DataSource = ".\\SQLExpress";
            //builder.Password = "123";
            builder.ConnectTimeout = 30;
            //builder.UserID = "test";
            builder.IntegratedSecurity = true;  // - заходить с правами текущего пользователя Windows

            cn.ConnectionString = builder.ConnectionString;
            //cn.ConnectionString = connection;
        }

        // select au_id, au_fname, au_lname, city, state into authors2 from authors
        private void button1_Click(object sender, EventArgs e)
        {
            // Открыть соединение с сервером БД
            cn.Open();

            listBox1.Items.Clear();

            // Команда SQL для выполнения на сервере
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;

            cmd.CommandText = "select * from authors2";

            // Объект, который читает информацию из результатов запроса
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // Прочитать одну строку из результатов запроса
            while (dr.Read())
            {
                string str = dr["au_id"] + "\t" + dr["au_fname"] + "\t" + dr["au_lname"] + "\t" + dr["city"] + "\t" + dr["state"];
                listBox1.Items.Add(str);
            }

            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cn.Open();

            string command = $"insert into authors2 (au_id, au_fname, au_lname, city, state) " +
                $"values ({textBox1.Text}, {textBox2.Text}, {textBox3.Text}, {textBox4.Text}, {textBox5.Text})";

            SqlCommand cmd2 = new SqlCommand(command, cn);
            MessageBox.Show(command);

            // Исполнение непараметризированного запроса, который не возвращает результатов
            cmd2.ExecuteNonQuery();

            cn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cn.Open();

            string command = "delete from authors2";
            
            SqlCommand cmd2 = new SqlCommand(command, cn);
            cmd2.ExecuteNonQuery();

            cn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cn.Open();

            // Параметризированный запрос
            string command = "update authors2 set au_fname=@fname, au_lname=@lname, city=@c, state=@st where au_id=@id";
            
            SqlCommand cmd2 = new SqlCommand(command, cn);

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@fname";
            param1.Value = textBox2.Text;
            param1.SqlDbType = SqlDbType.VarChar;
            param1.Size = 20;
            cmd2.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter("@lname", SqlDbType.VarChar, 100);
            param2.Value = textBox3.Text;
            cmd2.Parameters.Add(param2);

            SqlParameter param3 = new SqlParameter("@c", SqlDbType.VarChar, 20);
            param3.Value = textBox4.Text;
            cmd2.Parameters.Add(param3);

            SqlParameter param4 = new SqlParameter("@st", SqlDbType.VarChar, 2);
            param4.Value = textBox5.Text;
            cmd2.Parameters.Add(param4);

            SqlParameter param5 = new SqlParameter("@id", SqlDbType.VarChar, 11);
            param5.Value = textBox1.Text;
            cmd2.Parameters.Add(param5);

            cmd2.ExecuteNonQuery();

            cn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*
             create proc [dbo].[deltest]
             @id varchar(10),
             @n int output
             as begin
             select @n = count(*) from authors2 where au_id = @id
             delete from authors2 where au_id = @id
             end
            */

            cn.Open();

            // Команда, запускающая хранимую процедуру
            SqlCommand cmd2 = new SqlCommand("deltest", cn);

            // Тип - хранимая процедура
            cmd2.CommandType = CommandType.StoredProcedure;

            // Класс для передачи параметров
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@id";
            param1.Value = textBox1.Text;
            param1.SqlDbType = SqlDbType.VarChar;
            param1.Size = 11;
            param1.Direction = ParameterDirection.Input;
            cmd2.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@n";
            param2.SqlDbType = SqlDbType.Int;
            param2.Size = 4;
            param2.Direction = ParameterDirection.Output;
            cmd2.Parameters.Add(param2);

            // Исполнение хранимой процедуры на сервере
            cmd2.ExecuteNonQuery();

            // Получить выходной параметр из процедуры
            MessageBox.Show(cmd2.Parameters["@n"].Value.ToString());

            cn.Close();
        }

        // CREATE TABLE img (id int IDENTITY(1,1) primary key, name varchar (20) NULL, pict image NULL)

        int bytes;

        // Сохранение изображения без заголовка в БД
        private void button11_Click(object sender, EventArgs e)
        {
            // Открытие изображения
            Bitmap bmp = new Bitmap(@"pic1.jpg");

            // Получение массива байт, содержащего пикселы изображения
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            // Получить информацию об изображении
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            // Получить низкоуровневый указатель на изображение
            IntPtr ptr = bmpData.Scan0;

            // Вычислить количество байт в массиве для хранения точек изображения
            bytes = bmpData.Stride * bmp.Height;

            // Объявление массива байт нужного размера
            byte[] rgbValues = new byte[bytes];

            // Копирование байт из изображения в массив
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            bmp.UnlockBits(bmpData);

            cn.Open();

            // Команда заносит в БД название изображения и массив байт
            string command = "insert into img (name, pict) values (@name, @pict)";

            // Название изображения
            SqlCommand cmd2 = new SqlCommand(command, cn);
            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@name";
            param1.Value = textBox2.Text;
            param1.SqlDbType = SqlDbType.VarChar;
            param1.Size = 20;
            cmd2.Parameters.Add(param1);

            // Байты изображения
            SqlParameter param2 = new SqlParameter("@pict", SqlDbType.Image, rgbValues.Length);
            param2.Value = rgbValues;
            cmd2.Parameters.Add(param2);

            cmd2.ExecuteNonQuery();

            cn.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            cn.Open();

            // Чтение картинки из БД
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select id, name, pict from img where id=1";

            byte[] rgbValues=null;
            long l2 = 0;

            // Сообщить объекту SqlDataReader о том, что будут прочитаны бинарные данные
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
            while (dr.Read())
            {
                // Получить размер в байтах считываемых данных
                long length = dr.GetBytes(2, 0, null, 0, 0);

                // Выделить память под изображение
                rgbValues = new byte[length];

                // Прочитать из БД length байт в массив rgbValues, начиная с нулевого байта
                l2 = dr.GetBytes(2, 0, rgbValues, 0, (int)length);  // первый параметр - номер стобца в запросе, из которого будет происходить чтение

                // Показать количество реально прочитанных байт
                MessageBox.Show(l2.ToString());
            }

            cn.Close();

            // Конвертировать байты в изображение и показать картинку
            Bitmap bmp = new Bitmap(1280, 960, PixelFormat.Format24bppRgb);
            Rectangle rect = new Rectangle(0, 0, 1280, 960);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            Marshal.Copy(rgbValues, 0, ptr, (int)l2);

            bmp.UnlockBits(bmpData);

            pictureBox1.Image = bmp;
        }

        public class Author
        {
            private string au_lname;
            private string au_id;

            public Author(string id, string lname)
            {

                this.au_lname = lname;
                this.au_id = id;
            }

            public string Au_id
            {
                get
                {
                    return au_id;
                }
            }

            public string Au_lname
            {

                get
                {
                    return au_lname;
                }
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            cn.Open();

            comboBox1.Items.Clear();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = "select au_id, au_lname from authors2";

            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            List<Author> lst = new List<Author>();
            while (dr.Read())
            {
                lst.Add(new Author(dr["au_id"].ToString(),dr["au_lname"].ToString()));
            }

            // коллекция, отображаемая в listBox
            comboBox1.DataSource = lst;

            // поле, которое показывается
            comboBox1.DisplayMember = "Au_lname";

            // поле, которое выбирается в контроле
            comboBox1.ValueMember = "Au_id";

            cn.Close();

            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show(((Author)comboBox1.SelectedItem).Au_lname);
            MessageBox.Show(comboBox1.SelectedValue.ToString());
        }

        // create table img(id int identity, name varchar(20), pict image)

        // Загрузка выбранного файла в БД
        private void button14_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Считать содержимое файла в массив байт b
                FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs, Encoding.Default);
                byte[] b = new byte[fs.Length];
                b = br.ReadBytes(Convert.ToInt32(fs.Length));

                cn.Open();

                // Сохранение файла и его имени в БД
                string command = "insert into img (name, pict) values (@name, @pict)";

                SqlCommand cmd2 = new SqlCommand(command, cn);
                SqlParameter param1 = new SqlParameter();
                param1.ParameterName = "@name";
                param1.Value = openFileDialog1.FileName;
                param1.SqlDbType = SqlDbType.VarChar;
                param1.Size = 20;
                cmd2.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter("@pict", SqlDbType.Image, (int)fs.Length);
                param2.Value = b;
                cmd2.Parameters.Add(param2);

                cmd2.ExecuteNonQuery();

                cn.Close();
            }
        }

        // Выгрузка из БД в файл
        private void button15_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                cn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "select pict from img where id=2";

                byte[] b = null;
                long l2 = 0;

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
                while (dr.Read())
                {
                    // Получение размера файла из БД
                    long l = dr.GetBytes(0, 0, null, 0, 0);
                    b = new byte[l];

                    // Чтение байт в массив b
                    l2 = dr.GetBytes(0, 0, b, 0, (int)l);
                    MessageBox.Show(l2.ToString());
                }

                cn.Close();

                // Сохранение массива в бинарный файл
                FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs, Encoding.Default);
                bw.Write(b);
                bw.Close();
                fs.Close();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // Класс, обеспечивающий работу транзакций
            SqlTransaction trans = null;

            // Список команд для выполнения на сервере
            List<string> commands = new List<string>();
            commands.Add(@"insert into authors2 values ('123-45-6789', 'Alex', 'Petrov', 'Donetsk', 'DN')");
            commands.Add(@"insert into authors2 values ('123-45-6780', 'Ivan', 'Demidoff', 'Moskow', 'MS')");

            try
            {
                cn.Open();

                // Открыть транзакцию
                trans = cn.BeginTransaction();

                // Цикличное исполнение команд на сервере
                foreach (var commandString in commands)
                {
                    SqlCommand command = new SqlCommand(commandString, cn, trans);
                    command.ExecuteNonQuery();
                }
                
                // Фиксация транзакции
                trans.Commit();
            }
            catch (Exception ex)
            {
                // Откат транзакции
                trans.Rollback();
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Закрытие соединения
                cn.Close();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            cn.Open();

            string command = "insert into authors2 (au_id, au_fname, au_lname, city, state) values (";
            command = command + "'" + textBox1.Text + "',";
            command = command + "'" + textBox2.Text + "',";
            command = command + "'" + textBox3.Text + "',";
            command = command + "'" + textBox4.Text + "',";
            command = command + "'" + textBox5.Text + "')";

            SqlCommand cmd2 = new SqlCommand(command, cn);
            //MessageBox.Show(command);

            // Объект, который реагирует на обратный вызов после окончания выполнения кода на сервере
            AsyncCallback callBack = new AsyncCallback(EndExec);

            // Исполнение непараметризированного запроса, который не возвращает результатов
            IAsyncResult res = cmd2.BeginExecuteNonQuery(callBack, 123);

            // Подождать окончания работы асинхронного запроса
            //cmd2.EndExecuteNonQuery(res);
        }

        // Метод, который запускается по окончании выполнения кода на сервере
        void EndExec(IAsyncResult result)
        {
            // Показать пользовательский параметр
            MessageBox.Show(result.AsyncState.ToString());

            // Закрыть соединение только после окончания работы команды
            cn.Close();
        }

        /*
        CREATE TABLE app_test(
            [id] [int] IDENTITY primary key,
	        [str] [varchar] (200) NULL,
	        [number1] [int] NULL,
	        [number2] [int] NULL
        )*/

        private void button18_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            cn.Open();

            DateTime startTime = DateTime.Now;

            for (int i = 0; i < 10000; i++)
            {
                string command =
                    String.Format("insert into app_test (str, number1, number2) values ('{0}', {1}, {2})",
                    Guid.NewGuid().ToString("n"), random.Next(10000), random.Next(10000));

                SqlCommand cmd2 = new SqlCommand(command, cn);

                // Исполнение непараметризированного запроса, который не возвращает результатов
                cmd2.ExecuteNonQuery();
            }

            cn.Close();

            DateTime endTime = DateTime.Now;

            MessageBox.Show(String.Format("Done: {0}", (endTime - startTime).TotalMilliseconds.ToString()));
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            string command = "insert into app_test (str, number1, number2) values (@str, @num1, @num2)";

            SqlCommand cmd2 = new SqlCommand(command, cn);

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@str";
            param1.SqlDbType = SqlDbType.VarChar;
            param1.Size = 100;
            cmd2.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter("@num1", SqlDbType.Int, 4);
            cmd2.Parameters.Add(param2);

            SqlParameter param3 = new SqlParameter("@num2", SqlDbType.Int, 4);
            cmd2.Parameters.Add(param3);

            DateTime startTime = DateTime.Now;

            cn.Open();

            for (int i = 0; i < 10000; i++)
            {
                param1.Value = Guid.NewGuid().ToString("n");
                param2.Value = random.Next(10000);
                param3.Value = random.Next(10000);

                cmd2.ExecuteNonQuery();
            }

            cn.Close();

            DateTime endTime = DateTime.Now;

            MessageBox.Show(String.Format("Done: {0}", (endTime - startTime).TotalMilliseconds.ToString()));
        }

        /*
         create proc add_item (@str varchar(10), @num1 int, @num2 int)
         as begin
	        insert into app_test (str, number1, number2) values (@str, @num1, @num2)
         end
        */

        private void button20_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            cn.Open();

            string command = "add_item";

            SqlCommand cmd2 = new SqlCommand(command, cn);
            cmd2.CommandType = CommandType.StoredProcedure;

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@str";
            param1.SqlDbType = SqlDbType.VarChar;
            param1.Size = 100;
            cmd2.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter("@num1", SqlDbType.Int, 4);
            cmd2.Parameters.Add(param2);

            SqlParameter param3 = new SqlParameter("@num2", SqlDbType.Int, 4);
            cmd2.Parameters.Add(param3);

            DateTime startTime = DateTime.Now;

            for (int i = 0; i < 10000; i++)
            {
                param1.Value = Guid.NewGuid().ToString("n");
                param2.Value = random.Next(10000);
                param3.Value = random.Next(10000);

                cmd2.ExecuteNonQuery();
            }

            cn.Close();

            DateTime endTime = DateTime.Now;

            MessageBox.Show(String.Format("Done: {0}", (endTime - startTime).TotalMilliseconds.ToString()));
        }
    }

    /*
     create or replace procedure deltest(id in varchar2, n out number)
as
begin
delete from authors where au_id = id;
n:=id;
end;
     */
}