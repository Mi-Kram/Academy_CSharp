using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Main
{
    public static class MyIO
    {
        public static void CreateFolder(string path)
        {
            try
            {
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                if (!Directory.Exists(path + @"\Folder01")) Directory.CreateDirectory(path + @"\Folder01");
                if (!Directory.Exists(path + @"\Folder02")) Directory.CreateDirectory(path + @"\Folder02");
                if (!Directory.Exists(path + @"\Folder03")) Directory.CreateDirectory(path + @"\Folder03");
                if (!Directory.Exists(path + @"\Test")) Directory.CreateDirectory(path + @"\Test");
                if (!Directory.Exists(path + @"\Lost")) Directory.CreateDirectory(path + @"\Lost");

                string tmp = path + @"\Folder01\";
                if (!Directory.Exists(tmp + "Dir01")) Directory.CreateDirectory(tmp + "Dir01");
                if (!Directory.Exists(tmp + "Dir02")) Directory.CreateDirectory(tmp + "Dir02");
                if (!Directory.Exists(tmp + "Dir03")) Directory.CreateDirectory(tmp + "Dir03");
                if (!Directory.Exists(tmp + "Test")) Directory.CreateDirectory(tmp + "Test");
                if (!Directory.Exists(tmp + "Lost")) Directory.CreateDirectory(tmp + "Lost");

                tmp = path + @"\Folder02\";
                if (!Directory.Exists(tmp + "Dir01")) Directory.CreateDirectory(tmp + "Dir01");
                if (!Directory.Exists(tmp + "Dir02")) Directory.CreateDirectory(tmp + "Dir02");
                if (!Directory.Exists(tmp + "Dir03")) Directory.CreateDirectory(tmp + "Dir03");
                if (!Directory.Exists(tmp + "Test")) Directory.CreateDirectory(tmp + "Test");
                if (!Directory.Exists(tmp + "Lost")) Directory.CreateDirectory(tmp + "Lost");

                tmp = path + @"\Folder03\";
                if (!Directory.Exists(tmp + "Dir01")) Directory.CreateDirectory(tmp + "Dir01");
                if (!Directory.Exists(tmp + "Dir02")) Directory.CreateDirectory(tmp + "Dir02");
                if (!Directory.Exists(tmp + "Dir03")) Directory.CreateDirectory(tmp + "Dir03");
                if (!Directory.Exists(tmp + "Test")) Directory.CreateDirectory(tmp + "Test");
                if (!Directory.Exists(tmp + "Lost")) Directory.CreateDirectory(tmp + "Lost");

                tmp = path + @"\Test\";
                if (!Directory.Exists(tmp + "Dir01")) Directory.CreateDirectory(tmp + "Dir01");
                if (!Directory.Exists(tmp + "Dir02")) Directory.CreateDirectory(tmp + "Dir02");
                if (!Directory.Exists(tmp + "Dir03")) Directory.CreateDirectory(tmp + "Dir03");
                if (!Directory.Exists(tmp + "Test")) Directory.CreateDirectory(tmp + "Test");
                if (!Directory.Exists(tmp + "Lost")) Directory.CreateDirectory(tmp + "Lost");

                tmp = path + @"\Lost\";
                if (!Directory.Exists(tmp + "Dir01")) Directory.CreateDirectory(tmp + "Dir01");
                if (!Directory.Exists(tmp + "Dir02")) Directory.CreateDirectory(tmp + "Dir02");
                if (!Directory.Exists(tmp + "Dir03")) Directory.CreateDirectory(tmp + "Dir03");
                if (!Directory.Exists(tmp + "Test")) Directory.CreateDirectory(tmp + "Test");
                if (!Directory.Exists(tmp + "Lost")) Directory.CreateDirectory(tmp + "Lost");
            }
            catch
            {
                MessageBox.Show("Ошибка создания каталогов", "INFO", MessageBoxButton.OK);
            }
        }
    }
}
