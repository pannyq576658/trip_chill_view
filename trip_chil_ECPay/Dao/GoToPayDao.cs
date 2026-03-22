using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using trip_chil_ECPay.Logistics;
using trip_chil_ECPay.Models;

namespace trip_chil_ECPay.Dao
{
    public class GoToPayDao
    {
        
        public GoToPayDao()
        {
            
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
        public async Task update_payOrder(string payOrderID, string retrunCode)
        {
            string sqlString = $@"update payOrder set isPay='1',payDate=CURRENT_TIMESTAMP,retrunCode=@retrunCode where payOrderID=@payOrderID";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@retrunCode", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@retrunCode"].Value = retrunCode;
                    command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@payOrderID"].Value = payOrderID;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task insert_history_product(int historyPayOrderProductID, string payOrderID)
        {
            string sqlString = $@"insert into historyPayOrderProduct(historyPayOrderProductID,payOrderID,productID,quantity) select 'historyPayOrderProduct{historyPayOrderProductID}' as historyPayOrderProductID,payOrderProduct.payOrderID,cartItem.productID,cartItem.quantity from payOrderProduct left join cartItem on payOrderProduct.cartID=cartItem.cartID where payOrderProduct.payOrderID=@payOrderID";
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
        public async Task update_product_buyNum(string payOrderID)
        {
            string sqlString = $@"update product set buyTimeNum = buyTimeNum + 1 where productID in (select cartItem.productID from payOrderProduct left join cartItem on payOrderProduct.cartID = cartItem.cartID where payOrderID = @payOrderID)";
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
        public async Task update_Member_cartNum_after_pay(string payOrderID, int payOrderProduct_COUNT)
        {
            string sqlString = $@"update member set cartNum=cartNum-{payOrderProduct_COUNT} where id=(select ownerID from payOrder where payOrderID=@payOrderID )";
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
        public async Task<int> payOrderProduct_COUNT(string payOrderID)
        {
            string sqlString = $@"select COUNT(*) from payOrderProduct where payOrderID=@payOrderID";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@payOrderID"].Value = payOrderID;
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
        public async Task deleteCart_from_payOrder(string payOrderID)
        {
            string sqlString = $@"delete from cartItem where cartID in (select cartID from payOrderProduct where payOrderID=@payOrderID) ";
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
        public async Task delete_payOrderProduct(string payOrderID)
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
        public async Task update_payOrder_error(string payOrderID, string retrunCode)
        {
            string sqlString = $@"update payOrder set retrunCode=@retrunCode where payOrderID=@payOrderID";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@retrunCode", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@retrunCode"].Value = retrunCode;
                    command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@payOrderID"].Value = payOrderID;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        

    }
}