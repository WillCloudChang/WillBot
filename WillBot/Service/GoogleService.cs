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
    public class GoogleService :BaseService
    {
        private string GoogleApiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GoogleApiKey"];
        GeoCodingModels model = new GeoCodingModels();
        public string GetGeoCoding(string[] Message)
        {
            string result = "";
            if(Message.Length>2)
                result = Message[2].Replace(" ","+");
            string apiUrl = string.Format(@"https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", result, System.Web.Configuration.WebConfigurationManager.AppSettings["GoogleApiKey"]);
            //apiUrl = @"https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=AIzaSyDpgIPYzEbLl5gR8aqqRK3fCFP01eSe-p0";
            try { 
                result = GetApi(apiUrl);
                model = JsonConvert.DeserializeObject<GeoCodingModels>(result);

                string.Format("薇兒努力查到{0}的位置 經度:{1} 緯度:{2} ", Message[3], model.results[0].geometry.location.lat, model.results[0].geometry.location.lng);
            }
            catch(Exception ex) {
                return ex.Message;
            }
            return result;
        }
    }
}