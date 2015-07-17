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
        //private 客戶資料Entities1 db = new 客戶資料Entities1();
        客戶資料Repository 客戶資料repo = RepositoryHelper.Get客戶資料Repository();
        客戶銀行資訊Repository 銀行資訊repo = RepositoryHelper.Get客戶銀行資訊Repository();
        客戶聯絡人Repository 聯絡人repo = RepositoryHelper.Get客戶聯絡人Repository();
        // GET: 客戶資料
        public ActionResult Index()
        {
            //return View(db.客戶資料.Where(x => x.是否已刪除 == false).ToList());
            return View(客戶資料repo.AllExist().ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 customerData)
        {
            //db.客戶資料.Add(customerData);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            客戶資料repo.Add(customerData);
            客戶資料repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            //客戶資料 customer = db.客戶資料.Find(id.Value);
            //if (customer == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(customer);
            客戶資料 customer = 客戶資料repo.Find(id.Value);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 customerData)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(customerData).State = EntityState.Modified;
            //    db.SaveChanges();
            //}
            //return RedirectToAction("Index");
            if (ModelState.IsValid)
            {
                ((客戶資料Entities1)客戶資料repo.UnitOfWork.Context).Entry(customerData).State = EntityState.Modified;
                客戶資料repo.UnitOfWork.Commit();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //客戶資料 customer = db.客戶資料.Find(id.Value);
            //return View(customer);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 customer = 客戶資料repo.Find(id.Value);
            return View(customer);
        }
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 customerData)
        {
            //客戶資料 customer = db.客戶資料.Find(customerData.Id);
            //IQueryable<客戶聯絡人> customerMembers = db.客戶聯絡人.Where(x=>x.客戶Id == customer.Id);
            //IQueryable<客戶銀行資訊> customerAccounts = db.客戶銀行資訊.Where(x => x.客戶Id == customer.Id);
            //customer.是否已刪除 = true;
            //foreach (客戶聯絡人 member in customerMembers)
            //{
            //    member.是否已刪除 = true;
            //}
            //foreach (客戶銀行資訊 account in customerAccounts)
            //{
            //    account.是否已刪除 = true;
            //}
            //db.SaveChanges();
            //return RedirectToAction("Index");
            客戶資料 customer = 客戶資料repo.Find(customerData.Id);
            IQueryable<客戶聯絡人> customerMembers = 聯絡人repo.Where(x => x.客戶Id == customer.Id);
            IQueryable<客戶銀行資訊> customerAccounts = 銀行資訊repo.Where(x => x.客戶Id == customer.Id);
            customer.是否已刪除 = true;
            foreach (客戶聯絡人 member in customerMembers)
            {
                member.是否已刪除 = true;
            }
            foreach (客戶銀行資訊 account in customerAccounts)
            {
                account.是否已刪除 = true;
            }
            客戶資料repo.UnitOfWork.Commit();
            聯絡人repo.UnitOfWork.Commit();
            銀行資訊repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}