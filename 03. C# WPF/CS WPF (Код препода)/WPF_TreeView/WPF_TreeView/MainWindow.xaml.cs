using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_TreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            treeView1.Items.Clear();

            TreeViewItem itemPlanes = new TreeViewItem();
            itemPlanes.Header = "Planes";
            itemPlanes.IsExpanded = true;
            treeView1.Items.Add(itemPlanes);

            TreeViewItem itemTupolev = new TreeViewItem();
            itemTupolev.IsExpanded = true;
            itemTupolev.Header = "Tupolev";
            itemTupolev.Items.Add("Tu-134");
            itemTupolev.Items.Add("Tu-144");
            itemTupolev.Items.Add("Tu-154");
            itemTupolev.Items.Add("Tu-204");
            itemPlanes.Items.Add(itemTupolev);

            TreeViewItem itemBoeing = new TreeViewItem();
            itemBoeing.IsExpanded = true;
            itemBoeing.Header = "Boeing";

            itemBoeing.Items.Add("Boeing-737");

            TreeViewItem item = new TreeViewItem();
            item.Header = "Boeing-747";
            itemBoeing.Items.Add(item);

            Button btn = new Button();
            btn.Content = "Boeing-777";
            itemBoeing.Items.Add(btn);

            itemBoeing.Items.Add("Boeing-787");
            itemPlanes.Items.Add(itemBoeing);

            TreeViewItem itemAirbus = new TreeViewItem();
            itemAirbus.Header = "Airbus";
            itemAirbus.Items.Add("A-320");
            itemAirbus.Items.Add("A-350");
            itemAirbus.Items.Add("A-380");
            itemPlanes.Items.Add(itemAirbus);

            TreeViewItem itemIlyushin = new TreeViewItem();
            itemIlyushin.Header = "Ilyushin";
            itemIlyushin.Items.Add("Il-76");
            itemIlyushin.Items.Add("Il-86");
            itemIlyushin.Items.Add("Il-96");
            itemPlanes.Items.Add(itemIlyushin);

            TreeViewItem itemAntonov = new TreeViewItem();
            itemAntonov.Header = "Antonov";
            itemAntonov.Items.Add("An-24");
            itemAntonov.Items.Add("An-124 Ruslan");
            itemAntonov.Items.Add("An-225 Mriya");
            itemPlanes.Items.Add(itemAntonov);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Person c1 = new Person("Vasya", "Pupkin", @"admin.gif");
            c1.Parent = (Person)treeView1.SelectedItem;
            c1.Parent?.Items.Add(c1);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Person)treeView1.SelectedItem;
            selectedItem?.Parent?.Items.Remove(selectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            treeView1.Items.Clear();

            treeView1.Items.Add(new Person("Alex", "Petrov", @"admin.gif"));
            treeView1.Items.Add(new Person("Ivan", "Matveev", @"enotik.gif"));
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            treeView1.Items.Clear();

            // получить логические диски из ОС
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                TreeViewItem item = new TreeViewItem();

                // назначить обработчик разворачивания узла дерева
                item.Expanded += Item_Expanded;

                // сохранить информацию о диске в пункте дерева
                item.Tag = drive;

                // текст пункта
                item.Header = drive.ToString();

                // добавить пустой элемент для возможности развернуть узел
                item.Items.Add("*");

                treeView1.Items.Add(item);
            }
        }

        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            // получить ссылку на разворачиваемый пункт
            TreeViewItem parentItem = (TreeViewItem)e.OriginalSource;

            // очистить старые дочерние элементы
            parentItem.Items.Clear();

            DirectoryInfo dir;

            // если в пункте хранится лог. диск
            if (parentItem.Tag is DriveInfo)
            {
                // получить корневую папку диска
                DriveInfo drive = (DriveInfo)parentItem.Tag;
                dir = drive.RootDirectory;
            }
            else dir = (DirectoryInfo)parentItem.Tag;

            try
            {
                // получить подпапки текущей папки
                foreach (DirectoryInfo subDir in dir.GetDirectories())
                {
                    // создать для каждой подпапки отдельный узел дерева
                    TreeViewItem newItem = new TreeViewItem();
                    newItem.Tag = subDir;
                    newItem.Header = subDir.ToString();
                    newItem.Items.Add("*");

                    // включение поддержки Drag-n-Drop
                    newItem.AllowDrop = true;
                    newItem.DragEnter += NewItem_DragEnter;
                    newItem.Drop += NewItem_Drop;

                    parentItem.Items.Add(newItem);
                }
            }
            catch
            { }
        }

        // метод получает полный путь для заданного узла дерева
        public string GetFullPath(TreeViewItem node)
        {
            if (node == null)
                throw new ArgumentNullException();

            var result = Convert.ToString(node.Header);

            for (TreeViewItem i = (TreeViewItem)node.Parent; i != null; i = (i.Parent is TreeViewItem)? (TreeViewItem)i.Parent: null)
                result = i.Header + @"\" + result;

            return result;
        }

        private void NewItem_Drop(object sender, DragEventArgs e)
        {
            // Если перетаскивается список файлов
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
                (e.AllowedEffects & DragDropEffects.Copy) != 0)
            {
                // Получить список файлов
                string[] str = (string[])e.Data.GetData(DataFormats.FileDrop);

                MessageBox.Show(GetFullPath((TreeViewItem)sender));
            }

            // отменить обработку Drag-n-Drop для родительских вершин дерева
            e.Handled = true;
        }

        private void NewItem_DragEnter(object sender, DragEventArgs e)
        {
            // Если пользователь копирует объект перетаскиванием и это список файлов
            if (e.Data.GetDataPresent(DataFormats.FileDrop) &&
                (e.AllowedEffects & DragDropEffects.Copy) != 0)
            {
                // Разрешить копирование
                e.Effects = DragDropEffects.Copy;
            }
            else
                e.Effects = DragDropEffects.None;
        }
    }
}
