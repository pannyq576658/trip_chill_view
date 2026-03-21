using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trip_chill_backend_test.model
{
    public class member
    {
        public string id { get; set; }

        public string name { get; set; }

        public int cartNum { get; set; }
        
        public string platform { get; set; }

        public string gender { get; set; }

        public string email { get; set; }

        public string birthday { get; set; }

        public string pictureUrl { get; set; }

        public string password { get; set; }

        public string phone { get; set; }

        public bool VerifyApproved { get; set; }

    }
}
