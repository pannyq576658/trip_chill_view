using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.Dao;
using trip_chill_backend_test.model;
using BCrypt.Net;
using MimeKit;
using MailKit.Net.Smtp;

namespace trip_chill_backend_test.Service
{
    public class memberService
    {
        MemberDao _Dao = new MemberDao();
        
        public async Task<member> login(string id)
        {
            if (await _Dao.member_find(id) == "0")
            {
                throw new Exception("請去註冊");
            }
            else
            {
                return await _Dao.member_find_data(id);
            }
        }
        public async Task<member> apilogin(member Member)
        {
             if (await _Dao.member_find(Member.id) == "0")
             {
                 throw new Exception("請去註冊");
             }
             else
             {
                //await _Dao.member_update(Member);
                return await _Dao.member_find_data(Member.id);
            }          
        }
        public async Task<member> loginHasPwd(string id, string pwd)
        {
            if (await _Dao.member_find(id) == "0")
            {
                throw new Exception("請去註冊");
            }

            member m = await _Dao.member_find_data(id);
            if (m == null || !BCrypt.Net.BCrypt.Verify(pwd, m.password))
            {
                throw new Exception("請檢查密碼是否正確");
            }

            return m;
        }
        public async Task<string> insertMember(member Member)
        {
            if (await _Dao.member_find(Member.id) == "0")
            {
                // 密碼雜湊
                if (!string.IsNullOrEmpty(Member.password))
                {
                    Member.password = BCrypt.Net.BCrypt.HashPassword(Member.password);
                }

                await _Dao.member_insert(Member);
                return "註冊成功";
            }
            else
            {
                return "已經註冊過了";
            }
        }
       
        

        public async Task<string> updateMember(member Member)
        {
            await _Dao.member_update(Member);
            return "修改成功";
        }
        public async Task<bool> SaveAndSendEmail(string userId, string email, string code)
        {
            // 1. 呼叫 DAO 將驗證碼存入資料庫 (需記錄 userId, code, 建立時間)
            await _Dao.UpdateVerifyCode(userId, code);
            //return true;
            // 2. 發送郵件 (使用 SmtpClient)
              try
              {
                  var message = new MimeMessage();
                  message.From.Add(new MailboxAddress("TripChill 客服", "pannyq576658@gmail.com"));
                  message.To.Add(new MailboxAddress("", email));
                  message.Subject = "您的帳號驗證碼";
                  message.Body = new TextPart("plain") { Text = $"您的TripChill網站驗證碼是：{code}，請於 3 分鐘內輸入。" };

                  using (var client = new SmtpClient())
                  {
                      client.Connect("smtp.gmail.com", 587, false);
                      client.Authenticate("pannyq576658@gmail.com", "iylu hzvj lvaz iyus"); // 需使用應用程式密碼
                      client.Send(message);
                      client.Disconnect(true);
                  }
                  return true;
              }
              catch
              {
                  return false;
              }
        }
        public async Task<bool> confirmEmail(string userId, string code)
        {
            bool result = await _Dao.VerifyEmailCode(userId, code);
            await _Dao.clearVerifyCode(userId);
            if (result)
            {
                await _Dao.VerifyApproved(userId);

                return true;
            }
            else
                return false;
                                        
        }
    }
}
