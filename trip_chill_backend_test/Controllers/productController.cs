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
    public class productController : ControllerBase
    {
        ProductService _service = new ProductService();
        // GET: api/<productController>
        [HttpGet]
        public ApiResult<List<product>> Get()
        {
            ApiResult<List<product>> apiResult = new ApiResult<List<product>>();
            try
            {
                List<product> result = _service.getProductList();
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

        // GET api/<productController>/5
        [HttpGet("{id}")]
        public ApiResult<List<product>> Get(int id)
        {
            ApiResult<List<product>> apiResult = new ApiResult<List<product>>();
            try
            {
                List<product> result = _service.getProductRange(id);
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

          [Route("[action]/{ProductID}")]      
          public ApiResult<product> getProduct(string ProductID)
          {
            ApiResult<product> apiResult = new ApiResult<product>();
            try
            {
               product result = _service.getProduct(ProductID);
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
        [Route("[action]")]
        public ApiResult<int> productNum()
        {
            ApiResult<int> apiResult = new ApiResult<int>();
             try
             {
                 int result = _service.productNum();
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
