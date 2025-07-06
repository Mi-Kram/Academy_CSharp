using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CS_XmlDOM
{
    class Program
    {
        private static void PrintNode(XmlNode node1)
        {
            // обработка всех дочерних узлов
            foreach (XmlNode node in node1.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    // проверка на наличие атрибутов
                    if (node.Attributes.Count > 0)
                    {
                        string str = "< " + node.Name + "  ";

                        // обработка атрибутов
                        foreach (XmlAttribute attr in node.Attributes)
                        {
                            str = str + attr.Name + " = " + attr.Value + "  ";
                        }
                        Console.WriteLine(str + " >");
                    }
                    else Console.WriteLine("< " + node.Name + " >");

                    // рекурсивная обработка дочерних элементов
                    PrintNode(node);

                    Console.WriteLine("< /" + node.Name + " >");

                }
                if (node.NodeType == XmlNodeType.Text)
                {
                    Console.WriteLine(node.Value);
                }
            }
        }

        static void Main(string[] args)
        {
            // создание нового XML-документа
            XmlDocument doc1 = new XmlDocument();

            // создание и добавление заголовка документа
            XmlDeclaration decl = doc1.CreateXmlDeclaration("1.0", null, null);

            // добавление заголовка XML в документ
            doc1.AppendChild(decl);

            // создание корневого элемента
            XmlElement elem1 = doc1.CreateElement("Elem1");

            // добавление текстового узла
            XmlText text1 = doc1.CreateTextNode("text string");
            elem1.AppendChild(text1);

            // добавление элемента с атрибутом
            XmlElement elem2 = doc1.CreateElement("Elem2");
            XmlAttribute attr1 = doc1.CreateAttribute("attr1");
            attr1.InnerText = "Hello";

            //XmlText text2 = doc1.CreateTextNode("second string");
            //elem2.AppendChild(text2);

            // добавление атрибута к элементу
            elem2.Attributes.Append(attr1);

            // добавление элемента с поддеревом Xml
            XmlElement elem3 = doc1.CreateElement("Elem3");
            elem3.InnerXml = "<Node>Hello world!!!</Node>";
            elem3.SetAttribute("attr2", "OK!");
            elem2.AppendChild(elem3);

            // добавление корневого элемента к документу
            doc1.AppendChild(elem1);

            // добавление дополнительных отступов и переходов на новую строку
            //XmlWhitespace ws = doc1.CreateWhitespace("\r\n");
            //elem1.AppendChild(ws);

            elem1.AppendChild(elem2);

            // сохрание документа Xml
            //doc1.Save("test.xml");

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.IndentChars = "\r\n";
            settings.Indent = true;

            // сохрание документа Xml при помощи XmlWriter
            XmlWriter writer = XmlWriter.Create("test.xml", settings);

            doc1.WriteTo(writer);
            writer.Flush();
            writer.Close();
            
            // ----------------

            // создание пустого документа
            XmlDocument doc2 = new XmlDocument();

            // чтение из файла
            doc2.Load("test.xml");

            // рекурсивный парсинг
            PrintNode(doc2);
        }
    }
}
