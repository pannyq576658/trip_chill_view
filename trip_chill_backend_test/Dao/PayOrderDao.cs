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
        public bool OrderExist(string findIdName, string idValue)
        {

            String sqlString = $@"select * from payOrder where {findIdName}=@{findIdName} and isPay=@isPay ";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            string parameterName = "@" + findIdName;
            command.Parameters.Add(parameterName, System.Data.SqlDbType.NVarChar);
            command.Parameters[parameterName].Value = idValue;
            command.Parameters.Add("@isPay", System.Data.SqlDbType.Bit);
            command.Parameters["@isPay"].Value = false;
            SqlDataReader reader = command.ExecuteReader();
            bool HasRows = reader.HasRows;
            reader.Close();
            return HasRows;
        }
        public string insertOrder(string payOrderID,payOrderContect payOrderContect)
        {
            String sqlString = $@"insert into payOrder (payOrderID,ownerID,name,phone,email,isPay)
                values(@payOrderID,@ownerID,@name,@phone,@email,@isPay)";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
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
            int result = command.ExecuteNonQuery();
            //update_tableID("payOrder", id);
           // insertOrderProduct(payOrderID, payOrderContect.cartItem);
            return "";
            /* int id = get_tableID("payOrder");
             string payOrderID = "payOrder" + id;
             if (!OrderExist("ownerID", payOrderContect.ownerID))
             {
                 id++;
                 payOrderID = "payOrder" + id;
                 String sqlString = $@"insert into payOrder (payOrderID,ownerID,name,phone,email,isPay)
                 values(@payOrderID,@ownerID,@name,@phone,@email,@isPay)";
                 SqlCommand command = new SqlCommand(sqlString, sqlConnection);
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
                 int result = command.ExecuteNonQuery();
                 update_tableID("payOrder", id);
                 insertOrderProduct(payOrderID, payOrderContect.cartItem);
             }
             else
             {
                 deleteOrderProduct(payOrderID);
                 insertOrderProduct(payOrderID, payOrderContect.cartItem);
             }
             return Project_Set.Route + "/goToPay/Index?payOrderID=" + payOrderID + "@" + DateTime.Now.ToString("ddhmmss");*/

        }
        public void insertOrderProduct(string payOrderID,string payOrderProductId, string cartID)
        {
            /*  String sqlString = "";
              SqlCommand command;
              foreach (string cartID in cartID_List)
              {
                  int id = get_tableID("payOrderProduct") + 1;
                  string payOrderProductId = "payOrderProduct" + id;
                  sqlString = $@"insert into payOrderProduct (payOrderProductID,payOrderID,cartID)
                    values(@payOrderProductID,@payOrderID,@cartID)";
                  command = new SqlCommand(sqlString, sqlConnection);
                  command.Parameters.Add("@payOrderProductID", System.Data.SqlDbType.NVarChar);
                  command.Parameters["@payOrderProductID"].Value = payOrderProductId;
                  command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
                  command.Parameters["@payOrderID"].Value = payOrderID;
                  command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
                  command.Parameters["@cartID"].Value = cartID;
                  int result = command.ExecuteNonQuery();
                  update_tableID("payOrderProduct", id);
              }*/
            string sqlString = $@"insert into payOrderProduct (payOrderProductID,payOrderID,cartID)
                  values(@payOrderProductID,@payOrderID,@cartID)";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderProductID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderProductID"].Value = payOrderProductId;
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@cartID"].Value = cartID;
            int result = command.ExecuteNonQuery();
        }
        public void deleteOrderProduct(string payOrderID)
        {
            String sqlString = $@"delete from payOrderProduct where payOrderID=@payOrderID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
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
        public List<payOrder> getPayOrder(string ownerID)
        {
            List<payOrder> payOrderList = new List<payOrder>();
            String sqlString = $@"select * from payOrder where ownerID=@ownerID and isPay=@isPay ";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@ownerID"].Value = ownerID;
            command.Parameters.Add("@isPay", System.Data.SqlDbType.Bit);
            command.Parameters["@isPay"].Value = true;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                payOrderList.Add(new payOrder() { payOrderID = reader[0].ToString(), name = reader[2].ToString(), phone = reader[3].ToString(), email = reader[4].ToString(), payDate = reader[6].ToString() });
            }
            reader.Close();
            return payOrderList;
        }

    }
}
