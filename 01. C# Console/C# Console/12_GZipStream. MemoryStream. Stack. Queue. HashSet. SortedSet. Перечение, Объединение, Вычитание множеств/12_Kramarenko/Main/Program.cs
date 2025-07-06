using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Remoting.Channels;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Tasks.Task1();
            Tasks.Task2();


            End();
        }

        static void End()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(46, 12);
            Console.Write("                         ");

            Console.SetCursorPosition(46, 13);
            Console.Write("   Программа завершена   ");

            Console.SetCursorPosition(46, 14);
            Console.Write("                         ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
        }
    }

    class Tasks
    {
        public static void Task1()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task1. Добавить в программу с резервными копиями возможность сжатия копий в папке BackUp \n\n\n");
            Console.ResetColor();

            try
            {
                string path = @"C:\Users\Михаил\source\repos\12_Kramarenko\Main\Task1\Dir";
                string backUp = @"C:\Users\Михаил\source\repos\12_Kramarenko\Main\Task1\BackUp";
                string reserve = @"C:\Users\Михаил\source\repos\12_Kramarenko\Main\Task1\Reserve";
                string unPack = @"C:\Users\Михаил\source\repos\12_Kramarenko\Main\Task1\UnPack";

                ReserveFiles rf = new ReserveFiles();
                rf.ReserveAllFiles(path, backUp, new string[] { "*.*" });
                rf.PackDir(backUp, reserve);

                Console.WriteLine("Success reserved");
                Console.ReadKey();

                rf.UnPackDir(unPack, reserve);
                Console.WriteLine("\n\nSuccess unpacked");
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task1 не выполнен. {ex.Message}\b!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
        public static void Task2()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task2. Пользователь указавает папку с текстовыми файлами. Программа создаёт файл результата, в котором ");
            Console.WriteLine("       в столбик будут записаны слова, присутствующие во всех текстовых файлах из заданной папки       \n\n\n");
            Console.ResetColor();

            try
            {
                string path = @"C:\Users\Михаил\source\repos\12_Kramarenko\Main\Task2";
                string[] words = GetWords(path);

                foreach (var item in words)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {ex.Message}\b!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }


        public static string[] GetWords(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            if (!d.Exists) throw new Exception("Dir is not found");

            var files = d.GetFiles("*.txt");
            if (files.Length == 0) throw new Exception("TXT files is not found");

            Words result = new Words(File.ReadAllText(files[0].FullName).Split(" .,!?<>[]{}()-\'\";:/|`~#№$%^&*+=\n\r\t@".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            if (result.Length == 0) return new string[] { };

            for (int i = 1; i < files.Length; i++)
            {
                result.Intersect(File.ReadAllText(files[i].FullName).Split(" .,!?<>[]{}()-\'\";:/|`~#№$%^&*+=\n\r\t@".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                if (result.Length == 0) return new string[] { };
            }

            return result.ToStringArray();
        }
    }


    class ReserveFiles
    {
        List<string> FullFileName;

        public ReserveFiles()
        {
            FullFileName = new List<string>();
        }

        public void ReserveAllFiles(string path, string backUp, string[] exts)
        {
            Reserve(path, backUp, exts);

            var dirs = new DirectoryInfo(path).GetDirectories();
            foreach (var item in dirs)
            {
                ReserveAllFiles(item.FullName, backUp, exts);
            }
        }
        private void Reserve(string path, string backUp, string[] exts)
        {
            DirectoryInfo d = new DirectoryInfo(path);

            foreach (var ext in exts)
            {
                var files = d.GetFiles(ext);
                foreach (var file in files)
                {
                    bool flag = false;
                    foreach (var item in FullFileName)
                    {
                        if (item == file.FullName)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag) continue;

                    string fileName = backUp + '\\' + file.Name;
                    if (File.Exists(fileName))
                    {
                        fileName = fileName.Remove(fileName.Length - file.Extension.Length) + "(1)" + file.Extension;
                        int cnt = 2;
                        while (File.Exists(fileName))
                        {
                            fileName = fileName.Remove(fileName.Length - file.Extension.Length);
                            int index = fileName.Length - 1;
                            int dig = 0;
                            while (fileName[index] != '(')
                            {
                                dig++;
                                index--;
                            }

                            fileName = fileName.Remove(fileName.Length - dig, dig) + cnt.ToString() + ')' + file.Extension;
                            cnt++;
                        }
                    }

                    File.Copy(file.FullName, fileName);
                    FullFileName.Add(file.FullName);
                }
            }
        }

        public void PackDir(string path, string reserve)
        {
            if (!Directory.Exists(reserve)) throw new Exception("Dir is not found");
            DirectoryInfo d = new DirectoryInfo(path);
            if (!d.Exists) throw new Exception("Dir is not found");

            var files = d.GetFiles();
            foreach (var item in files)
            {
                PackGZip(item.FullName, reserve + "\\" + item.Name + ".GZIP");
            }
        }
        private void PackGZip(string path, string reserve)
        {
            FileStream destFile, infile;
            GZipStream compressedzipStream;

            destFile = File.Create(reserve);
            infile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            byte[] buf = new byte[infile.Length];
            infile.Read(buf, 0, buf.Length);

            compressedzipStream = new GZipStream(destFile, CompressionMode.Compress, true);
            compressedzipStream.Write(buf, 0, buf.Length);

            infile.Close();
            compressedzipStream.Close();
            destFile.Close();
        }

        public void UnPackDir(string path, string zip)
        {
            if (!Directory.Exists(path)) throw new Exception("Dir is not found");
            DirectoryInfo d = new DirectoryInfo(zip);
            if (!d.Exists) throw new Exception("Dir is not found");

            var files = d.GetFiles("*.GZIP");
            foreach (var item in files)
            {
                UnPackGZip(path + "\\" + Path.GetFileNameWithoutExtension(item.Name), item.FullName);
            }

        }
        private void UnPackGZip(string path, string zip)
        {
            FileStream infile = new FileStream(zip, FileMode.Open, FileAccess.Read, FileShare.Read);
            FileStream destFile = File.Create(path);

            GZipStream zipStream = new GZipStream(infile, CompressionMode.Decompress);

            int b = zipStream.ReadByte();
            while (b != -1)
            {
                destFile.WriteByte((byte)b);
                b = zipStream.ReadByte();
            }

            zipStream.Close();
            destFile.Close();
            infile.Close();
        }
    }


    #region Words Variant1

    //class Words
    //{
    //    List<string> words;
    //    public Words(string[] arr)
    //    {
    //        words = new List<string>();
    //        words.AddRange(arr);
    //    }

    //    public void Intersect(string[] arr)
    //    {
    //        if (arr.Length == 0) words.Clear();

    //        foreach (var word in words)
    //        {
    //            bool flag = false;
    //            foreach (var item in arr)
    //            {
    //                if (word == item)
    //                {
    //                    flag = true;
    //                    break;
    //                }
    //            }

    //            if (!flag) words.Remove(word);
    //        }
    //    }
    //    public int Length
    //    {
    //        get => words.Count;
    //    }

    //    public string[] ToStringArray()
    //    {
    //        string[] w = new string[words.Count];

    //        for (int i = 0; i < words.Count; i++)
    //        {
    //            w[i] = words[i];
    //        }

    //        return w;
    //    }
    //}

    #endregion
    class Words
    {
        HashSet<string> words;
        public Words(string[] arr)
        {
            words = new HashSet<string>();
            foreach (var item in arr)
            {
                words.Add(item);
            }
        }

        public void Intersect(string[] arr)
        {
            if (arr.Length == 0) words.Clear();

            foreach (var word in words)
            {
                bool flag = false;
                foreach (var item in arr)
                {
                    if (word == item)
                    {
                        flag = true;
                        break;
                    }
                }

                if(!flag) words.Remove(word);
            }
        }
        public int Length
        {
            get => words.Count;
        }

        public string[] ToStringArray()
        {
            string[] w = new string[words.Count];

            int i = 0;
            foreach (var item in words)
            {
                w[i++] = item;
            }

            return w;
        }
    }
}
