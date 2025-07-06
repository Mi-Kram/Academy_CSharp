using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service1 : IService1
    {

        //string mainDir = @"C:\Users\Setup\source\repos\Main\ServerData";
        string mainDir = @"C:\Users\Михаил\Desktop\Examen\Network_Examen_Kramarenko2\ServerData";
        string login = "";
        IClientCallback callback = null;

        //          path ,  data
        Dictionary<string, Stream> tempData = new Dictionary<string, Stream>();

        public bool CreateFolder(string path)
        {
            try
            {
                string folder = $"{mainDir}/Data/{login}{path}";

                if (Directory.Exists(folder)) return false;
                Directory.CreateDirectory(folder);

                return Directory.Exists(folder);
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveFolder(string path)
        {
            try
            {
                string folder = $"{mainDir}/Data/{login}{path}";

                if (!Directory.Exists(folder)) return true;
                Directory.Delete(folder, true);

                return !Directory.Exists(folder);
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveFile(string path)
        {
            try
            {
                string file = $"{mainDir}/Data/{login}{path}";

                if (!File.Exists(file)) return true;
                File.Delete(file);

                return !File.Exists(file);
            }
            catch
            {
                return false;
            }
        }


        public FolderData GetData(string path)
        {
            FolderData folderData = new FolderData();

            try
            {
                string folder = $"{mainDir}/Data/{login}{path}";
                DirectoryInfo dInfo = new DirectoryInfo(folder);

                if (dInfo.Exists)
                {
                    folderData.Folders = dInfo.GetDirectories().Select(x => x.Name).ToArray();
                    folderData.Files = dInfo.GetFiles().Select(x => x.Name).ToArray();
                }

                return folderData;
            }
            catch { }

            return folderData;
        }


        public bool LogIn(string login, string password)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo($"{mainDir}/UserLogins");
                FileInfo[] files = dInfo.GetFiles();

                var logins = files.Select(x => System.IO.Path.GetFileNameWithoutExtension(x.Name));
                if (!logins.Contains(login)) return false;

                if (password != File.ReadAllText(files.Where(x => System.IO.Path.GetFileNameWithoutExtension(x.Name) == login).First().FullName, Encoding.Default))
                    return false;

                this.login = login;
                callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Register(string login, string password)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo($"{mainDir}/UserLogins");
                FileInfo[] files = dInfo.GetFiles();

                var logins = files.Select(x => System.IO.Path.GetFileNameWithoutExtension(x.Name));
                if (logins.Contains(login)) return false;

                File.WriteAllText($"{mainDir}/UserLogins/{login}", password, Encoding.Default);

                Directory.CreateDirectory($"{mainDir}/Data/{login}");

                this.login = login;
                callback = OperationContext.Current.GetCallbackChannel<IClientCallback>();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CopyFile(string path, byte[] data, bool isEnd)
        {
            try
            {
                bool dataExists = tempData.ContainsKey(path);

                if (!dataExists)
                {
                    if (isEnd)
                    {
                        File.WriteAllBytes($"{mainDir}/Data/{login}{path}", data);
                    }
                    else
                    {
                        tempData.Add(path, new FileStream($"{mainDir}/Data/{login}{path}", FileMode.Create, FileAccess.Write));
                        tempData[path].Write(data, 0, data.Length);
                    }
                }
                else
                {
                    tempData[path].Write(data, 0, data.Length);
                    if (isEnd)
                    {
                        tempData[path].Close();
                        tempData.Remove(path);
                    }

                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GetFile(string path)
        {
            string fName = $"{mainDir}/Data/{login}{path}";
            byte[] buf = new byte[10000];
            using (FileStream fs = new FileStream(fName, FileMode.Open, FileAccess.Read))
            {
                while (true)
                {
                    int res = fs.Read(buf, 0, buf.Length);
                    if (res == 0)
                    {
                        callback.SetCloseStream(path);
                        break;
                    }
                    callback.GetFilePart(path, buf);
                }
            }

            return true;
        }
    }
}
