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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Simple_Animation
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

        DoubleAnimation dx;
        DoubleAnimation dy;
        DoubleAnimation dw;
        DoubleAnimation dh;

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // Создание экземпляра анимации (значение "от", значение "до", длительность анимации)
            dx = new DoubleAnimation(10, 100, TimeSpan.FromMilliseconds(900));
            dy = new DoubleAnimation(10, 100, TimeSpan.FromMilliseconds(900));
            dw = new DoubleAnimation(40, 300, TimeSpan.FromMilliseconds(900));
            dh = new DoubleAnimation(40, 300, TimeSpan.FromMilliseconds(900));

            // Добавление автореверса
            dx.AutoReverse = true;
            dy.AutoReverse = true;
            dw.AutoReverse = true;
            dh.AutoReverse = true;

            // Запустить метод по окончании анимации
            dx.Completed += Dx_Completed;

            // Флаг бесконечного повторения анимации
            // dx.RepeatBehavior = RepeatBehavior.Forever;

            // повторить 5 раз
            //dx.RepeatBehavior = new RepeatBehavior(5);

            // Запуск анимаций для нужных свойств заданных объектов
            image1.BeginAnimation(Canvas.LeftProperty, dx);
            image1.BeginAnimation(Canvas.TopProperty, dy);
            image1.BeginAnimation(Image.WidthProperty, dw);
            image1.BeginAnimation(Image.HeightProperty, dh);
        }

        // Метод запускается по окончании анимации
        private void Dx_Completed(object sender, EventArgs e)
        {
            MessageBox.Show("Animation completed!");
        }

        private void button2_MouseEnter(object sender, MouseEventArgs e)
        {
            // Анимация цвета бордюра
            ColorAnimation colorAnimation = new ColorAnimation(Colors.Red, Colors.Green, TimeSpan.FromMilliseconds(100));
            br.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            // Анимация тени
            DoubleAnimation opacityAnimation = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(100));
            shadow1.BeginAnimation(DropShadowBitmapEffect.OpacityProperty, opacityAnimation);

            // Анимация размера бордюра
            DoubleAnimation dx = new DoubleAnimation(1, 1.5, TimeSpan.FromMilliseconds(100));
            DoubleAnimation dy = new DoubleAnimation(1, 1.5, TimeSpan.FromMilliseconds(100));

            border_scale.BeginAnimation(ScaleTransform.ScaleXProperty, dx);
            border_scale.BeginAnimation(ScaleTransform.ScaleYProperty, dy);
        }

        private void button2_MouseLeave(object sender, MouseEventArgs e)
        {
            ColorAnimation colorAnimation = new ColorAnimation(Colors.Green, Colors.Red, TimeSpan.FromMilliseconds(100));
            br.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);

            DoubleAnimation opacityAnimation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(100));
            shadow1.BeginAnimation(DropShadowBitmapEffect.OpacityProperty, opacityAnimation);

            DoubleAnimation dx = new DoubleAnimation(1.5, 1, TimeSpan.FromMilliseconds(100));
            DoubleAnimation dy = new DoubleAnimation(1.5, 1, TimeSpan.FromMilliseconds(100));

            border_scale.BeginAnimation(ScaleTransform.ScaleXProperty, dx);
            border_scale.BeginAnimation(ScaleTransform.ScaleYProperty, dy);
        }
    }
}
