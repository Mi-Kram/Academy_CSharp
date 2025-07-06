using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NP_WCF_Callback_Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service1 : IService1
    {
        //private static System.Timers.Timer aTimer;

        public Service1()
        {
            /*aTimer = new System.Timers.Timer(10000);

            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(aTimer_Elapsed);

            aTimer.Interval = 1000;
            aTimer.Enabled = true;*/
        }

        void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //File.WriteAllText(@"c:\test.txt", DateTime.Now.ToString());
            SendMessage("Alex", DateTime.Now.ToString());
        }

        int n = 0;
        IMyCallback callback;

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value + n++);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        delegate void SetDelCallback();
        delegate void MesCallback(string mes);


        public void DComplete(IAsyncResult res)
        {
        }

        public void SendTo()
        {
            if (callback != null)
                callback.OnCallback();
        }

        public void SendMes(string mes)
        {
            if (callback != null)
                callback.OnSendMessage(mes);
        }

        public void SetCallback()
        {
            callback = OperationContext.Current.GetCallbackChannel<IMyCallback>();
            SetDelCallback d = new SetDelCallback(SendTo);
            d.BeginInvoke(new AsyncCallback(DComplete), null);
            //callback.OnCallback();
        }

        public void SendMessage(string name, string mes)
        {
            //callback = OperationContext.Current.GetCallbackChannel<IMyCallback>();

            callback = null;
            foreach (User user in lst)
            {
                if (user.name == name) callback = user.callback;
            }

            if (callback != null) callback.OnSendMessage(mes);

            //MesCallback d2 = new MesCallback(SendMes);
            //d2.BeginInvoke(mes, new AsyncCallback(DComplete), null);
            //d2.BeginInvoke(OperationContext.Current.SessionId, new AsyncCallback(DComplete), null);
        }

        List<User> lst = new List<User>();

        public void AddUser(string name)
        {
            callback = OperationContext.Current.GetCallbackChannel<IMyCallback>();
            User user = new User();
            user.name = name;
            user.callback = callback;
            lst.Add(user);
        }
    }

    public class User
    {
        public IMyCallback callback;
        public string name;
    }
}
