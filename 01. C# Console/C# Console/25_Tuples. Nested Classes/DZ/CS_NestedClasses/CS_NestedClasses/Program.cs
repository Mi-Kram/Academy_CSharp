using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_NestedClasses
{
    // внешний класс-контейнер
    public class Container
    {
        // закрытое поле
        private int a = 77;

        // ссылка на вложенный класс
        private Nested nested;

        // конструктор внешнего класса
        public Container()
        {
            Console.WriteLine("Container constr");
            nested = new Nested(this);
        }

        // вложенный класс
        public class Nested
        {
            // ссылка внешний класс
            private Container outer;

            public Nested()
            {
            }

            // конструктор получает ссылку на экземпляр внешнего класса
            public Nested(Container outer)
            {
                this.outer = outer;
            }

            public void Print()
            {
                // у вложенного класса есть доступ к закрытым и защищённым полям внешнего класса
                if(outer != null)
                    Console.WriteLine(outer.a);
            }
        }

        public static class Inner
        {
            public static int weight = 23;
        }
    }

    public static class Outer
    {
        static public int age = 34;

        // нестатический вложенный класс Inner
        public class Inner
        {
            public int height;

            public static int weight;
        }

        public static class Inner2
        {
            public static int weight;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // создание экземпляра внешнего класса
            Container container = new Container();

            // создание экземпляра вложенного класса
            // Container.Nested nested = new Container.Nested();
            Container.Nested nested = new Container.Nested(container);
            nested.Print();

            Console.WriteLine($"Weight = {Container.Inner.weight}");

            // доступ к статическому полю
            Outer.Inner.weight = 45;
            Outer.Inner2.weight = 89;

            Outer.Inner inner = new Outer.Inner();
            inner.height = 12;

            // фабрика, генерирующая закрытые продукты
            AbstractProduct product = new Factory().GetProduct();
            product.Print();
        }
    }
}
