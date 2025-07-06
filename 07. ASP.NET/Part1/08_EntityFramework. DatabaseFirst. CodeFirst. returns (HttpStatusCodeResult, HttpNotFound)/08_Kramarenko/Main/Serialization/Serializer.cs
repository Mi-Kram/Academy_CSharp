using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Main.Serialization
{
    public static class Serializer
    {
        public static void Serelize(object obj, string path, Type type)
        {
            XmlSerializer formatter = new XmlSerializer(type);
            if(File.Exists(path))File.Delete(path); 
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            { 
                formatter.Serialize(fs, obj);
            }
        }
        public static object Deserelize(string path, Type type)
        {
            object result = null;
            XmlSerializer formatter = new XmlSerializer(type);
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                 result = formatter.Deserialize(fs);
            }
            return result;
        }
    }
}