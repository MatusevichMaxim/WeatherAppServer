using System.Linq;
using DarkSky.Models;
using Microsoft.EntityFrameworkCore;

namespace WeatherServer.Models
{
    public class ForecastContext : DbContext
    {
        public DbSet<DarkSkyResponse> ForecastItems { get; set; }

        public ForecastContext(DbContextOptions<ForecastContext> options)
            : base(options)
        {
        }
    }
}
