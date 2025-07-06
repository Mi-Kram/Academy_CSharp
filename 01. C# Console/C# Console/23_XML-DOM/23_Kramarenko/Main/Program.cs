using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Resolvers;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\Users\Михаил\source\repos\23_Kramarenko\Main\Folder";
            string resultFile = @"C:\Users\Михаил\source\repos\23_Kramarenko\Main\result.xml";
            string resultFolder = @"C:\Users\Михаил\source\repos\23_Kramarenko\Main\ResultFolder";

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

            if (Directory.Exists(dirName))
            {
                DirectoryInfo d = new DirectoryInfo(dirName);

                DirectoryInfo[] dirs = d.GetDirectories();
                foreach (DirectoryInfo dir in dirs) dir.Delete(true);

                FileInfo[] files = d.GetFiles();
                foreach (FileInfo file in files) file.Delete();
            }
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

                XmlDocument doc = new XmlDocument();
                XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "windows-1251", null);

                doc.AppendChild(decl);
                XmlElement first = doc.CreateElement("StartFolder");
                first.SetAttribute("name", folderPath);
                CheckDir(folderPath, ref first, doc);
                doc.AppendChild(first);

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.Encoding = Encoding.Default;

                XmlWriter writer = XmlWriter.Create(resultFile, settings);
                doc.WriteTo(writer);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ReadFiles(string path, ref XmlElement xml, XmlDocument doc)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files = d.GetFiles();

            foreach (FileInfo file in files)
            {
                XmlElement elem = doc.CreateElement("file");
                elem.SetAttribute("name", file.Name);
                elem.SetAttribute("size", file.Length.ToString());
                xml.AppendChild(elem);
            }
        }
        static void CheckDir(string path, ref XmlElement xml, XmlDocument doc)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            DirectoryInfo[] dirs = d.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                XmlElement elem = doc.CreateElement("folder");
                elem.SetAttribute("name", dir.Name);
                CheckDir(dir.FullName, ref elem, doc);
                xml.AppendChild(elem);
            }

            ReadFiles(path, ref xml, doc);
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

                XmlDocument doc = new XmlDocument();
                doc.Load(resultFile);
                CreateDir(null, null, folderPath, doc);
                            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void CreateDir(string OldDirPath, string currentPath, string path, XmlNode xml)
        {
            foreach (XmlNode node in xml.ChildNodes)
            {
                if(node.NodeType == XmlNodeType.Element)
                {
                    if (node.Name == "StartFolder")
                    {
                        OldDirPath = node.Attributes[0].Value;
                        if (!Directory.Exists(OldDirPath))
                            throw new Exception("Directory in xml file is not found");

                        CreateDir(OldDirPath, currentPath, path, node);
                    }
                    else if (node.Name == "folder")
                    {
                        currentPath = newPath(currentPath, node.Attributes[0].Value);
                        newDir(Path.Combine(path, currentPath));

                        if (node.HasChildNodes) CreateDir(OldDirPath, currentPath, path, node);
                        currentPath = prevPath(currentPath);
                    }
                    else if (node.Name == "file")
                    {
                        string fName = node.Attributes[0].Value;
                        newFile(Path.Combine(OldDirPath, currentPath, fName), 
                            Path.Combine(path, currentPath, fName));
                    }
                }
                else if(node.NodeType == XmlNodeType.EndElement && node.Name == "folder")
                {
                    currentPath = prevPath(currentPath);
                }
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
            return (old == null || old == "") ? NEW : (old + "\\" + NEW);
        }
        static string prevPath(string path)
        {
            return Path.GetDirectoryName(path);
        }

    }
}
