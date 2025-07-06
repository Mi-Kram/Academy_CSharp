using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Properties
{
    class Matrix
    {
        int[,] matrix;

        public Matrix(int width, int height)
        {
            if (width <= 0 || width > 10)
                width = 5;

            if (height <= 0 || height > 10)
                height = 5;

            matrix = new int[width, height];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int k = 0; k < matrix.GetLength(1); k++)
                {
                    matrix[i, k] = i + k + 1;
                }
            }
        }

        public int Width { get => matrix.GetLength(1); }

        public int Height { get => matrix.GetLength(0); }

        public int this[int w, int h]
        {
            get
            {
                return matrix[w, h];
            }
            set
            {
                if(value > 0 && w>=0 && w<matrix.GetLength(0) && h>=0 && h < matrix.GetLength(1))
                {
                    matrix[w, h] = value;
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int k = 0; k < matrix.GetLength(1); k++)
                {
                    Console.Write(matrix[i, k] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
