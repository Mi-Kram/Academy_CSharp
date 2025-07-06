using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Generics
{
    class Vector<T>
    {
        T[] vector;

        public Vector(uint size)
        {
            vector = new T[size];

            for (int i = 0; i < vector.Length; i++)
            {
                // присвоить в ячейку массива значение по умолчанию для этого типа T
                vector[i] = default(T);
            }
        }

        public T this[int n]
        {
            get
            {
                if (n >= 0 && n < vector.Length)
                {
                    return vector[n];
                }
                else return vector[0];
            }
            set
            {
                if (n >= 0 && n < vector.Length)
                {
                    vector[n] = value;
                }
            }
        }

        public void Print()
        {
            foreach (var item in vector)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    }
}
