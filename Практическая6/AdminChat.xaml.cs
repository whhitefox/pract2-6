using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Практическая6
{
    /// <summary>
    /// Логика взаимодействия для AdminChat.xaml
    /// </summary>
    public partial class AdminChat : Window
    {
        private TcpServer server;
        private CancellationTokenSource isConnected;
        private bool isLogs;
        private string login;
        private List<string> logins;
        private List<string> logs;

        public AdminChat(string login)
        {
            InitializeComponent();
            this.login = login;

            isConnected = new CancellationTokenSource();
            server = new TcpServer(login);
            
            isLogs = false;
            logins = server.GetAllLogins();
            logs = new List<string>();
            UpdateLeftMenu();

            ListenToClients(isConnected.Token);
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string content = MessageTextBox.Text;
            if (content == "/disconnect")
            {
                Close();
                return;
            }

            await SendMessage(login, content);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LogsButton_Click(object sender, RoutedEventArgs e)
        {
            isLogs = !isLogs;
            UpdateLeftMenu();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isConnected.Cancel();
            server.socket.Close();
            MainWindow window = new MainWindow();
            window.Show();
        }

        private async Task ListenToClients(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var client = await server.socket.AcceptAsync();
                server.AddClient(new SocketClient(client, ""));
                RecieveMessage(token, client);
            }
        }

        private async Task RecieveMessage(CancellationToken token, Socket client)
        {
            while(!token.IsCancellationRequested)
            {
                ArraySegment<byte> bytes = new ArraySegment<byte>(new byte[8192]);
                await client.ReceiveAsync(bytes, SocketFlags.None);
                string data = Encoding.UTF8.GetString(bytes.Array);
                data = data.Replace("\0", "");  
                DateTime time = DateTime.Now;

                if (data.StartsWith("login:"))
                {
                    string login = data.Remove(0, 6);
                    server.UpdateLogin(client, login);
                    logins = server.GetAllLogins();
                    logs.Add($"[{time}] Пользователь [{login}] подключился");
                    await server.SendLogins();

                    UpdateLeftMenu();
                }
                else if (data.StartsWith("disconnect:"))
                {
                    string login = server.GetLogin(client);
                    server.RemoveClient(client);
                    await server.SendLogins();
                    logins = server.GetAllLogins();
                    logs.Add($"[{time}] Пользователь [{login}] отключился");

                    UpdateLeftMenu();
                }
                else if (data.StartsWith("message:"))
                {
                    string login = server.GetLogin(client);
                    string content = data.Remove(0, 8);
                    await SendMessage(login, content);
                }
            }
        }

        private async Task SendMessage(string login, string content)
        {
            if (content == "")
            {
                return;
            }

            DateTime time = DateTime.Now;
            string message = $"[{time}][{login}]: {content}";
            string sendMessage = $"message:{message}";
            await server.SendMessageToAll(sendMessage);
            MessagesListBox.Items.Add(message);
        }

        private void UpdateLeftMenu()
        {
            if (!isLogs)
            {
                LeftLabel.Content = "Пользователи";
                LeftListBox.ItemsSource = null;
                LeftListBox.ItemsSource = logins;
            }
            else
            {
                LeftLabel.Content = "Логи";
                LeftListBox.ItemsSource = null;
                LeftListBox.ItemsSource = logs;
            }
        }
    }
}
