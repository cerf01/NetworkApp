using Core;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    internal class Client
    {
        private static IPAddress _address => IPAddress.Parse(Service.IpStr);
        static void Main()
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            string msg;

            Console.WriteLine("Trying to connect to the server...");
            try
            {
                client.Connect(_address, Service.Port);

                if (!client.Connected)
                    throw new Exception("Error connection");


                Console.WriteLine(Service.RecieveMsg(client));

                do
                {
                    if (!ReadMsg(out msg))
                        break;
                    Service.SendMsg(client, msg);

                } while (isIncorrectAnswer(Service.RecieveMsg(client)));
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Console.WriteLine("Connection closed...");

            Console.ReadKey();
        }

        public static bool isIncorrectAnswer(string serverResp)
        {
            bool isCorrcet = serverResp == "f";

            Console.WriteLine(isCorrcet ? "Inccorect" : "Correct");

            return isCorrcet;
        }

        public static bool ReadMsg(out string msg)
        {
            while (true)
            {
                Console.WriteLine("Write your message:");
                msg = Console.ReadLine() ?? "";

                if (msg.ToLower().Length > 0)
                    return msg != "exit";

                Console.WriteLine("Message can't be empty");
            }
        }
    }
}
