using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Практическая6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            if (login == "")
            {
                MessageBox.Show("Заполните логин");
                return;
            }

            AdminChat window = new AdminChat(login);
            window.Show();
            Close();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string ip = IPTextBox.Text;
            if (login == "")
            {
                MessageBox.Show("Заполните логин");
                return;
            }
            if (ip == "")
            {
                MessageBox.Show("Заполните IP");
                return;
            }

            UserChat window = new UserChat(login, ip);
            window.Show();
            Close();
        }
    }
}
