using Microsoft.AspNetCore.Mvc;
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
    public class historyOrderController : ControllerBase
    {
        // GET: api/<historyOrderController>

        HistoryOrderService _service = new HistoryOrderService();
        // GET api/<historyOrderController>/5
        [HttpGet("{ownerID}")]
        public ApiResult<List<payOrder>> Get(string ownerID)
        {
            ApiResult<List<payOrder>> apiResult = new ApiResult<List<payOrder>>();
            try
            {
                List<payOrder> result = _service.getPayOrder(ownerID);
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


        [HttpGet]
        [Route("[action]/{payOrderID}")]
        public ApiResult<List<product>> getHisOrderProductList(string payOrderID)
        {
            ApiResult<List<product>> apiResult = new ApiResult<List<product>>();
            try
            {
                List<product> result = _service.getHisOrderProductList(payOrderID);
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

        [HttpGet]
        [Route("[action]/{retrunCode}")]
        public ApiResult<payOrderReturn> getPayOrderReturn(string retrunCode)
        {
            ApiResult<payOrderReturn> apiResult = new ApiResult<payOrderReturn>();
            if (string.IsNullOrWhiteSpace(retrunCode))
            {
                apiResult.Status = 0;
                apiResult.Msg = "沒資料";
            }

            try
            {
                payOrderReturn result = _service.getPayOrderReturn(retrunCode);
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
