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
            FromBDModels fromModel = new FromBDModels();
            ToBDModels toModel = new ToBDModels();
            try
            {
                BaseService bs = new BaseService();
                StockService ss = new StockService();

                //取得LINE POST過來的JSON資料
                var rawdata = Request.Content.ReadAsStringAsync().Result;
                //序列化成物件
                LineMessageApiSDK.LineReceivedObject.LineReceivedMsg ReceivedObject = JsonConvert.DeserializeObject<LineMessageApiSDK.LineReceivedObject.LineReceivedMsg>(rawdata);
                
                fromModel = JsonConvert.DeserializeObject<FromBDModels>(rawdata);
                
                string message = string.Empty;
                string[] messages = new string[] { };
                if (bs.IsCallMe(fromModel.message.text, out messages))
                {
                    message = ss.GetOneStock(messages);
                }
                toModel.data = message;
                toModel.messages = new string[] { };
                return Ok(toModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
