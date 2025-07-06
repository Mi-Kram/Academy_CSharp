using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Generics
{
    public interface IUnit
    {
        string Name { get; set; }
        double Speed { get; set; }
        double Health { get; set; }

        void Attack(int x, int y);
        void Move(int x, int y);
        void Stop();
        void Defeat(int x, int y);
        void Defend(int x, int y);
        void Print();
    }

    public class Soldier : IUnit
    {
        public int Age { get; set; }
        public int Weight { get; set; }
        public string Name { get; set; }
        public double Speed { get; set; }
        public double Health { get; set; }

        public void Attack(int x, int y)
        {
        }

        public void Defeat(int x, int y)
        {
        }

        public void Defend(int x, int y)
        {
        }

        public void Move(int x, int y)
        {
        }

        public void Print()
        {
            Console.WriteLine($"Soldier. Name: {Name}, Age: {Age}, Speed: {Speed}, Health: {Health}");
        }

        public void Stop()
        {
        }
    }

    public class Tank : IUnit
    {
        public int Weight { get; set; }
        public string Name { get; set; }
        public double Speed { get; set; }
        public double Health { get; set; }

        public void Attack(int x, int y)
        {
        }

        public void Defeat(int x, int y)
        {
        }

        public void Defend(int x, int y)
        {
        }

        public void Move(int x, int y)
        {
        }

        public void Print()
        {
            Console.WriteLine($"Tank. Name: {Name}, Weight: {Weight}, Speed: {Speed}, Health: {Health}");
        }

        public void Stop()
        {
        }
    }

    // разрешаются в качестве T переменные класса Soldier и его потомки и разрешается создание новых объектов
    //public class UnitGroup<T> : IUnit where T : Soldier, new()

    // разрешаются в качестве T переменные классов, 
    // реализующие интерфейс IUnit и их потомки и разрешается создание новых объектов
    public class UnitGroup<T>: IUnit where T: class, IUnit, new()
    {
        List<T> units = new List<T>();

        public string Name { get; set; }
        public double Speed { get; set; }
        public double Health { get; set; }

        public void AddUnit(T unit)
        {
            units.Add(unit);
        }

        public void AddNewUnit(string name, double speed, double health)
        {
            T unit = new T() { Name = name, Speed = speed, Health = health};
            units.Add(unit);
        }

        public void Attack(int x, int y)
        {
            foreach(T unit in units) 
                unit.Attack(x, y);
        }

        public void Move(int x, int y)
        {
            foreach (T unit in units)
                unit.Move(x, y);
        }

        public void Stop()
        {
            foreach (T unit in units)
                unit.Stop();
        }

        public void Defeat(int x, int y)
        {
            foreach (T unit in units)
                unit.Defeat(x, y);
        }

        public void Defend(int x, int y)
        {
            foreach (T unit in units)
                unit.Defend(x, y);
        }

        public void Print()
        {
            foreach (T unit in units)
                unit.Print();
        }
    }
}
