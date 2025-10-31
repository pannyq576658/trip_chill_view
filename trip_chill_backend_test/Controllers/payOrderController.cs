using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using trip_chill_backend_test.model;
using trip_chill_backend_test.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace trip_chill_backend_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class payOrderController : ControllerBase
    {
        PayOrderService _service = new PayOrderService();
        // GET: api/<payOrderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet]
        [Route("[action]/{payOrderID}")]
        public ApiResult<bool> payOrderExist(string payOrderID)
        {
            ApiResult<bool> apiResult = new ApiResult<bool>();
            try
            {
                bool result = _service.payOrderExist(payOrderID);
                apiResult.Status = 1;
                apiResult.Msg = "取得資料";
                apiResult.Data = result;
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }

        [HttpPost]
        [Route("[action]")]
        public OrderResultModel setContact_and_goToPay([FromBody] payOrderContect payOrderContect)
        {
            
            //檢查訂單資料是否正確
            if (string.IsNullOrWhiteSpace(payOrderContect.name)
                || string.IsNullOrWhiteSpace(payOrderContect.phone)
                || string.IsNullOrWhiteSpace(payOrderContect.email)
                || string.IsNullOrWhiteSpace(payOrderContect.ownerID))
            {
                return new OrderResultModel { Status = 0, Message = "訂單資料不完整，請重新輸入。" };
            }
            else if(!Regex.IsMatch(payOrderContect.phone, @"^09[0-9]{8}$") )
            {
                return new OrderResultModel { Status = 0, Message = "電話格式不對，請重新輸入。" };
            }
            else if (!Regex.IsMatch(payOrderContect.email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                return new OrderResultModel { Status = 0, Message = "email格式不對，請重新輸入。" };
            }
            else
            {
                try
                {
                    string result = _service.setContact_and_goToPay(payOrderContect);
                    return new OrderResultModel { Status = 1, Message = result };
                }
                catch (Exception ex)
                {
                    return new OrderResultModel { Status = 0, Message = ex.Message };
                }
               
            }
        }

        // GET api/<payOrderController>/5
        [HttpGet("{payOrderID}")]
        public ApiResult<List<payOrderProduct>> Get(string payOrderID)
        {
            ApiResult<List<payOrderProduct>> apiResult = new ApiResult<List<payOrderProduct>>();
            try
            {
                List<payOrderProduct> result = _service.getOrderProductList(payOrderID);
                apiResult.Status = 1;
                apiResult.Msg = "取得資料";
                apiResult.Data = result;
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }

    }
}
