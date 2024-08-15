using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NP_HW_13_08
{
    internal class AdditionalTask1
    {
        static void Main(string[] args)
        {
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 8888;
            TcpListener listener = new TcpListener(ipAddress, port);

            try
            {
                listener.Start();
                Console.WriteLine("Сервер запущен...");

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    IPEndPoint clientEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[256];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Console.WriteLine($"Сервер: В {DateTime.Now.ToShortTimeString()} от {clientEndPoint.Address} получена строка: {message}");

                    string response = "Привет, клиент!";
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseData, 0, responseData.Length);

                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            finally
            {
                listener.Stop();
            }
        }
    }
}
