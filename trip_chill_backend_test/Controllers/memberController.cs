using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.model;
using trip_chill_backend_test.Service;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace trip_chill_backend_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class memberController : ControllerBase
    {
        memberService _service = new memberService();
        private readonly IWebHostEnvironment _env;

        public memberController(IWebHostEnvironment env)
        {
            _env = env;
        }
        // GET api/<memberController>/5
        //會員平台登入
        [HttpGet("{id}")]
        public async Task<ApiResult<member>> Get(string id)
        {
            ApiResult<member> apiResult = new ApiResult<member>();
            try
            {
                member memberData = await _service.login(id);
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
        
        //會員登入 (改用 POST 並在 Body 傳遞資料)
        [HttpPost("login")]
        public async Task<ApiResult<member>> Login([FromBody] LoginRequest request)
        {
            ApiResult<member> apiResult = new ApiResult<member>();
            try
            {
                member memberData = await _service.loginHasPwd(request.id, request.password);
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

        //會員登入 (改用 POST 並在 Body 傳遞資料)
        [HttpPost("apiLogin")]
        public async Task<ApiResult<member>> apiLogin([FromBody] member Member)
        {
            ApiResult<member> apiResult = new ApiResult<member>();
            try
            {
                member memberData = await _service.apilogin(Member);
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

        //會員註冊
        [HttpPost]
        [Route("[action]")]       
        public async Task<ApiResult<string>> insertMember([FromBody] member Member)
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            try
            {

                if (!string.IsNullOrEmpty(Member.pictureUrl))
                {
                    string uploadsFolder = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", Member.id);
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_profile.jpg";
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    if (Member.pictureUrl.StartsWith("http"))
                    {
                        // 處理遠端 URL (如 LINE/Google 頭像)
                        using (var httpClient = new HttpClient())
                        {
                            var imageBytes = await httpClient.GetByteArrayAsync(Member.pictureUrl);
                            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                            Member.pictureUrl = $"/uploads/{Member.id}/{fileName}";
                        }
                    }
                    else if (Member.pictureUrl.StartsWith("data:image"))
                    {
                        // 處理 Base64 字串
                        var base64Data = Member.pictureUrl.Substring(Member.pictureUrl.IndexOf(",") + 1);
                        var imageBytes = Convert.FromBase64String(base64Data);
                        await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);
                        Member.pictureUrl = $"/uploads/{Member.id}/{fileName}";
                    }
                }

                string result = await _service.insertMember(Member);
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
        public async Task<ApiResult<string>> updateMember([FromBody] member Member)
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            try
            {
                string result = await _service.updateMember(Member);
                apiResult.Status = 1;
                apiResult.Msg = result;
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }//

        [HttpPost]
        [Route("[action]")]
        public async Task<bool> sendVerifyEmail([FromBody] VerifyEmailRequest Request)
        {
              string code = new Random().Next(100000, 999999).ToString();

              bool success = await _service.SaveAndSendEmail(Request.userId, Request.email, code);
              return success;

            
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ApiResult<bool>> confirmEmailAsync([FromBody] VerifyEmailRequest Request)
        {
            ApiResult<bool> apiResult = new ApiResult<bool>();
            try
            {
                bool result = await _service.confirmEmail(Request.userId, Request.code);
                if(result)
                 apiResult.Msg = "信箱驗證成功";
                else
                 apiResult.Msg = "信箱驗證失敗";
                apiResult.Data = result;
                
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = ex.Message;
            }
            return apiResult;
        }

        [HttpPost("uploadPicture")]
        public async Task<ApiResult<string>> UploadPicture([FromForm] string userId, IFormFile file)
        {
            ApiResult<string> apiResult = new ApiResult<string>();
            try
            {
                if (file == null || file.Length == 0)
                {
                    apiResult.Status = 0;
                    apiResult.Msg = "請選擇檔案";
                    return apiResult;
                }
                if (string.IsNullOrEmpty(userId))
                {
                    apiResult.Status = 0;
                    apiResult.Msg = "缺少使用者 ID";
                    return apiResult;
                }


                // 建立上傳目錄
                string uploadsFolder = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", userId);
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                else
                {
                    // 教育性見解：為了確保每個使用者資料夾內只有一張照片，
                    // 在儲存新檔案前，先刪除該目錄下的所有舊檔案。
                    DirectoryInfo di = new DirectoryInfo(uploadsFolder);
                    foreach (FileInfo fileInfo in di.GetFiles())
                    {
                        fileInfo.Delete();
                    }
                }

                // 產生檔名
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);

                // 儲存檔案
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // 回傳相對路徑網址
                apiResult.Status = 1;
                apiResult.Msg = "上傳成功";
                apiResult.Data = $"/uploads/{userId}/{fileName}";
            }
            catch (Exception ex)
            {
                apiResult.Status = 0;
                apiResult.Msg = "上傳失敗：" + ex.Message;
            }
            return apiResult;
        }

    }
}
