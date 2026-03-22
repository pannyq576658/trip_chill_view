using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace trip_chill_backend_test.Dao
{
    public class BaseDao
    {
       
        public BaseDao()
        {
         
        }    
        public async Task<int> get_tableID(string tableName)
        {
            string sqlString = $@"select currentlytableNameID from tableItem where tableName=@tableName";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@tableName", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@tableName"].Value = tableName;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return int.Parse(reader[0].ToString());
                        }
                    }
                }
            }
            return 0;
        }
           
        public async Task update_tableID(string tableName, int id)
        {
            string sqlString = $@"update tableItem set currentlytableNameID=@currentlytableNameID where tableName=@tableName";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@currentlytableNameID", System.Data.SqlDbType.Int);
                    command.Parameters["@currentlytableNameID"].Value = id;
                    command.Parameters.Add("@tableName", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@tableName"].Value = tableName;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
