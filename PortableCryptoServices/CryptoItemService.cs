using CryptoCompare;
using PortableCryptoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortableCryptoServices
{
    public class CryptoItemService : ICoinService<CoinInfo>
    {
        private readonly IEnumerable<CoinInfo> cryptoItem;

        public CryptoItemService(IEnumerable<CoinInfo> crypto)
        {
            cryptoItem = crypto;
        }

        public static async Task<CryptoItemService> Build()
        {
            var data = await HandyCryptoClient.Instance.GeneralCoinInfo.GetCoinInfo();
            return new CryptoItemService(data);
        }
        public CryptoItemService()
        {

        }


        public CoinInfo GetById(string id)
        {
            return cryptoItem?.FirstOrDefault(x => x.Symbol == id);
        }
    }
}
