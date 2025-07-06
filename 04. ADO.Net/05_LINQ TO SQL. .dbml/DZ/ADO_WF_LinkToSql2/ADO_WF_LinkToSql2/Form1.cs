using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_WF_LinkToSql2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        AuthorsDataContext context = new AuthorsDataContext();

        private void button1_Click(object sender, EventArgs e)
        {
            var result = from au in context.authors
                         select new
                         {
                             au.au_fname,
                             au.au_lname,
                             au.state
                         };

            dataGridView1.DataSource = result;

            // Заполнение выпадающего списка
            var states = context.authors.Select(t => t.state).Distinct();

            comboBox1.DataSource = states;
        }
    }
}
