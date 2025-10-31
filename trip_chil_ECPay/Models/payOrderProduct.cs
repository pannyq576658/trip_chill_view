using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trip_chil_ECPay.Models
{
    public class payOrderProduct
    {
        public string payOrderProductID { get; set; }

        public string cartID { get; set; }

        public string name { get; set; }


        public string type { get; set; }

        public int price { get; set; }

        public string pictureUrl { get; set; }


    }
}