using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using CryptoCompare;

namespace PortableCryptoLibrary
{
    public class CryptoItemModel : ISerializable
    {
        public CoinInfo Info { get; set; }
        public CoinFullAggregatedData AggregatedData { get; set; }
        public bool IsFavorite { get; set; }
        public CryptoItemModel()
        {

        }

        public CryptoItemModel(CoinInfo info,CoinFullAggregatedData aggregatedData)
        {
            this.Info = info;
            this.AggregatedData = aggregatedData;
        }
        protected CryptoItemModel(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                return;
            Info = (CoinInfo)info.GetValue("Info",typeof(CoinInfo));
            AggregatedData = (CoinFullAggregatedData)info.GetValue("AggregatedData",typeof(CoinFullAggregatedData));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                return;
            info.AddValue("Info", Info);
            info.AddValue("AggregatedData", AggregatedData);
        }
    }
}

