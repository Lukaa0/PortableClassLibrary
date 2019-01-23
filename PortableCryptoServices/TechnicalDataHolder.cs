using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PortableCryptoServices
{
   public  class TechnicalDataHolder
    {
        
            public decimal High { get; set; }
            public DateTime  Time { get; set; }
            public decimal Open { get; set; }
            public decimal Low { get; set; }
            public decimal Close { get; set; }
            public static TechnicalDataHolder CreateInstance<T>(T obj)
            {
            try
            {
                var h = GetPropertyValue("high", obj);
                var l = GetPropertyValue("low", obj);
                var c = GetPropertyValue("close", obj);
                var o = GetPropertyValue("open", obj);
                var t = (obj.GetType().GetProperty("time").GetValue(obj).ToString()).ToDate();

                return new TechnicalDataHolder(h,t,o,l,c);

            }
            catch (Exception)
            {
                return null;
            }

            }
        public static decimal GetPropertyValue<T>(string propName, T obj)
        {
            return decimal.Parse(obj.GetType().GetProperty(propName).GetValue(obj).ToString(),CultureInfo.InvariantCulture);
        }
            
            public TechnicalDataHolder()
            {

            }

        public TechnicalDataHolder(decimal high, DateTime time, decimal open, decimal low, decimal close)
        {
            High = high;
            Time = time;
            Open = open;
            Low = low;
            Close = close;
        }
    }
    
}
