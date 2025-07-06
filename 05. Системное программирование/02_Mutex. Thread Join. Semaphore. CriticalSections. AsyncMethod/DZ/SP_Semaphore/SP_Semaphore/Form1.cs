using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DSSemaphore
{
    public partial class Form1 : Form
    {

        int[] a = new int[10];
        Thread t1 = null, t2 = null, t3 = null;
        Semaphore semaphore1 = new Semaphore(1, 2, "TestSemaphore");

        public Form1()
        {
            InitializeComponent();
        }

        void print()
        {
            listBox1.Items.Clear();
            foreach (int c in a)
            {
                listBox1.Items.Add(c.ToString());
            }
        }

        void thread1()
        {
            for (int i = 0; ; i++)
            {
                textBox1.Text = "Start " + i.ToString();
                for (int k = 0; k < 10; k++)
                {
                    a[k] = 33;
                    Thread.Sleep(200);
                }
                print();
                textBox1.Text = "Done " + i.ToString();
                Thread.Sleep(1000);
            }
        }

        void thread2()
        {
            for (int i = 0; ; i++)
            {
                textBox2.Text = "Start " + i.ToString();
                for (int k = 0; k < 10; k++)
                {
                    a[k] = 555;
                    Thread.Sleep(300);
                }
                print();
                textBox2.Text = "Done " + i.ToString();
                Thread.Sleep(700);
            }
        }

        void thread3()
        {
            for (int i = 0; ; i++)
            {
                textBox3.Text = "Start " + i.ToString();
                for (int k = 0; k < 10; k++)
                {
                    a[k] = 7;
                    Thread.Sleep(200);
                }
                print();
                textBox3.Text = "Done " + i.ToString();
                Thread.Sleep(1000);
            }
        }

        void thread4()
        {
            for (int i = 0; ; i++)
            {
                textBox1.Text = "Start " + i.ToString() + " Pause...";

                // Попытка занять слот семафора
                semaphore1.WaitOne();

                textBox1.Text = "Start " + i.ToString();
                for (int k = 0; k < 10; k++)
                {
                    a[k] = 33;
                    Thread.Sleep(500);
                }
                textBox1.Text = "Done " + i.ToString();
                print();

                // Освободить семафор
                semaphore1.Release();

                //Thread.Sleep(500);
            }
        }

        void thread5()
        {
            for (int i = 0; ; i++)
            {
                textBox2.Text = "Start " + i.ToString() + " Pause...";

                semaphore1.WaitOne();

                textBox2.Text = "Start " + i.ToString();
                for (int k = 0; k < 10; k++)
                {
                    a[k] = 555;
                    Thread.Sleep(300);
                }
                textBox2.Text = "Done " + i.ToString();
                print();
                semaphore1.Release();
                //Thread.Sleep(700);
            }

        }

        void thread6()
        {
            for (int i = 0; ; i++)
            {
                textBox3.Text = "Start " + i.ToString() + " Pause...";
                semaphore1.WaitOne();
                textBox3.Text = "Start " + i.ToString();
                for (int k = 0; k < 10; k++)
                {
                    a[k] = 7;
                    Thread.Sleep(300);
                }
                textBox3.Text = "Done " + i.ToString();
                print();
                semaphore1.Release();
                //Thread.Sleep(700);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ThreadStart p1 = new ThreadStart(thread1);
            t1 = new Thread(p1);
            t1.IsBackground = true;
            t1.Start();

            p1 = new ThreadStart(thread2);
            t2 = new Thread(p1);
            t2.IsBackground = true;
            t2.Start();

            p1 = new ThreadStart(thread3);
            t3 = new Thread(p1);
            t3.IsBackground = true;
            t3.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (t1 != null && t1.IsAlive) t1.Abort();
            if (t2 != null && t2.IsAlive) t2.Abort();
            if (t3 != null && t3.IsAlive) t3.Abort();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ThreadStart p1 = new ThreadStart(thread4);
            t1 = new Thread(p1);
            t1.IsBackground = true;
            t1.Start();

            p1 = new ThreadStart(thread5);
            t2 = new Thread(p1);
            t2.IsBackground = true;
            t2.Start();

            p1 = new ThreadStart(thread6);
            t3 = new Thread(p1);
            t3.IsBackground = true;
            t3.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // По факту добавляет новый слот
            semaphore1.Release();
        }

    }
}
