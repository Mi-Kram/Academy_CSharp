using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyMessengerMessage
{
    public class ClientInfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public TcpClient Client { get; set; }

        public ClientInfo()
        {
            ID = "";
            Name = "";
            Client = null;
        }

        public ClientInfo(string id, string name, TcpClient client)
        {
            ID = id;
            Name = name;
            Client = client;
        }  
    }


    [Serializable]
    public class ClientMessage
    {
        public string FromUserID { get; set; }
        public string Name { get; set; }
        public string ToUserID { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public DateTime FullTime { get; set; }


        public ClientMessage()
        {
            FromUserID = "";
            ToUserID = "";
            Message = "";
            Time = "";
            FullTime = new DateTime();
        }

        public ClientMessage(string fromUserID, string name, string toUserID, string message, DateTime dt)
        {
            FromUserID = fromUserID;
            Name = name;
            ToUserID = toUserID;
            Message = message;
            FullTime = dt;
            Time = dt.ToString(@"HH\:mm");
        }
    }

    [Serializable]
    public class JoinExitMessage
    {
        public enum Type
        {
            Join,
            Exit
        }
        public string UserName { get; set; }
        public Type type { get; set; }

        public JoinExitMessage()
        {
            UserName = "";
            type = Type.Join;
        }

        public JoinExitMessage(string userName, Type typ)
        {
            UserName = userName;
            type = typ;
        }
    }

    [Serializable]
    public class ClientListMessage
    {
        // ID, Name
        public Dictionary<string, string> Clients { get; set; }

        public ClientListMessage()
        {
            Clients = new Dictionary<string, string>();
        }

        public ClientListMessage(Dictionary<string, string> clients)
        {
            Clients = clients;
        }
    }

    [Serializable]
    public class ClientIDMessage
    {
        public string ID { get; set; }

        public ClientIDMessage()
        {
            ID = "";
        }

        public ClientIDMessage(string id)
        {
            ID = id;
        }
    }

}
