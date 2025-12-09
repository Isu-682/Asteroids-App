using Asteroids_App.Models;
using System.Text.Json;

namespace Asteroids_App.Services
{
    public class NeoService
    {
        private const string ApiKey = "f1fDCyYt7LcJieTMdXSZDbycinJCX6sYO1WOvZcW";
        private readonly HttpClient _httpClient;

        public NeoService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Neo>> GetNeoFeedAsync(string startDate, string endDate)
        {
            var url = $"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate}&end_date={endDate}&api_key={ApiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var neos = new List<Neo>();
            var nearEarthObjects = doc.RootElement.GetProperty("near_earth_objects");

            foreach (var date in nearEarthObjects.EnumerateObject())
            {
                foreach (var item in date.Value.EnumerateArray())
                {
                    double missDistance = 0;
                    double.TryParse(item.GetProperty("close_approach_data")[0]
                        .GetProperty("miss_distance")
                        .GetProperty("kilometers")
                        .GetString(), out missDistance);

                    var neo = new Neo
                    {
                        Name = item.GetProperty("name").GetString(),
                        EstimatedDiameterMin = item.GetProperty("estimated_diameter")
                            .GetProperty("kilometers")
                            .GetProperty("estimated_diameter_min").GetDouble(),
                        EstimatedDiameterMax = item.GetProperty("estimated_diameter")
                            .GetProperty("kilometers")
                            .GetProperty("estimated_diameter_max").GetDouble(),
                        IsPotentiallyHazardous = item.GetProperty("is_potentially_hazardous_asteroid").GetBoolean(),
                        CloseApproachDate = item.GetProperty("close_approach_data")[0]
                            .GetProperty("close_approach_date").GetString(),
                        MissDistanceKm = missDistance
                    };
                    neos.Add(neo);
                }
            }

            return neos;
        }
    }
}
