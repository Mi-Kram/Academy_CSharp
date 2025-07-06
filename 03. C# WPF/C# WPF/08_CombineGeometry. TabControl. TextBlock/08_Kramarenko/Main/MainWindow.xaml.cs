using System;
using System.Collections.Generic;
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

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Icon = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Resources\\Notepad.ico", UriKind.Absolute));
        }


        private void MenuItemNew_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = CreateTabItem("new", new TextBox());
            tabControl.Items.Add(tabItem);
            tabControl.SelectedItem = tabItem;
        }
        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.Multiselect = false;
            dlg.CheckFileExists = true;
            dlg.FileName = "";
            dlg.Title = "Opening";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "TXT|*.txt|JPEG JPG PNG BMP|*.jpeg;*.jpg;*.png;*.bmp";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string path = dlg.FileName;
                string ext = System.IO.Path.GetExtension(path);
                if (ext == ".txt")
                {
                    try
                    {
                        TabItem tabItem = CreateTabItem(dlg.SafeFileName, 
                            new TextBox(), System.IO.File.ReadAllText(path));

                        tabControl.Items.Add(tabItem);
                        tabControl.SelectedItem = tabItem;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "INFO", MessageBoxButton.OK);
                    }
                }
                else if(ext == ".jpeg" || ext == ".jpg" || ext == ".png" || ext == ".bmp")
                {
                    try
                    {
                        TabItem tabItem = CreateTabItem(dlg.SafeFileName, 
                            new BitmapImage(new Uri(path,UriKind.Absolute)));

                        tabControl.Items.Add(tabItem);
                        tabControl.SelectedItem = tabItem;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "INFO", MessageBoxButton.OK);
                    }
                }
            }
        }
        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            int sIndex = tabControl.SelectedIndex;
            if (sIndex >= 0 && sIndex < tabControl.Items.Count)
            {
                SaveTabItem(sIndex);
            }
        }
        private void MenuItemClose_Click(object sender, RoutedEventArgs e)
        {
            int sIndex = tabControl.SelectedIndex;
            if (sIndex >= 0 && sIndex < tabControl.Items.Count)
            {
                CloseTabItem(sIndex);
            }
        }
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        

        private void MenuItemPaste_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                    if ((tabControl.SelectedItem as TabItem).Content is TextBox)
                    {
                        TextBox currTB = (tabControl.SelectedItem as TabItem).Content as TextBox;

                        if (currTB.IsSelectionActive)
                        {
                            int startS = currTB.SelectionStart;
                            currTB.Text = currTB.Text.Remove(startS, currTB.SelectionLength);
                            
                            string CBRD = Clipboard.GetText(TextDataFormat.Text);
                            currTB.Text = currTB.Text.Insert(startS, CBRD);
                            currTB.SelectionStart += CBRD.Length;
                        }
                        else
                        {
                            int startS = currTB.SelectionStart;

                            string CBRD = Clipboard.GetText(TextDataFormat.Text);
                            currTB.Text = currTB.Text.Insert(startS, CBRD);
                            currTB.SelectionStart += CBRD.Length;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ClipBRD Error");
            }
        }
        private void MenuItemCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((tabControl.SelectedItem as TabItem).Content is TextBox)
                {
                    TextBox currTB = (tabControl.SelectedItem as TabItem).Content as TextBox;

                    if (currTB.IsSelectionActive)
                    {
                        Clipboard.SetText(currTB.SelectedText, TextDataFormat.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ClipBRD Error");
            }
        }
        private void MenuItemCut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((tabControl.SelectedItem as TabItem).Content is TextBox)
                {
                    TextBox currTB = (tabControl.SelectedItem as TabItem).Content as TextBox;
                    
                    if (currTB.IsSelectionActive)
                    {
                        Clipboard.SetText(currTB.SelectedText, TextDataFormat.Text);
                        currTB.Text = currTB.Text.Remove(currTB.SelectionStart, currTB.SelectionLength);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ClipBRD Error");
            }
        }

        TabItem CreateTabItem(string header, object content, string text = "")
        {
            TabItem tabItem = new TabItem();
            tabItem.Header = header;

            object cont = null;
            if(content is TextBox)
            {
                TextBox textBox = content as TextBox;
                textBox.FontSize = 15;
                textBox.Focus();
                textBox.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                textBox.Margin = new Thickness(0);
                textBox.AcceptsTab = true;
                textBox.AcceptsReturn = true;
                textBox.Text = text;

                cont = textBox;
            }
            else if(content is BitmapImage)
            {
                Image image = new Image();
                image.Source = content as BitmapImage;
                image.Focus();
                image.Margin = new Thickness(0);
                image.Stretch = Stretch.Uniform;

                cont = image;
            }

            tabItem.Content = cont;
            return tabItem;
        }

        void SaveTabItem(int index)
        {
            if ((tabControl.Items[index] as TabItem).Content is TextBox)
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

                dlg.DefaultExt = ".txt";
                dlg.Filter = "TXT|*.txt";
                dlg.CheckPathExists = true;
                dlg.FileName = (tabControl.Items[index] as TabItem).Header.ToString();
                dlg.Title = "Save";

                bool? result = dlg.ShowDialog();

                if (result != null && result == true)
                {
                    MessageBox.Show("Saved", "INFO", MessageBoxButton.OK);
                }
            }
        }
        void CloseTabItem(int index)
        {
            if ((tabControl.Items[index] as TabItem).Content is TextBox)
            {
                if (MessageBox.Show("Save?", "INFO", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SaveTabItem(index);
                }
            }

            tabControl.Items.Remove(tabControl.SelectedItem);

            if (tabControl.Items.Count == 0)
            {
                TabItem tabItem = CreateTabItem("new", new TextBox());
                tabControl.Items.Add(tabItem);
                tabControl.SelectedItem = tabItem;
            }
            else if (index >= tabControl.Items.Count)
            {
                tabControl.SelectedIndex = tabControl.Items.Count - 1;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool flag = false;
            while (tabControl.Items.Count != 0)
            {
                if (tabControl.Items.Count == 1) flag = true;

                tabControl.SelectedIndex = 0;
                CloseTabItem(0);

                if (flag) break;
            }
        }
    }
}
