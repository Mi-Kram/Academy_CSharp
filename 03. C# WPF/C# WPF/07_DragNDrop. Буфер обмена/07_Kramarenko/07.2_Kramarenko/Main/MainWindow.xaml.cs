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
        ImageSource emptyPicture = new BitmapImage(new Uri(@"C:\Users\Setup\Desktop\07.2_Kramarenko\Main\Resources\Empty.bmp", UriKind.Absolute));

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < grid.ColumnDefinitions.Count; j++)
                {
                    Image image = new Image();

                    image.Margin = new Thickness(5, 5, 5, 5);
                    image.Stretch = Stretch.Uniform;
                    image.Source = emptyPicture;

                    image.MouseDown += SmallImage_MouseDown;

                    image.AllowDrop = true;
                    image.DragEnter += Image_DragEnter;
                    image.Drop += Image_Drop;

                    Grid.SetRow(image, i);
                    Grid.SetColumn(image, j);

                    grid.Children.Add(image);
                }
            }
        }

        #region Image

        private void Image_DragEnter(object sender, DragEventArgs e)
        {
            if ((e.AllowedEffects & DragDropEffects.Copy) != 0)
            {
                e.Effects = DragDropEffects.Copy;
            }
        }
        private void Image_Drop(object sender, DragEventArgs e)
        {
            // Если перетаскивается список файлов
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                
                foreach (string file in files)
                {
                    string ext = System.IO.Path.GetExtension(file);
                    if (ext == ".bmp" || ext == ".jpg" || ext == ".gif" || ext == ".png")
                    {
                        (sender as Image).Source = new BitmapImage(new Uri(file, UriKind.Absolute));
                        break;
                    }
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                (sender as Image).Source = (BitmapSource)e.Data.GetData(DataFormats.Bitmap);
            }
        }
        private void SmallImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((sender as Image).Source != emptyPicture)
            {
                Grid grid_for_image = new Grid();

                Grid.SetRowSpan(grid_for_image, grid.RowDefinitions.Count);
                Grid.SetColumnSpan(grid_for_image, grid.ColumnDefinitions.Count);

                grid_for_image.Margin = new Thickness(0, 0, 0, 0);
                grid_for_image.MouseDown += StackPanel_MouseDown;
                grid_for_image.Background = new LinearGradientBrush(Brushes.Gray.Color, Brushes.White.Color, 90);

                Image image = new Image();
                image.Margin = new Thickness(5, 5, 5, 5);
                image.Source = (sender as Image).Source;
                image.Stretch = Stretch.Uniform;

                grid_for_image.Children.Add(image);

                grid.ShowGridLines = false;
                grid.Children.Add(grid_for_image);
            }
        }

        #endregion

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            grid.Children.Remove(sender as FrameworkElement);
            grid.ShowGridLines = true;
            e.Handled = true;
        }
    }
}
