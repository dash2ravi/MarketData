using System.Collections.Generic;


namespace YahooFinanceData
{
    public interface IMarketdata
    {
        List<Contract> ParseResponse(string data);
        void FormatOutput(List<Contract> con);

    }
}
