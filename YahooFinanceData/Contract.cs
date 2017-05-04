using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceData
{
    public class Contract
    {
        public string Symbol { get; set; }
        public string Exchange { get; set; }
        public string Name { get; set; }
        public decimal LastPrice { get; set; }
        public decimal Volume { get; set; }
        public decimal Change { get; set; }
        public string Time { get; set; }
        public string info { get; set; }


    }
}
