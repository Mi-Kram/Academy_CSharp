using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_WF_LinqToSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MyDataContext pubs = new MyDataContext();

        private void button1_Click(object sender, EventArgs e)
        {
            // Получить все поля из таблицы
            //var result = pubs.authors.Select(t => t);

            // Получить анонимный объект анонимного класса, содержащий необходимые поля
            var result = pubs.authors.Select(t => new
            {
                Id = t.au_id,
                FirstName = t.au_fname,
                LastName = t.au_lname,
                City = t.city,
                State = t.state
            });

            dataGridView1.DataSource = result;

            // Заполнение выпадающего списка
            var states = pubs.authors.Select(t => t.state).Distinct();

            comboBox1.DataSource = states;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // преобразование LINQ в SQL-запрос
            /*var q = from p in pubs.authors
                    select p;

            DbCommand dc = pubs.GetCommand(q);

            MessageBox.Show($"Command Text: \n{dc.CommandText}");*/

            // Фильтрация результатов
            var result = pubs.authors.Select(t => new
            {
                Id = t.au_id,
                FirstName = t.au_fname,
                LastName = t.au_lname,
                City = t.city,
                State = t.state
            }).Where(n => n.State == comboBox1.Text);

            dataGridView1.DataSource = result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var result = pubs.authors.Select(t => new
            {
                Id = t.au_id,
                FirstName = t.au_fname,
                LastName = t.au_lname,
                City = t.city,
                State = t.state
            }).OrderBy(n => n.FirstName);

            dataGridView1.DataSource = result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Создание одиночного объекта
            author au = new author
            {
                au_id = "263-54-2034",
                au_fname = "Terry",
                au_lname = "Scott",
                address = "kiev",
                city = "Kiev",
                contract = true,
                phone = "242",
                state = "KI",
                zip = "34232"
            };

            // Добавление в локальную таблицу
            pubs.authors.InsertOnSubmit(au);

            // Синхронизация с БД
            pubs.SubmitChanges();

            button1_Click(this, null);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string del_id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            // Выбрать удаляемый набор объектов
            author au = pubs.authors.First(a => a.au_id == del_id);
            
            // Удалить из локальной таблицы
            pubs.authors.DeleteOnSubmit(au);

            // Синхронизировать БД
            pubs.SubmitChanges();

            button1_Click(this, null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Выбрать объект для изменения свойств
            author au = pubs.authors.First(a => a.au_id == textBox1.Text);

            // Изменить свойства
            au.city = "Donetsk";

            // Синхронизировать БД
            pubs.SubmitChanges();

            button1_Click(this, null);
        }

        /*
             create proc get_author(@st varchar(20), @n int output)
                as
                begin
                select * from authors where state = @st
                select @n=count(*) from authors where state = @st
                end
             */
        private void button7_Click(object sender, EventArgs e)
        {
            int? n = 0;
            var res = pubs.get_author("CA", ref n);
            dataGridView1.DataSource = res;
            if(n.HasValue)
                MessageBox.Show(n.ToString());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            DateTime startTime = DateTime.Now;

            for (int i = 0; i < 10000; i++)
            {
                // Создание одиночного объекта
                app_test row = new app_test
                {
                    str = Guid.NewGuid().ToString("n"),
                    number1 = random.Next(10000),
                    number2 = random.Next(10000)
                };

                // Добавление в локальную таблицу
                pubs.app_tests.InsertOnSubmit(row);
            }

            // Синхронизация с БД
            pubs.SubmitChanges();

            DateTime endTime = DateTime.Now;

            MessageBox.Show(String.Format("Done: {0}", (endTime - startTime).TotalMilliseconds.ToString()));
        }
    }
}
