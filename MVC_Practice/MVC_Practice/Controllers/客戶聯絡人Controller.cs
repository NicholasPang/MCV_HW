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
        //private 客戶資料Entities1 repo = new 客戶資料Entities1();
        客戶聯絡人Repository 聯絡人repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository 客戶資料repo = RepositoryHelper.Get客戶資料Repository();
        // GET: 客戶聯絡人
        public ActionResult Index()
        {
            return View(聯絡人repo.AllExist().ToList());
        }
        public ActionResult Create()
        {
            SelectList customerList = new SelectList(客戶資料repo.AllExist().ToList(), "Id", "客戶名稱");
            ViewBag.客戶名稱 = customerList;
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 customerInfo)
        {
            customerInfo.客戶Id = int.Parse(Request.Form["客戶名稱"]);
            聯絡人repo.Add(customerInfo);
            聯絡人repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            客戶聯絡人 customer = 聯絡人repo.Find(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 customerData)
        {
            ((客戶資料Entities1)聯絡人repo.UnitOfWork.Context).Entry(customerData).State = EntityState.Modified;
            聯絡人repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 customer = 聯絡人repo.Find(id.Value);
            return View(customer);
        }
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 customerData)
        {
            客戶聯絡人 customer = 聯絡人repo.Find(customerData.Id);
            customer.是否已刪除 = true;
            聯絡人repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}