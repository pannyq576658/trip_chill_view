using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.model;

namespace trip_chill_backend_test.Dao
{
    public class HistoryOrderDao
    {
        SqlConnection sqlConnection;
        ProjectSet Project_Set = new ProjectSet();
        public HistoryOrderDao()
        {
            sqlConnection = new SqlConnection(Project_Set.connectString);
            //開啟連線
            sqlConnection.Open();
        }
        public List<product> getHisOrderProduct(string payOrderID)
        {
            List<product> HisOrderProductList = new List<product>();
            String sqlString = $@"select product.* from historyPayOrderProduct left join product on historyPayOrderProduct.productID=product.productID where historyPayOrderProduct.payOrderID=@payOrderID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HisOrderProductList.Add(new product() { productID = reader[0].ToString(), name = reader[1].ToString(), type = reader[2].ToString(), price = int.Parse(reader[3].ToString()), background = reader[4].ToString(), buyTimeNum = int.Parse(reader[5].ToString()) });
            }
            reader.Close();
            return HisOrderProductList;
        }
        public payOrderReturn getPayOrderReturn(string retrunCode)
        {
            payOrderReturn Return = new payOrderReturn();
            String sqlString = $@"select payOrderID,isPay from payOrder where retrunCode=@retrunCode";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@retrunCode", System.Data.SqlDbType.NVarChar);
            command.Parameters["@retrunCode"].Value = retrunCode;
            SqlDataReader reader = command.ExecuteReader();
            bool HasRows = reader.HasRows;
            if (HasRows)
            {
                reader.Read();
                Return.hasData = true;
                Return.payOrderID = reader[0].ToString();
                Return.isPay = reader[1].ToString();
            }
            else
            {
                Return.hasData = false;
                Return.payOrderID = "";
                Return.isPay = "";
            }
            reader.Close();
            return Return;
        }
        public void updatePayOrderReturn(string retrunCode)
        {
            payOrderReturn Return = new payOrderReturn();
            String sqlString = $@"update payOrder set retrunCode='' where retrunCode=@retrunCode";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@retrunCode", System.Data.SqlDbType.NVarChar);
            command.Parameters["@retrunCode"].Value = retrunCode;
            int result = command.ExecuteNonQuery();
        }

    }
}
