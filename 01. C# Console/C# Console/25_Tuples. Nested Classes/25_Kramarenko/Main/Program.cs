using System;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Михаил\Desktop\C#\25_Tuples. Nested Classes\25_Kramarenko\Main\config.xml";

            Application app = new Application(path);
            app.Run();
        }
    }
}
