using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main2
{
    class MyFile
    {
        public static Dictionary<string, int> GetWordsDictionary(string path)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            DirectoryInfo dInfo = new DirectoryInfo(path);
            if (dInfo.Exists)
            {
                DirectoryInfo[] dirs = dInfo.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    Dictionary<string, int> dic = GetWordsDictionary(dir.FullName);
                    foreach (KeyValuePair<string, int> item in dic)
                    {
                        if (result.ContainsKey(item.Key)) result[item.Key] += item.Value;
                        else result.Add(item.Key, item.Value);
                    }
                }

                FileInfo[] files = dInfo.GetFiles("*.txt");
                foreach (FileInfo file in files)
                {
                    char[] delims = " 1234567890-_=+!@#№$;%^:&?*()/.,<>[]{}`~\\\''\"\t\r\n".ToArray();
                    string[] words = File.ReadAllText(file.FullName).ToLower()
                        .Split(delims, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string word in words)
                    {
                        if (result.ContainsKey(word)) result[word]++;
                        else result.Add(word, 1);
                    }
                }
            }

            return result;
        }
        public static void WriteToFile(Dictionary<string, int> dic, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);

            var Vweight = dic.Values.Select(x => x.ToString()).OrderByDescending(x => x.Length).Max(x => x.Length);

            foreach (KeyValuePair<string, int> item in dic)
            {
                sw.WriteLine($"{item.Value.ToString().PadLeft(Vweight, '0')}:   {item.Key}");
            }

            sw.Close();
            fs.Close();

        }

        public static void CreateFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);

            char[] arr = new char[1024];
            for (int i = 0; i < arr.Length; i++) arr[i] = 'W';
            string text = new string(arr);

            int n = 1024 * 200;
            for (int i = 0; i < n; i++)
            {
                sw.WriteLine(text);
            }

            sw.Close();
            fs.Close();
        }
    }
}
