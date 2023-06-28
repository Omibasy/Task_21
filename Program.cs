using Microsoft.AspNetCore;

namespace Task_20
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHostBuilder hostBuilder = CreateWebHostBuilder(args);       
            IWebHost myHost = hostBuilder.Build();

            myHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) 
        {          
            IWebHostBuilder hostBuilder = WebHost.CreateDefaultBuilder(args);

            hostBuilder.UseStartup<Startup>();

            return hostBuilder;
        }
    }
}