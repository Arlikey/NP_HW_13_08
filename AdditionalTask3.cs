using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NP_HW_13_08
{
    internal class AdditionalTask3
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

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[256];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string userNumber = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    string response = Math.Sqrt(double.Parse(userNumber)).ToString();
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
