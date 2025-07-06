using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_NestedClasses
{
    public abstract class AbstractProduct
    {
        public abstract void Print();
    }

    public class Factory
    {
        private int a = 0;

        public Factory()
        {
            Console.WriteLine("Factory constr");
        }

        public AbstractProduct GetProduct()
        {
            return new Product(this);
        }

        private class Product: AbstractProduct
        {
            private Factory parent;

            public Product(Factory parent)
            {
                this.parent = parent;
            }

            public override void Print()
            {
                if (parent != null)
                    Console.WriteLine(parent.a);
            }
        }
    }
}
