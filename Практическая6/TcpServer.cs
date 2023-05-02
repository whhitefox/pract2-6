using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Практическая6
{
    internal class TcpServer
    {
        public Socket socket { get; private set; }
        private List<SocketClient> clients = new List<SocketClient>();
        private string adminLogin;

        public TcpServer(string adminLogin)
        {
            this.adminLogin = adminLogin;

            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);
            socket.Listen(1000);
        }

        public void AddClient(SocketClient client)
        {
            clients.Add(client);
        }

        public void RemoveClient(Socket clientSocket)
        {
            SocketClient client = clients.Find(c => c.socket == clientSocket);
            if (client == null)
            {
                return;
            }
            clients.Remove(client);
        }

        public async Task SendMessageToAll(string message)
        {
            foreach (var client in clients)
            {
                await SendMessage(client.socket, message);
            }
        }

        public async Task SendMessage(Socket client, string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            await client.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }

        public void UpdateLogin(Socket clientSocket, string login)
        {
            SocketClient client = clients.Find(c => c.socket == clientSocket);
            if (client != null)
            {
                client.login = login;
            }
        }

        public List<string> GetAllLogins()
        {
            List<string> logins = new List<string>();
            logins.Add(adminLogin);
            foreach (var client in clients)
            {
                logins.Add(client.login);
            }
            return logins;
        }

        public string GetLogin(Socket clientSocket)
        {
            SocketClient client = clients.Find(c => c.socket == clientSocket);
            if (client != null)
            {
                return client.login;
            }
            return "";
        }

        public async Task SendLogins()
        {
            string logins = $"{adminLogin},";
            foreach (var client in clients)
            {
                logins += $"{client.login},";
            }
            logins.Remove(logins.Length - 1);
            string message = "logins:" + logins;
            await SendMessageToAll(message);
        }
    }
}
