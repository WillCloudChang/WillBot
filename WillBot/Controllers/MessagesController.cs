using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WillBot.Service;
namespace WillBot.Controllers
{
    public class MessagesController :ApiController
    {
        // GET: Messages
        [HttpPost]
        public IHttpActionResult POST()
        {
            BaseService bs = new BaseService();
            StockService ss = new StockService(); 

            try
            {
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                //回覆訊息
                string Message = ReceivedMessage.events[0].message.text;
                if (bs.IsCallMe(Message, out Message))
                {

                    Message = ss.GetOneStock(Message);
                }
                else {
                    Message = "薇兒看不懂這個ㄟ 0.0?";
                }
                
                //回覆用戶
                isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, Message, ConfigurationManager.AppSettings["ChannelAccessToken"]);
                //回覆API OK
                return Ok();
            }
            catch 
            {
                
                return Ok();
            }
        }
    }
}