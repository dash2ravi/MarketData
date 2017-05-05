using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceData
{
    public class Contract
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string ExchangeCode { get; set; }
        public string ExchangeName { get; set; }
        public string TimeZone { get; set; }
        public string Name { get; set; }
        public decimal LastPrice { get; set; }
        public decimal Volume { get; set; }
        public decimal Change { get; set; }
        public DateTime Time { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string info { get; set; }

        internal object ToArray()
        {
            throw new NotImplementedException();
        }
    }
    public static class Option
    {
        public static char  FormatOption { get; set; }
    }
}
