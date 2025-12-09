using Asteroids_App.Models;
using System.Text.Json;

namespace Asteroids_App.Services
{
    public class NeoService
    {
        private readonly HttpClient _httpClient;

        public NeoService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Neo>> GetNeoFeedAsync(string query)
        {
            var url = $"https://images-api.nasa.gov/search?q={query}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var neos = new List<Neo>();

            if (doc.RootElement.TryGetProperty("collection", out var collection) &&
                collection.TryGetProperty("items", out var items))
            {
                foreach (var item in items.EnumerateArray())
                {
                    var data = item.GetProperty("data")[0];

                    string urlMedia = null;
                    if (item.TryGetProperty("links", out var linksElement))
                    {
                        if (linksElement.GetArrayLength() > 0)
                            urlMedia = linksElement[0].GetProperty("href").GetString();
                    }

                    var neo = new Neo
                    {
                        Name = data.GetProperty("title").GetString(),
                        Description = data.GetProperty("description").GetString(),
                        MediaType = data.GetProperty("media_type").GetString(),
                        Url = urlMedia
                    };
                    neos.Add(neo);
                }
            }

            return neos;
        }
    }
}
