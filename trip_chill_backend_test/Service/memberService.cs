using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.Dao;
using trip_chill_backend_test.model;
using BCrypt.Net;
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

    }
}
