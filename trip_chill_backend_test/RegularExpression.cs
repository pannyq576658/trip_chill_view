using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace trip_chill_backend_test
{
    public class RegularExpression
    {
        public bool validateEmail(string email)
        {
            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }

        public bool validatePhone(string Phone)
        {
            return Regex.IsMatch(Phone, @"^09[0-9]{8}$");
        }
    }
}
