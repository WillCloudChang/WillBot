using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WillBot.Controllers
{
    public class MessagesController :ApiController
    {
        // GET: Messages
        [HttpPost]
        public IHttpActionResult POST()
        {
            string ChannelAccessToken = "fjWvGIGLGtlqU8U7vfWQWw8/nqZxJsjFh0mwVsrYl1to5aC3nmffXqEuYylYSBBjT5ZYwtG0kM27vKrA6oc9MxOGV1D3RIiwHjSVPqWOHktzdmKr2VOrut7KM8aHwAnJjI7uoyy5XCt5PVuQ9YoUigdB04t89/1O/w1cDnyilFU=";

            try
            {
                //取得 http Post RawData(should be JSON)
                string postData = Request.Content.ReadAsStringAsync().Result;
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(postData);
                //回覆訊息
                string Message;
                Message = "你說了:" + ReceivedMessage.events[0].message.text;
                //回覆用戶
                isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, Message, ChannelAccessToken);
                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
    }
}