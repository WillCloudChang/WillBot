using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using WillBot.Models;
using WillBot.Service;

namespace WillBot.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return View();
            }
            string[] messages = new string[] { };
            string result = "";
            GoogleService gs = new GoogleService();
            if (gs.IsCallMe(str, out messages))
            {
                result = gs.GetGeoCoding(messages);
                
            }
                //string GoogleApiKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GoogleApiKey"];
                //string apiUrl = string.Format(@"https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", str,  System.Web.Configuration.WebConfigurationManager.AppSettings["GoogleApiKey"]);
                //apiUrl= @"https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=AIzaSyDpgIPYzEbLl5gR8aqqRK3fCFP01eSe-p0";
                //using (WebClient wc = new WebClient())
                //{
                //    try
                //    {
                //        wc.Encoding = Encoding.UTF8;
                //        wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                //        byte[] bResult = wc.DownloadData(apiUrl);

                //        str = Encoding.UTF8.GetString(bResult);
                //    }
                //    catch (WebException ex)
                //    {
                //        throw new Exception(ex.Message);
                //    }
                //}

                //GeoCodingModels model = JsonConvert.DeserializeObject<GeoCodingModels>(str);
                return View();
        }


        // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
