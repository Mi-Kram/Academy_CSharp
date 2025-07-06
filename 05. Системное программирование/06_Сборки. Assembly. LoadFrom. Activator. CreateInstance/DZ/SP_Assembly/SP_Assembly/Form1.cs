using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace SP_Assembly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Type[] types;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // динамическая загрузка .NET сборки
                Assembly asm = Assembly.LoadFrom(textBox1.Text);

                // получить все типы данных, находящиеся в указанной сборке
                types = asm.GetTypes();
                foreach (Type t in types)
                {
                    listBox1.Items.Add(t.Name);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // перебор обнаруженных в сборке типов
            foreach (Type t in types)
            {
                if (t.Name == listBox1.Text)
                {
                    // перебор методов в текущем типе
                    MethodInfo[] mi = t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    
                    listBox2.Items.Clear();

                    // вывод обнаруженных методов
                    foreach (MethodInfo m in mi)
                    {
                        listBox2.Items.Add(m.Name);

                        // если обнаружен целевой метод - запустить его
                        if (m.Name == "SetBg")
                        {
                            try
                            {
                                // создание экземпляра класса, который выбрал пользователь в списке
                                // обнаруженных в сборке классов на основе информации в переменной типа Type
                                Object obj = Activator.CreateInstance(t);

                                // запуск целевого метода
                                object[] arg = new object[1];
                                arg[0] = this;

                                // obj - объект, для которого запускается метод
                                // arg - список аргументов метода
                                m.Invoke(obj, arg);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            
                        }
                    }
                }
            }
        }
    }
}
