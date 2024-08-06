using Newtonsoft.Json;

namespace CryptoMarketApi.Models
{  
    public class CryptoData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("market_cap_rank")]
        public int Rank { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("image")]
        public string ImageUrl { get; set; }

        [JsonProperty("current_price")]
        public decimal Price { get; set; }

        [JsonProperty("price_change_percentage_24h")]
        public decimal Change24h { get; set; }

        [JsonProperty("market_cap")]
        public decimal MarketCap { get; set; }
    }
    public class HistoricalData
    {
        [JsonProperty("prices")]
        public List<List<decimal>> Prices { get; set; }

        [JsonProperty("market_caps")]
        public List<List<decimal>> MarketCaps { get; set; }

        [JsonProperty("total_volumes")]
        public List<List<decimal>> TotalVolumes { get; set; }
    }
}
