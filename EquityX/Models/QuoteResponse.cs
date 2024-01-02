using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SQLite.SQLite3;

namespace EquityX.Models
{
    public class QuoteResponse
    {
        public List<Result> result { get; set; }
        public object error { get; set; }
    }

    public class Result
    {
        public string language { get; set; }
        public string region { get; set; }
        public string quoteType { get; set; }
        public bool triggerable { get; set; }
        public string quoteSourceName { get; set; }
        public string currency { get; set; }
        public string fullExchangeName { get; set; }
        public string longName { get; set; }
        public string financialCurrency { get; set; }
        public long averageDailyVolume3Month { get; set; }
        public long averageDailyVolume10Day { get; set; }
        public double fiftyTwoWeekLowChange { get; set; }
        public double fiftyTwoWeekLowChangePercent { get; set; }
        public string fiftyTwoWeekRange { get; set; }
        public double fiftyTwoWeekHighChange { get; set; }
        public double fiftyTwoWeekHighChangePercent { get; set; }
        public double fiftyTwoWeekLow { get; set; }
        public double fiftyTwoWeekHigh { get; set; }
        public long dividendDate { get; set; }
        public double bookValue { get; set; }
        public double fiftyDayAverage { get; set; }
        public double fiftyDayAverageChange { get; set; }
        public double fiftyDayAverageChangePercent { get; set; }
        public double twoHundredDayAverage { get; set; }
        public double twoHundredDayAverageChange { get; set; }
        public double twoHundredDayAverageChangePercent { get; set; }
        public object marketCap { get; set; }
        public double forwardPE { get; set; }
        public double priceToBook { get; set; }
        public long sourceInterval { get; set; }
        public string exchangeTimezoneName { get; set; }
        public string exchangeTimezoneShortName { get; set; }
        public long exchangeDataDelayedBy { get; set; }
        public string market { get; set; }
        public string shortName { get; set; }
        public double preMarketPrice { get; set; }
        public double regularMarketChangePercent { get; set; }
        public string regularMarketDayRange { get; set; }
        public double regularMarketPreviousClose { get; set; }
        public double bid { get; set; }
        public double ask { get; set; }
        public long bidSize { get; set; }
        public long askSize { get; set; }
        public string messageBoardId { get; set; }
        public string marketState { get; set; }
        public long earningsTimestamp { get; set; }
        public long earningsTimestampStart { get; set; }
        public long earningsTimestampEnd { get; set; }
        public double trailingAnnualDividendRate { get; set; }
        public double trailingPE { get; set; }
        public double trailingAnnualDividendYield { get; set; }
        public double epsTrailingTwelveMonths { get; set; }
        public double epsForward { get; set; }
        public double epsCurrentYear { get; set; }
        public double priceEpsCurrentYear { get; set; }
        public object sharesOutstanding { get; set; }
        public bool esgPopulated { get; set; }
        public bool tradeable { get; set; }
        public long gmtOffSetMilliseconds { get; set; }
        public long priceHint { get; set; }
        public double preMarketChange { get; set; }
        public double preMarketChangePercent { get; set; }
        public long preMarketTime { get; set; }
        public string exchange { get; set; }
        public double regularMarketPrice { get; set; }
        public long regularMarketTime { get; set; }
        public double regularMarketChange { get; set; }
        public double regularMarketOpen { get; set; }
        public double regularMarketDayHigh { get; set; }
        public double regularMarketDayLow { get; set; }
        public long regularMarketVolume { get; set; }
        public string? symbol { get; set; }
    }

    public class Root
    {
        public QuoteResponse quoteResponse { get; set; }
    }
}
