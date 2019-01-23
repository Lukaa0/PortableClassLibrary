using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PortableCryptoServices
{
    public static class ExtensionMethods
    {
        public static string ToEpoch(this DateTime date)
        {
            string epoch = (date - new DateTime(1970, 1, 1)).TotalSeconds.ToString();

            return epoch;
        }

        public static DateTime ToDate(this string unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(Convert.ToInt64(unixTime));
        }
        public static decimal ToAllTimePercentage(this decimal currentValue, decimal originalValue)
        {
            decimal change = currentValue - originalValue;
            decimal result = (change / originalValue);
            return result;
        }
        public static decimal ToDecimalWithCulture(this object str)
        {
            return Convert.ToDecimal(str, CultureInfo.InvariantCulture);
        }
        public static decimal ToDecimalWithCulture(this string str)
        {
            return decimal.Parse(str, NumberStyles.Any, CultureInfo.InvariantCulture);
        }


    }
}