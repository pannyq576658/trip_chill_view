using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trip_chill_backend_test.model
{
    public class VerifyEmailRequest
    {
        public string userId { get; set; }
        public string email { get; set; }

        public string code { get; set; }

    }
}
