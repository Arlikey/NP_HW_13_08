using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Listener
{
    internal class AdditionalTask1
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 8888);
                IPEndPoint clientEndPoint = (IPEndPoint)client.Client.LocalEndPoint;

                NetworkStream stream = client.GetStream();
                string message = "Привет, сервер!";
                byte[] data = Encoding.UTF8.GetBytes(message);

                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Клиент: В {DateTime.Now.ToShortTimeString()} от {clientEndPoint.Address} получена строка: {response}");

                client.Close();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}
