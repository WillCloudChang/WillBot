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
    public class StockService
    {
        private const string apiUrl = "http://query.yahooapis.com/v1/public/yql?format=json&env=store://datatables.org/alltableswithkeys&q=select * from yahoo.finance.quote where symbol in ('{0}.TW')";

        public string GetOneStock(string StrockID)
        {
            ResponseModels response = new ResponseModels();
            string result = "";

            if (StrockID.Length != 4)
            {
                result = "薇薇覺得股票代碼長度是不是不對啊???請檢查喔";
            }
            try
            {
                string url = string.Format(apiUrl, StrockID);
                using (WebClient client = new WebClient())
                {

                    byte[] apiResponse = client.DownloadData(apiUrl);
                    result = Encoding.UTF8.GetString(apiResponse);

                    response = JsonConvert.DeserializeObject<ResponseModels>(result);
                    if (response.query.count < 1)
                    {
                        result = "薇薇查不到這支股票代碼喔";
                    }

                    result = string.Format("股票代碼{0}，薇薇努力查到的成交價為{1}，本日漲跌幅度為{2} 最高價為{3} 最低價為{4}喔~~"
                        , response.query.results.quote.Name, response.query.results.quote.LastTradePriceOnly, response.query.results.quote.Change, response.query.results.quote.DaysHigh, response.query.results.quote.DaysLow);

                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private string CheckStrockID(string StrockID)
        {
            string result = "OK";
            if (StrockID.Length != 4)
            {
                result = "薇薇覺得股票代碼長度是不是不對啊???請檢查喔";
            }

            return result;
        }


    }
}