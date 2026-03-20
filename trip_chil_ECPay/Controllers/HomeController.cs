using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace trip_chil_ECPay.Controllers
{
    public class HomeController : Controller
    {
        // GET: Hone
        public ActionResult Index()
        {
            return Content("這是首頁 (綠界金流測試中)");
        }
    }
}