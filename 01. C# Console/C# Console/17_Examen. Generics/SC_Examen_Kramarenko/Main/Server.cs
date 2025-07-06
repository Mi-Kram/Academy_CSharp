using System;
using System.Timers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Server
    {
        Timer tmr;
        HashSet<string> fileNames;
        Dictionary<string, int> dic;

        delegate void Subscriber(DateTime dt, HashSet<string> NEW);
        event Subscriber Sub;

        string path;

        public string Path
        {
            get => path;
            set { path = value; }
        }

        public Server()
        {
            tmr = new Timer(1000);
            tmr.Elapsed += Tmr_Elapsed;
            fileNames = new HashSet<string>();
            dic = new Dictionary<string, int>();

            Sub += First;
            Sub += Second;

            path = string.Empty;
        }
        public Server(string p)
        {
            tmr = new Timer(1000);
            tmr.Elapsed += Tmr_Elapsed;
            fileNames = new HashSet<string>();
            dic = new Dictionary<string, int>();

            Sub += First;
            Sub += Second;

            path = p;
        }

        public void Start()
        {
            Console.WriteLine("Server start...\n\n");
            tmr.Start();

            while (Console.ReadLine().ToLower() != "stop")
            {
                Console.Clear();
                Console.WriteLine("Server start...\n\n");
            }
        }

        private void First(DateTime dt, HashSet<string> NEW)
        {
            foreach (var item in NEW)
            {
                AddToDic(item);
            }

            Write();
        }

        private void AddToDic(string fName)
        {
            try
            {
                if (!File.Exists(fName)) throw new Exception("File is not found");

                string str = File.ReadAllText(fName);

                str = str.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ");
                string[] words = str.ToLower().Split(" .,()!@#№$%^&*-_+=/*-[]{};:\'\"\\/|<>`~".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in words)
                {
                    if (!dic.ContainsKey(item)) dic.Add(item, 1);
                    else dic[item]++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Write()
        {
            try
            {
                FileStream fs = new FileStream(@"C:\Users\students\Desktop\SC_Examen_Kramarenko\Main\DIC.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                foreach (var item in dic)
                {
                    sw.WriteLine($"{item.Key} = {item.Value}");
                }

                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        private void Second(DateTime dt, HashSet<string> NEW)
        {
            string p = @"C:\Users\students\Desktop\SC_Examen_Kramarenko\Main\DIC.log";

            try
            {
                if (!File.Exists(p)) throw new Exception("File is not exist");

                foreach (var item in NEW)
                {
                    File.AppendAllText(p, $"{dt}  |  {item}\r\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                DirectoryInfo d = new DirectoryInfo(path);
                if (!d.Exists) { Console.WriteLine("Directory is not exist"); tmr.Stop(); Environment.Exit(0); }

                var files = d.GetFiles("*.txt");
                HashSet<string> NEW = new HashSet<string>();
                foreach (var item in files)
                {
                    NEW.Add(item.FullName);
                }

                NEW.ExceptWith(fileNames);

                if (NEW.Count != 0)
                {
                    Sub?.Invoke(e.SignalTime, NEW);
                    fileNames.UnionWith(NEW);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
