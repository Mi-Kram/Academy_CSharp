using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class MyIO
    {
        public static void CreateFile(string fName)
        {
            try
            {
                File.WriteAllText(fName, "Hello World!!!\nIt's my string.");
            }
            catch { }
        }
    }
}
