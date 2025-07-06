using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    delegate void itemHandler();

    class Element
    {
        public char letter{ get; }
        public string title{ get; }
        public itemHandler handler { get; }

        public Element(char l, string t, itemHandler h)
        {
            letter = l;
            title = t;
            handler = h;
        }
    }
    class Application
    {
        string config = @"C:\Users\Михаил\source\repos\24_Kramarenko\Main\config.xml";
        List<Element> menu;
        PhoneBook PB;

        public Application()
        {
            menu = new List<Element>();

            menu.Add(new Element('1', "Добавить контакт", AddContact));
            menu.Add(new Element('2', "Удалить контакт", DelContact));

            menu.Add(new Element('3', "Показать контакты", ShowContacts));
            menu.Add(new Element('4', "Показать контакты по категориям", ShowContactsCategoty));

            menu.Add(new Element('5', "Добавить категорию контакту", AddCategoryContact));
            menu.Add(new Element('6', "Удалить категорию контакту", DelCategoryContact));

            menu.Add(new Element('7', "Создать категорию", CreateCategory));
            menu.Add(new Element('8', "Удалить категорию", RemoveCategory));

            menu.Add(new Element('9', "Найти контакт", FindContact));
            menu.Add(new Element('s', "FullPrint", FullPrint));
            menu.Add(new Element('x', "Выход", Exit));

            PB = new PhoneBook();
            PB.LoadXML(config);
        }

        void Print()
        {
            foreach (Element item in menu)
            {
                Console.WriteLine($"{item.letter}. {item.title}");
            }
        }

        public void Run()
        {
            Console.CursorVisible = false;

            while (true)
            {
                try
                {
                    Console.Clear();
                    Print();

                    char letter = char.ToLower(Console.ReadKey(true).KeyChar);

                    foreach (Element item in menu)
                    {
                        if(letter == item.letter)
                        {
                            Console.Clear();
                            item.handler();
                            while (Console.ReadKey(true).KeyChar != '\r') { }
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error. {ex.Message}");
                    Console.Write("\n\nДля продолжения нажмите Enter...");
                    Console.CursorVisible = true;
                    while (Console.ReadKey(true).KeyChar != '\r') { }
                    Console.CursorVisible = false;
                }
            }
        }

        void AddContact()
        {
            Console.CursorVisible = true;

            Console.Write("Имя> ");
            string name = Console.ReadLine();
            Console.Write("Номер> ");
            string number = Console.ReadLine();
            Console.Write("Адресс> ");
            string nadressame = Console.ReadLine();
            Console.Write("Описание> ");
            string description = Console.ReadLine();

            Console.WriteLine("\n\nСписок категорий:");
            int cnt = 0;
            foreach (string item in PB.Categories)
            {
                Console.WriteLine($"  [{++cnt}] {item}");
            }
            Console.WriteLine('\n');

            Console.WriteLine("Выберите номера категорий, которые можно присвоить контакту (0 - \"прекратить выбор\"):");
            List<string> category = new List<string>();
            if (PB.Categories.Length > 0)
            {
                cnt = 0;
                while (true)
                {
                    string num = Console.ReadLine();
                    int n;

                    if (!int.TryParse(num, out n) || n > PB.Categories.Length || n < 0)
                    {
                        Console.CursorVisible = false;
                        Console.WriteLine("Неверный ввод\n");
                        continue;
                    }
                    if (n == 0) break;
                    if (category.Select(x => x.ToLower()).Contains(PB.Categories[n - 1].ToLower())) continue;

                    category.Add(PB.Categories[n - 1]);
                    Console.WriteLine($"  {PB.Categories[n - 1]}");
                }
                category.Sort();
            }
            else
            {
                Console.WriteLine("Категории невозможно выбрать т.к. их нет");
                while (Console.ReadKey(true).KeyChar != '\r') { }
            }

            PB.Add(new Contact(name, number, nadressame, description, category.ToArray()));
            Console.CursorVisible = false;

            Console.WriteLine("\n\nКонтакт добавлен");
        }
        void DelContact()
        {
            if (PB.Count == 0)
            {
                Console.WriteLine("Операция невозможна, контактов нет");
            }
            else
            {
                Console.CursorVisible = true;

                PB.Print();
                Console.WriteLine('\n');

                string num;
                int n;
                do
                {
                    Console.Write("Выбирете номер контакта для удаления> ");
                    num = Console.ReadLine();
                } while (!int.TryParse(num, out n) || n > PB.Count || n <= 0);

                PB.RemoveAt(n - 1);
                Console.CursorVisible = false;

                Console.WriteLine("\n\nКонтакт удалён");
            }
        }

        void FullPrint()
        {
            if(PB.Count == 0) Console.WriteLine("Контактов нет");
            else  PB.FullPrint();
        }
        void ShowContacts()
        {
            if (PB.Count == 0) Console.WriteLine("Контактов нет");
            else PB.Print();
        }
        void ShowContactsCategoty()
        {
            if (PB.Count == 0) Console.WriteLine("Контактов нет");
            else PB.PrintCategory();
        }

        void AddCategoryContact()
        {
            if(PB.Count == 0)
            {
                Console.WriteLine("Операция невозможна, контактов нет");
            }
            else
            {
                if (PB.Categories.Length == 0)
                {
                    Console.WriteLine("Операция невозможна, категорий нет");
                }
                else
                {
                    Console.CursorVisible = true;
                    PB.Print();
                    Console.WriteLine('\n');

                    string num;
                    int cont;
                    int n;

                    do
                    {
                        Console.Write("Выбирете номер контакта> ");
                        num = Console.ReadLine();
                    } while (!int.TryParse(num, out n) || n > PB.Count || n <= 0);
                    cont = n;

                    Console.WriteLine('\n');
                    Console.WriteLine("Категории:");
                    int cnt = 0;
                    foreach (string item in PB.Categories)
                    {
                        Console.WriteLine($"  [{++cnt}] {item}");
                    }

                    Console.WriteLine('\n');
                    Console.WriteLine($"Контакт: {PB.Contact(cont - 1).Name}   {PB.Contact(cont - 1).Number}");
                    Console.WriteLine("Категории контакта:");
                    foreach (string item in PB.Contact(cont - 1).Categories)
                    {
                        Console.WriteLine($"  {item}");
                    }
                    Console.WriteLine('\n');

                    if (PB.Contact(cont - 1).Categories.Length == PB.Categories.Length)
                    {
                        Console.WriteLine("Контакту недьзя добавить категории, т.к. у него они все присутствуют.");
                    }
                    else
                    {

                        link:
                        bool first = true;
                        do
                        {
                            if (!first) Console.WriteLine("Неверный ввод\n");
                            else first = false;

                            Console.Write("Выбирете номер категории> ");
                            num = Console.ReadLine();

                        } while (!int.TryParse(num, out n) || n <= 0 || n > PB.Categories.Length);


                        if (PB.Contact(cont - 1).Categories.Contains(PB.Categories[n - 1]))
                        {
                            Console.WriteLine("Такая категория уже в списке\n");
                            goto link; 
                        }

                        PB.AddContactCategory(cont - 1, PB.Categories[n - 1]);
                        Console.WriteLine("\n\nКатегория добавлена");
                    }

                    Console.CursorVisible = false;
                }
            }
        }
        void DelCategoryContact()
        {
            if (PB.Count == 0)
            {
                Console.WriteLine("Операция невозможна, контактов нет");
            }
            else
            {
                Console.CursorVisible = true;
                PB.Print();
                Console.WriteLine('\n');

                string num;
                int cont;
                int n;

                do
                {
                    Console.WriteLine("Выбирете номер контакта> ");
                    num = Console.ReadLine();
                } while (!int.TryParse(num, out n) || n > PB.Count || n <= 0);
                cont = n;

                Console.WriteLine('\n');
                if (PB.Contact(cont-1).Categories.Length == 0)
                {
                    Console.WriteLine("Операция невозможна, у контакта нет категорий");
                }
                else
                {
                    Console.WriteLine($"Контакт: {PB.Contact(cont - 1).Name}   {PB.Contact(cont - 1).Number}");
                    Console.WriteLine("Категории контакта:");
                    int cnt = 0;
                    foreach (string item in PB.Contact(cont - 1).Categories)
                    {
                        Console.WriteLine($"  [{++cnt}] {item}");
                    }
                    Console.WriteLine('\n');

                link:
                    bool first = true;
                    do
                    {
                        if (!first) Console.WriteLine("Неверный ввод\n");
                        else first = false;

                        Console.WriteLine("Выбирете номер категории для удаления> ");
                        num = Console.ReadLine();

                    } while (!int.TryParse(num, out n) || n <= 0 || n > PB.Contact(cont-1).Categories.Length);

                    PB.RemoveContactCategory(cont - 1, PB.Contact(cont - 1).Categories[n-1]);
                    Console.WriteLine("\n\nКатегория удалена");
                }

                Console.CursorVisible = false;
            }
        }

        void CreateCategory()
        {
            Console.CursorVisible = true;
            string[] arr = PB.Categories.Select(x => x.ToLower()).ToArray();
            string title;

            bool first = true;
            do
            {
                if (!first) Console.WriteLine("Такая категория уже существует\n");
                else first = false;

                Console.Write("Название новой категории> ");
                title = Console.ReadLine();
            } while (arr.Contains(title.ToLower()));

            PB.CreateCategory(title);

            Console.WriteLine("\n\nКатегория создана");
            Console.CursorVisible = false;
        }
        void RemoveCategory()
        {
            if (PB.Categories.Length == 0)
            {
                Console.WriteLine("Операция невозможна, категорий нет");
            }
            else
            {
                Console.CursorVisible = true;

                Console.WriteLine("Категории:");
                int cnt = 0;
                foreach (string item in PB.Categories)
                {
                    Console.WriteLine($"  [{++cnt}] {item}");
                }

                string num;
                int n;

                do
                {
                    Console.Write("Введите номер категории для удаления> ");
                    num = Console.ReadLine();
                } while (!int.TryParse(num, out n) || n <= 0 || n > PB.Categories.Length);

                PB.RemoveCategory(PB.Categories[n - 1]);

                Console.WriteLine("\n\nКатегория Удалена");
                Console.CursorVisible = false;
            }
        }

        void Exit()
        {
            PB.SaveXML(config);
            Console.WriteLine("Конец программы\n\n");
            Environment.Exit(0);
        }

        void FindContact()
        {
            if(PB.Count == 0)
            {
                Console.WriteLine("Операция невозможна, контактов нет");
            }
            else
            {
                Console.CursorVisible = true;

                Console.Write("Имя/номер для поиска> ");
                string s = Console.ReadLine();

                List<Contact> lst = PB.Find(s);
                Console.WriteLine('\n');

                int cnt = 0;
                foreach (Contact contact in lst)
                {
                    Console.WriteLine($"[{++cnt}] {contact.Name}   {contact.Number}");
                }


                Console.CursorVisible = false;
            }
        }
    }
}
