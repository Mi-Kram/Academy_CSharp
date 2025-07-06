using Main.Properties;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        BitmapImage cross;
        BitmapImage circle;
        BitmapImage empty;

        enum End
        {
            NONE,
            WIN,
            LOSE,
            DARW
        }
        enum CrossAndCircle
        {
            NONE,
            cross,
            circle
        }
        CrossAndCircle[,] matrix;

        public MainWindow()
        {
            InitializeComponent();

            cross = GetBitmapImage(new Uri($"{Environment.CurrentDirectory}\\..\\..\\Resources\\cross.png", UriKind.Absolute));
            circle = GetBitmapImage(new Uri($"{Environment.CurrentDirectory}\\..\\..\\Resources\\circle.png", UriKind.Absolute));
            empty = new BitmapImage();

            matrix = new CrossAndCircle[3, 3];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = CrossAndCircle.NONE;
                }
            }

            // первый ход со стороны бота
            //BotStep();
        }

        BitmapImage GetBitmapImage(Uri uri)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = uri;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        void BotStep()
        {
            int cnt = 0;
            
            // сдалать 3 кружка
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                cnt = 0;
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    if (matrix[y, x] == CrossAndCircle.circle) 
                        cnt++;
                }
                if(cnt == 2)
                {
                    for (int x = 0; x < matrix.GetLength(1); x++)
                    {
                        if(matrix[y,x] == CrossAndCircle.NONE)
                        {
                            int index = (y * 3) + x;

                            (grid.Children[index] as Image).Source = circle;
                            matrix[y, x] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }

            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                cnt = 0;
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    if (matrix[y,x] == CrossAndCircle.circle) 
                        cnt++;
                }
                if(cnt == 2)
                {
                    for (int y = 0; y < matrix.GetLength(1); y++)
                    {
                        if(matrix[y,x] == CrossAndCircle.NONE)
                        {
                            int index = (y * 3) + x;

                            (grid.Children[index] as Image).Source = circle;
                            matrix[y,x] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }

            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                cnt = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, i] == CrossAndCircle.circle)
                        cnt++;
                }
                if(cnt == 2)
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        if (matrix[i,i] == CrossAndCircle.NONE)
                        {
                            int index = (i * 3) + i;

                            (grid.Children[index] as Image).Source = circle;
                            matrix[i, i] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }

                cnt = 0;


                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, 2 - i] == CrossAndCircle.circle)
                        cnt++;
                }
                if(cnt == 2)
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        if (matrix[i, 2 - i] == CrossAndCircle.NONE)
                        {
                            int index = (i * 3) + (2-i);

                            (grid.Children[index] as Image).Source = circle;
                            matrix[i, 2 - i] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }


            // не дать сопернику сделать 3 креста
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                cnt = 0;
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    if (matrix[y, x] == CrossAndCircle.cross)
                        cnt++;
                }
                if (cnt == 2)
                {
                    for (int x = 0; x < matrix.GetLength(1); x++)
                    {
                        if (matrix[y, x] == CrossAndCircle.NONE)
                        {
                            int index = (y * 3) + x;

                            (grid.Children[index] as Image).Source = circle;
                            matrix[y, x] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }

            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                cnt = 0;
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    if (matrix[y,x] == CrossAndCircle.cross)
                        cnt++;
                }
                if (cnt == 2)
                {
                    for (int y = 0; y < matrix.GetLength(1); y++)
                    {
                        if (matrix[y, x] == CrossAndCircle.NONE)
                        {
                            int index = (y * 3) + x;

                            (grid.Children[index] as Image).Source = circle;
                            matrix[y, x] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }

            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                cnt = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, i] == CrossAndCircle.cross)
                        cnt++;
                }
                if (cnt == 2)
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        if (matrix[i, i] == CrossAndCircle.NONE)
                        {
                            int index = (i * 3) + i;

                            (grid.Children[index] as Image).Source = circle;
                            matrix[i, i] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }

                cnt = 0;


                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, 2 - i] == CrossAndCircle.cross)
                        cnt++;
                }
                if (cnt == 2)
                {
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        if (matrix[i, 2 - i] == CrossAndCircle.NONE)
                        {
                            int index = (i * 3) + (2 - i);

                            (grid.Children[index] as Image).Source = circle;
                            matrix[i, 2 - i] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }


            // сдалать 2 кружка
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    if (matrix[y, x] == CrossAndCircle.circle)
                    {
                        Point[] points = GetNear(new Point(x, y), CrossAndCircle.NONE);
                        if(points.Length != 0)
                        {
                            int X = (int)points[0].X;
                            int Y = (int)points[0].Y;

                            int index = (Y * 3) + X;

                            (grid.Children[index] as Image).Source = circle;
                            matrix[Y, X] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }

            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    if (matrix[x,y] == CrossAndCircle.circle)
                    {
                        Point[] points = GetNear(new Point(y,x), CrossAndCircle.NONE);
                        if (points.Length != 0)
                        {
                            int X = (int)points[0].X;
                            int Y = (int)points[0].Y;

                            int index = (X * 3) + Y;

                            (grid.Children[index] as Image).Source = circle;
                            matrix[X,Y] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }

            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, i] == CrossAndCircle.circle)
                    {
                        Point[] points = GetNear(new Point(i, i), CrossAndCircle.NONE);
                        if (points.Length != 0)
                        {
                            int X = (int)points[0].X;
                            int Y = (int)points[0].Y;

                            int index = (X * 3) + Y;

                            (grid.Children[index] as Image).Source = circle;
                            matrix[Y,X] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }

                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, 2 - i] == CrossAndCircle.circle)
                    {
                        Point[] points = GetNear(new Point(i, 2 - i), CrossAndCircle.NONE);
                        if (points.Length != 0)
                        {
                            int X = (int)points[0].X;
                            int Y = (int)points[0].Y;

                            int index = (X * 3) + Y;

                            (grid.Children[index] as Image).Source = circle;
                            matrix[Y, X] = CrossAndCircle.circle;
                            return;
                        }
                    }
                }
            }

            // сдалать 1-ый кружок
            while (true)
            {
                Random rand = new Random();
                int y = rand.Next(matrix.GetLength(0));
                int x = rand.Next(matrix.GetLength(1));

                if(matrix[y,x] == CrossAndCircle.NONE)
                {
                    int index = (y * 3) + x;

                    (grid.Children[index] as Image).Source = circle;
                    matrix[y, x] = CrossAndCircle.circle;

                    break;
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int columnNumber = 0;
            int rowNumber = 0;
            System.Windows.Point mousePos = e.GetPosition(grid);

            double width = 0;
            for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
            {
                // прибавсть к общему размеру размер текущего столбца
                width += grid.ColumnDefinitions[i].ActualWidth;
                if (width > mousePos.X)
                {
                    columnNumber = i;
                    break;
                }
            }

            // вычисление номера строки, по которому пришёлся щелчок
            double height = 0;
            for (int i = 0; i < grid.RowDefinitions.Count; i++)
            {
                height += grid.RowDefinitions[i].ActualHeight;
                if (height > mousePos.Y)
                {
                    rowNumber = i-1;
                    break;
                }
            }

            if (matrix[rowNumber, columnNumber] == CrossAndCircle.NONE)
            {
                int index = (rowNumber * 3) + columnNumber;

                (grid.Children[index] as Image).Source = cross;
                matrix[rowNumber, columnNumber] = CrossAndCircle.cross;

                End result = IsEnd();
                if (result == End.WIN)
                {
                    GameOver("YOU WIN!");
                    return;
                }
                else if(result == End.DARW)
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

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i, 0] == CrossAndCircle.NONE) continue;

                win = true; 
                
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j - 1] != matrix[i, j] || matrix[i,j] == CrossAndCircle.NONE)
                    {
                        win = false; 
                        break;
                    }
                }

                if (win) return matrix[i, 0] == CrossAndCircle.circle ? End.LOSE : End.WIN;
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[0, i] == CrossAndCircle.NONE) continue;

                win = true; 
                
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (matrix[j-1, i] != matrix[j, i] || matrix[j,i] == CrossAndCircle.NONE)
                    {
                        win = false; 
                        break;
                    }
                }

                if (win) return matrix[0,i] == CrossAndCircle.circle ? End.LOSE : End.WIN;
            }

            if (matrix.GetLength(0) == matrix.GetLength(1))
            {
                win = true;

                for (int i = 1; i < matrix.GetLength(0); i++)
                {
                    if(matrix[0,0] == CrossAndCircle.NONE) { win = false; break; }
                    if(matrix[i-1,i-1] != matrix[i, i] || matrix[i, i] == CrossAndCircle.NONE)
                    {
                        win = false;
                        break;
                    }
                }

                if (win) return matrix[0, 0] == CrossAndCircle.circle ? End.LOSE : End.WIN;

                win = true;

                for (int i = 0, j = 2; i < matrix.GetLength(0)-1; i++, j--)
                {
                    if (matrix[0,2] == CrossAndCircle.NONE) { win = false; break; }
                    if (matrix[i,j] != matrix[i+1, j-1] || matrix[i, j] == CrossAndCircle.NONE)
                    {
                        win = false;
                        break;
                    }
                }

                if (win) return matrix[0,2] == CrossAndCircle.circle ? End.LOSE : End.WIN;

            }


            foreach (CrossAndCircle item in matrix)
            {
                if (item == CrossAndCircle.NONE) return End.NONE;
            }

            return End.DARW;
        }

        void GameOver(string message, string caption = "INFO")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = CrossAndCircle.NONE;
                }
            }

            for (int i = 0; i < grid.Children.Count; i++)
            {
                (grid.Children[i] as Image).Source = empty;
            }
        }

        Point[] GetNear(Point p, CrossAndCircle c)
        {
            List<Point> points = new List<Point>();

            for (int y = (int)p.Y-1; y <= p.Y+1; y++)
            {
                if (y < 0 || y >= matrix.GetLength(0)) continue;

                for (int x = (int)p.X-1; x <= p.X+1; x++)
                {
                    if (x < 0 || x >= matrix.GetLength(1)) continue;
                    if (y == p.Y && x == p.X) continue;

                    if (matrix[y, x] == c) points.Add(new Point(x, y));
                }
            }

            return points.ToArray();
        }
    }
}
