using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Forms = System.Windows.Forms;
using System.Threading;

namespace Main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Forms.NotifyIcon notify;
        ControlProcess myWindow = new ControlProcess();

        public MainWindow()
        {
            InitializeComponent();

            notify = new Forms.NotifyIcon();
            notify.Icon = new System.Drawing.Icon(@"Resources\icon.ico");
            notify.Text = "Thread HomeWork02";
            notify.Click += Notify_Click;
            notify.ContextMenuStrip = new Forms.ContextMenuStrip();
            notify.ContextMenuStrip.Items.Add("Window State", System.Drawing.Image.FromFile(@"Resources\icon.ico"), OnStatusClick);
            notify.ContextMenuStrip.Items.Add("Exit", System.Drawing.Image.FromFile(@"Resources\exit.ico"), OnExitClick);
            notify.BalloonTipClicked += Notify_BalloonTipClicked;
            notify.Visible = true;

            this.Hide();
        }

        private void Notify_BalloonTipClicked(object sender, EventArgs e)
        {
            myWindow.Show();
        }

        private void Notify_Click(object sender, EventArgs e)
        {
            if (((Forms.MouseEventArgs)e).Button == Forms.MouseButtons.Left)
            {
                if (myWindow.WindowState == WindowState.Minimized)
                {
                    myWindow.WindowState = WindowState.Normal;
                    myWindow.Activate();
                }
                else
                {
                    notify.ShowBalloonTip(3000, "INFO", "Window is closed.\nClick to start...", Forms.ToolTipIcon.Info);
                }
            }
        }

        void OnStatusClick(object sender, EventArgs e)
        {
            if (myWindow.IsVisible) MessageBox.Show("Window is loaded");
            else MessageBox.Show("Window is disabled");
        }
        void OnExitClick(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            myWindow.timer.Stop();
            myWindow.Close();
            notify.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myWindow.Show();
        }

    }
}
