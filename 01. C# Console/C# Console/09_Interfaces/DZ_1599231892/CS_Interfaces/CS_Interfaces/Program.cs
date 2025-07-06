using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            //Helicopter choper = new Helicopter();
            //choper.Move(1, 2, 3);

            List<IFly> airForces = new List<IFly>();
            airForces.Add(new Helicopter());
            airForces.Add(new Fly());
            airForces.Add(new Fly());

            List<IPrintable> lst = new List<IPrintable>();
            lst.Add(new Person());
            lst.Add(new Fly());

            object fly = new Fly();

            // проверить, является ли ссылка объектом класса Fly
            if (fly is Helicopter)
            {
                ((Fly)fly).Eat();
            }

            // приведение к типу Fly
            Fly realFly = fly as Fly;
            if(realFly != null)
            {
                realFly.Eat();
            }

            // приведение к типу Fly
            (fly as Fly).Eat();

            Cat cat = new Cat();

            // вызов метода Move интерфейса IAlive
            IAlive alive = cat;
            alive.Move(1, 2);
            (cat as IAlive).Move(1, 2);
            ((IAlive)cat).Move(1, 2);

            // вызов метода Move интерфейса IHunter
            IHunter hunter = cat;
            hunter.Move(3, 4);

            // вызов метода Move класса Cat
            cat.Move(3, 5);
        }
    }
}
