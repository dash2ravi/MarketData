using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace YahooFinanceData
{
    class MarketDataHandler : IMarketdata
    {

        public List<Contract> ParseResponse(string csvData)
        {
            List<Contract> contracts = new List<Contract>();

            string[] rows = csvData.Replace("\r", "").Split('\n');

            foreach (string row in rows)
            {
                try
                {
                    if (string.IsNullOrEmpty(row)) continue;

                    string[] cols = row.Split(',');
                    //string str = cols[1].Replace("\"", "");
   
                    Contract c = new Contract();
                    c.Symbol = cols[0].Replace("\"", "");
                    c.ExchangeCode = cols[1].Replace("\"", "");
                    c.LastPrice = Convert.ToDecimal(cols[2]);
                    c.Volume = Convert.ToDecimal(cols[3]);
                    c.Change = Convert.ToDecimal(cols[4]);
                    c.Time = DateTime.Parse(cols[5].Replace("\"", ""));
                  //  c.info = cols[6];

                    contracts.Add(c);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("Unabkle to add symbol " + row.Split(',')[0]);
                }
            }

            return contracts;
        }

        public void FormatOutput(List<Contract> contracts)
        {
            List<Contract> contractInfo = LoadContractInfo(new List<Contract>());
            string filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Entities\");
            try
            {
                switch (Option.FormatOption)
                {
                    case '2':
                        string json = JsonConvert.SerializeObject(contracts.ToArray());
                        System.IO.File.WriteAllText(filePath + @"ymarketdataJSON.txt", json);
                        break;

                    default:
                        using (StreamWriter sw = new StreamWriter(filePath + @"ymarketdataCSV.csv"))
                        {
                            sw.WriteLine("Id,Symbol,Exchange,Exchange Full Name,Exchange Time Zone,Regular Market Price,Regular Market Volume,Regular Market Change %,Regular Market Time,Market State");

                            foreach (Contract con in contracts)
                            {
                                Contract row = (Contract)contractInfo.Find(x => x.ExchangeCode.Equals(con.ExchangeCode));
                                sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8} %,{9},{10}",
                                        row.Id, con.Symbol, con.ExchangeCode, row.ExchangeName, row.OpenTime, row.CloseTime, con.LastPrice, con.Volume, con.Change, con.Time.TimeOfDay,
                                        (DateTime.Now.TimeOfDay < con.Time.TimeOfDay) ? "Market Close" : "Market Open"));
                            }
                            sw.Close();
                        }
                        break;
                }
            }
            catch(IOException ioe)
            {
                Console.WriteLine(ioe.Message);
                Thread.Sleep(5000);
            }
        }

        public static List<Contract> LoadContractInfo(List<Contract> conInfo)
        {
            using (StreamReader sr = new StreamReader(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Entities\ContractInfo.txt")))
            {
                string temp;
                sr.ReadLine();
                while ((temp = sr.ReadLine()) != null)
                {
                    string[] flds = temp.Split(',');
                    Contract c = new Contract();
                    c.Id = int.Parse(flds[0]);
                    c.Symbol = flds[1];
                    c.ExchangeCode = flds[2];
                    c.ExchangeName = flds[3];
                    c.OpenTime = DateTime.Parse(flds[4]);
                    c.CloseTime = DateTime.Parse(flds[5]);
                    c.TimeZone = flds[6];

                    conInfo.Add(c);
                }
            }
            return conInfo;
        }
    }
}
