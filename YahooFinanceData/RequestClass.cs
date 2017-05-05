using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace YahooFinanceData
{
    public static class RequestClass
    {
        public static List<Contract> BuildRequest()
        {
            string MarketData;
            StringBuilder marketList = new StringBuilder();

            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Entities\MarketList.txt");

            using (StreamReader sr = new StreamReader(filePath))
            {
                string temp;
                marketList.Append(sr.ReadLine().Trim());
                while ((temp = sr.ReadLine()) != null)
                {
                    marketList.Append("+");
                    marketList.Append(temp.Trim());
                }
                marketList.Append("&");
            }

            using (WebClient web = new WebClient())
            {
                MarketData = web.DownloadString("http://finance.yahoo.com/d/quotes.csv?s=" + marketList + "f=sxl1vc1t1t1");
            }

            MarketDataHandler mdh = new YahooFinanceData.MarketDataHandler();
            return (mdh.ParseResponse(MarketData));
            
        }
        
    }
}
