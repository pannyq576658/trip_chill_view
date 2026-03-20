using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.model;

namespace trip_chill_backend_test.Dao
{
    public class PayOrderDao
    {
        SqlConnection sqlConnection;
        ProjectSet Project_Set = new ProjectSet();
        public PayOrderDao()
        {
            sqlConnection = new SqlConnection(Project_Set.connectString);
            //開啟連線
            sqlConnection.Open();
        }
       
        public async Task<bool> OrderExist(string findIdName, string idValue)
        {
            string sqlString = $@"select * from payOrder where {findIdName}=@{findIdName} and isPay=@isPay ";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    string parameterName = "@" + findIdName;
                    command.Parameters.Add(parameterName, System.Data.SqlDbType.NVarChar);
                    command.Parameters[parameterName].Value = idValue;
                    command.Parameters.Add("@isPay", System.Data.SqlDbType.Bit);
                    command.Parameters["@isPay"].Value = false;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
        public async Task<string> insertOrder(string payOrderID, payOrderContect payOrderContect)
        {
            string sqlString = $@"insert into payOrder (payOrderID,ownerID,name,phone,email,isPay)
                values(@payOrderID,@ownerID,@name,@phone,@email,@isPay)";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@payOrderID"].Value = payOrderID;
                    command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@ownerID"].Value = payOrderContect.ownerID;
                    command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@name"].Value = payOrderContect.name;
                    command.Parameters.Add("@phone", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@phone"].Value = payOrderContect.phone;
                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@email"].Value = payOrderContect.email;
                    command.Parameters.Add("@isPay", System.Data.SqlDbType.Bit);
                    command.Parameters["@isPay"].Value = false;
                    await command.ExecuteNonQueryAsync();
                }
            }
            return "";
        }
        public async Task insertOrderProduct(string payOrderID, string payOrderProductId, string cartID)
        {
            string sqlString = $@"insert into payOrderProduct (payOrderProductID,payOrderID,cartID)
                  values(@payOrderProductID,@payOrderID,@cartID)";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@payOrderProductID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@payOrderProductID"].Value = payOrderProductId;
                    command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@payOrderID"].Value = payOrderID;
                    command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@cartID"].Value = cartID;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task deleteOrderProduct(string payOrderID)
        {
            string sqlString = $@"delete from payOrderProduct where payOrderID=@payOrderID";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@payOrderID"].Value = payOrderID;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<List<payOrderProduct>> getOrderProductList(string payOrderID)
        {
            List<payOrderProduct> payOrderProductArray = new List<payOrderProduct>();
            string sqlString = $@"select payOrderProduct.payOrderProductID,cartItem.cartID,product.name,product.type,product.price,product.background
                              from payOrderProduct left join cartItem on payOrderProduct.cartID=cartItem.cartID 
                              left join product on cartItem.productID=product.productID where payOrderID=@payOrderID";

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
                            payOrderProductArray.Add(new payOrderProduct()
                            {
                                payOrderProductID = reader[0].ToString(),
                                cartID = reader[1].ToString(),
                                name = reader[2].ToString(),
                                type = reader[3].ToString(),
                                price = int.Parse(reader[4].ToString()),
                                pictureUrl = reader[5].ToString()
                            });
                        }
                    }
                }
            }
            return payOrderProductArray;
        }
             
        public async Task<List<payOrder>> getPayOrder(string ownerID)
        {
            List<payOrder> payOrderList = new List<payOrder>();
            string sqlString = $@"select * from payOrder where ownerID=@ownerID and isPay=@isPay ";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@ownerID"].Value = ownerID;
                    command.Parameters.Add("@isPay", System.Data.SqlDbType.Bit);
                    command.Parameters["@isPay"].Value = true;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            payOrderList.Add(new payOrder()
                            {
                                payOrderID = reader[0].ToString(),
                                name = reader[2].ToString(),
                                phone = reader[3].ToString(),
                                email = reader[4].ToString(),
                                payDate = reader[6].ToString()
                            });
                        }
                    }
                }
            }
            return payOrderList;
        }
    }
}
