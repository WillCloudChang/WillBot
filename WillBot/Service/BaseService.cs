using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;

namespace WillBot.Service
{
    public class BaseService
    {
        private const string MyName = "W";
        private string[] Calls = new string[] { };
        public BaseService()
        {
            //Calls = MyName.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }
        public bool IsCallMe(string Message, out string[] Result)
        {
            string[] strArr = Message.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            Result = strArr;
            if(MyName.Equals(strArr[0]))
            {
                Result = strArr;
                return true;
            }
            return false;
        }

        public string GetApi(string ApiUrl)
        {
            string result = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    byte[] bResult = wc.DownloadData(ApiUrl);
                    result = Encoding.UTF8.GetString(bResult);
                    return result;
                }
                catch (WebException ex)
                {
                    return ex.Message;
                }
            }
        }

    }
}