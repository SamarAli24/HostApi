using Microsoft.AspNetCore;
using Microsoft.OpenApi.Models;
namespace HostProjectAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build();
        }
    }
}

