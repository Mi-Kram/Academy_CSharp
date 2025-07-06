using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Interfaces
{
    interface IPrintable
    {
        void Print();
    }

    // наследование интерфейсов
    interface IColorPrintable: IPrintable
    {
        void ColorPrint();
    }

    class SuperPrinter: IColorPrintable
    {
        public void ColorPrint()
        {
        }

        public void Print()
        {
        }
    }
}
