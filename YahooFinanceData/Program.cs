using System;
using System.Threading;

namespace YahooFinanceData
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            Console.WriteLine("Choose 1 for csv or 2 for Json format, if you dont choose anything before 10 secs, the default format in settings file will be selected");
            char option = '1';

            DateTime beginWait = DateTime.Now;
            while (!Console.KeyAvailable && DateTime.Now.Subtract(beginWait).TotalSeconds < 5)
                Thread.Sleep(3000);

            if (!Console.KeyAvailable)
            {
                Console.WriteLine("You didn't press anything! So downloading the default based on settings in config file");
                if (Properties.Settings.Default.OutputFormat.ToLower() == "json")
                    option = '2';
            }
            else
                Console.WriteLine("You pressed: {0} for JSon", option = Console.ReadKey().KeyChar);

            RequestClass.BuildRequest(option);

        }
    }
}