using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CS_XmlWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            // класс для записи XML
            XmlWriter writer;

            // настройки записи
            XmlWriterSettings settings = new XmlWriterSettings();

            // перенос на новые строки
            settings.Indent = true;

            // символы перехода на следующую строку
            settings.NewLineChars = "\r\n";

            // кодировка
            settings.Encoding = Encoding.ASCII;

            // переход на новую строку для атрибутов
            settings.NewLineOnAttributes = false;

            // создание нового файла
            writer = XmlWriter.Create("test.xml", settings);

            // вывести заголовок XML
            writer.WriteStartDocument();

            // записать начальный тег элемента
            writer.WriteStartElement("Item1");

            // запись атрибута
            //writer.WriteAttributeString("pref", "attr1","ns", "value");
            writer.WriteAttributeString("attr1", "value");

            // добавляет в поток строку: <City>Donetsk</City>
            writer.WriteElementString("City", "Donetsk");

            writer.WriteStartElement("Item2");

            // добавление строк в отрытый элемент Item2
            writer.WriteString("Item2 text");
            writer.WriteString("Other string");

            // закрытие элемента Item2
            writer.WriteEndElement();

            // добавление текста в элемент Item1
            writer.WriteString("Text string");

            // запись коментария xml
            writer.WriteComment("Comments here...");

            // закрыть элемент Item1
            writer.WriteEndElement();

            // закрытие записи всего документа
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
            
            // объект для чтения XML
            XmlReader reader;

            // открытие существующего файла
            reader = XmlReader.Create("adress.xml");

            // читать файл по одному тегу
            while (reader.Read())
            {
                // проверка разных типов узлов XML

                if (reader.NodeType == XmlNodeType.Comment)
                {
                    Console.WriteLine("<!--" + reader.Value + " -->\r\n");
                }

                if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.XmlDeclaration)
                {
                    // если есть атрибуты в элементе
                    if (reader.HasAttributes)
                    {
                        Console.Write("\n<" + reader.Name + " ");

                        // переместить указатель на чтение атрибутов
                        reader.MoveToFirstAttribute();

                        // вывод данных текущего атрибута
                        Console.Write(reader.Name + " = " + reader.Value + " ");

                        // вывод информации обо всех атрибутах элемента
                        while (reader.MoveToNextAttribute())
                        {
                            Console.Write(reader.Name + " = " + /*reader.Value*/reader[reader.Name] + " ");
                        }

                        // переместить указатель на элменты
                        reader.MoveToElement();

                        // проверка на наличие дочерних элементов
                        if (reader.IsEmptyElement)
                            Console.Write("/>");
                        else
                            Console.Write(">");
                    }
                    else
                    {
                        // проверка на наличие дочерних элементов
                        if (reader.IsEmptyElement)
                        {
                            Console.Write("\n<" + reader.Name + "/>");
                        }
                        else
                            Console.Write("\n<" + reader.Name + ">");
                    }
                }

                if (reader.NodeType == XmlNodeType.Text)
                    Console.Write(reader.Value);

                if (reader.NodeType == XmlNodeType.EndElement)
                    Console.WriteLine("\n</" + reader.Name + ">");
            }
        }
    }
}
