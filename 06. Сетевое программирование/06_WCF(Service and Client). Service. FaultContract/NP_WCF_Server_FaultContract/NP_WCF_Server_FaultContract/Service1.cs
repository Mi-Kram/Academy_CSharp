using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NP_WCF_Server_FaultContract
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service1 : IService1
    {
        int n = 0;

        int delta = 0;

        private static System.Timers.Timer aTimer;

        public Service1()
        {
            aTimer = new System.Timers.Timer(10000);

            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(aTimer_Elapsed);

            aTimer.Interval = 1000;
            aTimer.Enabled = true;
        }

        void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            n++;
        }

        public int GetTimerNumber()
        {
            return n;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value + delta++);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int Divide(int a, int b)
        {
            if (b == 0) throw new FaultException<MyError>(new MyError());
            //if (b == 0) throw new FaultException("проверка FaultException!!!");
            if (a == 0) throw new FaultException<string>("FaultException test");
            return a / b;
        }
    }
}
