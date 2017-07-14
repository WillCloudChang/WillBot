using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using WillBot.Models;

namespace WillBot.Service
{
    public class StockService : BaseService
    {
        private const string apiUrl = "http://query.yahooapis.com/v1/public/yql?format=json&env=store://datatables.org/alltableswithkeys&q=select * from yahoo.finance.quote where symbol in ('{0}.TW')";

        public string GetOneStock(string[] Messages)
        {
            ResponseModels response = new ResponseModels();
            string result = "";
            if (Messages.Length > 2)
                result = Messages[2];
            if (result.Length != 4)
            {
                return "薇兒覺得股票代碼長度是不是不對啊???請檢查喔";
            }
            try
            {
                result = GetApi(string.Format(apiUrl, result));
                response = JsonConvert.DeserializeObject<ResponseModels>(result);
                if (string.IsNullOrEmpty(response.query.results.quote.Name))
                {
                    result = "薇兒查不到這支股票代碼喔";
                }
                else
                {
                    result = string.Format("薇兒努力查到{0}的成交價為{1}，本日漲跌幅度為{2} 最高價為{3} 最低價為{4}喔~~"
                        , response.query.results.quote.Name, response.query.results.quote.LastTradePriceOnly, response.query.results.quote.Change, response.query.results.quote.DaysHigh, response.query.results.quote.DaysLow);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

    }
}