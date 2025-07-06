using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//CS_CreateWordsDictionary
//$(OutDir)..\Main\bin\Debug\CPlus_DLL\$(TargetName)$(TargetExt)
namespace CS_CreateWordsDictionary
{
    public class Words
    {
        public static void CreateWordsDictionary(string folderPath, string resultPath)
        {
            DirectoryInfo dInfo = new DirectoryInfo(folderPath);
            if (dInfo.Exists)
            {
                Dictionary<string, int> wordsDic = new Dictionary<string, int>();
                char[] delims = " 1234567890`~!@#№$%^&*()_-=+/.\\|[]{}\'\";:?<>,\n\t\r".ToArray();

                try
                {
                    FileInfo[] files = dInfo.GetFiles("*.txt", SearchOption.AllDirectories);
                    foreach (FileInfo file in files)
                    {
                        try
                        {
                            string text = File.ReadAllText(file.FullName, Encoding.Default).ToLower();
                            string[] words = text.Split(delims, StringSplitOptions.RemoveEmptyEntries);

                            foreach (string word in words)
                            {
                                if (wordsDic.ContainsKey(word)) wordsDic[word]++;
                                else wordsDic.Add(word, 1);
                            }
                        }
                        catch { }
                    }

                     wordsDic = wordsDic.OrderBy(x => x.Value).ThenBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                    int w = wordsDic.Count == 0 ? 1 : wordsDic.Last().Value.ToString().Length;

                    FileStream fs = new FileStream(resultPath, FileMode.Create, FileAccess.Write, FileShare.Read);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                    foreach (KeyValuePair<string, int> pair in wordsDic)
                    {
                        sw.WriteLine($"{pair.Value.ToString().PadLeft(w, '0')}. {pair.Key}");
                    }

                    sw.Close();
                    fs.Close();
                }
                catch { }

            }
        }
    }
}
