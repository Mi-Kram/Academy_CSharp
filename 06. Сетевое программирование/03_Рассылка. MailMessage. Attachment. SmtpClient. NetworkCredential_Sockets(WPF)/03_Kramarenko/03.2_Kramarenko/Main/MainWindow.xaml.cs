using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddLetterDialog dialog = new AddLetterDialog();
            if(dialog.ShowDialog() == true)
            {
                letters.Items.Add(new CheckBox() { Content = dialog.Header, Tag = dialog.Body });
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            attachments.Items.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            if(dialog.ShowDialog() == true)
            {
                foreach (string file in dialog.FileNames)
                {
                    attachments.Items.Add(new TextBlock() { Text = System.IO.Path.GetFileName(file), Tag = file });
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddDestanator dialog = new AddDestanator();
            if(dialog.ShowDialog() == true)
            {
                destinators.Items.Add(new CheckBox() { Content = dialog.GMAIL });
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SenderChangeDialog dialog = new SenderChangeDialog();
            if(dialog.ShowDialog() == true)
            {
                Sender.Text = dialog.Sender;
                Sender.Tag = dialog.Password;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                // настройка входа на сервер
                client.Credentials = new NetworkCredential(Sender.Text, Sender.Tag.ToString());
                client.EnableSsl = true;

                string[] dests = GetDestinators();
                string msg = GetMessage();
                Attachment[] atchs = GetAttachments();

                client.Send(CreateMailMessage(dests, msg, atchs));
                MessageBox.Show("Почта успешно отправлена!");
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка такая: " + exp.Message);
            }
            finally
            {
                Clear();
            }
        }

        void Clear()
        {
            Sender.Text = "gmail@gmail.com";
            Sender.Tag = "";

            foreach (CheckBox checkBox in destinators.Items)
            {
                checkBox.IsChecked = false;
            }
            attachments.Items.Clear();
            foreach (CheckBox checkBox in letters.Items)
            {
                checkBox.IsChecked = false;
            }
        }

        private Attachment[] GetAttachments()
        {
            List<Attachment> lst = new List<Attachment>();

            foreach (TextBlock textBlock in attachments.Items)
            {
                lst.Add(new Attachment(textBlock.Tag.ToString()));
            }

            return lst.ToArray();
        }

        private string GetMessage()
        {
            string msg = string.Empty;

            foreach (CheckBox checkBox in letters.Items)
            {
                if (checkBox.IsChecked == true)
                {
                    msg += checkBox.Tag.ToString() + "\n\n";
                }
            }

            return msg.TrimEnd('\n');
        }

        private string[] GetDestinators()
        {
            List<string> lst = new List<string>();

            foreach (CheckBox checkBox in destinators.Items)
            {
                if (checkBox.IsChecked == true) lst.Add(checkBox.Content.ToString());
            }

            return lst.ToArray();
        }

        private MailMessage CreateMailMessage(string[] TOs, string body, Attachment[] attachments)
        {
            MailMessage mailMsg = new MailMessage();

            mailMsg.From = new MailAddress(Sender.Text);
            mailMsg.Subject = "Рассылка";
            mailMsg.Body = body;

            foreach (string to in TOs)
            {
                mailMsg.To.Add(new MailAddress(to));
            }
            foreach (Attachment attachment in attachments)
            {
                mailMsg.Attachments.Add(attachment);
            }

            return mailMsg;
        }

    }
}
