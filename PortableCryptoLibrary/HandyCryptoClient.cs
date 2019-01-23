using CryptoCompare;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using PortableCryptoServices;
using System.Linq;
using System.Net;
using System.Text;

using System.Threading.Tasks;
namespace PortableCryptoLibrary
{
    public class HandyCryptoClient
    {
        private static HandyCryptoClient instance;
        private static object threadLock = new object();

        private HandyCryptoClient() { }

        public static HandyCryptoClient Instance
        {
            get
            {
                lock (threadLock)
                {
                    if (instance == null)
                    {
                        instance = new HandyCryptoClient();
                    }

                    return instance;
                }
            }
        }

        public void RegisterApiKey(string apiKey)
        {
            CryptoCompareClient.Instance.SetApiKey(apiKey);
        }
        public ICryptoCoinsClient GeneralCoinInfo { get; } = new CryptoCoinsClient();
        
        public ICryptoNewsClient News => new CryptoNewsClient();

        
    }
}
