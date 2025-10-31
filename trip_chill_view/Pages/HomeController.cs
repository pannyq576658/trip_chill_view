using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trip_chill_view.Pages
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("productList/{id?}")]
        public IActionResult productDetail(string ID)
        {
            return View("~/Pages/productDetail.cshtml");
        }

        [Route("cart/payCheckout")]///{id?}
        public IActionResult checkout(string ID)
        {
            return View("~/Pages/checkout.cshtml");
        }

        [Route("shoppingCart-return/{id?}")]
        public IActionResult shoppingCart_return(string ID)
        {
            return View("~/Pages/shoppingCart-return.cshtml");
        }

    }
}
