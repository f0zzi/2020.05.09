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
using System.Net;
using System.Net.Sockets;

namespace socket_client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(IPAddress.Parse("192.168.31.124"), 2020);
            if(client.Connected)
            {
                string message = tbMessage.Text;
                byte[] data = Encoding.UTF8.GetBytes(message);
                client.Send(data);
                const int SIZE = 1024;
                byte[] response = new byte[SIZE];
                int count = client.Receive(response);
                lbReply.Content += Encoding.UTF8.GetString(response) + Environment.NewLine;
                tbMessage.Clear();
                client.Close();
            }
        }
    }
}
