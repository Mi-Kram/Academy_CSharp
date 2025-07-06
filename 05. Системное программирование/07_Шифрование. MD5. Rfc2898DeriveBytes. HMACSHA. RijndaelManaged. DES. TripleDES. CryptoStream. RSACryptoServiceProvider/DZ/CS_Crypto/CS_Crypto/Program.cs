using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CS_Crypto
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Enter message: ");
            string message = Console.ReadLine();

            // получение MD5 хэша
            MD5 md5 = new MD5CryptoServiceProvider();

            // преобразовать сообщение в массив байт
            byte[] mes = Encoding.Unicode.GetBytes(message);

            // вычислить хэш
            md5.ComputeHash(mes);

            // преобразовать хэш в строку
            Console.WriteLine($"MD5 hash: {Convert.ToBase64String(md5.Hash)}");

            // ввод пароля для генерации хэша
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();

            // получение "соли" для генерации хэша
            byte[] salt = Encoding.ASCII.GetBytes("testing!!!");

            // стандартный класс, преобразующий пароль и "соль" в массив байт, влияющий на генерацию хэша
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);
            byte[] bk = key.GetBytes(16);

            // создание объекта для генерации хэша с определёнными параметрами, влияющими на генерацию
            HMACSHA1 hash1 = new HMACSHA1(bk);
            mes = Encoding.Unicode.GetBytes(message);

            // вычисление хэша
            hash1.ComputeHash(mes);
            Console.WriteLine($"HMACSHA1 hash: {Convert.ToBase64String(hash1.Hash)}");

            HMACSHA256 hash256 = new HMACSHA256(bk);
            mes = Encoding.Unicode.GetBytes(message);
            hash256.ComputeHash(mes);
            Console.WriteLine($"HMACSHA256 hash: {Convert.ToBase64String(hash256.Hash)}");

            HMACSHA512 hash512 = new HMACSHA512(bk);
            mes = Encoding.Unicode.GetBytes(message);
            hash512.ComputeHash(mes);
            Console.WriteLine($"HMACSHA512 hash: {Convert.ToBase64String(hash512.Hash)}");
            */

            /*
            // ввод имени исходного файла
            Console.WriteLine("Enter source file name: ");
            string sourceName = Console.ReadLine();

            // ввод имени зашифрованного файла
            Console.WriteLine("Enter crypted file name: ");
            string cryptedName = Console.ReadLine();

            // поток для сохранения зашифрованного файла
            FileStream destFile = File.Create(cryptedName);

            // поток исходного файла
            FileStream infile = new FileStream(sourceName, FileMode.Open, FileAccess.Read, FileShare.Read);

            byte[] buffer = new byte[infile.Length];

            infile.Read(buffer, 0, buffer.Length);

            // ввод пароля
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();

            // создание экземпляра алгоритма шифрования
            RijndaelManaged alg = new RijndaelManaged();
            //DES alg = DES.Create();
            //TripleDES alg = TripleDES.Create();

            // генерация параметров алгоритма шифрования
            byte[] salt = Encoding.ASCII.GetBytes("testing!!!");

            // генерация пароля для шифрования
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);

            // задание параметров шифрования
            alg.Key = key.GetBytes(alg.KeySize / 8);
            alg.IV = key.GetBytes(alg.BlockSize / 8);

            // получение совместимого с CryptoStream шифратора
            ICryptoTransform encriptor = alg.CreateEncryptor();

            // создание потока для шифрования
            CryptoStream encstream = new CryptoStream(destFile, encriptor, CryptoStreamMode.Write);

            // шифрование исходного файла
            encstream.Write(buffer, 0, buffer.Length);

            encstream.Close();
            infile.Close();
            destFile.Close();

            Console.WriteLine("Enter decrypted file name: ");
            string decryptedName = Console.ReadLine();

            // поток расшифровываемого файла
            FileStream decryptedFile = File.Create(decryptedName);

            // чтение зашифрованного файла
            FileStream cryptedFile = new FileStream(cryptedName, FileMode.Open, FileAccess.Read, FileShare.Read);

            // ввод пароля для расшифровки
            Console.WriteLine("Enter password: ");
            password = Console.ReadLine();

            // получение параметров алгоритма расшифровки
            salt = Encoding.ASCII.GetBytes("testing!!!");

            Rfc2898DeriveBytes key2 = new Rfc2898DeriveBytes(password, salt);
            alg.Key = key2.GetBytes(alg.KeySize / 8);
            alg.IV = key2.GetBytes(alg.BlockSize / 8);

            // получение объекта для расшифровки
            ICryptoTransform decriptor = alg.CreateDecryptor();

            // создание расшифровывающего потока
            CryptoStream decstream = new CryptoStream(cryptedFile, decriptor, CryptoStreamMode.Read);

            // побайтовая расшифровка файла
            int b = decstream.ReadByte();
            while (b != -1)
            {
                decryptedFile.WriteByte((byte)b);
                b = decstream.ReadByte();
            }

            decstream.Close();
            cryptedFile.Close();
            decryptedFile.Close();
            */

            // генератор ключей
            RSACryptoServiceProvider gen = new RSACryptoServiceProvider(2048);

            // генерация публичного ключа (для шифрования)
            string public_key = gen.ToXmlString(false);

            // генерация секретного ключа (для расшифровки)
            string private_key = gen.ToXmlString(true);

            // вывод ключей на экран
            //Console.WriteLine("Public key:");
            //Console.WriteLine(public_key);
            //Console.WriteLine();
            //Console.WriteLine("Private key:");
            //Console.WriteLine(private_key);
            //Console.WriteLine();

            // ввод сообщения для шифрования
            Console.WriteLine("Enter message: ");
            string message = Console.ReadLine();

            // создание объекта для шифрования
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

            // указание пароля для шифрования
            rsa.FromXmlString(public_key);
            byte[] mes = Encoding.Unicode.GetBytes(message);

            // шифрование с открытым ключом
            byte[] encripted_mes = rsa.Encrypt(mes, false);

            // вывод зашифрованного сообщения
            string res2 = Encoding.Unicode.GetString(encripted_mes);

            Console.WriteLine($"Encrypted message: {res2}");

            // расшифровка с закрытым ключом
            RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider();

            // указание закрытого ключа
            rsa2.FromXmlString(private_key);

            // расшифровка сообщение
            byte[] decripted_mes = rsa2.Decrypt(encripted_mes, false);

            // получение расшифрованного сообщения
            string res = Encoding.Unicode.GetString(decripted_mes);

            Console.WriteLine($"Decrypted message: {res}");

            // Схема работы алгоритмов с открытым ключом
            // 0. Получатель генерирует пару открытый и закрытый ключ
            // 1. Получатель шлёт отправителю открытый ключ
            // 2. Оправитель шифрует файл алгоритмом AES с паролем
            // 3. Пароль для алгоритма AES шифруется полученным открытым ключом
            // 4. Зашифрованный пароль отправляется получателю
            // 5. Зашифрованный файл отправляется получателю
            // 6. Получатель расшифровывает пароль закрытым ключом
            // 7. Получатель расшифровывает файл паролем к симметричному алгоритму AES
        }
    }
}
