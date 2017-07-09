using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WillBot.Models
{

        public class ResponseModels
        {
            public Query query { get; set; }
        }

        public class Query
        {
            public int count { get; set; }
            public DateTime created { get; set; }
            public string lang { get; set; }
            public Results results { get; set; }
        }

        public class Results
        {
            public Quote quote { get; set; }
        }

        public class Quote
        {
            
            public string symbol { get; set; }
            public string AverageDailyVolume { get; set; }
        //漲跌
            public string Change { get; set; }
            //最低
            public string DaysLow { get; set; }
        //最高
        public string DaysHigh { get; set; }
            public string YearLow { get; set; }
            public string YearHigh { get; set; }
            public string MarketCapitalization { get; set; }
        //成交價
            public string LastTradePriceOnly { get; set; }
            public string DaysRange { get; set; }
        //名稱
            public string Name { get; set; }
            public string Symbol { get; set; }
            public string Volume { get; set; }
            public string StockExchange { get; set; }
        }

    
}