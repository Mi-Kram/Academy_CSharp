using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace CS_Serialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Student man = new Student("Alex", "Petrov", 23, "Donetsk", "Lenina", 3);

            /*try
            {               
                // бинарная сериализация
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fstream = new FileStream("student.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                bf.Serialize(fstream, man);
                fstream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // десериализация одиночного объекта
            FileStream fstream2 = File.OpenRead("student.dat");
            BinaryFormatter bf2 = new BinaryFormatter();
            Student man2 = (Student)bf2.Deserialize(fstream2);
            fstream2.Close();

            man2.Print();*/

            // сериализующий объект в формат XML
            /*XmlSerializer xs = new XmlSerializer(typeof(Student));

            using (FileStream fs = new FileStream("student.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // сериализация в файловый поток
                xs.Serialize(fs, man);
            }

            // XML-десериализация
            Stream fstream3 = File.OpenRead("student.xml");
            Student man3 = (Student)xs.Deserialize(fstream3);
            fstream3.Close();

            man3.Print();*/

            using (StreamWriter writer = new StreamWriter("student.json", false, Encoding.Default))
            {
                var json = new JavaScriptSerializer().Serialize(man);
                writer.Write(json);
            }

            using (StreamReader reader = new StreamReader("student.json", Encoding.Default))
            {
                Student obj = new JavaScriptSerializer().Deserialize<Student>(reader.ReadToEnd());
                obj.Print();
            }
            
        }
    }
}
