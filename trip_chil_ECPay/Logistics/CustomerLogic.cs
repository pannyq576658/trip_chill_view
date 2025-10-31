using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using trip_chil_ECPay.Models;

namespace trip_chil_ECPay.Logistics
{
    public class CustomerLogic
    {
        public bool payOrderExist(string payOrderID)
        {
            sql SQL = new sql();
            bool OrderExist = SQL.OrderExist("payOrderID", payOrderID);
            SQL.sql_close();
            return OrderExist;
        }

        public List<payOrderProduct> Get_payOrderProduct_List(string payOrderID)
        {
            sql SQL = new sql();
            List<payOrderProduct> payOrderProductList = SQL.getOrderProductList(payOrderID);
            SQL.sql_close();
            return payOrderProductList;
        }

        public void update_payOrder(string payOrderID,string retrunCode)
        {
            sql SQL = new sql();
            SQL.update_payOrder(payOrderID, retrunCode);
            SQL.sql_close();

        }

        public void update_payOrder_error(string payOrderID, string retrunCode)
        {
            sql SQL = new sql();
            SQL.update_payOrder_error(payOrderID, retrunCode);
            SQL.sql_close();

        }

        public void deleteCart_from_payOrder(string payOrderID)
        {
            sql SQL = new sql();
            SQL.deleteCart_from_payOrder(payOrderID);
            SQL.sql_close();
        }

        public void delete_payOrderProduct(string payOrderID)
        {
            sql SQL = new sql();
            SQL.delete_payOrderProduct(payOrderID);
            SQL.sql_close();
        }

        public void update_Member_cartNum_after_pay(string payOrderID)
        {
            sql SQL = new sql();
            SQL.update_Member_cartNum_after_pay(payOrderID);
            SQL.sql_close();
        }
        public void update_product_buyNum(string payOrderID)
        {
            sql SQL = new sql();
            SQL.update_product_buyNum(payOrderID);
            SQL.sql_close();
        }

        public void insert_history_product(string payOrderID)
        {
            sql SQL = new sql();
            SQL.insert_history_product(payOrderID);
            SQL.sql_close();
        }

    }
    
        public class sql
        {          
        ProjectSet Project_Set = new ProjectSet();
        SqlConnection sqlConnection;
            public sql()
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

        public void update_Member_cartNum_after_pay(string payOrderID)
        {
            
            String sqlString = $@"update member set cartNum=cartNum-{payOrderProduct_COUNT(payOrderID)} where id=(select ownerID from payOrder where payOrderID=@payOrderID )";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();
        }

        public void update_product_buyNum(string payOrderID)
        {
            String sqlString = $@"update product set buyTimeNum = buyTimeNum + 1 where productID in (select cartItem.productID from payOrderProduct left join cartItem on payOrderProduct.cartID = cartItem.cartID where payOrderID = @payOrderID)";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();
        }

        public void insert_history_product(string payOrderID)
        {
            int i=get_tableID("historyPayOrderProduct") +1;
            String sqlString = $@"insert into historyPayOrderProduct(historyPayOrderProductID,payOrderID,productID,quantity) select 'historyPayOrderProduct{i}' as historyPayOrderProductID,payOrderProduct.payOrderID,cartItem.productID,cartItem.quantity from payOrderProduct left join cartItem on payOrderProduct.cartID=cartItem.cartID where payOrderProduct.payOrderID=@payOrderID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();
            update_tableID("historyPayOrderProduct", i);
        }
        public bool OrderExist(string findIdName, string idValue)
        {

            String sqlString = $@"select * from payOrder where {findIdName}=@{findIdName} ";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            string parameterName = "@" + findIdName;
            command.Parameters.Add(parameterName, System.Data.SqlDbType.NVarChar);
            command.Parameters[parameterName].Value = idValue;
            SqlDataReader reader = command.ExecuteReader();
            bool HasRows = reader.HasRows;
            reader.Close();
            return HasRows;
        }

           public void update_payOrder(string payOrderID,string retrunCode)
           {

            String sqlString = $@"update payOrder set isPay='1',payDate=CURRENT_TIMESTAMP,retrunCode=@retrunCode where payOrderID=@payOrderID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@retrunCode", System.Data.SqlDbType.NVarChar);
            command.Parameters["@retrunCode"].Value = retrunCode;
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



            public void sql_close()
            {
                sqlConnection.Close();
            }
        }

   


}