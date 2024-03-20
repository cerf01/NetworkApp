using Core;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Server
    {
        private static IPAddress _address => IPAddress.Parse(Service.IpStr);

        public static string Riddle = "It can be lost but never can it be found again";

        public static string Answer = "time";
        static async Task Main()
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            server.Bind(new IPEndPoint(_address, Service.Port));

            server.Listen();

            try
            {
                await ClientListenerAsync(server);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return;
            }

        }

        public static async Task WorkWithClientAsync(Socket client)
        {
            string msg;

            Service.SendMsg(client, Riddle);

            while (true)
            {
                msg = Service.RecieveMsg(client);

                Service.SendMsg(client, msg.ToLower() == Answer ? "t" : "f");

                await Console.Out.WriteLineAsync($"Sended message to {client.RemoteEndPoint}: {msg}");
            }
        }

        public static async Task ClientListenerAsync(Socket server)
        {
            Socket client;
            while (true)
            {
                await Console.Out.WriteLineAsync("Waiting for client to connect...");

                client = server.Accept();

                await Console.Out.WriteLineAsync("Accepted client");

                Task.Run(() => WorkWithClientAsync(client));

            }
        }

    }
}
