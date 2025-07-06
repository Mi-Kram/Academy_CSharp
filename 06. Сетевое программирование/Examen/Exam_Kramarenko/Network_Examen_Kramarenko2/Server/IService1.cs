using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    public interface IClientCallback
    {
        [OperationContract]
        void GetFilePart(string path, byte[] arr);

        [OperationContract]
        void SetCloseStream(string path);
    }



    [ServiceContract(CallbackContract = typeof(IClientCallback))]
    public interface IService1
    {
        [OperationContract]
        bool Register(string login, string password);

        [OperationContract]
        bool LogIn(string login, string password);


        [OperationContract]
        FolderData GetData(string path);


        [OperationContract]
        bool CreateFolder(string path);
        [OperationContract]
        bool RemoveFolder(string path);
        [OperationContract]
        bool RemoveFile(string path);

        [OperationContract]
        bool CopyFile(string key, byte[] data, bool isEnd);

        [OperationContract]
        bool GetFile(string path);
    }

    // Используйте контракт данных, как показано на следующем примере, чтобы добавить сложные типы к сервисным операциям.
    // В проект можно добавлять XSD-файлы. После построения проекта вы можете напрямую использовать в нем определенные типы данных с пространством имен "Server.ContractType".
    [DataContract]
    public class FolderData
    {
        string[] _folders = new string[0];
        string[] _files = new string[0];

        [DataMember]
        public string[] Folders
        {
            get { return _folders; }
            set { _folders = value; }
        }

        [DataMember]
        public string[] Files
        {
            get { return _files; }
            set { _files = value; }
        }
    }
}
