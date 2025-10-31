using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.model;
using trip_chill_backend_test.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace trip_chill_backend_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class cartController : ControllerBase
    {
        cartService _service = new cartService();


        [HttpGet("{owner_id}")]
        
        public ApiResult<List<cart1>> Get(string owner_id)
        {
            ApiResult<List<cart1>> apiResult = new ApiResult<List<cart1>>();
            try
            {
                List<cart1> cartArray = _service.getCart(owner_id);
                apiResult.Status = 1;
                apiResult.Msg = "取得資料";
                apiResult.Data = cartArray;
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }

        [HttpGet]
        [Route("[action]/{owner_id}")]
        public ApiResult<List<cart1>> getCartSelectCheck(string owner_id)
        {
            ApiResult<List<cart1>> apiResult = new ApiResult<List<cart1>>();
            try
            {
                List<cart1> cartArray = _service.getCartSelectCheck(owner_id);
                apiResult.Status = 1;
                apiResult.Msg = "取得資料";
                apiResult.Data = cartArray;
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }


        // POST api/<cartController>
        [HttpPost]
        public ApiResult<string> Post([FromBody] cart value)
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            try
            {
                string result = _service.deleteCart(value);
                apiResult.Status = 1;
                apiResult.Msg = result;
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }


        // POST api/<cartController>
        [HttpPost]
        [Route("[action]")]
        public ApiResult<string> deleteSlect([FromBody] List<cart> value)
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            try
            {
                string result = _service.deleteSlect(value);
                apiResult.Status = 1;
                apiResult.Msg = result;
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }

        // POST api/<cartController>
        [HttpPost]
        [Route("[action]")]
        public ApiResult<string> setCartSelectCheck([FromBody] List<cartSelectCheck> CartDataList)
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            try
            {
                string result = _service.setCartSelectCheck(CartDataList);
                apiResult.Status = 1;
                apiResult.Msg = result;              
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
        public ApiResult<bool> addCart([FromBody] object data)
        {
            JObject jsonObj = JObject.Parse(data.ToString());
            ApiResult<bool> apiResult = new ApiResult<bool>();
            try
            {
                bool result = _service.addCart((string)jsonObj["productID"], (string)jsonObj["ownerID"]);
                apiResult.Status = 1;
                apiResult.Msg = "取得成功";
                apiResult.Data = result;
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;                
            }
            return apiResult;
        }


        [HttpGet]
        [Route("[action]/{productID}/{ownerID}")]
        public ApiResult<bool> CartItemExist(string productID, string ownerID)
        {
            ApiResult<bool> apiResult = new ApiResult<bool>();
            try
            {
                bool result = _service.CartItemExist(productID, ownerID);
                apiResult.Status = 1;
                apiResult.Msg = "取得成功";
                apiResult.Data = result;
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }
        // PUT api/<cartController>/5
        
    }
}
