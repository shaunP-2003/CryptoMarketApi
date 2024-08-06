using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CryptoMarketApi.Models;

namespace CryptoMarketApi.Services
{
    public class CryptoServices
    {
        private readonly HttpClient _httpClient;

        public CryptoServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CryptoData>> GetCryptoDataAsync(string currency = "usd")
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.coingecko.com/api/v3/coins/markets?vs_currency={currency}"),
                Headers =
                {
                    { "accept", "application/json" },
                    // Make sure to include an API key if required by the API
                    { "x-cg-demo-api-key", "CG-Fbi1gQ9VFPMnq3jH219cjLQi" }
                }
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error fetching data from API: {response.ReasonPhrase}");
                }

                var body = await response.Content.ReadAsStringAsync();
                var crypto = JsonConvert.DeserializeObject<List<CryptoData>>(body);
                return crypto;
            }
        }
        public async Task<HistoricalData> GetCryptoHistoricalDataAsync(string sId, string currency = "usd", string days = "10")
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.coingecko.com/api/v3/coins/{sId}/market_chart?vs_currency={currency}&days={days}&interval=daily"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "x-cg-demo-api-key", "CG-Fbi1gQ9VFPMnq3jH219cjLQi" }
                }
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error fetching data from API: {response.ReasonPhrase}");
                }

                var body = await response.Content.ReadAsStringAsync();
                var historicalData = JsonConvert.DeserializeObject<HistoricalData>(body);
                return historicalData;
            }
        }
    }
}


