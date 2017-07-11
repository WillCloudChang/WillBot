using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LineMessageApiSDK;
using Newtonsoft.Json;
using WillBot.Service;

namespace WillBot.Controllers
{
    public class BDController : ApiController
    {
        [HttpPost]
        public IHttpActionResult POST()
        {
            try
            {
                BaseService bs = new BaseService();
                StockService ss = new StockService();
                //取得LINE POST過來的JSON資料
                var rawdata = Request.Content.ReadAsStringAsync().Result;
                //序列化成物件
                //LineMessageApiSDK.LineReceivedObject.LineReceivedMsg ReceivedObject = JsonConvert.DeserializeObject<LineMessageApiSDK.LineReceivedObject.LineReceivedMsg>(rawdata);
                //取得event物件
                //var eventObj = ReceivedObject.events[0];
                //string message = string.Empty;

                //if (bs.IsCallMe(eventObj.message.text, out message))
                //{
                //    message = ss.GetOneStock(message);
                //}
                 
                return Ok(rawdata);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
