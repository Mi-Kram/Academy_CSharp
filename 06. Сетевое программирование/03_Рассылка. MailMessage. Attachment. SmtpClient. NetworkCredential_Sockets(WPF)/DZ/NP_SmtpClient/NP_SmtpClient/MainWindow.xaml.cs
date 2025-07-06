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

namespace NP_SmtpClient
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

        Attachment mailAttachment = new Attachment(@"admin.gif");

        private MailMessage CreateMailMessage()
        {
            MailMessage mailMsg = new MailMessage();

            // заполнить поля почтового сообщения из полей окна
            mailMsg.From = new MailAddress(txtFrom.Text);
            mailMsg.To.Add(new MailAddress(txtTo.Text));
            mailMsg.IsBodyHtml = false;
            mailMsg.Subject = txtSubject.Text;
            mailMsg.Body = txtBody.Text;

            if (mailAttachment != null)
                mailMsg.Attachments.Add(mailAttachment);

            return mailMsg;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // отправляем почту с учётной записи на сервере Google
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                // настройка входа на сервер
                client.Credentials = new NetworkCredential(txtAccount.Text, txtPassword.Password);
                client.EnableSsl = true;

                client.Send(CreateMailMessage());
                MessageBox.Show("Почта успешно отправлена!");
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка такая: " + exp.Message);
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                // отправляем почту с учётной записи на сервере Google
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                // настройка входа на сервер
                client.Credentials = new NetworkCredential(txtAccount.Text, txtPassword.Password);
                client.EnableSsl = true;

                client.SendCompleted += Client_SendCompleted;

                await client.SendMailAsync(CreateMailMessage());
            }
            catch (Exception exp)
            {
                MessageBox.Show("Ошибка такая: " + exp.Message);
            }
        }

        private void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Почта успешно отправлена асинхронно!");
        }
    }
}
