using CryptoCompare;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using PortableCryptoServices;

namespace PortableCryptoLibrary
{
    public class CryptoCoinsClient : ICryptoCoinsClient
    {
        public IEnumerable<CoinInfo> Coins { get; set; }

        public async Task<List<CoinInfo>> GetCoinInfo()
        {
            if (Coins == null || !Coins.Any())
            {
                var response = await CryptoCompareClient.Instance.Coins.ListAsync();
                Coins = response.Coins.Values;
            }

            return Coins.ToList();
        }
        
        public async Task<List<CryptoItemModel>> Construct(int startPosition, int numberOfCoins)
        {
            var data = Coins;
            List<CryptoItemModel> models = new List<CryptoItemModel>();
            if (data != null && data.Any())
            {
                models = await GetCryptoItemModels(data.Skip(startPosition).Take(numberOfCoins).ToList());
            }
            else
            {
                data = await this.GetCoinInfo();
                models = await GetCryptoItemModels(data.Skip(startPosition).Take(numberOfCoins).ToList());
            }

            return models;
        }



        public async Task<CryptoItemModel> Construct(CoinInfo coinInstance) => await GetCryptoItemModel(coinInstance);

        private async Task<List<CryptoItemModel>> GetCryptoItemModels(List<CoinInfo> data)
        {
            var models = new List<CryptoItemModel>();
            var priceData = new List<IReadOnlyDictionary<string, CoinFullAggregatedData>>();
            try
            {
                decimal a;
                data.RemoveAll(x => x.Symbol.Contains("*"));
                var symbols = data.Select(x => x.Symbol).ToList();
                var priceResponse = await this.GetPriceResponseRaw(symbols, new[] { "USD" });
                priceData = priceResponse.Values.ToList();
                for (int i = 0; i < priceData.Count; i++)
                {
                    var item = priceData[i].Values.ToList()[0];
                    var infoItem = data.Find(x => x.Symbol == item.FromSymbol);
                    models.Add(new CryptoItemModel(infoItem, item));
                }
            }
            catch (Exception)
            {

            }
            return models;
        }

        public async Task<List<CoinFullAggregatedData>> GetPriceData(string[] symbols)
        {
            List<CoinFullAggregatedData> coinPriceInfo = new List<CoinFullAggregatedData>();
            var prices = (await this.GetPriceResponseRaw(symbols, new[] {"USD"})).Values.ToList();
            for (int i = 0; i < prices.Count; i++)
            {
                var item = prices[i].ToList()[0].Value;
                coinPriceInfo.Add(item);
            }

            return coinPriceInfo;
        }

        private async Task<CryptoItemModel> GetCryptoItemModel(CoinInfo coin)
        {
            var priceData = (await CryptoCompareClient.Instance.Prices.MultipleSymbolFullDataAsync(new[] { coin.Symbol }, new[] { "USD" })).Raw
                .Values.ToArray()[0].Values.ToArray()[0];
            return new CryptoItemModel(coin,priceData);
        }
        public async Task<decimal> GetHistoricalPrice(string symbol, IEnumerable<string> currencySymbols, DateTime date)
        {
            PriceHistoricalReponse response = await CryptoCompareClient.Instance.History.HistoricalForTimestampAsync(symbol, currencySymbols, date);
            return response.Values.ToList()[0].Values.ToList()[0];
        }
        public async Task<string> GetCoinImageUrl(string symbol)
        {
            var coinInfo = await GetCoinInfo();
            return coinInfo.FirstOrDefault(x => x.Symbol == symbol).ImageUrl;
        }

        private async Task<PriceMultiFullRaw> GetPriceResponseRaw(IEnumerable<string> fromSymbols, IEnumerable<string> toSymbols) =>
            (await CryptoCompareClient.Instance.Prices.MultipleSymbolFullDataAsync(fromSymbols, toSymbols)).Raw;


        public async Task<List<CandleData>> GetHistoricalDayPrices(string fromSymbol, string ToSymbol, int? limit, bool? allData = null, DateTimeOffset? toDate = null, string exchangeName = "CCCAGG", int? aggregate = null, bool? tryConvention = null)
        {
            var response = await CryptoCompareClient.Instance.History.DailyAsync(fromSymbol, ToSymbol, limit, exchangeName, toDate, allData, aggregate, tryConvention);
            return response.Data.ToList();
        }

        public async Task<List<CandleData>> GetHistoricalMinutePrices(string fromSymbol, string ToSymbol, int? limit, bool? allData = null, DateTimeOffset? toDate = null, string exchangeName = "CCCAGG", int? aggregate = null, bool? tryConvention = false)
        {
            var response = await CryptoCompareClient.Instance.History.MinutelyAsync(fromSymbol, ToSymbol, limit,
                exchangeName, toDate, allData, aggregate, tryConvention);
            return response.Data.ToList();
        }

        public async Task<List<CandleData>> GetHistoricalHourPrices(string fromSymbol, string ToSymbol, int? limit, bool? allData = null, DateTimeOffset? toDate = null, string exchangeName = "CCCAGG",  int? aggregate = null, bool? tryConvention = false)
        {
            var response = await CryptoCompareClient.Instance.History.HourlyAsync(fromSymbol, ToSymbol, limit,
                exchangeName, toDate, allData, aggregate, tryConvention);
            return response.Data.ToList();
        }


        public async Task<decimal> GetPrice(string fromSymbol, string toSymbol)
        {
            var priceResponse = (await CryptoCompareClient.Instance.Prices.SingleSymbolPriceAsync(fromSymbol, new[] {toSymbol}));
            return priceResponse.Values.First();

        }

        public CoinInfo GetInfoById(string id)
        {
           return Coins.FirstOrDefault(x => x.Id == id);
        }

        public async Task<CryptoItemModel> GetCryptoItemModel(string symbol)
        {
            var coin = GetInfoById(symbol);
            return await this.Construct(coin);
        }

        public async Task<CoinFullAggregatedData> GetPriceData(string symbol)
        {
            var response = await this.GetPriceResponseRaw(new[] {symbol}, new[] {"USD"});
           return response.Values.ToList()[0].Values.ToList()[0];
        }
    }
}