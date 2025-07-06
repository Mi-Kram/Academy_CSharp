using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinForms_ListView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // изменение режима отображения listView
            listView1.View = View.Details;

            // создание коллекций пользовательских картинок
            // картинки для режима маленьких значков
            listView1.SmallImageList = new ImageList();

            // картинки для режима больших значков
            listView1.LargeImageList = new ImageList();

            // картинки для пользовательских checkbox
            listView1.StateImageList = new ImageList();

            // задание размера для больших значков 
            listView1.LargeImageList.ImageSize = new Size(48, 48);

            // загрузка значков для пуктов
            listView1.LargeImageList.Images.Add(Image.FromFile("Bitmap1.bmp"));
            listView1.SmallImageList.Images.Add(Image.FromFile("Bitmap2.bmp"));

            // загрузка значков для checkbox
            listView1.StateImageList.Images.Add(Image.FromFile("Bitmap3.bmp"));
            listView1.StateImageList.Images.Add(Image.FromFile("Bitmap4.bmp"));

            // программное создание столбцов
            listView1.Columns.Add("ФИО", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Адрес", 120, HorizontalAlignment.Center);
            listView1.Columns.Add("Оклад", 60, HorizontalAlignment.Right);

            // программное создание групп
            listView1.Groups.Add("group3", "Отдел IT");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem("Иванов В.И.");
            item.ImageIndex = 0;
            item.StateImageIndex = 0;
            item.Checked = false;
            item.Tag = 123;
            item.SubItems.Add("Донецк");
            item.SubItems.Add("$1234");
            listView1.Items.Add(item);

            item = new ListViewItem("Петров К.Т.");
            item.ImageIndex = 0;
            item.StateImageIndex = 0;
            item.SubItems.Add("Киев");
            item.SubItems.Add("$600");
            item.Group = listView1.Groups[0];
            listView1.Items.Add(item);

            item = new ListViewItem("Матвеев К.Д.");
            item.ImageIndex = 0;
            item.StateImageIndex = 0;
            item.SubItems.Add("Москва");
            item.SubItems.Add("$3345");
            item.Group = listView1.Groups[1];
            listView1.Items.Add(item);

            item = new ListViewItem("Никифоров В.Т.");
            item.ImageIndex = 0;
            item.StateImageIndex = 0;
            item.SubItems.Add("Киев");
            item.SubItems.Add("$445");
            item.Group = listView1.Groups[0];
            listView1.Items.Add(item);

            item = new ListViewItem("Зайцев М.Л.");
            item.ImageIndex = 0;
            item.StateImageIndex = 0;
            item.SubItems.Add("Москва");
            item.SubItems.Add("$1235");
            item.Group = listView1.Groups[2];
            listView1.Items.Add(item);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // программное добавление пункта listView
            ListViewItem item = new ListViewItem(textBox1.Text);

            // номер загруженной картинки в списках картинок SmallImageList, LargeImageList
            item.ImageIndex = 0;
            item.SubItems.Add(textBox2.Text);
            item.SubItems.Add(textBox3.Text);
            listView1.Items.Add(item);

            // программное включение режима редактирования пункта
            // item.BeginEdit();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    listView1.View = View.Details;
                    break;
                case 1:
                    listView1.View = View.SmallIcon;
                    break;
                case 2:
                    listView1.View = View.LargeIcon;
                    break;
                case 3:
                    listView1.View = View.List;
                    break;
                case 4:
                    listView1.View = View.Tile;
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            while (listView1.CheckedItems.Count > 0)
                listView1.CheckedItems[0].Remove();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem(textBox1.Text);
            item.ImageIndex = 0;
            item.Tag = 123;
            item.SubItems.Add(textBox2.Text);
            item.SubItems.Add(textBox3.Text);
            ListViewItem selectedItem = listView1.SelectedItems[0];
            item.Group = selectedItem.Group;
            listView1.Items.Insert(selectedItem.Index, item);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Показать количество выделенных элементов
            MessageBox.Show(listView1.SelectedItems.Count.ToString());

            if (listView1.SelectedItems[0] != null)
                MessageBox.Show(listView1.SelectedItems[0].Text);
        }

        private void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            //MessageBox.Show("listView1_BeforeLabelEdit");

            // разрешить или нет редактирование пунктов
            if(listView1.SelectedItems[0].Text.Length > 12)
                e.CancelEdit = true;
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            //MessageBox.Show("listView1_AfterLabelEdit");

            // отменить изменения
            //e.CancelEdit = true;
        }
    }
}