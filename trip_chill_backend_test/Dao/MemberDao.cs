using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.model;

namespace trip_chill_backend_test.Dao
{
    public class MemberDao
    {
        SqlConnection sqlConnection;
        ProjectSet Project_Set = new ProjectSet();
        public MemberDao()
        {
            sqlConnection = new SqlConnection(Project_Set.connectString);

            //開啟連線
            sqlConnection.Open();
        }
        public string member_find(string id)
        {

            String sqlString = $@"select COUNT(*) from member where id=@id";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = id;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string aaa = reader[0].ToString();
            reader.Close();
            //sqlConnection.Close();
            return aaa;
        }
        public member member_find_data(string id)
        {

            String sqlString = $@"select * from member where id=@id";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = id;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            member m = new member();

            m.id = reader[0].ToString();
            m.name = reader[1].ToString();
            m.cartNum = int.Parse(reader[2].ToString());
            m.gender = reader[4].ToString();
            m.platform = reader[3].ToString();
            m.email = reader[5].ToString();
            m.birthday = reader[6].ToString();
            m.pictureUrl = reader[7].ToString();
            reader.Close();
            //sqlConnection.Close();
            return m;
        }      
        public string member_find_hasPwd(string id, string pwd)
        {

            String sqlString = $@"select COUNT(*) from member where id=@id and password=@password";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = id;
            command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
            command.Parameters["@password"].Value = pwd;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string aaa = reader[0].ToString();
            reader.Close();
            //sqlConnection.Close();
            return aaa;
        }
        public member member_find_data_hasPwd(string id, string pwd)
        {

            String sqlString = $@"select * from member where id=@id and password=@password";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = id;
            command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
            command.Parameters["@password"].Value = pwd;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            member m = new member();

            m.id = reader[0].ToString();
            m.name = reader[1].ToString();
            m.cartNum = int.Parse(reader[2].ToString());
            m.gender = reader[4].ToString();
            m.platform = reader[3].ToString();
            m.email = reader[5].ToString();
            m.birthday = reader[6].ToString();
            m.pictureUrl = reader[7].ToString();
            reader.Close();
            //sqlConnection.Close();
            return m;
        }
        public void member_insert(member value)
        {
            //將sql語法組成字串
            String sqlString = $@"insert into member (id, name, cartNum,platform,gender,email,birthday,pictureUrl,password,phone)
                          values(@id,@name,@cartNum,@platform,@gender,@email,@birthday,@pictureUrl,@password,@phone)";

            //執行sql語法
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = value.id;
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
            command.Parameters["@name"].Value = value.name;
            command.Parameters.Add("@cartNum", System.Data.SqlDbType.Int);
            command.Parameters["@cartNum"].Value = value.cartNum;
            command.Parameters.Add("@platform", System.Data.SqlDbType.NVarChar);
            command.Parameters["@platform"].Value = value.platform;
            command.Parameters.Add("@gender", System.Data.SqlDbType.NVarChar);
            command.Parameters["@gender"].Value = value.gender;
            command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
            command.Parameters["@email"].Value = value.email;
            command.Parameters.Add("@birthday", System.Data.SqlDbType.NVarChar);
            command.Parameters["@birthday"].Value = value.birthday;
            command.Parameters.Add("@pictureUrl", System.Data.SqlDbType.NVarChar);
            command.Parameters["@pictureUrl"].Value = value.pictureUrl;
            command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
            command.Parameters["@password"].Value = (value.password is null)?"": value.password;
            command.Parameters.Add("@phone", System.Data.SqlDbType.NVarChar);
            command.Parameters["@phone"].Value = (value.phone is null) ? "" : value.phone;

            //取回結果並顯示
            int result = command.ExecuteNonQuery();

            //sqlConnection.Close();
        }
        public void member_update(member value)
        {
            String sqlString = $@"update member set name=@name, cartNum=@cartNum,gender=@gender,email=@email,birthday=@birthday,pictureUrl=@pictureUrl where id=@id ";
            //執行sql語法
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
            command.Parameters["@name"].Value = value.name;
            command.Parameters.Add("@cartNum", System.Data.SqlDbType.Int);
            command.Parameters["@cartNum"].Value = value.cartNum;
            command.Parameters.Add("@gender", System.Data.SqlDbType.NVarChar);
            command.Parameters["@gender"].Value = value.gender;
            command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
            command.Parameters["@email"].Value = value.email;
            command.Parameters.Add("@birthday", System.Data.SqlDbType.NVarChar);
            command.Parameters["@birthday"].Value = value.birthday;
            command.Parameters.Add("@pictureUrl", System.Data.SqlDbType.NVarChar);
            command.Parameters["@pictureUrl"].Value = value.pictureUrl;
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = value.id;
            //取回結果並顯示
            int result = command.ExecuteNonQuery();
        }


    }
}
