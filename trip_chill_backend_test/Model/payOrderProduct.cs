using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trip_chill_backend_test.model
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
