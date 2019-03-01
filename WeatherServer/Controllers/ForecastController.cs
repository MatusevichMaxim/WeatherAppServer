using System.Threading.Tasks;
using DarkSky.Models;
using Microsoft.AspNetCore.Mvc;
using WeatherServer.Models;
using static DarkSky.Services.DarkSkyService;

namespace WeatherServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly ForecastContext _context;

        public ForecastController(ForecastContext context)
        {
            _context = context;
        }

        // GET: api/forecast?lat=53.906433&lon=27.532220&units=us&lang=en
        [HttpGet]
        public async Task<ActionResult<DarkSkyResponse>> GetForecast([FromQuery]double lat, double lon, string units, string lang)
        {
            var options = new OptionalParameters
            { 
                MeasurementUnits = units,
                LanguageCode = lang
            };
            return await WeatherManager.Instance.GetForecastByCoordinates(lat, lon, options);
        }
    }
}
