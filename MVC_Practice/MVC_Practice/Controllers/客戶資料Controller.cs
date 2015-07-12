using MVC_Practice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC_Practice.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities1 db = new 客戶資料Entities1();
        // GET: 客戶資料
        public ActionResult Index()
        {
            return View(db.客戶資料.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 customerData)
        {
            db.客戶資料.Add(customerData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            客戶資料 customer = db.客戶資料.Find(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 customerData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerData).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 customer = db.客戶資料.Find(id.Value);
            return View(customer);
        }
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 customerData)
        {
            客戶資料 customer = db.客戶資料.Find(customerData.Id);
            db.客戶資料.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}