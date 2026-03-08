using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace trip_chil_ECPay.Logistics
{
    public class ProjectSet
    {
        public string Route;
        public string ReuteBackendEC;
        public string connectString;
        public ProjectSet()
        {
            Route = ConfigurationManager.AppSettings["Route"];
            ReuteBackendEC = ConfigurationManager.AppSettings["ReuteBackendEC"];
            connectString = ConfigurationManager.AppSettings["ConnectString"];
        }
    }
}