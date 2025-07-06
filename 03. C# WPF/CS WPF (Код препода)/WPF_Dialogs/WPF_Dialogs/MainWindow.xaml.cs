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
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Drawing;

namespace WPF_Dialogs
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

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            Window2 w1 = new Window2();
            w1.Owner = this;
            w1.ChangeColor += W1_ChangeColor;

            bool? result = w1.ShowDialog();
            if(result.HasValue)
            {
                MessageBox.Show(result.Value.ToString());
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            Window2 w1 = new Window2();
            w1.Owner = this;
            w1.ChangeColor += W1_ChangeColor;
            w1.Show();
        }

        private void W1_ChangeColor(byte R, byte G, byte B)
        {
            SolidColorBrush brush2 = new SolidColorBrush(System.Windows.Media.Color.FromRgb(R, G, B));
            grid1.Background = brush2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                MessageBox.Show(dlg.FileName);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            bool? result = dlg.ShowDialog();

            if (result != null && result == true)
            {
                MessageBox.Show(dlg.FileName);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // CommonFileDialog не работает на ОС ниже Windows Vista
            if (CommonFileDialog.IsPlatformSupported)
            {
                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                dialog.InitialDirectory = @"c:\";
                CommonFileDialogResult result = dialog.ShowDialog();

                if(result == CommonFileDialogResult.Ok)
                {
                    MessageBox.Show(dialog.FileName);

                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            // Задание начальной папки для диалога
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            //folderBrowserDialog1.SelectedPath = "d:\\";

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show(folderBrowserDialog.SelectedPath);
            }
        }

        private void ClrPcker_Background_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<System.Windows.Media.Color?> e)
        {
            SolidColorBrush brush2 = new SolidColorBrush(ClrPcker_Background.SelectedColor.Value);
            grid1.Background = brush2;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorDialog1 = new System.Windows.Forms.ColorDialog();

            // Открыть полную версию диалога
            colorDialog1.FullOpen = false;

            // Разместить кнопку открытия полной версии
            colorDialog1.AllowFullOpen = true;

            // Открытие диалога Windows Forms
            System.Windows.Forms.DialogResult result = colorDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // конвертирование System.Drawing.Color в System.Windows.Media.Color
                System.Windows.Media.Color color = new System.Windows.Media.Color();
                color.A = colorDialog1.Color.A;
                color.R = colorDialog1.Color.R;
                color.G = colorDialog1.Color.G;
                color.B = colorDialog1.Color.B;

                SolidColorBrush brush2 = new SolidColorBrush(color);
                grid1.Background = brush2;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog1 = new System.Windows.Forms.FontDialog();

            fontDialog1.ShowEffects = true;
            fontDialog1.ShowColor = true;
            fontDialog1.Color = System.Drawing.Color.Red;
            fontDialog1.MinSize = 20;
            fontDialog1.MaxSize = 50;

            // открытие диалога Windows Forms
            if (fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                label1.FontFamily = new System.Windows.Media.FontFamily(fontDialog1.Font.Name);
                label1.FontSize = fontDialog1.Font.Size * 96.0 / 72.0;
                label1.FontWeight = fontDialog1.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                label1.FontStyle = fontDialog1.Font.Italic ? FontStyles.Italic : FontStyles.Normal;

                // настройка цвета текста
                System.Windows.Media.Color color = new System.Windows.Media.Color();
                color.A = fontDialog1.Color.A;
                color.R = fontDialog1.Color.R;
                color.G = fontDialog1.Color.G;
                color.B = fontDialog1.Color.B;
                SolidColorBrush brush2 = new SolidColorBrush(color);
                label1.Foreground = brush2;
            }
        }
    }
}
