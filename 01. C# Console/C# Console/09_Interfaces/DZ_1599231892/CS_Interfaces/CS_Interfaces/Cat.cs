using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Interfaces
{
    abstract class AbstractCat
    {
        int weight;

        int height;

        int age;

        abstract public void Move(int x, int y);
        abstract public void Eat();
        abstract public void Drink();
    }

    interface IAlive
    {
        void Move(int x, int y);
        void Eat();
        void Drink();
    }

    interface IHunter
    {
        void Move(int x, int y);
        void Kill();
    }

    class Cat : IAlive, IHunter
    {
        void IAlive.Drink()
        {
        }

        void IAlive.Eat()
        {
        }

        void IHunter.Kill()
        {
        }

        void IAlive.Move(int x, int y)
        {
            Console.WriteLine("IAlive Move!");
        }

        void IHunter.Move(int x, int y)
        {
            Console.WriteLine("IHunter Move!");
        }

        public void Move(int x, int y)
        {
            Console.WriteLine($"Method Move: x: {x}, y: {y}");
        }
    }
}
