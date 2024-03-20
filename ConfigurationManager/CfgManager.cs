using Microsoft.Extensions.Configuration;

namespace ConfigurationManager
{
    public static class CfgManager
    {
        public static string ServerPath { get;}     
        public static string ClientPath { get; }

        public static string ServerIp { get; }

        public static int Port { get; }

        static CfgManager() 
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(AppContext.BaseDirectory)
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            var configuration = builder.Build();

            ServerPath = configuration.GetRequiredSection("FilesPath:Server").Value?? AppContext.BaseDirectory;

            ClientPath = configuration.GetRequiredSection("FilesPath:Client").Value ?? AppContext.BaseDirectory;

            ServerIp = configuration.GetRequiredSection("IPAddresses:DefaultServerIP").Value ?? "127.00.1";

            Port = int.Parse(configuration.GetRequiredSection("Ports:DefaultPort").Value??"80");

        }
    }
}