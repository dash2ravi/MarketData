using System;
using System.Collections.Generic;
using System.Threading;

namespace YahooFinanceData
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            Console.WriteLine("Choose 1 for csv or 2 for Json format, \nIf you dont choose anything before 10 secs, the default format in settings file will be selected \n" +
                                "You can change the setting in the app.config folder");
            Option.FormatOption = '1';

            DateTime beginWait = DateTime.Now;
            while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                Thread.Sleep(5000);

            if (!Console.KeyAvailable)
            {
                Console.WriteLine("You didn't press anything! So downloading the default based on settings in config file");
                if (Properties.Settings.Default.OutputFormat.ToLower() == "json")
                    Option.FormatOption = '2';
            }
            else
                Console.WriteLine("You pressed: {0}", Option.FormatOption = Console.ReadKey().KeyChar);

            MarketDataHandler mdh = new YahooFinanceData.MarketDataHandler();
            //Calling the BuildReuest method which sets the parameters and returns the recevied response be formatted
            mdh.FormatOutput(RequestData.BuildRequest());
        }
    }
}