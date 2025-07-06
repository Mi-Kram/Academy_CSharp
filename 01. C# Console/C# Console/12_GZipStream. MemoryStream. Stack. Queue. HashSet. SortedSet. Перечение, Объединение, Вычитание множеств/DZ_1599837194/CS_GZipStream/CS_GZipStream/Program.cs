using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_GZipStream
{
    class Program
    {
        FileStream destFile, infile;
        GZipStream compressedzipStream;
        string sourceName, compressedName, decompressedName;

        public void Compress()
        {
            Console.Write("Enter source file name: ");
            sourceName = Console.ReadLine();

            Console.Write("Enter compressed file name: ");
            compressedName = Console.ReadLine();

            // сжатый файл (вначале пустой)
            destFile = File.Create(compressedName);

            // исходный файл
            infile = new FileStream(sourceName, FileMode.Open, FileAccess.Read, FileShare.Read);

            // буфер в памяти
            byte[] buffer = new byte[infile.Length];

            infile.Read(buffer, 0, buffer.Length);

            // создание сжимающего потока
            compressedzipStream = new GZipStream(destFile, CompressionMode.Compress, true);

            // синхронное сжатие
            /*compressedzipStream.Write(buffer, 0, buffer.Length);

            infile.Close();
            compressedzipStream.Close();
            destFile.Close();*/

            // указать метод, который будет вызван автоматически, когда сжатие закончится
            AsyncCallback callBack = new AsyncCallback(EndWrite);

            compressedzipStream.BeginWrite(buffer, 0, buffer.Length, callBack, 33);

            Console.WriteLine($"Source file size: {infile.Length}");
            Console.WriteLine("Packing...\n");
            Console.WriteLine("Press any key to continue...\n");
            Console.ReadLine();
        }

        // метод, который будет запущен по окончании сжатия
        private void EndWrite(IAsyncResult result)
        {
            Console.WriteLine("Compression successful!\n");
            Console.WriteLine($"Result file size: {destFile.Length}");

            // result.AsyncState - опциональный параметр, переданный из вызова метода BeginWrite
            Console.WriteLine($"AsyncState parameter received: {result.AsyncState.ToString()}");

            // закрытие потоков по окончании сжатия
            infile.Close();
            compressedzipStream.Close();
            destFile.Close();
        }

        public void Decompress()
        {
            Console.Write("Enter decompressed (result) file name: ");
            decompressedName = Console.ReadLine();

            // исходный сжатый файл
            FileStream infile = new FileStream(compressedName, FileMode.Open, FileAccess.Read, FileShare.Read);

            // результирующий расжатый файл
            FileStream destFile = File.Create(decompressedName);

            // поток для расжатия
            GZipStream zipStream = new GZipStream(infile, CompressionMode.Decompress);

            // побайтное расжатие исходного файла
            int b = zipStream.ReadByte();
            while (b != -1)
            {
                // запись ОДНОГО байта в результирующий файл
                destFile.WriteByte((byte)b);

                // чтение следующего байта из распаковывающего потока
                b = zipStream.ReadByte();
            }

            Console.WriteLine("Decompression successful!\n");
            Console.WriteLine($"Result file size: {destFile.Length}\n");

            // закрытие файлов после расжатия
            zipStream.Close();
            destFile.Close();
            infile.Close();
        }

        public void MemoryTest()
        {
            // поток в оперативной памяти
            MemoryStream mem = new MemoryStream();

            Console.WriteLine("Loading source file to MemoryStream...\n");

            FileStream infile = new FileStream(sourceName, FileMode.Open, FileAccess.Read, FileShare.Read);

            byte[] buffer = new byte[infile.Length];

            infile.Read(buffer, 0, buffer.Length);

            // запись в поток в оперативной памяти
            mem.Write(buffer, 0, buffer.Length);

            infile.Close();

            Console.WriteLine("Saving file from MemoryStream...\n");

            FileStream destFile = File.Create(sourceName+".result");

            // запись из потока в оперативной памяти в результирующий файл
            mem.WriteTo(destFile);

            mem.Close();

            destFile.Close();
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Compress();
            program.Decompress();
            program.MemoryTest();
        }
    }
}
