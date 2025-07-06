using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\Users\Михаил\source\repos\22_Kramarenko\Main\Folder";
            string resultFile = @"C:\Users\Михаил\source\repos\22_Kramarenko\Main\result.xml";
            string resultFolder = @"C:\Users\Михаил\source\repos\22_Kramarenko\Main\ResultFolder";

            Clear(resultFile, resultFolder);
            TapEnter();

            XMLWrite.WriteDir(folderPath, resultFile);
            Console.WriteLine("WriteDir завершил работу");
            TapEnter();

            XMLRead.ReadDir(resultFolder, resultFile);
            Console.WriteLine("ReadDir завершил работу");
            TapEnter();
        }

        static void TapEnter()
        {
            while (Console.ReadKey(true).KeyChar != '\r') { }
        }
        static void Clear(string fileName, string dirName)
        {
            File.Create(fileName).Close();

            DirectoryInfo d = new DirectoryInfo(dirName);

            DirectoryInfo[] dirs = d.GetDirectories();
            foreach (DirectoryInfo dir in dirs) dir.Delete(true);

            FileInfo[] files = d.GetFiles();
            foreach (FileInfo file in files) file.Delete();
        }
    }

    class XMLWrite
    {
        public static void WriteDir(string folderPath, string resultFile)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(resultFile))) throw new Exception("Wrong path to result file");
                DirectoryInfo d = new DirectoryInfo(folderPath);
                if (!d.Exists) throw new Exception("Wrong path to directory");

                XmlWriter xmlWriter;
                XmlWriterSettings xmlSettings = new XmlWriterSettings();

                xmlSettings.Indent = true;
                xmlSettings.NewLineChars = "\r\n";
                xmlSettings.Encoding = Encoding.Default;

                xmlWriter = XmlWriter.Create(resultFile, xmlSettings);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("StartFolder");
                xmlWriter.WriteAttributeString("fullName", folderPath);
                CheckDir(folderPath, xmlWriter);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();

                xmlWriter.Flush();
                xmlWriter.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ReadFiles(string path, XmlWriter xml)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files = d.GetFiles();

            foreach (FileInfo file in files)
            {
                xml.WriteStartElement("file");
                xml.WriteAttributeString("name", file.Name);
                xml.WriteAttributeString("size", file.Length.ToString());
                xml.WriteEndElement();
            }
        }
        static void CheckDir(string path, XmlWriter xml)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            DirectoryInfo[] dirs = d.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                xml.WriteStartElement("folder");
                xml.WriteAttributeString("name", dir.Name);
                CheckDir(dir.FullName, xml);
                xml.WriteEndElement();
            }

            ReadFiles(path, xml);
        }
    }
    class XMLRead
    {
        public static void ReadDir(string folderPath, string resultFile)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(resultFile))) 
                    throw new Exception("Wrong path to result file");
                DirectoryInfo d = new DirectoryInfo(folderPath);
                if (!d.Exists) throw new Exception("Wrong path to directory");

                string OldDirPath = string.Empty;
                string currentPath = string.Empty;
                XmlReader xmlReader;
                xmlReader = XmlReader.Create(resultFile);

                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if(xmlReader.Name == "StartFolder")
                        {
                            xmlReader.MoveToFirstAttribute();
                            OldDirPath = xmlReader.Value;
                            if (!Directory.Exists(OldDirPath)) 
                                throw new Exception("Directory in xml file is not found");
                            xmlReader.MoveToElement();
                        }
                        else if(xmlReader.Name == "folder")
                        {
                            xmlReader.MoveToFirstAttribute();
                            currentPath = newPath(currentPath, xmlReader.Value);
                            newDir(Path.Combine(folderPath, currentPath));
                            xmlReader.MoveToElement();
                            if(xmlReader.IsEmptyElement) currentPath = prevPath(currentPath);
                        }
                        else if(xmlReader.Name == "file")
                        {
                            xmlReader.MoveToFirstAttribute();
                            newFile(Path.Combine(OldDirPath, currentPath, xmlReader.Value), 
                                Path.Combine(folderPath, currentPath, xmlReader.Value));
                            xmlReader.MoveToElement();
                        }
                    }
                    else if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "folder")
                    {
                        currentPath = prevPath(currentPath);
                    }
                }

                xmlReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void newDir(string path)
        {
            Directory.CreateDirectory(path);
        }
        static void newFile(string old, string NEW)
        {
            File.Copy(old, NEW);
        }

        static string newPath(string old, string NEW)
        {
            return old + (old == "" ? "" : "\\") + NEW;
        }
        static string prevPath(string path)
        {
            return Path.GetDirectoryName(path);
        }

    }
}
