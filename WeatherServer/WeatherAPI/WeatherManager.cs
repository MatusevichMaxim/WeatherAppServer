using System;
using DarkSky;
using DarkSky.Models;
using DarkSky.Services;
using DarkSky.Extensions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using static DarkSky.Services.DarkSkyService;

namespace WeatherServer
{
    public class WeatherManager
    {
        private const int maxCallsCount = 999;

        private readonly List<string> _apiKeys = new List<string> 
        { 
            "0392bfa29c1a06a8fce3a9fa91036738", 
            "98eb3fe729328855ab9b3dfd208871df", 
        };
        private string _currentApiKey;
        private DarkSkyResponse _lastResponse;

        public static WeatherManager Instance { get; } = new WeatherManager();

        public DarkSkyService DarkSkyService { get; set; }

        private WeatherManager() { }

        public void Init()
        {
            _currentApiKey = _apiKeys.First();
            DarkSkyService = new DarkSkyService(_currentApiKey);
        }

        private void SwitchApiKeyIfNeeded()
        {
            if (_lastResponse == null)
                return;

            var callsCount = _lastResponse.Headers?.ApiCalls;
            if (!_lastResponse.IsSuccessStatus || callsCount >= maxCallsCount)
            {
                _currentApiKey = _apiKeys.First(x => x != _currentApiKey);
                DarkSkyService = new DarkSkyService(_currentApiKey);
            }
        }

        public async Task<DarkSkyResponse> GetForecastByCoordinates(double latitude, double longitude, OptionalParameters parameters)
        {
            _lastResponse = await DarkSkyService.GetForecast(latitude, longitude, parameters);
            SwitchApiKeyIfNeeded();
            return _lastResponse;
        }
    }
}
