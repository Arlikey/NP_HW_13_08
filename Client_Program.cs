using System.Net.Sockets;
using System.Text;

namespace TCP_Listener
{
    internal class Program
    {
        static void Main()
        {
            ConnectToServer();
            
        }
        static void ConnectToServer()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 8888);
                Console.WriteLine("Подключено к серверу...");

                NetworkStream stream = client.GetStream();

                Console.WriteLine("Available commands:\n[get current time]\n[get current date]");
                string userCommand = Console.ReadLine();

                byte[] userCmnd = Encoding.ASCII.GetBytes(userCommand);
                stream.Write(userCmnd, 0, userCmnd.Length);

                byte[] data = new byte[256];
                StringBuilder responseData = new StringBuilder();
                int bytes;

                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    responseData.Append(Encoding.ASCII.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

                Console.WriteLine("Ответ с сервера: " + responseData.ToString());

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
