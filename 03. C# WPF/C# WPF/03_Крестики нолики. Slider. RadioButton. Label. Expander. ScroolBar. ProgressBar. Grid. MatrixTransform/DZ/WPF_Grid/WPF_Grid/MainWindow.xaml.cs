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

namespace WPF_Grid
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
            // очистка всех дочерних элементов
            myGrid.Children.Clear();
            
            // очистка столбцов и строк
            myGrid.ColumnDefinitions.Clear();
            myGrid.RowDefinitions.Clear();

            // показать линии сетки
            myGrid.ShowGridLines = true;

            // создание 2 столбцов
            ColumnDefinition colDef1 = new ColumnDefinition();
            ColumnDefinition colDef2 = new ColumnDefinition();

            // Задать размер столбца в 100 пикселей и задать возможность увеличения размера
            //colDef1.Width = new GridLength(100, GridUnitType.Star);

            // Задать размер столбца в 100 пикселей без возможности увеличения с окном
            //colDef2.Width = new GridLength(100);

            // добавление 2 столбцов
            myGrid.ColumnDefinitions.Add(colDef1);
            myGrid.ColumnDefinitions.Add(colDef2);

            RowDefinition rowDef1 = new RowDefinition();

            // Задать размер строки в 100 пикселей без возможности увеличения с окно
            //rowDef1.Height = new GridLength(70);

            RowDefinition rowDef2 = new RowDefinition();

            // добавление 2 строк
            myGrid.RowDefinitions.Add(rowDef1);
            myGrid.RowDefinitions.Add(rowDef2);

            // Создание кнопки внутри ячейки
            Button btn1 = new Button();
            btn1.Content = "Hello";

            Thickness t = new Thickness(10, 10, 10, 10);
            //t.Left = 20;
            //t.Top = 50;
            btn1.Margin = t;

            // Указание ячейки для кнопки
            Grid.SetColumn(btn1, 1);
            Grid.SetRow(btn1, 1);

            // добавление кнопки на сетку
            myGrid.Children.Add(btn1);
        }

        Random rand = new Random();

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // создание кисти со случайным цветом
            SolidColorBrush colorBrush = new SolidColorBrush(Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)));

            // получить элемент управления, по которому пришёлся щелчок
            var element = e.Source;

            // щелчок по цветному прямоугольнику внутри ячейки
            if(element is Rectangle)
            {
                // для контрола получить координаты ячейки в сетке
                /*int elementColumnNumber = Grid.GetColumn((UIElement)element);
                int elementRowNumber = Grid.GetRow((UIElement)element);

                MessageBox.Show($"Column: {elementColumnNumber}, Row: {elementRowNumber}");*/

                ((Rectangle)element).Fill = colorBrush;
                return;
            }

            if(element is Grid)
            {
                int columnNumber = 0;
                int rowNumber = 0;

                // получить координаты мыши в Grid
                Point mousePos = e.GetPosition(myGrid);

                //MessageBox.Show($"Coords: {mousePos}");

                // вычисление номера столбца, по которому пришёлся щелчок
                double width = 0;
                for (int i = 0; i < myGrid.ColumnDefinitions.Count; i++)
                {
                    // прибавсть к общему размеру размер текущего столбца
                    width += myGrid.ColumnDefinitions[i].ActualWidth;
                    if (width > mousePos.X)
                    {
                        columnNumber = i;
                        break;
                    }
                }

                // вычисление номера строки, по которому пришёлся щелчок
                double height = 0;
                for (int i = 0; i < myGrid.RowDefinitions.Count; i++)
                {
                    height += myGrid.RowDefinitions[i].ActualHeight;
                    if (height > mousePos.Y)
                    {
                        rowNumber = i;
                        break;
                    }
                }

                // показать координаты ячейки, по которой пришёлся щелчок
                //MessageBox.Show($"Column: {columnNumber}, Row: {rowNumber}");

                // создание прямоугольника в выбранной ячейке
                Rectangle rect = new Rectangle();
                rect.Fill = colorBrush;

                rect.Margin = new Thickness(0, 0, 0, 0);

                // поместить новый прямоугольник в определённую ячейку сетки
                Grid.SetColumn(rect, columnNumber);
                Grid.SetRow(rect, rowNumber);

                // добавление прямоугольника в ячейку
                myGrid.Children.Add(rect);

                // задание Z-index для прямоугольника
                Panel.SetZIndex(rect, 3);

                return;
            }
        }
    }
}
