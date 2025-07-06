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
using System.Drawing;
using System.IO;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum End
        {
            NONE,
            WIN,
            LOSE,
            DARW
        }

        enum Gamemode
        {
            x3 = 3,
            x4 = 4,
            x5 = 5
        }
        Gamemode gamemode;

        enum CrossAndCircle
        {
            NONE,
            cross,
            circle
        }
        CrossAndCircle[,] matrix;

        BitmapImage cross;
        BitmapImage circle;
        BitmapImage empty;


        public MainWindow()
        {
            InitializeComponent();

            gamemode = Gamemode.x3;
            cross = new BitmapImage(new Uri(@"C:\Users\setup\Desktop\04_Kramarenko\Main\Resources\cross.png", UriKind.Absolute));
            circle = new BitmapImage(new Uri(@"C:\Users\setup\Desktop\04_Kramarenko\Main\Resources\circle.png", UriKind.Absolute));
            empty = new BitmapImage();

            CreateBoard();
        }

        void BotStep()
        {
            int cnt = 0;
            int max = (int)gamemode;

            // сдалать max кол-во кружков
            for (int y = 0; y < max; y++)
            {
                cnt = 0;
                for (int x = 0; x < max; x++)
                {
                    if (matrix[y, x] == CrossAndCircle.circle)
                        cnt++;
                }
                if (cnt == max-1)
                {
                    for (int x = 0; x < max; x++)
                    {
                        if (matrix[y, x] == CrossAndCircle.NONE)
                        {
                            int index = (y * max) + x;

                            (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
                            matrix[y, x] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }
            for (int x = 0; x < max; x++)
            {
                cnt = 0;
                for (int y = 0; y < max; y++)
                {
                    if (matrix[y, x] == CrossAndCircle.circle)
                        cnt++;
                }
                if (cnt == max-1)
                {
                    for (int y = 0; y < max; y++)
                    {
                        if (matrix[y, x] == CrossAndCircle.NONE)
                        {
                            int index = (y * max) + x;

                            (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
                            matrix[y, x] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }

            cnt = 0;
            for (int i = 0; i < max; i++)
            {
                if (matrix[i, i] == CrossAndCircle.circle)
                    cnt++;
            }
            if (cnt == max-1)
            {
                for (int i = 0; i < max; i++)
                {
                    if (matrix[i, i] == CrossAndCircle.NONE)
                    {
                        int index = (i * max) + i;

                        (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
                        matrix[i, i] = CrossAndCircle.circle;
                        return;
                    }
                }
            }

            cnt = 0;
            for (int i = 0; i < max; i++)
            {
                if (matrix[i, (max-1) - i] == CrossAndCircle.circle)
                    cnt++;
            }
            if (cnt == max-1)
            {
                for (int i = 0; i < max; i++)
                {
                    if (matrix[i, (max-1) - i] == CrossAndCircle.NONE)
                    {
                        int index = (i * max) + ((max-1) - i);

                        (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
                        matrix[i, (max-1) - i] = CrossAndCircle.circle;
                        return;
                    }
                }
            }


            // не дать сопернику сделать max крестов
            for (int y = 0; y < max; y++)
            {
                cnt = 0;
                for (int x = 0; x < max; x++)
                {
                    if (matrix[y, x] == CrossAndCircle.cross)
                        cnt++;
                }
                if (cnt == max-1)
                {
                    for (int x = 0; x < max; x++)
                    {
                        if (matrix[y, x] == CrossAndCircle.NONE)
                        {
                            int index = (y * max) + x;

                            (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
                            matrix[y, x] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }
            for (int x = 0; x < max; x++)
            {
                cnt = 0;
                for (int y = 0; y < max; y++)
                {
                    if (matrix[y, x] == CrossAndCircle.cross)
                        cnt++;
                }
                if (cnt == max-1)
                {
                    for (int y = 0; y < max; y++)
                    {
                        if (matrix[y, x] == CrossAndCircle.NONE)
                        {
                            int index = (y * max) + x;

                            (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
                            matrix[y, x] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }

            cnt = 0;
            for (int i = 0; i < max; i++)
            {
                if (matrix[i, i] == CrossAndCircle.cross)
                    cnt++;
            }
            if (cnt == max-1)
            {
                for (int i = 0; i < max; i++)
                {
                    if (matrix[i, i] == CrossAndCircle.NONE)
                    {
                        int index = (i * max) + i;

                        (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
                        matrix[i, i] = CrossAndCircle.circle;
                        return;
                    }
                }
            }

            cnt = 0;


            for (int i = 0; i < max; i++)
            {
                if (matrix[i, (max-1) - i] == CrossAndCircle.cross)
                    cnt++;
            }
            if (cnt == max-1)
            {
                for (int i = 0; i < max; i++)
                {
                    if (matrix[i, (max-1) - i] == CrossAndCircle.NONE)
                    {
                        int index = (i * max) + ((max-1) - i);

                        (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
                        matrix[i, (max-1) - i] = CrossAndCircle.circle;
                        return;
                    }
                }
            }


            #region s

            //// сдалать 2 кружка
            //for (int y = 0; y < max; y++)
            //{
            //    for (int x = 0; x < max; x++)
            //    {
            //        if (matrix[y, x] == CrossAndCircle.circle)
            //        {
            //            Point[] points = GetNear(new Point(x, y), CrossAndCircle.NONE);
            //            if (points.Length != 0)
            //            {
            //                int X = (int)points[0].X;
            //                int Y = (int)points[0].Y;

            //                int index = (Y * max) + X;

            //                (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
            //                matrix[Y, X] = CrossAndCircle.circle;
            //                return;
            //            }
            //        }
            //    }
            //}

            //for (int x = 0; x < max; x++)
            //{
            //    for (int y = 0; y < max; y++)
            //    {
            //        if (matrix[x, y] == CrossAndCircle.circle)
            //        {
            //            Point[] points = GetNear(new Point(y, x), CrossAndCircle.NONE);
            //            if (points.Length != 0)
            //            {
            //                int X = (int)points[0].X;
            //                int Y = (int)points[0].Y;

            //                int index = (X * max) + Y;

            //                (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
            //                matrix[X, Y] = CrossAndCircle.circle;
            //                return;
            //            }
            //        }
            //    }
            //}

            //for (int i = 0; i < max; i++)
            //{
            //    if (matrix[i, i] == CrossAndCircle.circle)
            //    {
            //        Point[] points = GetNear(new Point(i, i), CrossAndCircle.NONE);
            //        if (points.Length != 0)
            //        {
            //            int X = (int)points[0].X;
            //            int Y = (int)points[0].Y;

            //            int index = (X * max) + Y;

            //            (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
            //            matrix[Y, X] = CrossAndCircle.circle;
            //            return;
            //        }
            //    }
            //}

            //for (int i = 0; i < max; i++)
            //{
            //    if (matrix[i, 2 - i] == CrossAndCircle.circle)
            //    {
            //        Point[] points = GetNear(new Point(i, 2 - i), CrossAndCircle.NONE);
            //        if (points.Length != 0)
            //        {
            //            int X = (int)points[0].X;
            //            int Y = (int)points[0].Y;

            //            int index = (X * max) + Y;

            //            (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
            //            matrix[Y, X] = CrossAndCircle.circle;
            //            return;
            //        }
            //    }
            //}
            #endregion

            Random rand = new Random();
            while (true)
            {
                int y = rand.Next(max);
                int x = rand.Next(max);

                if (matrix[y, x] == CrossAndCircle.NONE)
                {
                    int index = (y * max) + x;

                    (playBoard.Children[index] as System.Windows.Controls.Image).Source = circle;
                    matrix[y, x] = CrossAndCircle.circle;

                    break;
                }
            }
        }


        void CreateBoard()
        {
            playBoard.Children.Clear();
            playBoard.ColumnDefinitions.Clear();
            playBoard.RowDefinitions.Clear();

            int n = (int)gamemode;

            for (int i = 0; i < n; i++)
            {
                playBoard.ColumnDefinitions.Add(new ColumnDefinition());
                playBoard.RowDefinitions.Add(new RowDefinition());
            }

            matrix = new CrossAndCircle[n, n];

            Random rand = new Random();

            SolidColorBrush colorBrush = new SolidColorBrush(
    System.Windows.Media.Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)));

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                    image.Margin = new Thickness(5,5,5,5);
                    image.Source = empty;
                    image.Stretch = Stretch.Uniform;

                    Grid.SetColumn(image, j);
                    Grid.SetRow(image, i);

                    matrix[i, j] = CrossAndCircle.NONE;
                    playBoard.Children.Add(image);
                }
            }
        }

        private void GM_3x3_Click(object sender, RoutedEventArgs e)
        {
            if(gamemode != Gamemode.x3)
            {
                switch (gamemode)
                {
                    case Gamemode.x4:
                        {
                            boardX4.IsChecked = false;
                            break;
                        }
                    case Gamemode.x5:
                        {
                            boardX5.IsChecked = false;
                            break;
                        }
                    default: break;
                }
                gamemode = Gamemode.x3;
                boardX3.IsChecked = true;
            }

            CreateBoard();
        }
        private void GM_4x4_Click(object sender, RoutedEventArgs e)
        {
            if (gamemode != Gamemode.x4)
            {
                switch (gamemode)
                {
                    case Gamemode.x3:
                        {
                            boardX3.IsChecked = false;
                            break;
                        }
                    case Gamemode.x5:
                        {
                            boardX5.IsChecked = false;
                            break;
                        }
                    default: break;
                }
                gamemode = Gamemode.x4;
                boardX4.IsChecked = true;
            }

            CreateBoard();
        }
        private void GM_5x5_Click(object sender, RoutedEventArgs e)
        {
            if (gamemode != Gamemode.x5)
            {
                switch (gamemode)
                {
                    case Gamemode.x3:
                        {
                            boardX3.IsChecked = false;
                            break;
                        }
                    case Gamemode.x4:
                        {
                            boardX4.IsChecked = false;
                            break;
                        }
                    default: break;
                }
                gamemode = Gamemode.x5;
                boardX5.IsChecked = true;
            }

            CreateBoard();
        }

        private void BackGR_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.ColorDialog colorDialog = new System.Windows.Forms.ColorDialog();

            colorDialog.FullOpen = true;
            colorDialog.AllowFullOpen = true;

            if(colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Media.Color color = new System.Windows.Media.Color();
                color.A = colorDialog.Color.A;
                color.R = colorDialog.Color.R;
                color.G = colorDialog.Color.G;
                color.B = colorDialog.Color.B;

                playBoard.Background = new SolidColorBrush(color);
            }
        }
        private void BackIM_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.FileName = "";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "JPG JPEG PNG BMP|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog.Title = "Background picture";

            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));

                playBoard.Background = imgBrush;
            }
            openFileDialog.Dispose();
        }

        private void playBoard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int columnNumber = 0;
            int rowNumber = 0;
            int n = (int)gamemode;
            System.Windows.Point mousePos = e.GetPosition(playBoard);

            double width = 0;
            for (int i = 0; i < playBoard.ColumnDefinitions.Count; i++)
            {
                width += playBoard.ColumnDefinitions[i].ActualWidth;
                if (width > mousePos.X)
                {
                    columnNumber = i;
                    break;
                }
            }

            double height = 0;
            for (int i = 0; i < playBoard.RowDefinitions.Count; i++)
            {
                height += playBoard.RowDefinitions[i].ActualHeight;
                if (height > mousePos.Y)
                {
                    rowNumber = i;
                    break;
                }
            }

            Console.WriteLine($"{rowNumber} : {columnNumber}");

            if (matrix[rowNumber, columnNumber] == CrossAndCircle.NONE)
            {
                int index = (rowNumber * n) + columnNumber;

                (playBoard.Children[index] as System.Windows.Controls.Image).Source = cross;
                matrix[rowNumber, columnNumber] = CrossAndCircle.cross;

                End result = IsEnd();
                if (result == End.WIN)
                {
                    GameOver("YOU WIN!");
                    return;
                }
                else if (result == End.DARW)
                {
                    GameOver("Nobody won!");
                    return;
                }

                BotStep();

                result = IsEnd();
                if (result == End.LOSE)
                {
                    GameOver("YOU LOSE!");
                    return;
                }
                else if (result == End.DARW)
                {
                    GameOver("Nobody won!");
                    return;
                }
            }
        }

        End IsEnd()
        {
            bool win = true;
            int n = (int)gamemode;

            for (int i = 0; i < n; i++)
            {
                if (matrix[i, 0] == CrossAndCircle.NONE) continue;

                win = true;

                for (int j = 1; j < n; j++)
                {
                    if (matrix[i, j - 1] != matrix[i, j] || matrix[i, j] == CrossAndCircle.NONE)
                    {
                        win = false;
                        break;
                    }
                }

                if (win) return matrix[i, 0] == CrossAndCircle.circle ? End.LOSE : End.WIN;
            }

            for (int i = 0; i < n; i++)
            {
                if (matrix[0, i] == CrossAndCircle.NONE) continue;

                win = true;

                for (int j = 1; j < n; j++)
                {
                    if (matrix[j - 1, i] != matrix[j, i] || matrix[j, i] == CrossAndCircle.NONE)
                    {
                        win = false;
                        break;
                    }
                }

                if (win) return matrix[0, i] == CrossAndCircle.circle ? End.LOSE : End.WIN;
            }

            win = true;

            for (int i = 1; i < n; i++)
            {
                if (matrix[0, 0] == CrossAndCircle.NONE) { win = false; break; }
                if (matrix[i - 1, i - 1] != matrix[i, i] || matrix[i, i] == CrossAndCircle.NONE)
                {
                    win = false;
                    break;
                }
            }

            if (win) return matrix[0, 0] == CrossAndCircle.circle ? End.LOSE : End.WIN;

            win = true;

            for (int i = 0, j = n-1; i < n - 1; i++, j--)
            {
                if (matrix[0, n-1] == CrossAndCircle.NONE) { win = false; break; }
                if (matrix[i, j] != matrix[i + 1, j - 1] || matrix[i, j] == CrossAndCircle.NONE)
                {
                    win = false;
                    break;
                }
            }

            if (win) return matrix[0, n-1] == CrossAndCircle.circle ? End.LOSE : End.WIN;


            foreach (CrossAndCircle item in matrix)
            {
                if (item == CrossAndCircle.NONE) return End.NONE;
            }

            return End.DARW;
        }
        void GameOver(string message, string caption = "INFO")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
            CreateBoard();
        }

        private void MenuItemCross_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.FileName = "";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "JPG JPEG PNG BMP|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog.Title = "Cross picture";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cross = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                Repaint(CrossAndCircle.cross, cross);
            }
            openFileDialog.Dispose();
        }
        private void MenuItemCircle_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.FileName = "";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "JPG JPEG PNG BMP|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog.Title = "Circle picture";

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                circle = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
                Repaint(CrossAndCircle.circle, circle);
            }
            openFileDialog.Dispose();
        }

        private void Repaint(CrossAndCircle img, BitmapImage newImage)
        {
            int n = (int)gamemode;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if(matrix[i,j] == img)
                    {
                        int index = (i * n) + j;

                        (playBoard.Children[index] as System.Windows.Controls.Image).Source = newImage;
                    }
                }
            }
        }
    }
}
