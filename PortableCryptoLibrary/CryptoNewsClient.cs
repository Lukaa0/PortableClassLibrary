using System;
using CryptoCompare;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PortableCryptoLibrary
{
    public class CryptoNewsClient : ICryptoNewsClient
    {
        public async Task<List<NewsEntity>> GetNewsAsync()
        {
            var items = (await CryptoCompareClient.Instance.News.News()).ToList();
            return items;
        }
    }
}