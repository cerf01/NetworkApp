using ConfigurationManager;
using System;
using System.Diagnostics;

namespace EnterPoint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (Process.GetProcessesByName("Server").Length == 0)
            {
                var server = new Process();

                server.StartInfo.FileName = CfgManager.ServerPath;
                server.StartInfo.UseShellExecute = true;
                server.StartInfo.CreateNoWindow = false;
                server.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                server.Start();
            }

            var client = new Process();

            client.StartInfo.FileName = CfgManager.ClientPath;
            
            client.Start();
        }
    }
}