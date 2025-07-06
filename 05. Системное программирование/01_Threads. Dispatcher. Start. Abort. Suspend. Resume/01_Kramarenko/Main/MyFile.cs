using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    static class MyFile
    {
        public static void CreateSource(string path)
        {
            char[] str = new char[10000];
            for (int i = 0; i < str.Length; i++)
            {
                str[i] = (i + 1) % 100 == 0 ? '\n' : 'W';
            }
            string text = new string(str);

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            for (int i = 0; i < 1000; i++)
            {
                File.WriteAllText($@"{path}\file{(i + 1).ToString().PadLeft(4, '0')}.txt", text, Encoding.Default);
            }

            path += @"\SubDir";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            for (int i = 0; i < 1000; i++)
            {
                File.WriteAllText($@"{path}\file{(i + 1).ToString().PadLeft(4, '0')}.txt", text, Encoding.Default);
            }
        }

        public static void ClearFiles(params string[] paths)
        {
            foreach (string path in paths)
            {
                ClearDir(path);
            }
        }

        static void ClearDir(string path)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);
            DirectoryInfo[] dirs = dInfo.GetDirectories();

            foreach (DirectoryInfo dir in dirs)
            {
                ClearDir(dir.FullName);
                Directory.Delete(dir.FullName);
            }

            FileInfo[] files = dInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                File.Delete(file.FullName);
            }
        }
    }
}
