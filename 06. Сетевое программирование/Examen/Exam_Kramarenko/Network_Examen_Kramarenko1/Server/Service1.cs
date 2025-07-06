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
    public class Service1 : IService1
    {
        public List<string> GetDirData(string address)
        {
            List<string> result = new List<string>();

            DirectoryInfo dInfo = new DirectoryInfo(address);
            if (dInfo.Exists)
            {
                result.AddRange(dInfo.GetDirectories().Select(x => x.FullName));
                result.AddRange(dInfo.GetFiles().Select(x => x.FullName));
            }

            return result;
        }

        public Stream GetStreamData(string address)
        {
            if (File.Exists(address)) return new FileStream(address, FileMode.Open, FileAccess.Read);
            else return null;
        }
    }
}
