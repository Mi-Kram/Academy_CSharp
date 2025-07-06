using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;

namespace Sounds_test
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = "tada.wav";
            try
            {
                player.Load();
                //player.Play();
                player.PlayLooping();
            }
            catch (System.IO.FileNotFoundException err)
            {
            }
            catch (FormatException err)
            {
            }
        }

        MediaPlayer mp = new MediaPlayer();

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            mp.Open(new Uri("runaway.mp3", UriKind.Relative));
            mp.Volume = 1;
            mp.Balance = 0;
            mp.Position = new TimeSpan(0, 0, 0);
            mp.SpeedRatio = 1;
            mp.Play();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Play();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            mediaElement2.Source = new Uri("admin.gif", UriKind.RelativeOrAbsolute);
            mediaElement2.Play();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            mediaElement1.Close();
            mediaElement2.Close();
        }
    }
}
