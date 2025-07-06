using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class FileText : Text
    {
        public FileText() :base() { }
        public FileText(string str) : base(str) { }
        public FileText(Text source) : base(source) { }

        public void Save(string path)
        {            
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                foreach (var item in text)
                {
                    sw.WriteLine(item.ToString());
                }

                sw.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Load(string path)
        {
            try
            {
                if (!File.Exists(path)) throw new Exception("File is bot exist");

                text.Clear();
                string fileText = File.ReadAllText(path).Replace("\r", "").Replace("\n", "");
                Parse(fileText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
