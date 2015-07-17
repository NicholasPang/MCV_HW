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
        客戶資料Repository 客戶資料repo = RepositoryHelper.Get客戶資料Repository();
        客戶銀行資訊Repository 銀行資訊repo = RepositoryHelper.Get客戶銀行資訊Repository();
        // GET: 客戶銀行帳戶
        public ActionResult Index()
        {
            return View(銀行資訊repo.AllExist().ToList());
        }
        public ActionResult Create()
        {
            SelectList customerList = new SelectList(客戶資料repo.AllExist().ToList(), "Id", "客戶名稱");
            ViewBag.客戶名稱 = customerList;
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 custBankInfo)
        {
            custBankInfo.客戶Id = int.Parse(Request.Form["客戶名稱"]);
            銀行資訊repo.Add(custBankInfo);
            銀行資訊repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            客戶銀行資訊 customer = 銀行資訊repo.Find(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 custBankInfo)
        {

            ((客戶資料Entities1)銀行資訊repo.UnitOfWork.Context).Entry(custBankInfo).State = EntityState.Modified;
            銀行資訊repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 customer = 銀行資訊repo.Find(id.Value);
            return View(customer);
        }
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 custBankInfo)
        {
            客戶銀行資訊 customer = 銀行資訊repo.Find(custBankInfo.Id);
            customer.是否已刪除 = true;
            銀行資訊repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}