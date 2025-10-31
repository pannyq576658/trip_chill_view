using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECPay.Payment.Integration;
using System.Collections;
using trip_chil_ECPay.Logistics;
using trip_chil_ECPay.Models;
using trip_chil_ECPay.Service;

namespace trip_chil_ECPay.Controllers
{
    public class goToPayController : Controller
    {
        // GET: CreatOrderMoney
        goToPayService _Service = new goToPayService();
        ProjectSet Project_Set = new ProjectSet();

        public ActionResult Index(string payOrderID)
        {
            List<string> enErrors = new List<string>();
            CustomerLogic Logic = new CustomerLogic();
            //加上基本的防呆
            if (payOrderID == null)  
                return Redirect(Project_Set.Route + "/");
            else if (!payOrderID.Contains("@"))
                return Redirect(Project_Set.Route + "/");
            else if (!Logic.payOrderExist(payOrderID.Split('@')[0]))
                return Redirect(Project_Set.Route + "/");
            string payOrder_ID = payOrderID.Split('@')[0];
            string DateTimeNumber = payOrderID.Split('@')[1];
            var result = _Service.EC_Page(payOrder_ID, DateTimeNumber);

            return Content(result);

        }
        public ActionResult OrderResult(string payOrderID)
        {
            if (payOrderID == null)
                return Redirect(Project_Set.Route + "/" );
            else if (!payOrderID.Contains("@"))
                return Redirect(Project_Set.Route + "/");            
           
            var result = _Service.orderResult(payOrderID);
            if (result.Status == 0)
            {
                return Content(result.Message);
            }
            else
            {
                return Redirect(result.Redirect_url);
            }
            


        }

    }
}