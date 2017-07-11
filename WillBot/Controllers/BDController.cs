using System;
using System.Web.Http;
using Newtonsoft.Json;
using WillBot.Models;
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
                LineMessageApiSDK.LineReceivedObject.LineReceivedMsg ReceivedObject = JsonConvert.DeserializeObject<LineMessageApiSDK.LineReceivedObject.LineReceivedMsg>(rawdata);
                FromBDModels model = JsonConvert.DeserializeObject<FromBDModels>(rawdata);

                string message = string.Empty;

                if (bs.IsCallMe(model.message.text, out message))
                {
                    message = ss.GetOneStock(message);
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
