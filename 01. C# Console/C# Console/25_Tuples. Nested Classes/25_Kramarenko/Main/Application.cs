using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    class Application
    {
        string configFile;
        bool showCategory;
        bool isFind;
        string find;

        PhoneBook PB;
        PhoneBook PBFind;

        #region ALL DELEGATE

        delegate void itemHandlerChoose(Contact c);
        delegate void itemHandlerMove(ref int i);
        delegate void itemHandler();
        delegate void itemHandlerContactCategory(ref int i, Contact c);

        #endregion

        #region ALL MENU

        Menu<Element> menu;
        Menu<Move> move;
        Menu<Act> act;

        Menu<CategoryMove> categoryMove;
        Menu<CategoryAct> categoryAct;

        Menu<ContactCategory> contactCategory;
        Menu<ContactCategoryMove> contactCategoryMove;

        #endregion

        #region MENU Classes

        private class Menu<T>
        {
            List<T> menu_;

            public T[] menu { get => menu_.ToArray(); }

            public Menu()
            {
                menu_ = new List<T>();
            }

            public void Add(T elem)
            {
                menu_.Add(elem);
            }
        }


        class Element
        {
            public char letter { get; }
            public string title { get; }
            public itemHandlerChoose handler { get; }

            public Element(char l, string t, itemHandlerChoose h)
            {
                letter = l;
                title = t;
                handler = h;
            }
        }
        class Move
        {
            public ConsoleKey key { get; }
            public itemHandlerMove handler { get; }

            public Move(ConsoleKey k, itemHandlerMove h)
            {
                key = k;
                handler = h;
            }
        }
        class Act
        {
            public char letter { get; }
            public string title { get; }
            public itemHandler handler { get; }

            public Act(char l, string t, itemHandler h)
            {
                letter = l;
                title = t;
                handler = h;
            }
        }

        class CategoryMove
        {
            public ConsoleKey key { get; }
            public itemHandlerMove handler { get; }

            public CategoryMove(ConsoleKey k, itemHandlerMove h)
            {
                key = k;
                handler = h;
            }
        }
        class CategoryAct
        {
            public char letter { get; }
            public string title { get; }
            public itemHandlerMove handler { get; }

            public CategoryAct(char l, string t, itemHandlerMove h)
            {
                letter = l;
                title = t;
                handler = h;
            }
        }

        class ContactCategoryMove
        {
            public ConsoleKey key { get; }
            public itemHandlerContactCategory handler { get; }

            public ContactCategoryMove(ConsoleKey k, itemHandlerContactCategory h)
            {
                key = k;
                handler = h;
            }
        }
        class ContactCategory
        {
            public ConsoleKey key { get; }
            public string title { get; }
            public itemHandlerContactCategory handler { get; }

            public ContactCategory(ConsoleKey k, string t, itemHandlerContactCategory h)
            {
                key = k;
                title = t;
                handler = h;
            }
        }

        #endregion

        public Application(string fName)
        {
            PBFind = new PhoneBook();

            configFile = fName;
            isFind = false;
            find = string.Empty;
            showCategory = false;

            PB = new PhoneBook();
            PB.LoadXML(configFile);

            menu = new Menu<Element>();
            {
                menu.Add(new Element('1', "Удалить контакт", DelContact));

                menu.Add(new Element('2', "Изменить имя", ChangeName));
                menu.Add(new Element('3', "Изменить номер", ChangeNumber));
                menu.Add(new Element('4', "Изменить адрес", ChangeAdress));
                menu.Add(new Element('5', "Изменить описание", ChangeDescription));

                menu.Add(new Element('6', "Изменить категории контакта", ChangeCategoryContact));

                menu.Add(new Element('x', "Назад", ContactExit));
            }
            move = new Menu<Move>();
            {
                move.Add(new Move(ConsoleKey.DownArrow, DownArrow));
                move.Add(new Move(ConsoleKey.UpArrow, UpArrow));
            }
            act = new Menu<Act>();
            {
                act.Add(new Act('1', "Добавить контакт", AddContact));

                act.Add(new Act('2', "Показать контакты по алфавиту/категориям", ChangeShow));
                act.Add(new Act('3', "Категории", Categories));

                act.Add(new Act('4', "Поиск контактов", FindContact));
                act.Add(new Act('5', "Сбросить поиск", ResetFind));

                act.Add(new Act('x', "Выход", Exit));
            }


            categoryMove = new Menu<CategoryMove>();
            {
                categoryMove.Add(new CategoryMove(ConsoleKey.DownArrow, DownCategory));
                categoryMove.Add(new CategoryMove(ConsoleKey.UpArrow, UpCategory));
            }
            categoryAct = new Menu<CategoryAct>();
            {
                categoryAct.Add(new CategoryAct('1', "Создать категорию", CreateCategory));
                categoryAct.Add(new CategoryAct('2', "Удалить категорию", RemoveCategory));

                categoryAct.Add(new CategoryAct('x', "Выйти", ExitCategory));
            }


            contactCategoryMove = new Menu<ContactCategoryMove>();
            {
                contactCategoryMove.Add(new ContactCategoryMove(ConsoleKey.DownArrow, DownContactCategory));
                contactCategoryMove.Add(new ContactCategoryMove(ConsoleKey.UpArrow, UpContactCategory));
            }
            contactCategory = new Menu<ContactCategory>();
            {
                contactCategory.Add(new ContactCategory(ConsoleKey.DownArrow, "", DownContactCategory));
                contactCategory.Add(new ContactCategory(ConsoleKey.UpArrow, "", UpContactCategory));
                contactCategory.Add(new ContactCategory(ConsoleKey.Spacebar, "Выбрать/убрать", SpacebarContactCategory));
                contactCategory.Add(new ContactCategory(ConsoleKey.Enter, "Сохранить и выйти", ExitContactCategory));
            }
        }

        string setw(int w, string str, bool site = false, char symbol = ' ')
        {
            string space = string.Empty;
            for (int i = 0; i < w - str.Length; i++) space += symbol;

            if (site) return str += space;
            else return space += str;
        }
        int GetW(params string[] arr)
        {
            return arr.Length == 0 ? 0 : arr.OrderBy(x => x.Length).Last().Length;
        }

        void DownArrow(ref int i)
        {
            i++;
            if (showCategory)
            {
                int max = 0;

                foreach (Contact item in PB.Contacts) max += item.Categories.Length;

                if (i >= max) i = 0;
            }
            else if (isFind)
            {
                if (i >= PBFind.Count) i = 0;
            }
            else
            {
                if (i >= PB.Count) i = 0;
            }
        }
        void UpArrow(ref int i)
        {
            if (--i < 0)
            {
                if (showCategory)
                {
                    int max = 0;

                    foreach (Contact item in PB.Contacts) max += item.Categories.Length;

                    i = max - 1;
                }
                else if (isFind)
                {
                    i = PBFind.Count - 1;
                }
                else
                {
                    i = PB.Count - 1;
                }
            }
            if (i < 0) i = 0;
        }

        void PrintContactMenu(int x, int y)
        {
            int w = GetW(menu.menu.Select(s => s.letter + ": " + s.title).ToArray());
            if (w < 4) w = 4;

            Console.SetCursorPosition(x, y++);
            Console.Write("+-" + setw(w, "", false, '-') + "-+");
            Console.SetCursorPosition(x, y++);
            Console.Write("| " + setw(w / 2 - 2, "") + "МЕНЮ" + setw(w / 2 - 2 + (w % 2 != 0 ? 1 : 0), "") + " |");
            Console.SetCursorPosition(x, y++);
            Console.Write("+-" + setw(w, "", false, '-') + "-+");

            if (menu.menu.Length == 0)
            {
                Console.SetCursorPosition(x, y++);
                Console.Write("| " + setw(w, "") + " |");
            }
            foreach (Element item in menu.menu)
            {
                Console.SetCursorPosition(x, y++);
                Console.Write("| " + setw(w, item.letter + ": " + item.title, true) + " |");
            }

            Console.SetCursorPosition(x, y++);
            Console.Write("+-" + setw(w, "", false, '-') + "-+");
        }
        void PrintContact(Contact c)
        {
            int w1 = 9;
            int w2 = GetW(c.Name, c.Number, c.Adress);
            {
                int tmp = GetW(c.Categories);
                if (w2 < tmp) w2 = tmp;
            }
            if (w2 < 5) w2 = 5;
            if (c.Description.Length > w2)
            {
                if (c.Description.Length < 20) w2 = c.Description.Length;
                else w2 = 20;
            }
            int w = w1 + w2 + 3;

            string line = $"+-{setw(w1, "", true, '-')}-+-{setw(w2, "", true, '-')}-+";

            Console.WriteLine();

            Console.WriteLine($"\t+{setw(w + 2, "", true, '-')}+");
            Console.WriteLine($"\t| {setw(w / 2 - 5, "")}ИНФОРМАЦИЯ{setw(w / 2 - 5 + (w % 2 == 0 ? 0 : 1), "")} |");
            Console.WriteLine($"\t{line}");

            Console.WriteLine($"\t| {setw(w1, "Имя", true)} | {setw(w2, c.Name, true)} |");
            Console.WriteLine($"\t{line}");

            Console.WriteLine($"\t| {setw(w1, "Номер", true)} | {setw(w2, c.Number, true)} |");
            Console.WriteLine($"\t{line}");

            Console.WriteLine($"\t| {setw(w1, "Адрес", true)} | {setw(w2, c.Adress, true)} |");
            Console.WriteLine($"\t{line}");

            if (c.Categories.Length == 0)
            {
                Console.WriteLine($"\t| {setw(w1, "Категории", true)} | {setw(w2, "")} |");
            }
            else
            {
                bool first = true;
                foreach (string item in c.Categories)
                {
                    if (first) { Console.WriteLine($"\t| {setw(w1, "Категории", true)} | {setw(w2, item, true)} |"); first = false; }
                    else Console.WriteLine($"\t| {setw(w1, "")} | {setw(w2, item, true)} |");
                }
            }
            Console.WriteLine($"\t{line}");

            if (c.Description.Length <= w2)
            {
                Console.WriteLine($"\t| {setw(w1, "Описание", true)} | {setw(w2, c.Description, true)} |");
            }
            else
            {
                string[] words = c.Description.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                int start = 0;
                List<int> wordsIndexes = new List<int>();
                bool first = true;

                string res = string.Empty;
                while (start < words.Length)
                {
                    if ((res + words[start] + (res.Length == 0 ? "" : " ")).Length <= w2) res += (res.Length == 0 ? "" : " ") + words[start++];
                    else
                    {
                        if (res.Length == 0)
                        {
                            res = words[start].Remove(w2);
                            words[start] = words[start].Remove(0, w2);
                        }
                        if (first) { Console.WriteLine($"\t| {setw(w1, "Описание", true)} | {setw(w2, res, true)} |"); first = false; }
                        else Console.WriteLine($"\t| {setw(w1, "")} | {setw(w2, res, true)} |");
                        res = string.Empty;
                    }
                }
                if (res.Length != 0)
                {
                    if (first) Console.WriteLine($"\t| {setw(w1, "Описание", true)} | {setw(w2, res, true)} |");
                    else Console.WriteLine($"\t| {setw(w1, "")} | {setw(w2, res, true)} |");
                }
            }
            Console.WriteLine($"\t{line}");
        }
        void ChooseContact(Contact contact)
        {
            bool flag = true;
            while (true)
            {
                if (flag)
                {
                    Console.Clear();
                    PrintContact(contact);
                    PrintContactMenu(Console.WindowWidth - 35, 0);
                    flag = false;
                }

                char letter = char.ToLower(Console.ReadKey(true).KeyChar);
                if (letter == 'x') break;
                foreach (Element item in menu.menu)
                {
                    if (letter == item.letter)
                    {
                        item.handler(contact);
                        if (item.handler == DelContact) return;
                        flag = true;
                        break;
                    }
                }
            }
        }

        void PrintActMenu(int x, int y)
        {
            int w = GetW(act.menu.Select(a => a.letter + ": " + a.title).ToArray());
            if (w < 4) w = 4;

            Console.SetCursorPosition(x, y++);
            Console.Write("+-" + setw(w, "", false, '-') + "-+");
            Console.SetCursorPosition(x, y++);
            Console.Write("| " + setw(w / 2 - 2, "") + "МЕНЮ" + setw(w / 2 - 2 + (w % 2 != 0 ? 1 : 0), "") + " |");
            Console.SetCursorPosition(x, y++);
            Console.Write("+-" + setw(w, "", false, '-') + "-+");

            if (act.menu.Length == 0)
            {
                Console.SetCursorPosition(x, y++);
                Console.Write("| " + setw(w, "") + " |");
            }
            foreach (Act item in act.menu)
            {
                Console.SetCursorPosition(x, y++);
                Console.Write("| " + setw(w, item.letter + ": " + item.title, true) + " |");
            }

            Console.SetCursorPosition(x, y++);
            Console.Write("+-" + setw(w, "", false, '-') + "-+");
            Console.SetCursorPosition(0, 1);
        }
        void PrintContacts(int index)
        {
            int wCnt = PB.Count.ToString().Length;
            int wName = GetW(PB.Contacts.Select(x => x.Name).ToArray());
            int wNum = GetW(PB.Contacts.Select(x => x.Number).ToArray());

            if (wCnt < 1) wCnt = 1;
            if (wName < 5) wName = 5;
            if (wNum < 5) wNum = 5;

            int w = wCnt + wName + wNum + 6;

            string line = $"\t+-{setw(wCnt, "", true, '-')}-+-{setw(wName, "", true, '-')}-+-{setw(wNum, "", true, '-')}-+";

            Console.WriteLine($"\t+{setw(w + 2, "", true, '-')}+");
            Console.WriteLine($"\t| {setw(w / 2 - 4, "")}КОНТАКТЫ{setw(w / 2 - 4 + (w % 2 != 0 ? 1 : 0), "")} |");

            if (PB.Contacts.Length == 0)
            {
                Console.WriteLine($"\t+{setw(w + 2, "", true, '-')}+");
                Console.WriteLine($"\t|{setw((w + 2) / 2 - 2 - (w % 2 == 0 ? 1 : 0), "")}ПУСТО{setw((w + 2) / 2 - 2, "")}|");
                Console.WriteLine($"\t+{setw(w + 2, "", true, '-')}+");
            }
            else
            {
                Console.WriteLine(line);

                int cnt = 0;
                foreach (Contact item in PB.Contacts)
                {
                    Console.Write('\t');
                    if (cnt == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }

                    Console.WriteLine($"| {setw(wCnt, (cnt + 1).ToString(), true)} | {setw(wName, item.Name, true)} | {setw(wNum, item.Number, true)} |");

                    if (cnt++ == index)
                    {
                        Console.ResetColor();
                    }

                    Console.WriteLine(line);
                }
            }
        }
        void PrintContactsFind(int index)
        {
            int wCnt = PBFind.Count.ToString().Length;
            int wName = GetW(PBFind.Contacts.Select(x => x.Name).ToArray());
            int wNum = GetW(PBFind.Contacts.Select(x => x.Number).ToArray());

            if (wCnt < 1) wCnt = 1;
            if (wName < 5) wName = 5;
            if (wNum < 5) wNum = 5;

            int w = wCnt + wName + wNum + 6;

            string line = $"\t+-{setw(wCnt, "", true, '-')}-+-{setw(wName, "", true, '-')}-+-{setw(wNum, "", true, '-')}-+";

            Console.WriteLine($"\t+{setw(w + 2, "", true, '-')}+");
            Console.WriteLine($"\t| {setw(w / 2 - 7 - (w % 2 == 1 ? 0 : 1), "")}КОНТАКТЫ(ПОИСК){setw(w / 2 - 7, "")} |");

            if (PBFind.Contacts.Length == 0)
            {
                Console.WriteLine($"\t+{setw(w + 2, "", true, '-')}+");
                Console.WriteLine($"\t| {setw(w / 2 - 2 - (w % 2 == 0 ? 1 : 0), "")}ПУСТО{setw(w / 2 - 2, "")} |");
                Console.WriteLine($"\t+{setw(w + 2, "", true, '-')}+");
            }
            else
            {
                Console.WriteLine(line);

                int cnt = 0;
                foreach (Contact item in PBFind.Contacts)
                {
                    Console.Write('\t');
                    if (cnt == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }

                    Console.WriteLine($"| {setw(wCnt, (cnt + 1).ToString(), true)} | {setw(wName, item.Name, true)} | {setw(wNum, item.Number, true)} |");

                    if (cnt++ == index)
                    {
                        Console.ResetColor();
                    }

                    Console.WriteLine(line);
                }
            }
        }
        void PrintContactsByCategory(int index)
        {
            int wCnt = PB.Contacts.Select(x => x.Categories.Length).Sum().ToString().Length;
            int wName = GetW(PB.Contacts.Select(x => x.Name).ToArray());
            int wNum = GetW(PB.Contacts.Select(x => x.Number).ToArray());

            if (wCnt < 1) wCnt = 1;
            if (wName < 5) wName = 5;
            if (wNum < 5) wNum = 5;

            int w = wCnt + wName + wNum + 4;

            string line = $"\t+-{setw(wCnt, "", true, '-')}-+-{setw(wName, "", true, '-')}-+-{setw(wNum, "", true, '-')}-+";

            Console.WriteLine($"\t+{setw(w + 4, "", true, '-')}+");
            Console.WriteLine($"\t| {setw(w / 2 - 3, "")}КОНТАКТЫ{setw(w / 2 - 3 + (w % 2 != 0 ? 1 : 0), "")} |");
            Console.WriteLine($"\t+{setw(w + 4, "", true, '-')}+");

            if (PB.Contacts.Length == 0)
            {
                Console.WriteLine($"\t|       ПУСТО       |");
                Console.WriteLine($"\t+{setw(w + 2 + 2, "", true, '-')}+");
            }
            else if (PB.Categories.Length == 0)
            {
                Console.WriteLine($"\t| {setw((w + 2) / 2 - 6 - (w % 2 == 0 ? 1 : 0), "")}НЕТ КАТЕГОРИЙ{setw((w + 2) / 2 - 6, "")} |");
                Console.WriteLine($"\t+{setw(w + 4, "", true, '-')}+");
            }
            else
            {
                int cnt = 0;
                foreach (string categoty in PB.Categories)
                {
                    Console.Write('\t');
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"| {setw(w + 2, categoty, true)} |");
                    Console.ResetColor();

                    List<Contact> lst = PB.GetContactsByCategory(categoty);

                    if (lst.Count() > 0)
                    {
                        Console.WriteLine(line);
                        foreach (Contact contact in lst)
                        {
                            Console.Write('\t');
                            if (cnt == index)
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.BackgroundColor = ConsoleColor.Gray;
                            }

                            Console.WriteLine($"| {setw(wCnt, (cnt + 1).ToString(), true)} | {setw(wName, contact.Name, true)} | {setw(wNum, contact.Number, true)} |");

                            if (cnt++ == index)
                            {
                                Console.ResetColor();
                            }

                            Console.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\t+{setw(w + 4, "", true, '-')}+");
                        Console.WriteLine($"\t| {setw(w + 2, "")} |");
                        Console.WriteLine($"\t+{setw(w + 4, "", true, '-')}+");
                    }
                }
            }
        }
        void Print(int index)
        {
            PrintActMenu(Console.WindowWidth - 50, 0);

            if (showCategory) PrintContactsByCategory(index);
            else if (isFind) PrintContactsFind(index);
            else PrintContacts(index);
        }

        public void Run()
        {
            Console.CursorVisible = false;
            int index = 0;
            bool flag = true;

            while (true)
            {
                try
                {
                    if (flag)
                    {
                        Console.Clear();
                        Print(index);
                        flag = false;
                    }

                    ConsoleKeyInfo key = Console.ReadKey(true);


                    if (key.Key == ConsoleKey.Enter)
                    {
                        if (PB.Count == 0 || PB.Categories.Length == 0) continue;
                        Contact c = null;
                        if (showCategory)
                        {
                            int tmpIndex = index;
                            foreach (string categoty in PB.Categories)
                            {
                                List<Contact> lst = PB.GetContactsByCategory(categoty);

                                if (tmpIndex >= lst.Count()) tmpIndex -= lst.Count();
                                else
                                {
                                    c = lst.ElementAt(tmpIndex);
                                    break;
                                }
                            }
                        }
                        else if (isFind)
                        {
                            c = PBFind.Contact(index);
                        }
                        else
                        {
                            c = PB.Contact(index);
                        }

                        Console.Clear();
                        ChooseContact(c);
                        CheckIndex(PB.Count, ref index);
                        flag = true;
                    }

                    if (!flag)
                    {
                        foreach (Move item in move.menu)
                        {
                            if (item.key == key.Key)
                            {
                                item.handler(ref index);

                                flag = true;
                                break;
                            }
                        }

                        if (!flag)
                        {
                            foreach (Act item in act.menu)
                            {
                                if (key.KeyChar == item.letter)
                                {
                                    Console.Clear();
                                    item.handler();
                                    if ("245".Contains(item.letter)) ResetIndex(ref index);
                                    flag = true;
                                    break;
                                }
                            }
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

        void DelContact(Contact c)
        {
            PB.Remove(c);
            if (isFind) PBFind.Remove(c);
        }
        void ChangeName(Contact c)
        {
            Console.CursorVisible = true;
            Console.Clear();
            string name;

            do
            {
                Console.Write("Имя> ");
                name = Console.ReadLine();
            } while (name.Length == 0);

            c.Name = name;
            Console.CursorVisible = false;
        }
        void ChangeNumber(Contact c)
        {
            Console.CursorVisible = true;
            Console.Clear();
            string number;

            do
            {
                Console.Write("Номер> ");
                number = Console.ReadLine();
            } while (number.Length == 0 || !CheckPhoneNumber(number));

            c.Number = number;
            Console.CursorVisible = false;
        }
        void ChangeAdress(Contact c)
        {
            Console.CursorVisible = true;
            Console.Clear();
            string adress;

            Console.Write("Адресс> ");
            adress = Console.ReadLine();

            c.Adress = adress;
            Console.CursorVisible = false;
        }
        void ChangeDescription(Contact c)
        {
            Console.CursorVisible = true;
            Console.Clear();
            string description;

            Console.Write("Описание> ");
            description = Console.ReadLine();

            c.Description = description;
            Console.CursorVisible = false;
        }

        bool CheckPhoneNumber(string num)
        {
            if (num.Length == 1 && num[0] == '+') return false;

            for (int i = 0; i < num.Length; i++)
            {
                if (!char.IsDigit(num[i]))
                {
                    if (i == 0 && num[i] == '+') continue;
                    return false;
                }
            }
            return true;
        }

        void PrintContactCategoryMenu()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;

            Console.Write("|");
            foreach (ContactCategory item in contactCategory.menu)
            {
                if (item.title.Length != 0)
                {
                    int last = item.key.ToString().LastIndexOf('.');
                    Console.Write($" {item.key.ToString().Remove(0, (last == -1 ? 0 : last))} - {item.title} |");
                }
            }
            Console.WriteLine("\n\n");
            Console.ResetColor();
        }
        void PrintContactCategory(int index, int[] indexes)
        {
            int w = GetW(PB.Categories);
            if (w < 18) w = 18;

            Console.WriteLine($"+{setw(w + 2, "", true, '-')}+");
            Console.WriteLine($"| {setw(w / 2 - 9, "")}КАТЕГОРИИ КОНТАКТА{setw(w / 2 - 9 + (w % 2 == 0 ? 0 : 1), "")} |");
            Console.WriteLine($"+{setw(w + 2, "", true, '-')}+");
            if (PB.Categories.Length == 0)
            {
                Console.WriteLine($"|{setw(w + 2, "")}|");
            }
            else
            {
                int cnt = 0;
                foreach (string item in PB.Categories)
                {
                    if (cnt == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    Console.Write($"| {setw(w, item, true)} |");
                    if (cnt == index)
                    {
                        Console.ResetColor();
                    }
                    if (indexes.Contains(cnt++)) Console.WriteLine(" +");
                    else Console.WriteLine();
                }
            }
            Console.WriteLine($"+{setw(w + 2, "", true, '-')}+");

        }
        void DownContactCategory(ref int i, Contact c)
        {
            if (++i >= PB.Categories.Length) i = 0;
        }
        void UpContactCategory(ref int i, Contact c)
        {
            if (--i < 0) i = PB.Categories.Length - 1;
            if (i < 0) i = 0;
        }
        void SpacebarContactCategory(ref int i, Contact c)
        {
            if (c.Categories.Contains(PB.Categories[i])) c.RemoveCategory(PB.Categories[i]);
            else c.AddCategory(PB.Categories[i]);
        }
        void ExitContactCategory(ref int i, Contact c)
        {
        }

        void ChangeCategoryContact(Contact c)
        {
            if (PB.Categories.Length == 0)
            {
                Console.WriteLine("Операция невозможна, категорий нет");
                while (Console.ReadKey(true).KeyChar != '\r') { }
            }
            else
            {
                bool flag = true;
                int index = 0;
                while (true)
                {
                    if (flag)
                    {
                        Console.Clear();
                        PrintContactCategoryMenu();
                        PrintContactCategory(index, c.CategoryIndexes(PB.Categories));

                        flag = false;
                    }

                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter) break;
                    foreach (ContactCategory item in contactCategory.menu)
                    {
                        if (item.key == key.Key)
                        {
                            item.handler(ref index, c);
                            flag = true;
                            break;
                        }
                    }
                }
            }
        }

        void ContactExit(Contact c)
        {
        }


        void AddContact()
        {
            Console.CursorVisible = true;

            string name;
            string number;
            string adress;
            string description;

            do
            {
                Console.Write("Имя> ");
                name = Console.ReadLine();
            } while (name.Length == 0);

            do
            {
                Console.Write("Номер> ");
                number = Console.ReadLine();
            } while (number.Length == 0 || !CheckPhoneNumber(number));

            Console.Write("Адрес> ");
            adress = Console.ReadLine();

            Console.Write("Описание> ");
            description = Console.ReadLine();

            Contact NEW = new Contact(name, number, adress, description, new string[] { });

            Console.CursorVisible = false;
            if (PB.Categories.Length != 0)
            {
                Console.WriteLine('\n');
                bool flag = true;
                int index = 0;

                while (true)
                {
                    if (flag)
                    {
                        Console.Clear();
                        Console.WriteLine($"Имя> {name}");
                        Console.WriteLine($"Номер> {number}");
                        Console.WriteLine($"Адрес> {adress}");
                        Console.WriteLine($"Описание {description}\n\n");

                        PrintContactCategoryMenu();
                        PrintContactCategory(index, NEW.CategoryIndexes(PB.Categories));

                        flag = false;
                    }

                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter) break;

                    foreach (ContactCategory item in contactCategory.menu)
                    {
                        if (item.key == key.Key)
                        {
                            item.handler(ref index, NEW);

                            flag = true;
                            break;
                        }
                    }
                }
            }

            PB.Add(NEW);
            if (isFind)
            {
                if (NEW.Name.ToLower().Contains(find.ToLower()) || NEW.Number.Contains(find)) PBFind.Add(NEW);
            }
        }
        void ChangeShow()
        {
            showCategory = !showCategory;
        }

        void DownCategory(ref int i)
        {
            if (++i >= PB.Categories.Length) i = 0;
        }
        void UpCategory(ref int i)
        {
            if (--i < 0) i = PB.Categories.Length - 1;
            if (i < 0) i = 0;
        }

        void PrintCategoryAct()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;

            Console.Write("|");
            foreach (CategoryAct item in categoryAct.menu)
            {
                Console.Write($" {item.letter} - {item.title} |");
            }
            Console.WriteLine("\n\n");
            Console.ResetColor();
        }
        void PrintCategories(int index)
        {
            int w = GetW(PB.Categories);
            if (w < 9) w = 9;
            string line = $"+{setw(w + 2, "", true, '-')}+";

            Console.WriteLine($"\t{line}");
            Console.WriteLine($"\t| {setw(w / 2 - 4 - (w % 2 == 0 ? 1 : 0), "")}КАТЕГОРИИ{setw(w / 2 - 4, "")} |");
            Console.WriteLine($"\t{line}");

            if (PB.Categories.Length == 0)
            {
                Console.WriteLine($"\t|{setw(w + 2, "")}|");
                Console.WriteLine($"\t{line}");
            }
            else
            {
                int cnt = 0;
                foreach (string item in PB.Categories)
                {
                    Console.Write('\t');
                    if (cnt == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    Console.WriteLine($"| {setw(w, item, true)} |");
                    if (cnt++ == index)
                    {
                        Console.ResetColor();
                    }
                    Console.WriteLine($"\t{line}");
                }
            }
        }
        void Categories()
        {
            bool flag = true;
            int index = 0;

            while (true)
            {
                if (flag)
                {
                    Console.Clear();
                    PrintCategoryAct();
                    PrintCategories(index);
                    flag = false;
                }

                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.X) return;

                foreach (CategoryMove item in categoryMove.menu)
                {
                    if (key.Key == item.key)
                    {
                        item.handler(ref index);

                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    char letter = char.ToLower(key.KeyChar);
                    foreach (CategoryAct item in categoryAct.menu)
                    {
                        if (letter == item.letter)
                        {
                            item.handler(ref index);

                            flag = true;
                            break;
                        }
                    }
                }
            }
        }


        void CreateCategory(ref int i)
        {
            Console.WriteLine('\t');
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
            } while (title.Length == 0 || arr.Contains(title.ToLower()));

            PB.CreateCategory(title);
            Console.CursorVisible = false;
        }
        void RemoveCategory(ref int i)
        {
            if (i >= 0 && i < PB.Categories.Length)
            {
                PB.RemoveCategory(PB.Categories[i]);
                CheckIndex(PB.Categories.Length, ref i);
            }
        }
        void ExitCategory(ref int i)
        {
        }


        void Exit()
        {
            PB.SaveXML(configFile);
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(46, 12);
            Console.Write("                         ");

            Console.SetCursorPosition(46, 13);
            Console.Write("   Программа завершена   ");

            Console.SetCursorPosition(46, 14);
            Console.Write("                         ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
            Environment.Exit(0);
        }

        void FindContact()
        {
            if (PB.Count == 0)
            {
                Console.WriteLine("Операция невозможна, контактов нет");
                while (Console.ReadKey(true).KeyChar != '\r') { }
            }
            else
            {
                Console.CursorVisible = true;

                do
                {
                    Console.Write("Имя/номер для поиска> ");
                    find = Console.ReadLine();
                } while (find.Length == 0);

                if (PBFind.Count != 0) PBFind.Clear();
                PBFind.AddRange(PB.Find(find).ToArray());

                isFind = true;
                Console.CursorVisible = false;
            }
        }
        void ResetFind()
        {
            isFind = false;
            find = string.Empty;
            PBFind.Clear();
        }
        void ResetIndex(ref int i)
        {
            i = 0;
        }
        void CheckIndex(int n, ref int i)
        {
            if (n == 0) i = 0;
            else if (i >= n) i = n - 1;
        }
    }
}
