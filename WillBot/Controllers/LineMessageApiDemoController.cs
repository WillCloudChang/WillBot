using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LineMessageApiSDK;
using Newtonsoft.Json;

namespace WillBot.Controllers
{
    public class LineMessageApiDemoController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post()
        {
            try
            {
                //建立channel物件 
                LineChannel channel = new LineChannel("輸入你的 Channel Access Token");
                //取得LINE POST過來的JSON資料
                var rawdata = Request.Content.ReadAsStringAsync().Result;
                //序列化成物件
                LineMessageApiSDK.LineReceivedObject.LineReceivedMsg ReceivedObject = JsonConvert.DeserializeObject<LineMessageApiSDK.LineReceivedObject.LineReceivedMsg>(rawdata);
                //取得event物件
                var eventObj = ReceivedObject.events[0];

                LineMessageApiSDK.LineReceivedObject.UserProfile userprofile = null;
                string toid = string.Empty;
                //可以判斷傳送訊息者的類型
                switch (eventObj.source.type)
                {
                    case SourceType.user:
                        //傳送訊息者為一般使用者時 可以利用userid取得 使用者資料
                        userprofile = channel.Get_User_Data(eventObj.source.userId);
                        toid = eventObj.source.userId;
                        break;
                    case SourceType.group:

                        toid = eventObj.source.groupId;
                        break;
                    case SourceType.room:
                        toid = eventObj.source.roomId;
                        break;
                }


                //可以判斷事件類型
                switch (eventObj.type)
                {
                    case EventType.message:
                        //使用者傳送訊息 

                        //取得訊息類型
                        switch (eventObj.message.type)
                        {
                            case MessageType.text:
                                //被動回復文字訊息
                                channel.SendReplyMessage(eventObj.replyToken, new LineMessageApiSDK.LineMessageObject.TextMessage("你說：" + eventObj.message.text));
                                //主動推送訊息
                                if (userprofile == null)
                                {
                                    channel.SendPushMessage(toid, new LineMessageApiSDK.LineMessageObject.TextMessage("第一次主動推送"), new LineMessageApiSDK.LineMessageObject.TextMessage("第二次主動推送"));
                                }
                                else
                                {

                                    channel.SendPushMessage(userprofile.userId, new LineMessageApiSDK.LineMessageObject.TextMessage($"Hello {userprofile.displayName} 你是不是說{eventObj.message.text}"));
                                    //推送貼圖
                                    channel.SendPushMessage(userprofile.userId, new LineMessageApiSDK.LineMessageObject.StickerMessage(1, 1));
                                }
                                break;
                            case MessageType.image:
                                break;
                            case MessageType.video:
                                break;
                            case MessageType.audio:
                                break;
                            case MessageType.file:
                                break;
                            case MessageType.location:
                                break;
                            case MessageType.sticker:
                                break;
                            default:
                                break;
                        }


                        break;
                    case EventType.follow:
                        break;
                    case EventType.unfollow:
                        break;
                    case EventType.join:
                        break;
                    case EventType.leave:
                        break;
                    case EventType.postback:
                        break;
                    case EventType.beacon:
                        break;
                    default:
                        break;
                }


                return Ok();


            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }



        }
    }
}
