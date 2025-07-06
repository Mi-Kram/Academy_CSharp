using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_BinaryReader
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream(@"c:\temp\data.dat", FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs, Encoding.Default);
            string s = "Hello";
            byte[] a = { 3, 5, 6, 7, 8 };
            bw.Write(s);
            bw.Write(a);
            //bw.Close();
            fs.Close();

            fs = new FileStream(@"c:\temp\data.dat", FileMode.Open, FileAccess.Read);
            Console.WriteLine($"Lenght: {fs.Length}");
            BinaryReader br = new BinaryReader(fs, Encoding.Default);
            s = "!!!";
            byte[] b = null;
            s = br.ReadString();
            b = br.ReadBytes(5);

            Console.WriteLine(s);
            foreach (byte i in b)
            {
                Console.WriteLine(i);
            }
            //br.Close();
            fs.Close();

        }
    }
}
