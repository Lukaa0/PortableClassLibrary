using Newtonsoft.Json;

namespace PortableCryptoLibrary
{
    public class DefaultWatchlist
    {

        [JsonProperty("CoinIs")]
        public string CoinIs { get; set; }

        [JsonProperty("Sponsored")]
        public string Sponsored { get; set; }
    }
}