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

namespace WPF_User_Control
{
    public delegate void OnBarMouseDownDelegate(object sender, int BarIndex);

    /// <summary>
    /// Interaction logic for BarDiagram.xaml
    /// </summary>
    public partial class BarDiagram : UserControl
    {
        // значения столбиков диаграм
        public List<double> Values { get; set; } = new List<double>();

        // сообщение о нажатии мышью по одному из столбиков диаграммы
        public event OnBarMouseDownDelegate OnBarMouseDown;

        public BarDiagram()
        {
            InitializeComponent();

            // обработка изменения размера элемента управления
            this.SizeChanged += BarDiagram_SizeChanged;
        }

        private void BarDiagram_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // если есть значения столбиков
            for (int i = 0; i < Values?.Count; i++)
            {
                // перебрать все значения
                if(mainGrid.Children[i] is TextBlock)
                {
                    // получить текущий столбик
                    TextBlock currentBar = (TextBlock)mainGrid.Children[i];

                    // вычисление длины столбца
                    double barWidth = mainGrid.ActualWidth / (Values.Count * 2);

                    // вычисление высоты столбца
                    double barHeight = Values[i] / Values.Max() * mainGrid.ActualHeight * 0.9;

                    currentBar.Width = barWidth;

                    // отключение анимации HeightProperty перед изменением размера
                    currentBar.BeginAnimation(HeightProperty, null);

                    // изменить высоту столбика в соответствии с размером диаграммы
                    currentBar.Height = barHeight;

                    currentBar.HorizontalAlignment = HorizontalAlignment.Left;
                    currentBar.VerticalAlignment = VerticalAlignment.Bottom;

                    currentBar.Margin = new Thickness(2 * i * barWidth + barWidth / 2, 0, 0, -1);
                }
            }
        }

        Random rand = new Random();

        // обновление блоков диаграммы
        public void RefreshData()
        {
            // очистить старые блоки диаграммы
            mainGrid.Children.Clear();

            // цикл по новым значениям диаграммы
            for (int i = 0; i < Values?.Count; i++)
            {
                TextBlock currentBar = new TextBlock();

                currentBar.Text = $"\n{Values[i]}";
                currentBar.TextAlignment = TextAlignment.Center;
                //currentBar.Stroke = Brushes.Black;

                // покрасить блок в случайный цвет
                currentBar.Background = new SolidColorBrush(Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)));

                currentBar.HorizontalAlignment = HorizontalAlignment.Left;
                currentBar.VerticalAlignment = VerticalAlignment.Center;

                // вычисление размеров блока
                double barWidth = mainGrid.ActualWidth / (Values.Count * 2);
                double barHeight = Values[i] / Values.Max() * mainGrid.ActualHeight * 0.9;

                currentBar.Width = barWidth;
                currentBar.Height = 10;

                // записать в свойство Tag номер прямоугольника
                currentBar.Tag = i;

                // метод, реагирующий на нажатие мышью по столбцу
                currentBar.MouseDown += CurrentBar_MouseDown;

                currentBar.HorizontalAlignment = HorizontalAlignment.Left;
                currentBar.VerticalAlignment = VerticalAlignment.Bottom;

                currentBar.Margin = new Thickness(2 * i * barWidth + barWidth / 2, 0, 0, 0);

                Grid.SetColumn(currentBar, 0);
                Grid.SetRow(currentBar, 0);

                mainGrid.Children.Add(currentBar);

                // анимированное появление блоков диаграммы
                DoubleAnimation heightAnimation = new DoubleAnimation();
                heightAnimation.To = barHeight;
                heightAnimation.EasingFunction = new CubicEase();
                heightAnimation.Duration = TimeSpan.FromSeconds(1);

                currentBar.BeginAnimation(HeightProperty, heightAnimation);
            }
        }

        private void CurrentBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OnBarMouseDown?.Invoke(this, (int)((TextBlock)sender).Tag);
        }
    }
}
