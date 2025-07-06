using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        string publicKey, privateKey;
        Random rand = new Random();
        string filePassword, fileSalt;
        byte[] cryptoPass, cryptoSalt;
        string decryptedPassword, decryptedSalt;

        public MainWindow()
        {
            InitializeComponent();

            //MyIO.CreateFile("source.txt");

            publicKey = string.Empty;
            privateKey = string.Empty;
            filePassword = string.Empty;
            fileSalt = string.Empty;
            decryptedPassword = string.Empty;
            decryptedSalt = string.Empty;
            cryptoPass = null;
            cryptoSalt = null;
        }

        void Next(int curIndex, string msg = "")
        {
            BtnStackPanel.Children[curIndex].IsEnabled = false;
            (BoxesStackPanel.Children[curIndex] as Rectangle).Fill = (FindResource("greenBrush") as SolidColorBrush);
            if(curIndex+1 < BtnStackPanel.Children.Count) BtnStackPanel.Children[curIndex+1].IsEnabled = true;

            msgTextBlock.Text = msg;
        }

        void Restart()
        {
            SolidColorBrush redColor = FindResource("redBrush") as SolidColorBrush;
            int n = BtnStackPanel.Children.Count;

            for (int i = 0; i < n; i++)
            {
                (BoxesStackPanel.Children[i] as Rectangle).Fill = redColor;
                BtnStackPanel.Children[i].IsEnabled = i == 0;
            }

            msgTextBlock.Text = "";
            publicKey = string.Empty;
            privateKey = string.Empty;
            filePassword = string.Empty;
            fileSalt = string.Empty;
            decryptedPassword = string.Empty;
            decryptedSalt = string.Empty;
            cryptoPass = null;
            cryptoSalt = null;
        }

        string GeneratePassword()
        {
            string password = string.Empty;
            int passLength = rand.Next(8, 30);
            for (int i = 0; i < passLength; i++)
            {
                char ch = (char)('a' + rand.Next('z' - 'a' + 1));
                password += rand.Next(100) % 2 == 0 ? char.ToUpper(ch) : ch;
            }
            return password;
        }

        private void Step1_Click(object sender, RoutedEventArgs e)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

            publicKey = rsa.ToXmlString(false);
            privateKey = rsa.ToXmlString(true);

            Next(0, "Созданы 2 ключа: public и private.");
        }

        private void Step2_Click(object sender, RoutedEventArgs e)
        {
            /*
             Send public key to sender
             */

            Next(1, "Разослал public key.");
        }
        private void Step3_Click(object sender, RoutedEventArgs e)
        {
            filePassword = GeneratePassword();
            fileSalt = GeneratePassword();

            FileStream source = null;
            FileStream destFile = null;
            CryptoStream cryptoFile = null;
            try
            {
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(filePassword, Encoding.ASCII.GetBytes(fileSalt));
                
                RijndaelManaged rm = new RijndaelManaged();
                rm.Key = rfc.GetBytes(rm.KeySize / 8);
                rm.IV = rfc.GetBytes(rm.BlockSize / 8);

                source = new FileStream("source.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] sourceByteArray = new byte[source.Length];
                source.Read(sourceByteArray, 0, sourceByteArray.Length);

                destFile = File.Create("crypto.txt");
                cryptoFile = new CryptoStream(destFile, rm.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoFile.Write(sourceByteArray, 0, sourceByteArray.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "INFO", MessageBoxButton.OK);
                return;
            }
            finally
            {
                if (cryptoFile != null) cryptoFile.Close(); 
                if (destFile != null) destFile.Close(); 
                if (source != null) source.Close(); 
            }

            string msg = "Файл зашифрован:\n" +
                         "  Название файла: text.txt\n" +
                         "  Название зашифрованного файла: crText.txt\n" +
                         "  Пароль: " + filePassword + "\n" +
                         "  Соль: " + fileSalt + ".";

            Next(2, msg);
        }

        private void Step4_Click(object sender, RoutedEventArgs e)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(600);
            rsa.FromXmlString(publicKey);

            cryptoPass = rsa.Encrypt(Encoding.Unicode.GetBytes(filePassword), false);
            cryptoSalt = rsa.Encrypt(Encoding.Unicode.GetBytes(fileSalt), false);

            Next(3, "Пароль и соль зашифрованы.");
        }
        private void Step5_Click(object sender, RoutedEventArgs e)
        {
            /*
                Send cryptoPass and cryptoSalt to recipient 
            */

            Next(4, "Зашифрованные пароль и соль отправленны получателю.");
        }
        private void Step6_Click(object sender, RoutedEventArgs e)
        {
            /*
                Send cryptoFile to recipient 
            */

            Next(5, "Зашифрованный файл отправлен получателю.");
        }
        private void Step7_Click(object sender, RoutedEventArgs e)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);

            decryptedPassword = Encoding.Unicode.GetString(rsa.Decrypt(cryptoPass, false));
            decryptedSalt = Encoding.Unicode.GetString(rsa.Decrypt(cryptoSalt, false));

             Next(6, "Пароль расшифрован.");
        }
        private void Step8_Click(object sender, RoutedEventArgs e)
        {
            FileStream cryptedFile = null;
            CryptoStream destStream = null;
            FileStream resultFile = null;

            try
            {
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(decryptedPassword, Encoding.ASCII.GetBytes(decryptedSalt));
                
                RijndaelManaged rm = new RijndaelManaged();
                rm.Key = rfc.GetBytes(rm.KeySize / 8);
                rm.IV = rfc.GetBytes(rm.BlockSize / 8);

                cryptedFile = new FileStream("crypto.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
                destStream = new CryptoStream(cryptedFile, rm.CreateDecryptor(), CryptoStreamMode.Read);
                resultFile = File.Create("result.txt");

                int b = destStream.ReadByte();
                while (b != -1)
                {
                    resultFile.WriteByte((byte)b);
                    b = destStream.ReadByte();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                if (destStream != null) destStream.Close();
                if (cryptedFile != null) cryptedFile.Close();
                if (resultFile != null) resultFile.Close();
            }

            string msg = "Файл расшифрован:\n" +
             "  Название файла: result.txt.";

            Next(7, msg);
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }
    }
}
