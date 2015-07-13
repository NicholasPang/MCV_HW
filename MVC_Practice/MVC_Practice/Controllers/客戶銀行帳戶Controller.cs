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
    public class 客戶銀行帳戶Controller : Controller
    {
        private 客戶資料Entities1 db = new 客戶資料Entities1();
        // GET: 客戶銀行帳戶
        public ActionResult Index()
        {
            return View(db.客戶銀行資訊.OrderBy(x=>x.客戶Id).ToList());
        }
        public ActionResult Create()
        {
            SelectList customerList = new SelectList(db.客戶資料.ToList(), "Id", "客戶名稱");
            ViewBag.客戶名稱 = customerList;
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 custBankInfo)
        {
            custBankInfo.客戶Id = int.Parse(Request.Form["客戶名稱"]);
            db.客戶銀行資訊.Add(custBankInfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            客戶銀行資訊 customer = db.客戶銀行資訊.Find(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 custBankInfo)
        {
            
            db.Entry(custBankInfo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 customer = db.客戶銀行資訊.Find(id.Value);
            return View(customer);
        }
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 custBankInfo)
        {
            客戶銀行資訊 customer = db.客戶銀行資訊.Find(custBankInfo.Id);
            db.客戶銀行資訊.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}