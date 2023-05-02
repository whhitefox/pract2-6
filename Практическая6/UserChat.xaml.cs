using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    /// Логика взаимодействия для UserChat.xaml
    /// </summary>
    public partial class UserChat : Window
    {
        private TcpClient client;
        private CancellationTokenSource isConnected;

        public UserChat(string login, string ip)
        {
            InitializeComponent();
            
            client = new TcpClient(ip);
            isConnected = new CancellationTokenSource();
            RecieveMessage(isConnected.Token);
            client.SendLogin(login);
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string content = MessageTextBox.Text;
            if (content == "/diconnect")
            {
                Close();
                return;
            }

            await SendMessage(content);
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.Disconnect();
            isConnected.Cancel();
            MainWindow window = new MainWindow();
            window.Show();
        }

        private async Task RecieveMessage(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                ArraySegment<byte> bytes = new ArraySegment<byte>(new byte[8192]);
                await client.server.ReceiveAsync(bytes, SocketFlags.None);
                string data = Encoding.UTF8.GetString(bytes.Array);
                data = data.Replace("\0", "");

                if (data.StartsWith("logins:"))
                {
                    string loginsStr = data.Remove(0, 7);
                    string[] logins = loginsStr.Split(',');
                    LeftListBox.ItemsSource = logins;
                }
                else if (data.StartsWith("message:"))
                {
                    string content = data.Remove(0, 8);
                    MessagesListBox.Items.Add(content);
                }
            }
        }

        private async Task SendMessage(string content)
        {
            if (content == "")
            {
                return;
            }

            string message = $"message:{content}";
            await client.SendMessage(message);
        }
    }
}
