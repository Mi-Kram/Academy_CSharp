using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Main
{
    class Category
    {
        List<string> category;

        public string[] List { get => category.ToArray(); }

        public Category()
        {
            category = new List<string>();
            category.Add("Family");
            category.Add("Friend");
            category.Add("Work");
        }

        public void Add(string s)
        {
            if(s == null && s.Length == 0)
            {
                throw new Exception("Wrong category");
            }
            else if (category.Find(x=>x.ToLower() == s.ToLower()) == null)
            {
                category.Add(s);
                category.Sort();
            }
            else
            {
                throw new Exception("This category already exists");
            }
        }
        public void Del(string s)
        {
            if (s == null && s.Length == 0)
            {
                throw new Exception("Wrong category");
            }
            else if (category.Find(x => x.ToLower() == s.ToLower()).Length != 0)
            {
                category.RemoveAll(x => x.ToLower() == s.ToLower());
            }
            else
            {
                throw new Exception("This category was not found");
            }
        }

        public void Clear()
        {
            category.Clear();
        }
    }

    class Contact
    {
        string name;
        string adress;
        List<string> categories;
        string number;
        string description;

        public string Name { get => name; }
        public string Adress { get => adress; }
        public string Number { get => number; }
        public string Description { get => description; }
        public string[] Categories { get => categories.ToArray(); }

        public Contact(string Name, string Number, string Adress, string Description, params string[] Categories)
        {
            categories = new List<string>();

            name = Name;
            number = Number;
            adress = Adress;
            description = Description == null ? string.Empty : Description;

            foreach (string Category in Categories)
            {
                categories.Add(Category);
            }
        }

        public void AddCategory(string s)
        {
            categories.Add(s);
            categories.Sort();
        }
        public void RemoveCategory(string s)
        {
            categories.Remove(s);
        }
        public bool ExistCategory(string s)
        {
            return categories.Select(x => x.ToLower()).Contains(s.ToLower());
        }
    }

    class PhoneBook
    {
        List<Contact> contacts;
        Category categories;

        public string[] Categories { get => categories.List; }
        public int Count { get => contacts.Count(); }
        public Contact Contact(int index) => contacts.ElementAt(index); 

        public PhoneBook()
        {
            contacts = new List<Contact>();
            categories = new Category();
        }

        public void Add(Contact contact)
        {
            contacts.Add(contact);
            contacts = contacts.OrderBy(x => x.Name).ToList();
        }
        public void RemoveAt(int index)
        {
            contacts.RemoveAt(index);
        }

        public void AddContactCategory(int index, string s)
        {
            contacts[index]?.AddCategory(s);
        }
        public void RemoveContactCategory(int index, string s)
        {
            contacts[index]?.RemoveCategory(s);
        }

        public void CreateCategory(string s)
        {
            categories.Add(s);
        }
        public void RemoveCategory(string s)
        {
            if(categories.List.Select(x=> x.ToLower()).Contains(s.ToLower()))
            {
                categories.Del(s);

                foreach (Contact contact in contacts)
                {
                    if (contact.ExistCategory(s)) contact.RemoveCategory(s);
                }
            }
        }

        public void FullPrint()
        {
            int cnt = 0;
            foreach (Contact contact in contacts)
            {
                Console.WriteLine($"{++cnt}. контакт:");
                Console.WriteLine($"имя = {contact.Name}");
                Console.WriteLine($"номер = {contact.Number}");
                Console.WriteLine($"адрес = {contact.Adress}");
                Console.WriteLine($"описание: \n{contact.Description}");
                Console.WriteLine("категории:");
                int cnt2 = 0;
                foreach (string item in contact.Categories)
                {
                    Console.WriteLine($"  {item}");
                }
                Console.WriteLine('\n');
            }
        }
        public void Print()
        {
            int cnt = 0;
            foreach (Contact contact in contacts)
            {
                Console.WriteLine($"[{++cnt}] {contact.Name}   {contact.Number}");
            }
        }
        public void PrintCategory()
        {
            foreach (string categoty in categories.List)
            {
                List<Contact> lst = GetContactsByCategory(categoty);

                if (lst.Count() > 0)
                {
                    Console.WriteLine($"{categoty}:");
                    int cnt = 0;
                    foreach (Contact contact in lst)
                    {
                        Console.WriteLine($"   [{++cnt}] {contact.Name}   {contact.Number}");
                    }
                    Console.WriteLine();
                }
            }
        }
        private List<Contact> GetContactsByCategory(string s)
        {
            List<Contact> lst = new List<Contact>();
            foreach (Contact contact in contacts)
            {
                if (contact.ExistCategory(s)) lst.Add(contact);
            }
            return lst;
        }

        public List<Contact> Find(string s)
        {
            //s = s.ToLower();

            //List<Contact> result = new List<Contact>();
            //foreach (Contact contact in contacts)
            //{
            //    if (contact.Name.ToLower().Contains(s) || contact.Number.Contains(s)) result.Add(contact);
            //}

            //return result;

            return contacts.Where(x => x.Name.ToLower().Contains(s.ToLower()) || x.Number.Contains(s)).ToList();
        }


        public void SaveXML(string fName)
        {
            if (File.Exists(fName))
            {

                XmlDocument doc = new XmlDocument();
                XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "windows-1251", null);

                doc.AppendChild(decl);

                XmlElement root = doc.CreateElement("PhoneBook");
                XmlElement cont = doc.CreateElement("Contacts");
                XmlElement categ = doc.CreateElement("Categories");
                int cnt = 0;
                foreach (Contact contact in contacts)
                {
                    XmlElement elem = doc.CreateElement("contact");
                    elem.SetAttribute("name", contact.Name);
                    elem.SetAttribute("number", contact.Number);
                    elem.SetAttribute("adress", contact.Adress);

                    XmlElement c = doc.CreateElement("category");
                    cnt = 0;
                    foreach (string item in contact.Categories)
                    {
                        c.SetAttribute($"title{++cnt}", item);
                    }

                    elem.AppendChild(c);
                    elem.AppendChild(doc.CreateTextNode(contact.Description));
                    cont.AppendChild(elem);
                }
                cnt = 0;
                foreach (string categoty in categories.List)
                {
                    categ.SetAttribute($"title{++cnt}", categoty);
                }

                root.AppendChild(categ);
                root.AppendChild(cont);
                doc.AppendChild(root);


                XmlWriterSettings setting = new XmlWriterSettings();
                setting.Indent = true;
                setting.NewLineOnAttributes = true;

                try
                {
                    XmlWriter writer = XmlWriter.Create(fName, setting);
                    doc.WriteTo(writer);
                    writer.Flush();
                    writer.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void LoadXML(string fName)
        {
            if (File.Exists(fName))
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(fName);

                    contacts.Clear();
                    categories.Clear();
                    ParseXML(doc);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        void ParseXML(XmlNode node)
        {
            foreach (XmlNode item in node.ChildNodes)
            {
                if (item.NodeType == XmlNodeType.Element) {
                    if (item.Name == "contact")
                    {
                        string name = string.Empty;
                        string number = string.Empty;
                        string adress = string.Empty;
                        string description = string.Empty;
                        List<string> category = new List<string>();

                        foreach (XmlAttribute attribute in item.Attributes)
                        {
                            switch (attribute.Name)
                            {
                                case "name": name = attribute.Value; break;
                                case "number": number = attribute.Value; break;
                                case "adress": adress = attribute.Value; break;
                                default: break;
                            }

                        }

                        foreach (XmlNode n in item)
                        {
                            if(n.NodeType == XmlNodeType.Element && n.Name == "category")
                            {
                                foreach (XmlAttribute attribute in n.Attributes)
                                {
                                    category.Add(attribute.Value);
                                }
                            }
                            else if(n.NodeType == XmlNodeType.Text)
                            {
                                description = n.Value;
                            }
                        }

                        contacts.Add(new Contact(name, number, adress, description, category.ToArray()));
                    }
                    else if (item.Name == "Categories")
                    {
                        foreach (XmlAttribute category in item.Attributes)
                        {
                            categories.Add(category.Value);
                        }
                    }
                    else ParseXML(item);
                }
            }
        }
    }
}
