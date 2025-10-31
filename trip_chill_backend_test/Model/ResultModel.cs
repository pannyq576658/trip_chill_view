using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trip_chill_backend_test.model
{
    public class ApiResult<T>
    {
        public int Status { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }
}
