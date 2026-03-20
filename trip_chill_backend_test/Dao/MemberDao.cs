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
        public async Task<string> member_find(string id)
        {
            string sqlString = $@"select COUNT(*) from member where id=@id";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@id"].Value = id;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return reader[0].ToString();
                        }
                    }
                }
            }
            return "0";
        }
        public async Task<member> member_find_data(string id)
        {
            string sqlString = $@"select * from member where id=@id";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@id"].Value = id;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            member m = new member();
                            m.id = reader[0].ToString();
                            m.name = reader[1].ToString();
                            m.cartNum = int.Parse(reader[2].ToString());
                            m.gender = reader[4].ToString();
                            m.platform = reader[3].ToString();
                            m.email = reader[5].ToString();
                            m.birthday = reader[6].ToString();
                            m.pictureUrl = reader[7].ToString();
                            m.password = reader["password"].ToString();
                            return m;
                        }
                    }
                }
            }
            return null;
        }
        public async Task member_insert(member value)
        {
            string sqlString = $@"insert into member (id, name, cartNum,platform,gender,email,birthday,pictureUrl,password,phone)
                          values(@id,@name,@cartNum,@platform,@gender,@email,@birthday,@pictureUrl,@password,@phone)";

            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
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
                    command.Parameters["@password"].Value = (value.password is null) ? "" : value.password;
                    command.Parameters.Add("@phone", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@phone"].Value = (value.phone is null) ? "" : value.phone;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }       
        
        public async Task member_update(member value)
        {
            string sqlString = $@"update member set name=@name, cartNum=@cartNum,gender=@gender,email=@email,birthday=@birthday,pictureUrl=@pictureUrl where id=@id ";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
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
                    await command.ExecuteNonQueryAsync();
                }
            }
        }



    }
}
