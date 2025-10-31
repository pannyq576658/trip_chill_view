using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trip_chill_backend_test.model
{
    public class payOrderContect
    {       
        public string ownerID { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        public List<string> cartItem { get; set; }
    }
}
