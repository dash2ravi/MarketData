using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace YahooFinanceData
{
    public static class RequestData
    {
        public static List<Contract> BuildRequest()
        {
            string MarketData;
            StringBuilder marketList = new StringBuilder();

            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Entities\MarketList.txt");

            // Iterate through the list of markets present in the file
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

            // Set the url with parameters including symbols and the fields required
            using (WebClient web = new WebClient())
            {
                MarketData = web.DownloadString("http://finance.yahoo.com/d/quotes.csv?s=" + marketList + "f=sxl1vc1t1t1");
            }

            // Parse the response and return it
            MarketDataHandler mdh = new YahooFinanceData.MarketDataHandler();
            return (mdh.ParseResponse(MarketData));
            
        }
        
    }
}

