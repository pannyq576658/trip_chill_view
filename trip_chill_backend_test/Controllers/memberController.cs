using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.model;
using trip_chill_backend_test.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace trip_chill_backend_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class memberController : ControllerBase
    {
        memberService _service = new memberService();

        // GET api/<memberController>/5
        //會員平台登入
        [HttpGet("{id}")]
        public ApiResult<member> Get(string id)
        {
            ApiResult<member> apiResult = new ApiResult<member>();
            try
            {
                member memberData = _service.login(id);
                apiResult.Status = 1;
                apiResult.Msg = "取得資料";
                apiResult.Data = memberData;              
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;           
            }
            return apiResult;
        }
        //會員純登入
        /*  [HttpGet("{id}/{pwd}")]
          public ApiResult<member> Get(string id, string pwd)
          {
              ApiResult<member> apiResult = new ApiResult<member>();
              try
              {
                  member memberData = _service.loginHasPwd(id,pwd);
                  apiResult.Status = 1;
                  apiResult.Msg = "取得資料";
                  apiResult.Data = memberData;
              }
              catch (Exception ex)
              {
                  apiResult.Status = 0;
                  apiResult.Msg = ex.Message;
              }
              return apiResult;
          }*/

        //會員登入 (改用 POST 並在 Body 傳遞資料)
        [HttpPost("login")]
        public ApiResult<member> Login([FromBody] LoginRequest request)
        {
            ApiResult<member> apiResult = new ApiResult<member>();
            try
            {
                member memberData = _service.loginHasPwd(request.id, request.password);
                apiResult.Status = 1;
                apiResult.Msg = "登入成功";
                apiResult.Data = memberData;
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }


        //會員註冊
        [HttpPost]
        [Route("[action]")]       
        public ApiResult<string> insertMember([FromBody] member Member)
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            try
            {
                string result = _service.insertMember(Member);
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
        public ApiResult<string> updateMember([FromBody] member Member)
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            try
            {
                string result = _service.updateMember(Member);
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
       
    }
}
