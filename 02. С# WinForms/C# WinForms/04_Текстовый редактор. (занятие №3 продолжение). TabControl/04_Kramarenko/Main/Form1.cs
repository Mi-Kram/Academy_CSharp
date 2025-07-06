using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.firstTabPage.Tag = new PageInfo();
            textBox.TextChanged += TextBOX_TextChanged;
        }

        private TabPage CreateNewTabPage(string caption, string text)
        {
            TabPage tabPage = new TabPage(caption);

            TextBox textBOX = new TextBox();
            textBOX.Parent = tabPage;
            textBOX.Dock = DockStyle.Fill;
            textBOX.Font = new Font(FontFamily.GenericSansSerif, 13);
            textBOX.Multiline = true;
            textBOX.WordWrap = false;
            textBOX.ScrollBars = ScrollBars.Both;
            textBOX.Text = text;
            textBOX.TextChanged += TextBOX_TextChanged;

            return tabPage;
        }

        private void TextBOX_TextChanged(object sender, EventArgs e)
        {
            ((sender as TextBox).Parent.Tag as PageInfo).IsEdit = true;
        }

        private string CreateNewName()
        {
            string newName = "new";
            bool flag = false;

            int cnt = 1;
            while (!flag)
            {
                flag = true;

                foreach (TabPage page in pages.TabPages)
                {
                    if (page.Text == newName + cnt.ToString())
                    {
                        flag = false;
                        break;
                    }
                }
                if (!flag) cnt++;
            }
            
            return newName += cnt.ToString();
        }

        private bool SaveFile()
        {
            PageInfo pageInfo = pages.SelectedTab.Tag as PageInfo;

            if (pageInfo.IsEdit || pageInfo.Path == "")
            {
                if (pageInfo.Path == "")
                {
                    saveFileDialog.Title = "Savekeeping";
                    saveFileDialog.Filter = "TXT files|*.txt|All files|*.*";
                    saveFileDialog.FileName = pages.SelectedTab.Text;
                    saveFileDialog.CheckPathExists = true;
                    saveFileDialog.DefaultExt = ".txt";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        pageInfo.Path = saveFileDialog.FileName;
                    else return false;
                }

                try
                {
                    File.WriteAllText(pageInfo.Path, pages.SelectedTab.Controls[0].Text);

                    pageInfo.IsEdit = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK);
                    return false;
                }
            }

            return true;
        }
        private bool SaveAsFile()
        {
            PageInfo pageInfo = pages.SelectedTab.Tag as PageInfo;
            
            saveFileDialog.Title = "Savekeeping";
            saveFileDialog.Filter = "TXT files|*.txt|All files|*.*";
            saveFileDialog.FileName = pages.SelectedTab.Text;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.DefaultExt = ".txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                pageInfo.Path = saveFileDialog.FileName;
            else return false;

            try
            {
                File.WriteAllText(pageInfo.Path, pages.SelectedTab.Controls[0].Text);
                pageInfo.IsEdit = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        private bool CloseFile(string fName)
        {
            DialogResult dialogResult = MessageBox.Show($"Save file {fName}?", "INFO", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
            {
                if (!SaveFile()) return false;
            }
            else if (dialogResult == DialogResult.Cancel) return false;

            int sPage = pages.SelectedIndex;
            pages.TabPages.Remove(pages.SelectedTab);
            if (pages.TabCount == 0) return true;
            pages.SelectedIndex = sPage >= pages.TabCount ? pages.TabCount - 1 : sPage;

            return true;
        }

        private void openToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                openFileDialog.Title = "Opening";
                openFileDialog.Filter = "TXT documents|*.txt";
                openFileDialog.FilterIndex = 1;
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                openFileDialog.Multiselect = false;
                openFileDialog.FileName = "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (pages.TabPages.Count == 1 && pages.TabPages[0].Text == "new" && pages.TabPages[0].Controls[0].Text == "")
                        {
                            string text = File.ReadAllText(openFileDialog.FileName);
                            pages.TabPages[0].Text = openFileDialog.SafeFileName;
                            pages.TabPages[0].Controls[0].Text = text;
                            pages.TabPages[0].Tag = new PageInfo(openFileDialog.FileName);
                        }
                        else
                        {
                            string text = File.ReadAllText(openFileDialog.FileName);
                            TabPage tabPage = CreateNewTabPage(openFileDialog.SafeFileName, text);
                            tabPage.Tag = new PageInfo(openFileDialog.FileName);

                            pages.TabPages.Add(tabPage);
                            pages.SelectedIndex = pages.TabCount - 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "INFO", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void newToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string newName = CreateNewName();
                TabPage tabPage = CreateNewTabPage(newName, "");
                tabPage.Tag = new PageInfo();

                pages.TabPages.Add(tabPage);
                pages.SelectedIndex = pages.TabCount - 1;
            }
        }

        private void closeToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (pages.TabCount == 1 && pages.TabPages[0].Text == "new" && 
                pages.TabPages[0].Controls[0].Text == "" && 
                !(pages.TabPages[0].Tag as PageInfo).IsEdit &&
                (pages.TabPages[0].Tag as PageInfo).Path == "") return;

            if((pages.SelectedTab.Tag as PageInfo).IsEdit)
            {
                if (MessageBox.Show("Save file?", "INFO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    if (!SaveFile()) return;
            }

            int sPage = pages.SelectedIndex;
            pages.TabPages.Remove(pages.SelectedTab);
            if(pages.TabCount == 0)
            {
                string newName = CreateNewName();
                TabPage tabPage = CreateNewTabPage(newName, "");
                tabPage.Tag = new PageInfo();

                pages.TabPages.Add(tabPage);
            }
            pages.SelectedIndex = sPage >= pages.TabCount ? pages.TabCount - 1 : sPage;
        }

        private void saveToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SaveFile();
            }
        }

        private void saveAsToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SaveAsFile();
            }
        }

        private void ReplaceSubStrings(string find, string replace)
        {
            pages.SelectedTab.Controls[0].Text = 
                pages.SelectedTab.Controls[0].Text.Replace(find, replace);
        }

        private void replaceToolStripMenuItem_MouseUp(object sender, MouseEventArgs e)
        {
            FormReplace formReplace = new FormReplace();

            formReplace.replaceSubStringsEvent += new ReplaceSubStrings(ReplaceSubStrings);

            formReplace.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (TabPage tabPage in pages.TabPages)
            {
                pages.SelectedTab = tabPage;

                if((tabPage.Tag as PageInfo).IsEdit)
                {
                    if (!CloseFile(tabPage.Text))
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                else
                {
                    int sPage = pages.SelectedIndex;
                    pages.TabPages.Remove(pages.SelectedTab);
                    if (pages.TabCount == 0) break;
                    pages.SelectedIndex = sPage >= pages.TabCount ? pages.TabCount - 1 : sPage;
                }
            }
        }
    }
}
