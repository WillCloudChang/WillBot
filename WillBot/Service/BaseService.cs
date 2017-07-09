using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
namespace WillBot.Service
{
    public class BaseService
    {
        private const string MyName = "薇兒";
        public bool IsCallMe(string Message, out string Result)
        {
            string[] strArr = Message.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            Result = "";
            if (strArr[0] == MyName)
            {
                Result = strArr[1];
                return true;
            }
            return false;
        }
    }
}