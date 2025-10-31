using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trip_chil_ECPay.Models
{
    public class EC_reult_Model
    {
        public int Status { get; set; } //0 失敗 1 成功

        public string Message { get; set; } //失敗：錯誤訊息；成功：結帳網址
        public string Redirect_url { get; set; }
    }
}