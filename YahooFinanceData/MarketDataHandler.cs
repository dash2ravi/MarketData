using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooFinanceData
{
    class MarketDataHandler : IMarketdata
    {

        public List<Contract> ProcessResponse(string csvData)
        {
            List<Contract> contracts = new List<Contract>();

            string[] rows = csvData.Replace("\r", "").Split('\n');

            foreach (string row in rows)
            {
                try
                {
                    if (string.IsNullOrEmpty(row)) continue;

                    string[] cols = row.Split(',');

                    Contract c = new Contract();
                    c.Symbol = cols[0];
                    c.Exchange = cols[1];
                    c.LastPrice = Convert.ToDecimal(cols[2]);
                    c.Volume = Convert.ToDecimal(cols[3]);
                    c.Change = Convert.ToDecimal(cols[4]);
                    c.Time = cols[5];
                    c.info = cols[6];


                    contracts.Add(c);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Unabkle to add symbol " + row.Split(',')[0]);
                }
            }

            return contracts;
        }

    }
}
