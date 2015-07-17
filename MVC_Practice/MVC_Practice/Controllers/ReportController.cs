using MVC_Practice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Practice.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            客戶資料Entities1 db = new 客戶資料Entities1();
            return View(db.Views.Where(x => x.是否已刪除 == false).OrderBy(x => x.Id).ToList());
        }
    }
}