using ECPay.Payment.Integration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using trip_chil_ECPay.Dao;
using trip_chil_ECPay.Logistics;
using trip_chil_ECPay.Models;

namespace trip_chil_ECPay.Service
{
    public class goToPayService
    {
        CustomerLogic Logic = new CustomerLogic();
        ProjectSet Project_Set = new ProjectSet();
        GoToPayDao _GoToPayDao = new GoToPayDao();
        BaseDao _BaseDao = new BaseDao();
        public async Task<string> EC_Page(string payOrderID, string DateTimeNumber)
        {
            List<string> enErrors = new List<string>();          
            List<payOrderProduct> payOrderProduct_List = await _GoToPayDao.getOrderProductList(payOrderID);            
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    /* 服務參數 */
                    oPayment.ServiceMethod = HttpMethod.HttpPOST;//介接服務時，呼叫 API 的方法
                    oPayment.ServiceURL = "https://payment-stage.ecpay.com.tw/Cashier/AioCheckOut/V5";//要呼叫介接服務的網址
                    oPayment.HashKey = "pwFHCqoQZGmho4w6";//ECPay提供的Hash Key
                    oPayment.HashIV = "EkRm7iFT261dpevs";//ECPay提供的Hash IV
                    oPayment.MerchantID = "3002607";//ECPay提供的特店編號


                    /* 基本參數 */
                    oPayment.Send.ReturnURL = "http://example.com";//付款完成通知回傳的網址
                    oPayment.Send.ClientBackURL = "http://www.ecpay.com.tw/";//瀏覽器端返回的廠商網址
                    oPayment.Send.OrderResultURL = Project_Set.ReuteBackendEC + "/goToPay/orderResult?payOrderID=" + payOrderID +"@"+ DateTimeNumber;//瀏覽器端回傳付款結果網址
                    oPayment.Send.MerchantTradeNo = payOrderID+ DateTimeNumber;//廠商的交易編號
                    oPayment.Send.MerchantTradeDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//廠商的交易時間
                    oPayment.Send.TotalAmount = Decimal.Parse("240");//交易總金額
                    oPayment.Send.TradeDesc = "交易描述2";//交易描述
                    oPayment.Send.ChoosePayment = PaymentMethod.Credit;//使用的付款方式
                    oPayment.Send.Remark = "備註欄位";//備註欄位

                    oPayment.Send.ChooseSubPayment = PaymentMethodItem.None;//使用的付款子項目
                    oPayment.Send.NeedExtraPaidInfo = ExtraPaymentInfo.Yes;//是否需要額外的付款資訊
                    oPayment.Send.DeviceSource = DeviceType.PC;//來源裝置
                    oPayment.Send.IgnorePayment = ""; //不顯示的付款方式
                    oPayment.Send.PlatformID = "";//特約合作平台商代號
                    oPayment.Send.CustomField1 = "";
                    oPayment.Send.CustomField2 = "";
                    oPayment.Send.CustomField3 = "";
                    oPayment.Send.CustomField4 = "";
                    oPayment.Send.EncryptType = 1;
                    foreach (payOrderProduct payOrderProduct in payOrderProduct_List)
                    {
                        oPayment.Send.Items.Add(new Item()
                        {
                            Name = payOrderProduct.name,//商品名稱
                            Price = payOrderProduct.price,//商品單價
                            Currency = "NT",//幣別單位
                            Quantity = Int32.Parse("1"),//購買數量
                            URL = "http://google.com",//商品的說明網址

                        });
                    }
                    Decimal TotalPrice = 0;
                    foreach (var itemPrice in oPayment.Send.Items)
                        TotalPrice += itemPrice.Price * itemPrice.Quantity;
                    oPayment.Send.TotalAmount = TotalPrice;//交易總金額
                    
                    var _html = string.Empty;
                    enErrors.AddRange(oPayment.CheckOutString(ref _html));
                    return _html;
                }
            }
            catch (Exception ex)
            {
                // 例外錯誤處理。
                enErrors.Add(ex.Message);
                var _errorMsg = string.Join(",", enErrors);
                return _errorMsg;

            }
        }
        public async Task<EC_reult_Model> orderResult(string payOrderID)
        {
            List<string> enErrors = new List<string>();
            Hashtable htFeedback = null;        
            try
            {
                using (AllInOne oPayment = new AllInOne())
                {
                    oPayment.HashKey = "pwFHCqoQZGmho4w6";//ECPay提供的Hash Key
                    oPayment.HashIV = "EkRm7iFT261dpevs";//ECPay提供的Hash IV
                    enErrors.AddRange(oPayment.CheckOutFeedback(ref htFeedback));
                }

                if (enErrors.Any() == false)
                {
                    if (htFeedback["MerchantTradeNo"].ToString() != payOrderID.Replace("@", ""))
                       return new EC_reult_Model { Status = 1, Redirect_url = Project_Set.Route + "/" };
                    string payOrder_ID = payOrderID.Split('@')[0];
                    string retrunCode = payOrderID + htFeedback["TradeNo"].ToString();
                    //把payOrder改成已購買並且加入購買時間
                    await _GoToPayDao.update_payOrder(payOrder_ID, retrunCode);
                    //新增歷史訂單清單
                    int historyPayOrderProductID = await _BaseDao.get_tableID("historyPayOrderProduct") + 1;
                    await _GoToPayDao.insert_history_product(historyPayOrderProductID, payOrder_ID);
                    await _BaseDao.update_tableID("historyPayOrderProduct", historyPayOrderProductID);
                    //更新產品的購買數
                    await _GoToPayDao.update_product_buyNum(payOrder_ID);
                    //更新會員購物車的數量
                    int payOrderProduct_COUNT = await _GoToPayDao.payOrderProduct_COUNT(payOrder_ID);
                    await _GoToPayDao.update_Member_cartNum_after_pay(payOrder_ID, payOrderProduct_COUNT);
                    //刪除購物車的資料
                    await _GoToPayDao.deleteCart_from_payOrder(payOrder_ID);
                    //刪除訂單清單的資料
                    await _GoToPayDao.delete_payOrderProduct(payOrder_ID);

                    return new EC_reult_Model { Status = 1, Redirect_url = Project_Set.Route + "/shoppingCart-return/" + retrunCode };
                }
                else
                {                 
                    string payOrder_ID = payOrderID.Split('@')[0];
                    string retrunCode = payOrderID + htFeedback["TradeNo"].ToString();
                    await _GoToPayDao.update_payOrder_error(payOrder_ID, retrunCode);
                    return new EC_reult_Model { Status = 1, Redirect_url = Project_Set.Route + "/shoppingCart-return/" + retrunCode };
                }

            }
            catch (Exception ex)
            {
                enErrors.Add(ex.Message);
                return new EC_reult_Model { Status = 0, Redirect_url = null,Message= string.Join("<br />", enErrors) };             
            }
        }



    }
}