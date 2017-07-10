using System;
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
            BaseService bs = new BaseService();
            StockService ss = new StockService();

            try
            {

                string Message = str;
                if (bs.IsCallMe(Message, out Message))
                {

                    Message = ss.GetOneStock(Message);
                }
                else
                {
                    Message = "薇兒看不懂這個ㄟ 0.0?";
                }


                return View();
            }
            catch
            {

            }

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
