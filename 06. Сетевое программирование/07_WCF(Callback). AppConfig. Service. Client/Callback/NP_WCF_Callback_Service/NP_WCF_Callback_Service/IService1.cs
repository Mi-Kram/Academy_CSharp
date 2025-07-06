using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NP_WCF_Callback_Service
{
    public interface IMyCallback
    {
        [OperationContract]
        void OnCallback();

        [OperationContract]
        void OnSendMessage(string mes);
    }

    [ServiceContract(CallbackContract = typeof(IMyCallback))]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        void AddUser(string name);

        [OperationContract]
        void SetCallback();

        [OperationContract]
        void SendMessage(string name, string mes);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "NP_WCF_Callback_Service.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
