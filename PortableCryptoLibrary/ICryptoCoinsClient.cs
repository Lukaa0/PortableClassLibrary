using CryptoCompare;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortableCryptoLibrary
{
    public interface ICryptoCoinsClient
    {
         IEnumerable<CoinInfo> Coins { get; set; }
        Task<List<CryptoItemModel>> Construct(int startPosition, int numberOfCoins);
          Task<CryptoItemModel> Construct(CoinInfo coinInstance);
 
        Task<decimal> GetHistoricalPrice(string symbol, IEnumerable<string> currencySymbols, DateTime date);
        Task<string> GetCoinImageUrl(string symbol);
        Task<List<CoinInfo>> GetCoinInfo();

        Task<List<CandleData>> GetHistoricalDayPrices(string fromSymbol, string ToSymbol, int? limit,
            bool? allData = false, DateTimeOffset? toDate = null, string exchangeName = "CCCAGG", int? aggregate = null,
            bool? tryConvention = null);

        Task<List<CandleData>> GetHistoricalMinutePrices(string fromSymbol, string ToSymbol, int? limit,
            bool? allData = false, DateTimeOffset? toDate = null, string exchangeName = "CCCAGG", int? aggregate = null,
            bool? tryConvention = null);

        Task<List<CandleData>> GetHistoricalHourPrices(string fromSymbol, string ToSymbol, int? limit,
            bool? allData = null, DateTimeOffset? toDate=null, string exchangeName = "CCCAGG", int? aggregate = null,
            bool? tryConvention = null);

        Task<List<CoinFullAggregatedData>> GetPriceData(string[] symbols);
        Task<CoinFullAggregatedData> GetPriceData(string symbol);
        Task<decimal> GetPrice(string fromSymbol, string toSymbol);
        CoinInfo GetInfoById(string id);





    }
}