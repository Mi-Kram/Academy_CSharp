using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

class MyArrayList : ArrayList
{
    public void print()
    {
        Console.WriteLine("!!!!!!!!!!!!!");
        ((Car)this[0]).Print();
        Console.WriteLine("!!!!!!!!!!!!!");
    }
}

class Car: IComparable
{
    string marka;
    string model;
    int speed;

    public Car(string marka, string model, int speed)
    {
        this.marka = marka;
        this.model = model;
        this.speed = speed;
    }

    public void Print()
    {
        Console.WriteLine("Brand: {0}\tModel: {1}\tSpeed: {2}", marka, model, speed);
    }
    
    int IComparable.CompareTo(object obj)
    {
        return string.Compare(this.marka, ((Car)obj).marka);
        //return this.speed - ((Car)obj).speed;

        //if (this.marka.Equals(((Car)obj).marka))
        //{
        //    return this.speed - ((Car)obj).speed;
        //}
        //else return string.Compare(this.marka, ((Car)obj).marka);
    }
}

class Sample
{
    static void Main()
    {
        ArrayList ar = new ArrayList();

        Console.WriteLine(ar.ToString());

        //Car car1 = new Car("BMW", "Z8", 250);
        //ar.Add(car1);  // Добавление элемента в конец

        ar.Add(new Car("BMW", "Z8", 250));  // Добавление элемента в конец

        Car[] cars = {
            new Car("BMW", "M5", 230),
            new Car("Ford", "Fiesta", 170),
            new Car("Opel", "Astra", 200),
            new Car("Ford", "Escort", 200),
            new Car("Dodge", "Viper", 230),
            new Car("Opel", "Omega", 190),
            new Car("Ford", "Scorpio", 220)
         };

        ar.AddRange(cars);                  // Добавление массива элементов

        //ar.AddRange(new Car[] { new Car("BMW", "M5", 230),  new Car("Ford", "Fiesta", 170)});

        foreach (Car b in ar)               // Просмотр всех элементов
        {
            b.Print();
        }

        Console.WriteLine("\n");

        ar.RemoveAt(0);                         // Удаление первого элемента
        ar.Sort();                              // Сортировка элементов

        Car[] cars2 = (Car[])ar.ToArray(typeof(Car)); // Преобразование коллекции в массив

        foreach (Car b in cars2)
        {
            b.Print();
        }

        Console.WriteLine("\n Reverse:");

        ar.Reverse(0, 3);                   // Реверс первых 3 элементов

        foreach (Car b in ar)
        {
            b.Print();
        }

        Console.WriteLine("\n");

        Car tmp = (Car)ar[0];
        Console.WriteLine(ar.Contains(tmp));    // Проверка наличия элемента

        // вставка элементов
        ar.Insert(0, new Car("Mercedes", "600", 260));   // Вставка элемента
        ar.InsertRange(0, new Car[] { new Car("Renault", "Laguna", 200), new Car("Renault", "Megan", 220) });

        foreach (Car b in ar)
        {
            b.Print();
        }

        Console.WriteLine("\n");

        Console.WriteLine(ar.Capacity);         // Получить размер коллекции
        Console.WriteLine(ar.Count);            // Получить кол-во заполненных элементов
        ar.TrimToSize();                        // Стереть пустые места
        Console.WriteLine(ar.Capacity);
        ar.Add(new Car("Lamborghini", "Countach", 280));
        Console.WriteLine(ar.Capacity);         // Получить размер коллекции
        Console.WriteLine(ar.Count);            // Получить кол-во заполненных элементо

        Console.WriteLine("\n");

        ArrayList ar2 = new ArrayList(ar.GetRange(0, 5));   // Возвратить часть коллекции

        //ar2.Sort();

        foreach (Car b in ar2)
        {
            b.Print();
        }

        Console.WriteLine("\n");

        ArrayList ar3 = (ArrayList)ar.Clone(); // Создать точную копию
        //ArrayList ar3 = ar;

        ar.Clear();                             // Удалить все элементы

        foreach (Car b in ar3)
        {
            b.Print();
        }

        List<string> lst = new List<string>();
        lst.Add("23");

        string str = lst[0];

        List<double> dbl = new List<double>();

        List<Car> myCars = new List<Car>();
        myCars.Add(new Car("Opel", "Corsa", 190));
    }
}

