using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trip_chill_backend_test.model
{
    public class OrderResultModel
    {
        public int Status { get; set; } //0 失敗 1 成功
        public string Message { get; set; } //失敗：錯誤訊息；成功：結帳網址
    }
}
