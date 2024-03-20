using ConfigurationManager;
using System.Net.Sockets;
using System.Text;

namespace Core
{
    public class Service
    {
        public static string IpStr = CfgManager.ServerIp;

        public static int Port => CfgManager.Port;

        public static void SendMsg(Socket socket, string msg)
        {
            socket.Send(Encoding.UTF8.GetBytes(msg));
        }

        public static void ShowLog(Socket socket)
        {
            Console.WriteLine($"At {DateTime.Now} from {socket.RemoteEndPoint} recieved line: {RecieveMsg(socket)}");
        }

        public static string RecieveMsg(Socket socket)
        {
            var buffer = new byte[1024];

            int size = socket.Receive(buffer);

            return Encoding.UTF8.GetString(buffer, 0, size);
        }


    }
}
