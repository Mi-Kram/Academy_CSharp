using System;
using System.Collections.Generic;
using System.Text;

// объявление типа данных делегат
delegate int Subscriber(string str);

class Server
{
    // создание переменной-списка адресов функций (event)
    protected event Subscriber subsribers;

    // метод, подписывающий клиентские классы на сообщения
    public void Add(Subscriber ev)
    {
        // оператор += добавляет ссылку на метод в event
        subsribers += ev;
    }

    // метод, отписывающий клиентские классы на сообщения
    public void Remove(Subscriber ev)
    {
        // оператор -= удаляет ссылку на метод из event
        subsribers -= ev;
    }

    // метод, стартующий событие и вызывающий обработчики события в классах-подписчиках
    public void StartEvent(string str)
    {
        // если есть подписчики, то сообщить им о событии (вызвать их обработчики)
        /*if(subsribers != null)
            subsribers(str);*/

        subsribers?.Invoke(str);
    }
}

// первый класс-подписчик
class Client1                                       
{
    // обработчик события
    public int OnEvent(string str)
    {
        Console.WriteLine("Client 1: {0}", str);
        return 0;
    }
}

// второй класс подписчик
class Client2
{
    // обработчик события
    public int OnGetEvent(string str)
    {
        Console.WriteLine("Client 2: {0}", str);
        return 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // создание подписчиков и сервера
        Client1 c1 = new Client1();
        Client2 c2 = new Client2();
        Server ev1 = new Server();

        // подписка на события
        ev1.Add(c1.OnEvent);
        ev1.Add(c2.OnGetEvent);
        ev1.Add(c1.OnEvent);
        ev1.Remove(c1.OnEvent);

        // старт события на сервере
        ev1.StartEvent("Alarm !!! Fire !!!");

        // старт события на сервере
        ev1.StartEvent("Disk failure!");
    }
}
