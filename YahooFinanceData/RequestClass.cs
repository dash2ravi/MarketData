using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Reflection;
using System;

namespace YahooFinanceData
{
    public static class RequestClass
    {
        public static void BuildRequest(char option)
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
                MarketData = web.DownloadString("http://finance.yahoo.com/d/quotes.csv?s=" + marketList + "f=sxl1vc1t1n4t7");
            }

            MarketDataHandler mdh = new YahooFinanceData.MarketDataHandler();
            List<Contract> contracts = mdh.ProcessResponse(MarketData);

            switch (option)
            {
                case '2':
                    string json = JsonConvert.SerializeObject(contracts.ToArray());
                    System.IO.File.WriteAllText(@"c:\ravi\ymarketdataJSON.txt", json);
                    break;

                default:
                    using (StreamWriter sw = new StreamWriter(@"c:\ravi\ymarketdataCSV.csv"))
                    {
                        foreach (Contract con in contracts)
                        {
                            sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6}",
                                    con.Symbol, con.Exchange, con.LastPrice, con.Volume, con.Change, con.Time, con.info));
                        }
                        sw.Close();
                    }
                    break;
            }
        }
    }
}
