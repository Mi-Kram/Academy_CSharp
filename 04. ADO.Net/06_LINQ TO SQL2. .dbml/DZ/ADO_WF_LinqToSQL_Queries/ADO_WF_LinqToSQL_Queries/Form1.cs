using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_WF_LinqToSQL_Queries
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MyDataContext db = new MyDataContext();

        private void button1_Click(object sender, EventArgs e)
        {
            var result = db.authors.Select(t => t);

            //var result = from t in db.authors 
            //             select t;

            dataGridView1.DataSource = result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*var result = db.authors.Select(t =>
            new {
                Id = t.au_id,
                FirstName = t.au_fname,
                LastName = t.au_lname,
                City = t.city,
                State = t.state
            });*/

            var result = from t in db.authors
                      select new 
                      {
                          Id = t.au_id,
                          FirstName = t.au_fname,
                          LastName = t.au_lname,
                          City = t.city,
                          State = t.state
                      };

            dataGridView1.DataSource = result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //var result = db.titles.Select(t => t).Where(t => t.price > 10);

            //var result = db.titles.Select(t => t).Where(t => t.price > 10).Select(t => t.pubdate);

            var result = from t in db.titles
                         where t.price > 10
                         //select t;

                         select t.price;
                         //select t.pubdate;

            dataGridView1.DataSource = result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //var result = db.titles.Select(t => t).OrderBy(t => t.price);
            //var result = db.titles.Select(t => t).OrderByDescending(t => t.price);
            //var result = db.titles.Select(t => t).OrderBy(t => t.type).ThenByDescending(t => t.price);

            /*var result = from t in db.titles
                         orderby t.price ascending
                         select t;*/

            /*var result = from t in db.titles
                         orderby t.price descending
                         select t;*/

            var result = from t in db.titles
                         orderby t.type ascending, t.price descending
                         select t;

            dataGridView1.DataSource = result;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*var result = db.titles.GroupBy(t => t.type).Select(g =>
            new {
                Type = g.Key,
                Count = g.Count(),
                Min = g.Min(n => n.price),
                Max = g.Max(n => n.price),
                Avg = g.Average(n => n.price)
            });*/

            var result = from t in db.titles
                         group t by t.type into g
                         select new
                         {
                             Type = g.Key,
                             Count = g.Count(),
                             Min = g.Min(n => n.price),
                             Max = g.Max(n => n.price),
                             Avg = g.Average(n => n.price)
                         };

            dataGridView1.DataSource = result;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //var result = db.publishers.Join(db.titles, p => p.pub_id, t => t.pub_id, (p, t) =>
            //    new { Publisher = p.pub_name, Title = t.title1, Type = t.type, Price = t.price });

            /*var result = from t in db.titles
                         join p in db.publishers on t.pub_id equals p.pub_id
                         select new
                         {
                             Publisher = p.pub_name,
                             Title = t.title1,
                             Type = t.type,
                             Price = t.price
                         };*/

            /*var result = db.titles.Join(db.titleauthors.Join(db.authors, p => p.au_id, a => a.au_id, 
                (p, a) => new { FirstName = a.au_fname, LastName = a.au_lname, title_id = p.title_id}),
                t => t.title_id, p => p.title_id, (t, p) =>
                new { FirstName = p.FirstName, LastName = p.LastName, Title = t.title1, Type = t.type, Price = t.price });*/

            var result = (from t in db.titles
                         join p in db.titleauthors on t.title_id equals p.title_id
                         join a in db.authors on p.au_id equals a.au_id
                         select new
                         {
                             FirstName = a.au_fname,
                             LastName = a.au_lname,
                             Title = t.title1,
                             Type = t.type,
                             Price = t.price
                         });

            dataGridView1.DataSource = result;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var result = db.publishers.GroupJoin(db.titles, p => p.pub_id, t => t.pub_id, (p, t) =>
                new {
                    PubName = p.pub_name,
                    AvgPrice = t.Average(n => n.price),
                    MaxPrice = t.Max(n => n.price),
                    Books = t.Select(book =>
                    new {
                        Title = book.title1,
                        Type = book.type,
                        Price = book.price
                    })
        });

            /*var result = from p in db.publishers
                         join t in db.titles on p.pub_id equals t.pub_id into g
                         select new
                         {
                             PubName = p.pub_name,
                             AvgPrice = g.Average(n => n.price),
                             MaxPrice = g.Max(n => n.price),
                             Books = from book in g
                                     select new
                                     {
                                         Title = book.title1,
                                         Type = book.type,
                                         Price = book.price
                                     }
                         };*/

            /*foreach (var publisher in result)
            {
                Console.WriteLine(@"Publisher: {0}, Avg price: {1}, Max price: {2}", publisher.PubName, publisher.AvgPrice, publisher.MaxPrice);
                foreach(var book in publisher.Books)
                {
                    Console.WriteLine(@"Title: {0}, Type: {1}, Price: {2}", book.Title, book.Type, book.Price);
                }
                Console.WriteLine();
            }*/

            dataGridView1.DataSource = result;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //var result = db.titles.Select(t => t).Max(t => t.price);

            //var result = db.titles.Max(n => n.price);

            var result = new {
                Max = db.titles.Max(n => n.price),
                Min = db.titles.Min(n => n.price)
            };

            Console.WriteLine(result);

            //dataGridView1.DataSource = result;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var result = db.titles.Where(t => t.price == db.titles.Max(n => n.price));

            /*var result = from t in db.titles
                         where t.price == db.titles.Max(n => n.price)
                         select t;*/

            dataGridView1.DataSource = result;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // показать издателей книг в жанре 'Business'
            var result = db.publishers.Where(p => db.titles.Where(n => n.type == "business").
                         Select(t => t.pub_id).Contains(p.pub_id));

            /*var result = from p in db.publishers
                         where (from t in db.titles where t.type == "business" select t.pub_id).Contains(p.pub_id)
                         select p;*/

            dataGridView1.DataSource = result;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            /*var result = db.authors.Select(t =>
            new
            {
                Id = t.au_id,
                FirstName = t.au_fname,
                LastName = t.au_lname,
                City = t.city,
                State = t.state
            }).Union(db.authors.Where(n => n.state == "UT").Select(t =>
            // Concat, Except, Intersect
            new
            {
                Id = t.au_id,
                FirstName = t.au_fname,
                LastName = t.au_lname,
                City = t.city,
                State = t.state
            }));*/

            var result = (from t in db.authors
                         select new
            {
                Id = t.au_id,
                FirstName = t.au_fname,
                LastName = t.au_lname,
                City = t.city,
                State = t.state
            }).Union(db.authors.Where(n => n.state == "UT").Select(t =>
            // Concat, Except, Intersect
            new
            {
                Id = t.au_id,
                FirstName = t.au_fname,
                LastName = t.au_lname,
                City = t.city,
                State = t.state
            }));

            dataGridView1.DataSource = result;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //var result = db.authors.Select(t => t).First();

            var result = (from t in db.authors 
                          select t).First();

            Console.WriteLine($"First name: {result.au_fname}, Last name: {result.au_lname}");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var result = db.titles.Where(t => t.title1.StartsWith("T"));

            /*var result = from t in db.titles
                         where t.title1.StartsWith("T")
                         select t;*/

            dataGridView1.DataSource = result;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //var result = db.titles.Where(t => t.price == db.titles.Max(n => n.price));

            /*var result = from t in db.titles
                         where t.price == db.titles.Max(n => n.price)
                         select t;*/

            var result = from t in db.titles
                         let MaxPrice = db.titles.Max(n => n.price)
                         where t.price == MaxPrice
                         select t;

            dataGridView1.DataSource = result;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            /*var result = db.authors.Select(t =>
            new {
                City = t.city,
                State = t.state
            }).Distinct();*/

            var result = (from t in db.authors
                      select new 
                      {
                          //City = t.city,
                          State = t.state
                      }).Distinct();

            dataGridView1.DataSource = result;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // Показать издательства, которые публикуют самые дорогие книги

            /*var result = db.publishers.Where(p => 
                        (db.titles.Where(book => book.price == db.titles.Max(n => n.price))
                        .Select(book => book.pub_id).Contains(p.pub_id)));*/

            var result = from p in db.publishers
                         where (from book in db.titles
                                where book.price == db.titles.Max(n => n.price)
                                select book.pub_id).Contains(p.pub_id)
                         select p;

            dataGridView1.DataSource = result;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            // Показать книги авторов из CA

            /*var result = db.titles.Where(book =>
                (db.titleauthors.Where(ta => 
                (db.authors.Where(a => a.state == "CA").Select(a => a.au_id)).Contains(ta.au_id))
                .Select(ta => ta.title_id)).Contains(book.title_id));*/

            var result = from book in db.titles where
                            (from ta in db.titleauthors where
                                (from a in db.authors
                                 where a.state == "CA"
                                 select a.au_id).Contains(ta.au_id)
                            select ta.title_id).Contains(book.title_id)
                         select book;

             dataGridView1.DataSource = result;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // Показать издательтсва и книги (JOIN) для издательств книг в жанре business,
            // которые находятся в штате CA

            var result = from t in db.titles
                         join p in db.publishers on t.pub_id equals p.pub_id
                         where p.state == "CA" && t.type == "business"
                         select new
                         {
                             Publisher = p.pub_name,
                             Title = t.title1,
                             Type = t.type,
                             Price = t.price
                         };

            dataGridView1.DataSource = result;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            // Показать авторов, которые не пишут книги

            var result = db.authors.Select(a => a)
                .Except(db.authors.Where(a => db.titleauthors.Select(t => t.au_id).Contains(a.au_id)));

            /*var result = (from a in db.authors select a).Except(from a in db.authors
                                   where (from t in db.titleauthors select t.au_id).Contains(a.au_id)
                                   select a);*/

            dataGridView1.DataSource = result;
        }
    }
}
