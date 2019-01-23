using CryptoCompare;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortableCryptoLibrary
{
    public interface ICryptoNewsClient
    {
        Task<List<NewsEntity>> GetNewsAsync();

    }
}