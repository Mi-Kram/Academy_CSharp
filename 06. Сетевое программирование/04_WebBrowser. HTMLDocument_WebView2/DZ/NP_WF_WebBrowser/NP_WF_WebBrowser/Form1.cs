using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace NP_WF_WebBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // блокировать ошибки JS на странице
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(new Uri(comboBox1.Text));
            //webBrowser1.Url = new Uri(comboBox1.Text);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // если загрузка документа действительно закончена
            if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                textBox1.Text = webBrowser1.DocumentText;

                HtmlDocument doc = webBrowser1.Document;

                List<string> urls = new List<string>();

                MessageBox.Show(doc.Links.Count.ToString());

                foreach (HtmlElement link in doc.Links)
                {
                    if (link.InnerText != null)
                    {
                        urls.Add(link.InnerText
                        + " (" + link.GetAttribute("href") + ") " 
                        + link.OffsetRectangle.Left.ToString());
                    }
                }

                listBox1.DataSource = urls;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HtmlDocument doc = webBrowser1.Document;

            listBox1.DataSource = null;

            foreach (HtmlElement el in doc.All)
            {
                string parent = "null";
                if(el.OffsetParent!=null) parent = el.OffsetParent.TagName;
                listBox1.Items.Add(el.TagName+
                    " (" + el.OffsetRectangle.Top.ToString() + ", " 
                    + el.OffsetRectangle.Left.ToString() + ", " 
                    + el.OffsetRectangle.Width.ToString() + ", " 
                    + el.OffsetRectangle.Height.ToString() + ") "+parent+"("+el.InnerText+")");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HtmlDocument doc = webBrowser1.Document;

            listBox1.DataSource = null;

            List<string> urls = new List<string>();

            foreach (HtmlElement imgElement in doc.Images)
            {
                urls.Add(imgElement.GetAttribute("src"));
            }

            listBox1.DataSource = urls;
        }
    }
}
