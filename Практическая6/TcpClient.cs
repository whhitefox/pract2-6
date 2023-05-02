using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Практическая6
{
    internal class TcpClient
    {
        public Socket server;

        public TcpClient(string ip)
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.ConnectAsync(ip, 8888);
        }

        public async void Disconnect()
        {
            await SendMessage("disconnect:");
        }

        public async Task SendLogin(string login)
        {
            await SendMessage($"login:{login}");
        }
        
        public async Task SendMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await server.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }
    }
}
