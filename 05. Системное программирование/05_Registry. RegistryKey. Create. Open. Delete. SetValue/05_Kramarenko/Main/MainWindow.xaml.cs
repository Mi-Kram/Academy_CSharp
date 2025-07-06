using Microsoft.Win32;
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
        List<PCKeys> allowedPCKeys = new List<PCKeys>(new PCKeys[] { PCKeys.CURRENT_USER, PCKeys.LOCAL_MACHINE });
        public MainWindow()
        {
            InitializeComponent();
        }

        void UpdateMyRegistry()
        {
            dataGrid.Items.Clear();
            treeView.Items.Clear();

            foreach (PCKeys pcKey in allowedPCKeys)
            {
                RegistryKey firstKey = GetRegistryKey(pcKey);
                if (firstKey == null)
                {
                    MessageBox.Show("pcKey is wrong!");
                    return;
                }

                TreeViewItem item = new TreeViewItem();
                item.Header = firstKey.ToString();
                item.Items.Add("*");
                item.Tag = new KeyTag() { PCKey = pcKey, DataGridItems = GetValues(firstKey), Path = "" };
                item.Expanded += Item_Expanded;
                item.Collapsed += Item_Collapsed;
                treeView.Items.Add(item);
            }
        }

        private void Item_Collapsed(object sender, RoutedEventArgs e)
        {
            TreeViewItem source = e.OriginalSource as TreeViewItem;
            source.Items.Clear();
            source.Items.Add("*");
        }

        private void Item_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem source = e.OriginalSource as TreeViewItem;
            source.Items.Clear();

            KeyTag keyTag = source.Tag as KeyTag;

            RegistryKey key = GetRegistryKey(keyTag.PCKey).OpenSubKey(keyTag.Path);
            if (key == null) return;

            if(key != null)
            {
                string[] keyNames = key.GetSubKeyNames();
                foreach (string name in keyNames)
                {
                    try
                    {
                        RegistryKey newKey = key.OpenSubKey(name);
                        if (newKey != null)
                        {
                            TreeViewItem item = new TreeViewItem();
                            item.Header = name;
                            item.Items.Add("*");
                            item.Tag = new KeyTag()
                            {
                                PCKey = keyTag.PCKey,
                                DataGridItems = GetValues(newKey),
                                Path = keyTag.Path + (keyTag.Path.Length == 0 ? "" : "\\") + name
                            };
                            item.Expanded += Item_Expanded;
                            source.Items.Add(item);

                            newKey.Close();
                        }
                    }
                    catch { }
                }
            }
            key.Close();
        }
        RegistryKey GetRegistryKey(PCKeys pcKey)
        {
            RegistryKey key = null;
            switch (pcKey)
            {
                case PCKeys.CLASSES_ROOT:
                    key = Registry.ClassesRoot;
                    break;
                case PCKeys.CURRENT_USER:
                    key = Registry.CurrentUser;
                    break;
                case PCKeys.LOCAL_MACHINE:
                    key = Registry.LocalMachine;
                    break;
                case PCKeys.USERS:
                    key = Registry.Users;
                    break;
                case PCKeys.CURRENT_CONFIG:
                    key = Registry.CurrentConfig;
                    break;
                default: break;
            }

            return key;
        }

        List<DataGridItem> GetValues(RegistryKey key)
        {
            List<DataGridItem> lst = new List<DataGridItem>();

            if(key != null)
            {
                string[] valueNames = key.GetValueNames();
                foreach (string valName in valueNames)
                {
                    DataGridItem item = new DataGridItem();
                    item.Key = valName;
                    item.Type = key.GetValueKind(valName).ToString();
                    item.Value = Convert.ToString(key.GetValue(valName));

                    lst.Add(item);
                }
            }

            return lst;
        }
        List<string> GetKeys(RegistryKey key)
        {
            List<string> lst = new List<string>();

            if(key != null)
            {
                lst = key.GetSubKeyNames().ToList();
            }

            return lst;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateMyRegistry();
        }

        private void SelectedItemChanged_TreeView(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            dataGrid.Items.Clear();
            if (treeView.SelectedItem != null)
            {
                List<DataGridItem> lst = ((treeView.SelectedItem as TreeViewItem).Tag as KeyTag).DataGridItems;
                foreach (DataGridItem item in lst)
                {
                    dataGrid.Items.Add(item);
                }
            }
        }

        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            e.Cancel = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateKey_Click(object sender, RoutedEventArgs e)
        {
            if(treeView.SelectedItem != null)
            {
                TreeViewItem selected = treeView.SelectedItem as TreeViewItem;
                KeyTag keyTag = selected.Tag as KeyTag;

                RegistryKey selectedKey = GetRegistryKey(keyTag.PCKey).OpenSubKey(keyTag.Path, true);
                CreateKeyDialog dialog = new CreateKeyDialog();
                dialog.ValParent = selected.Header.ToString();

                dialog.DenniedKeys = GetKeys(selectedKey);

                if(dialog.ShowDialog() == true)
                {
                    RegistryKey newKey = null;
                    try
                    {
                        newKey = selectedKey.CreateSubKey(dialog.KeyName);

                        TreeViewItem newItem = new TreeViewItem();
                        newItem.Header = dialog.KeyName;
                        newItem.Expanded += Item_Expanded;
                        newItem.Collapsed += Item_Collapsed;
                        newItem.Tag = new KeyTag() { PCKey = keyTag.PCKey, DataGridItems = GetValues(newKey), Path = keyTag.Path + (keyTag.Path.Length > 0 ? "\\" : "") + dialog.KeyName };
                        newItem.Items.Add("*");
                        selected.Items.Add(newItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка!\n\n{ex.Message}", "INFO", MessageBoxButton.OK);
                    }
                    finally
                    {
                        if (newKey != null) newKey.Close();
                        if (selectedKey != null) selectedKey.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите место для создания ключа!", "INFO", MessageBoxButton.OK);
            }
        }

        private void DeleteKey_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null && 
                MessageBox.Show("Хотите удалить выделенную ветку?","INFO",MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                TreeViewItem selected = treeView.SelectedItem as TreeViewItem;
                KeyTag keyTag = selected.Tag as KeyTag;

                if(keyTag.Path.Count(x => x == '\\') < 1)
                {
                    MessageBox.Show("Я не хочу, чтобы эти ветки были удалены!", "INFO", MessageBoxButton.OK);
                    return;
                }

                TreeViewItem itemParent = selected.Parent as TreeViewItem;
                KeyTag keyTagParent = itemParent.Tag as KeyTag;

                RegistryKey parentKey = GetRegistryKey(keyTagParent.PCKey).OpenSubKey(keyTagParent.Path, true);
                try
                {
                    parentKey.DeleteSubKey(selected.Header.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка!\n\n{ex.Message}", "INFO", MessageBoxButton.OK);
                }
                finally
                {
                    if (parentKey != null) parentKey.Close();
                }

                itemParent.Items.Remove(selected);
            }
            else
            {
                MessageBox.Show("Выберите ключ для удаления!", "INFO", MessageBoxButton.OK);
            }
        }

        private void CreateValue_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null)
            {
                TreeViewItem selected = treeView.SelectedItem as TreeViewItem;
                KeyTag keyTag = selected.Tag as KeyTag;

                if (keyTag.Path.Count(x => x == '\\') < 1)
                {
                    MessageBox.Show("Я не хочу, чтобы в эти ветки были добавлены значения!", "INFO", MessageBoxButton.OK);
                    return;
                }

                CreateValueDialog dialog = new CreateValueDialog();
                dialog.ValParent = selected.Header.ToString();

                RegistryKey key = GetRegistryKey(keyTag.PCKey).OpenSubKey(keyTag.Path, true);
                dialog.DenniedValNames = GetValues(key).Select(x => x.Key).ToList();

                if(dialog.ShowDialog() == true)
                {
                    try
                    {
                        key.SetValue(dialog.ValName, dialog.Value, dialog.Type);

                        DataGridItem newItem = new DataGridItem();
                        newItem.Key = dialog.ValName;
                        newItem.Value = dialog.Value;
                        newItem.Type = dialog.Type.ToString();
                        dataGrid.Items.Add(newItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка!\n\n{ex.Message}", "INFO", MessageBoxButton.OK);
                    }
                    finally
                    {
                        if (key != null) key.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите ключ для добавления значения!", "INFO", MessageBoxButton.OK);
            }
        }

        RegistryValueKind GetRegistryValueKind(string kind)
        {
            RegistryValueKind result;
            switch (kind.ToLower())
            {
                case "dword":
                    result = RegistryValueKind.DWord;
                    break;
                case "binary":
                    result = RegistryValueKind.Binary;
                    break;
                default:
                    result = RegistryValueKind.String;
                    break;
            }
            return result;
        }
        private void DeleteValue_Click(object sender, RoutedEventArgs e)
        {
            if(treeView.SelectedItem != null && dataGrid.SelectedItem != null &&
                MessageBox.Show("Хотите удалить выделенные значения?", "INFO", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                TreeViewItem item = treeView.SelectedItem as TreeViewItem;
                KeyTag keyTag = item.Tag as KeyTag;

                RegistryKey key = GetRegistryKey(keyTag.PCKey).OpenSubKey(keyTag.Path, true);
                try
                {
                    while (dataGrid.SelectedItems.Count > 0)
                    {
                        try
                        {
                            DataGridItem row = dataGrid.SelectedItem as DataGridItem;
                            key.DeleteValue(row.Key);
                            dataGrid.Items.Remove(row);
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка!\n\n{ex.Message}", "INFO", MessageBoxButton.OK);
                }
                finally
                {
                    if (key != null) key.Close();
                }
            }
            else
            {
                MessageBox.Show("Выберите ключ для удаления!", "INFO", MessageBoxButton.OK);
            }
        }


        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateMyRegistry();
        }

        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (treeView.SelectedItem != null && dataGrid.SelectedItem != null)
            {
                TreeViewItem item = treeView.SelectedItem as TreeViewItem;
                KeyTag keyTag = item.Tag as KeyTag;

                int index = dataGrid.SelectedIndex;
                DataGridItem gridItem = dataGrid.Items[index] as DataGridItem;

                ModifyValueDialog dialog = new ModifyValueDialog();
                dialog.ValParent = item.Header.ToString();
                dialog.ValName = gridItem.Key;
                dialog.Value = gridItem.Value;
                if(dialog.ShowDialog() == true && MessageBox.Show("Изменить запись?","INFO",MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                {
                    RegistryKey key = GetRegistryKey(keyTag.PCKey).OpenSubKey(keyTag.Path, true);
                    try
                    {
                        key.SetValue(gridItem.Key, dialog.Value);

                        DataGridItem updateItem = new DataGridItem();
                        updateItem.Key = gridItem.Key;
                        updateItem.Type = gridItem.Type;
                        updateItem.Value = dialog.Value;

                        dataGrid.Items.Remove(gridItem);
                        dataGrid.Items.Insert(index, updateItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка!\n\n{ex.Message}", "INFO", MessageBoxButton.OK);
                    }
                    finally
                    {
                        if (key != null) key.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите ключ для редактирования!", "INFO", MessageBoxButton.OK);
            }
        }

    }

    public enum PCKeys
    {
        CLASSES_ROOT,
        CURRENT_USER,
        LOCAL_MACHINE,
        USERS,
        CURRENT_CONFIG
    }
    public class KeyTag
    {
        public string Path { get; set; }
        public PCKeys PCKey { get; set; }
        public List<DataGridItem> DataGridItems { get; set; }
    }

    public class DataGridItem
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public DataGridItem()
        {
            Key = "";
            Type = "";
            Value = "";
        }
    }
}
