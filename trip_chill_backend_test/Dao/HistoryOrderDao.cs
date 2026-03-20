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
        public async Task<List<product>> getHisOrderProduct(string payOrderID)
        {
            List<product> HisOrderProductList = new List<product>();
            string sqlString = $@"select product.* from historyPayOrderProduct left join product on historyPayOrderProduct.productID=product.productID where historyPayOrderProduct.payOrderID=@payOrderID";

            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@payOrderID"].Value = payOrderID;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            HisOrderProductList.Add(new product()
                            {
                                productID = reader[0].ToString(),
                                name = reader[1].ToString(),
                                type = reader[2].ToString(),
                                price = int.Parse(reader[3].ToString()),
                                background = reader[4].ToString(),
                                buyTimeNum = int.Parse(reader[5].ToString())
                            });
                        }
                    }
                }
            }
            return HisOrderProductList;
        }           
        public async Task<payOrderReturn> getPayOrderReturn(string retrunCode)
        {
            payOrderReturn Return = new payOrderReturn();
            string sqlString = $@"select payOrderID,isPay from payOrder where retrunCode=@retrunCode";

            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@retrunCode", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@retrunCode"].Value = retrunCode;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
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
                    }
                }
            }
            return Return;
        }
        public async Task updatePayOrderReturn(string retrunCode)
        {
            string sqlString = $@"update payOrder set retrunCode='' where retrunCode=@retrunCode";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@retrunCode", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@retrunCode"].Value = retrunCode;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


    }
}
