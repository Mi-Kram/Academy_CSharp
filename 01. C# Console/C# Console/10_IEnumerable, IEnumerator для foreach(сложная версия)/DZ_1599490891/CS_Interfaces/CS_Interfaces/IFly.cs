using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Interfaces
{
    interface IFly
    {
        // взлёт
        void TakeOff();

        // посадка
        void Land();

        // перемещение в воздухе
        void Move(double x, double y, double z);

        // получить высоту полёта
        double Height
        {
            get;
        }

        // получить все пройденные координаты полёта
        double this[int pos]
        {
            get;
        }
    }
}
