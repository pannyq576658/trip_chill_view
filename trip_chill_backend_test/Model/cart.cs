using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trip_chill_backend_test.model
{
    public class cart
    {
        public string cartID { get; set; }

        public string productID { get; set; }


        public int quantity { get; set; }

        
        public string ownerID { get; set; }

    }
}
