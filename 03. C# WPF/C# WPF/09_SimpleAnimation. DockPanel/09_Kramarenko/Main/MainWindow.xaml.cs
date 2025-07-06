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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Forms.ColorDialog colorDialog;
        List<ImageSource> imageSources;
        Random rand;
        Image selectedImage;
        bool canMouseDown;

        public MainWindow()
        {
            InitializeComponent();

            colorDialog = new System.Windows.Forms.ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.FullOpen = true;

            imageSources = new List<ImageSource>();
            LoadImages(@"Resourсes\Images");

            rand = new Random();
            selectedImage = null;
            canMouseDown = true;
        }

        void LoadImages(string imgsPath)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(Environment.CurrentDirectory + '\\' + imgsPath);

                FileInfo[] files = dInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    string ext = System.IO.Path.GetExtension(file.Name);
                    if (ext == ".png" || ext == ".bmp" || ext == ".jpg" || ext == ".jpeg")
                    {
                        imageSources.Add(new BitmapImage(new Uri(file.FullName, UriKind.Absolute)));
                    }
                }
            }
            catch { }
        }

        void SelectImage(Image sender)
        {
            Border border = sender.Tag as Border;
            DoubleAnimation scale = new DoubleAnimation(1, 1.5, TimeSpan.FromMilliseconds(100));

            border.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scale);
            border.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scale);
        }
        void DeSelectImage(Image sender)
        {
            Border border = sender.Tag as Border;
            DoubleAnimation scale = new DoubleAnimation(1.5, 1, TimeSpan.FromMilliseconds(100));

            border.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scale);
            border.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scale);
        }
        void GoToPos(Point point)
        {
            if (selectedImage != null)
            {
                canMouseDown = false;
                double time = GetTime(point);

                DoubleAnimation dx = new DoubleAnimation(point.X, TimeSpan.FromSeconds(time));
                DoubleAnimation dy = new DoubleAnimation(point.Y, TimeSpan.FromSeconds(time));

                dx.AutoReverse = false;
                dy.AutoReverse = false;

                dx.Completed += Dx_Completed;

                Border border = selectedImage.Tag as Border;

                border.BeginAnimation(Canvas.LeftProperty, dx);
                border.BeginAnimation(Canvas.TopProperty, dy);
            }
        }
        double GetTime(Point point)
        {
            double result = 1;

            if(selectedImage != null)
            {
                double max = Math.Max(Math.Abs(point.X - Canvas.GetLeft(selectedImage.Tag as Border)), 
                                      Math.Abs(point.Y - Canvas.GetTop(selectedImage.Tag  as Border)));
                result = max / 500;
            }

            return result;
        }

        private void Dx_Completed(object sender, EventArgs e)
        {
            DeSelectImage(selectedImage);
            selectedImage = null;
            canMouseDown = true;
        }

        private void MenuItemBackground_Click(object sender, RoutedEventArgs e)
        {
            if(colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Color color = new Color();
                color.A = colorDialog.Color.A;
                color.R = colorDialog.Color.R;
                color.G = colorDialog.Color.G;
                color.B = colorDialog.Color.B;

                ColorAnimation colorAnimation = new ColorAnimation(color, TimeSpan.FromSeconds(1));
                canvasBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            }
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (canMouseDown)
            {
                Point point = e.GetPosition(canvas);

                if (selectedImage != null)
                {
                    GoToPos(point);
                }
                else
                {
                    Border border = new Border();
                    border.Background = Brushes.White;
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(3);
                    border.Width = 70;
                    border.Height = 70;
                    border.MouseDown += Border_MouseDown; ;

                    border.RenderTransform = new ScaleTransform(1, 1, 0.5, 0.5);
                    border.RenderTransformOrigin = new Point(0.5, 0.5);

                    Image image = new Image();

                    image.Source = imageSources.Count > 0 ?
                        imageSources[rand.Next(imageSources.Count)] : new BitmapImage();

                    image.Stretch = Stretch.Uniform;
                    image.MouseDown += Image_MouseDown;
                    image.Tag = border;

                    border.Child = image;

                    Canvas.SetLeft(border, point.X);
                    Canvas.SetTop(border, point.Y);

                    canvas.Children.Add(border);
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image_MouseDown((sender as Border).Child, e);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (canMouseDown)
            {
                if (selectedImage == null)
                {
                    SelectImage(sender as Image);
                    selectedImage = sender as Image;
                }
                else
                {
                    DeSelectImage(selectedImage);

                    if (selectedImage == sender as Image)
                    {
                        selectedImage = null;
                    }
                    else
                    {
                        SelectImage(sender as Image);
                        selectedImage = sender as Image;
                    }
                }

                e.Handled = true;
            }
        }
    }
}
