using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Практическая6
{
    internal class SocketClient
    {
        public Socket socket;
        public string login;

        public SocketClient(Socket socket, string login)
        {
            this.socket = socket;
            this.login = login;
        }
    }
}
