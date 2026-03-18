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
        public member login(string id)
        {
            if (_Dao.member_find(id) == "0")
            {
                throw new Exception("請去註冊");
            }
            else
            {
                member m = _Dao.member_find_data(id);
                return m;
            }
        }

        /*public member loginHasPwd(string id,string pwd)
        {
            if (_Dao.member_find(id) == "0")
            {
                throw new Exception("請去註冊");
            }
            else if (_Dao.member_find_hasPwd(id, pwd) == "0")
            {
                throw new Exception("請檢查密碼是否正確");
            }
            else
            {
                member m = _Dao.member_find_data_hasPwd(id, pwd);
                return m;
            }
            
        }*/
        public member loginHasPwd(string id, string pwd)
        {
            if (_Dao.member_find(id) == "0")
            {
                throw new Exception("請去註冊");
            }

            member m = _Dao.member_find_data(id);
            if (m == null || !BCrypt.Net.BCrypt.Verify(pwd, m.password))
            {
                throw new Exception("請檢查密碼是否正確");
            }

            return m;
        }
        public string insertMember(member Member)
        {
            if (_Dao.member_find(Member.id) == "0")
            {
                // 密碼雜湊
                if (!string.IsNullOrEmpty(Member.password))
                {
                    Member.password = BCrypt.Net.BCrypt.HashPassword(Member.password);
                }

                _Dao.member_insert(Member);
                return "註冊成功";
            }
            else
            {
                return "已經註冊過了";
            }

        }

        public string updateMember(member Member)
        {
            _Dao.member_update(Member);
            return "修改成功";
        }


    }
}
