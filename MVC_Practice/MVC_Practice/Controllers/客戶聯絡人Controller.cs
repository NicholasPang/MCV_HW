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
    public class 客戶聯絡人Controller : Controller
    {
        private 客戶資料Entities1 db = new 客戶資料Entities1();
        // GET: 客戶聯絡人
        public ActionResult Index()
        {
            return View(db.客戶聯絡人.ToList());
        }
        public ActionResult Create()
        {
            SelectList customerList = new SelectList(db.客戶資料.ToList(), "Id", "客戶名稱");
            ViewBag.客戶名稱 = customerList;
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 customerInfo)
        {
            customerInfo.客戶Id = int.Parse(Request.Form["客戶名稱"]);
            db.客戶聯絡人.Add(customerInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            客戶聯絡人 customer = db.客戶聯絡人.Find(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 customerData)
        {
            db.Entry(customerData).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 customer = db.客戶聯絡人.Find(id.Value);
            return View(customer);
        }
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 customerData)
        {
            客戶聯絡人 customer = db.客戶聯絡人.Find(customerData.Id);
            db.客戶聯絡人.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}