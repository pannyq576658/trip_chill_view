using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using trip_chil_ECPay.Logistics;

namespace trip_chil_ECPay.Dao
{
    public class BaseDao
    {
        SqlConnection sqlConnection;
        ProjectSet Project_Set = new ProjectSet();
        public BaseDao()
        {
            sqlConnection = new SqlConnection(Project_Set.connectString);

            //開啟連線
            sqlConnection.Open();
        }
        public int get_tableID(string tableName)
        {
            String sqlString = $@"select currentlytableNameID  from tableItem where tableName=@tableName";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@tableName", System.Data.SqlDbType.NVarChar);
            command.Parameters["@tableName"].Value = tableName;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int id = int.Parse(reader[0].ToString());
            reader.Close();

            return id;
        }
        public void update_tableID(string tableName, int id)
        {
            String sqlString = $@"update tableItem set currentlytableNameID=@currentlytableNameID where tableName=@tableName";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@currentlytableNameID", System.Data.SqlDbType.Int);
            command.Parameters["@currentlytableNameID"].Value = id;
            command.Parameters.Add("@tableName", System.Data.SqlDbType.NVarChar);
            command.Parameters["@tableName"].Value = tableName;
            int result = command.ExecuteNonQuery();
        }
    }
}