using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    class ChatClient
    {
        public string Login { get; set; }

        public DateTime LoginDate { get; set; }

        public TcpClient TcpClient { get; set; }
    }
}
