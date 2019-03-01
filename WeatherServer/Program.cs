using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WeatherServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WeatherManager.Instance.Init();

            var configuration = new ConfigurationBuilder()
               .AddCommandLine(args)
               .Build();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
