using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using trip_chil_ECPay.Logistics;
using trip_chil_ECPay.Models;

namespace trip_chil_ECPay.Dao
{
    public class GoToPayDao
    {
        SqlConnection sqlConnection;
        ProjectSet Project_Set = new ProjectSet();
        public GoToPayDao()
        {
            sqlConnection = new SqlConnection(Project_Set.connectString);

            //開啟連線
            sqlConnection.Open();
        }
        public List<payOrderProduct> getOrderProductList(string payOrderID)
        {


            List<payOrderProduct> payOrderProductArray = new List<payOrderProduct>();
            String sqlString = $@"select payOrderProduct.payOrderProductID,cartItem.cartID,product.name,product.type,product.price,product.background
                              from payOrderProduct left join cartItem on payOrderProduct.cartID=cartItem.cartID 
                              left join product on cartItem.productID=product.productID where payOrderID=@payOrderID";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                payOrderProductArray.Add(new payOrderProduct() { payOrderProductID = reader[0].ToString(), cartID = reader[1].ToString(), name = reader[2].ToString(), type = reader[3].ToString(), price = int.Parse(reader[4].ToString()), pictureUrl = reader[5].ToString() });
            }
            reader.Close();

            return payOrderProductArray;

        }
        public void update_payOrder(string payOrderID, string retrunCode)
        {

            String sqlString = $@"update payOrder set isPay='1',payDate=CURRENT_TIMESTAMP,retrunCode=@retrunCode where payOrderID=@payOrderID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@retrunCode", System.Data.SqlDbType.NVarChar);
            command.Parameters["@retrunCode"].Value = retrunCode;
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();

        }
        public void insert_history_product(int historyPayOrderProductID,string payOrderID)
        {
            //int i = get_tableID("historyPayOrderProduct") + 1;
            String sqlString = $@"insert into historyPayOrderProduct(historyPayOrderProductID,payOrderID,productID,quantity) select 'historyPayOrderProduct{historyPayOrderProductID}' as historyPayOrderProductID,payOrderProduct.payOrderID,cartItem.productID,cartItem.quantity from payOrderProduct left join cartItem on payOrderProduct.cartID=cartItem.cartID where payOrderProduct.payOrderID=@payOrderID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();
            //update_tableID("historyPayOrderProduct", i);
        }
        public void update_product_buyNum(string payOrderID)
        {
            String sqlString = $@"update product set buyTimeNum = buyTimeNum + 1 where productID in (select cartItem.productID from payOrderProduct left join cartItem on payOrderProduct.cartID = cartItem.cartID where payOrderID = @payOrderID)";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();
        }
        public void update_Member_cartNum_after_pay(string payOrderID,int payOrderProduct_COUNT)
        {

            String sqlString = $@"update member set cartNum=cartNum-{payOrderProduct_COUNT} where id=(select ownerID from payOrder where payOrderID=@payOrderID )";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();
        }
        public int payOrderProduct_COUNT(string payOrderID)
        {
            String sqlString = $@"select COUNT(*) from payOrderProduct where payOrderID=@payOrderID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int COUNT = int.Parse(reader[0].ToString());
            reader.Close();
            //sqlConnection.Close();
            return COUNT;
        }
        public void deleteCart_from_payOrder(string payOrderID)
        {
            String sqlString = $@"delete from cartItem where cartID in (select cartID from payOrderProduct where payOrderID=@payOrderID) ";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();
        }
        public void delete_payOrderProduct(string payOrderID)
        {
            String sqlString = $@"delete from payOrderProduct where payOrderID=@payOrderID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();
        }
        public void update_payOrder_error(string payOrderID, string retrunCode)
        {

            String sqlString = $@"update payOrder set retrunCode=@retrunCode where payOrderID=@payOrderID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@retrunCode", System.Data.SqlDbType.NVarChar);
            command.Parameters["@retrunCode"].Value = retrunCode;
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();

        }

    }
}