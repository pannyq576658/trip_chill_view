using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trip_chil_ECPay.Logistics
{
    public class ProjectSet
    {      
        public bool is_azure = false;
        public string Route = "https://tripview240306.azurewebsites.net";
        public string ReuteBackendEC = "https://tripchillec240306.azurewebsites.net";
        public string connectString = "Data Source = tripviewserver.database.windows.net; Initial Catalog = tripViewDB03070110; User ID = tripViewAdmin; Password=UT997889Z@;Connect Timeout = 30; Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public ProjectSet()
        {
            if(is_azure)
            {
                Route = "https://tripview240306.azurewebsites.net";
                ReuteBackendEC = "https://tripchillec240306.azurewebsites.net";
                connectString = "Data Source = tripviewserver.database.windows.net; Initial Catalog = tripViewDB03070110; User ID = tripViewAdmin; Password=UT997889Z@;Connect Timeout = 30; Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            }
            else
            {
                Route = "https://localhost:44329";
                ReuteBackendEC = "https://localhost:44372";
                connectString = "Data Source = localhost; Initial Catalog = test3; Integrated Security = SSPI";
            }
        }
    }
}