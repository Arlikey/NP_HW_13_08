using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TCP_Listener
{
    internal class AdditionalTask3
    {
        static void Main(string[] args)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 8888);

                NetworkStream stream = client.GetStream();
                Console.Write("Введите число : ");
                string userNumber = Console.ReadLine(); 
                byte[] data = Encoding.ASCII.GetBytes(userNumber);

                stream.Write(data, 0, data.Length);

                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Результат сервера : {response}");

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
