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
    /// <summary>
    /// Interaction logic for MusicPlayer.xaml
    /// </summary>
    public partial class MusicPlayer : UserControl
    {
        public MusicPlayer()
        {
            InitializeComponent();
        }

        private bool suppressSeek;
        private void sliderPosition_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (!suppressSeek)
                MediaStoryboard.Storyboard.Seek(st1, TimeSpan.FromSeconds(sliderPosition.Value), TimeSeekOrigin.BeginTime);
        }
        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
            sliderPosition.Maximum = media.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void storyboard_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            Clock storyboardClock = (Clock)sender;

            if (storyboardClock.CurrentProgress == null)
            {
                lblTime.Text = "";
            }
            else
            {
                lblTime.Text = storyboardClock.CurrentTime.ToString();
                suppressSeek = true;
                sliderPosition.Value = storyboardClock.CurrentTime.Value.TotalSeconds;
                suppressSeek = false;
            }
        }
    }
}
