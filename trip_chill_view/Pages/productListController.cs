using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trip_chill_view.Pages
{
    public class productListController : Controller
    {
        public string id { get; set; } = "";
        public IActionResult Index()
        {
            return View();
        }

     /*   [Route("productList/{id?}")]
        public IActionResult productDetail(string ID)
        {
            return View("~/Pages/productDetail.cshtml");
        }*/
    }
}
