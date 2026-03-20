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
        public async Task<ApiResult<List<payOrder>>> Get(string ownerID)
        {
            ApiResult<List<payOrder>> apiResult = new ApiResult<List<payOrder>>();
            try
            {
                List<payOrder> result = await _service.getPayOrder(ownerID);
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
        public async Task<ApiResult<List<product>>> getHisOrderProductList(string payOrderID)
        {
            ApiResult<List<product>> apiResult = new ApiResult<List<product>>();
            try
            {
                List<product> result = await _service.getHisOrderProductList(payOrderID);
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
        public async Task<ApiResult<payOrderReturn>> getPayOrderReturn(string retrunCode)
        {
            ApiResult<payOrderReturn> apiResult = new ApiResult<payOrderReturn>();
            if (string.IsNullOrWhiteSpace(retrunCode))
            {
                apiResult.Status = 0;
                apiResult.Msg = "沒資料";
            }

            try
            {
                payOrderReturn result = await _service.getPayOrderReturn(retrunCode);
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
