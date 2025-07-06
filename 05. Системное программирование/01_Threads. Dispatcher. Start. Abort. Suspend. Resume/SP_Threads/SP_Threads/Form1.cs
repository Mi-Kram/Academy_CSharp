using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Threads
{
    public partial class Form1 : Form
    {
        Thread t1 = null;

        public Form1()
        {
            InitializeComponent();
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)   // запущена ли эта функция в чужом потоке?
            {
                SetTextCallback d = new SetTextCallback(SetText);

                // Запустить функцию SetText в основном потоке
                this.textBox1.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }

        void thread1(object obj)
        {
            int i = 0;
            for (; ; )
            {
                //textBox1.Text = i.ToString();
                SetText(i.ToString());
                Thread.Sleep((int)obj);
                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (t1 == null)
                {
                    // Создать параметрический поток (параметр - потоковая функция)
                    ParameterizedThreadStart p1 = new ParameterizedThreadStart(thread1);

                    // Создать поток
                    t1 = new Thread(p1);

                    // Закрыть ли поток при закрытии окна
                    t1.IsBackground = true;

                    // Старт потока
                    t1.Start(Convert.ToInt32(textBox2.Text));
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            t1?.Abort();
            t1 = null;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //t1?.Abort();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            t1.Suspend();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            t1.Resume();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (t1 == null)
                return;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    t1.Priority = ThreadPriority.Normal;
                    break;
                case 1:
                    t1.Priority = ThreadPriority.Lowest;
                    break;
                case 2:
                    t1.Priority = ThreadPriority.Highest;
                    break;
                case 3:
                    t1.Priority = ThreadPriority.AboveNormal;
                    break;
                case 4:
                    t1.Priority = ThreadPriority.BelowNormal;
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            thread1(Convert.ToInt32(textBox2.Text));
        }
    }
}