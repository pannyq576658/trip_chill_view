using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.model;

namespace trip_chill_backend_test.Dao
{
    public class CartDao
    {
        SqlConnection sqlConnection;
        ProjectSet Project_Set = new ProjectSet();
        public CartDao()
        {
            sqlConnection = new SqlConnection(Project_Set.connectString);

            //開啟連線
            sqlConnection.Open();
        }

        public List<cart1> getCart(string ownerID)
        {
            List<cart1> cartArray = new List<cart1>();
            cart1 Cart = new cart1();
            String sqlString = $@"select cartItem.cartID,cartItem.productID,product.name,product.type,product.price,product.background,cartItem.ownerID from cartItem left join product on cartItem.productID=product.productID where ownerID=@ownerID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@ownerID"].Value = ownerID;
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Cart.cartID = reader[0].ToString();
                Cart.productID = reader[1].ToString();
                Cart.name = reader[2].ToString();
                Cart.type = reader[3].ToString();
                Cart.price = int.Parse(reader[4].ToString());
                Cart.pictureUrl = reader[5].ToString();
                Cart.ownerID = reader[6].ToString();
                cartArray.Add(Cart);
                Cart = new cart1();
            }
            reader.Close();
            //sqlConnection.Close();
            return cartArray;
        }

        public List<cart1> getCartSelectCheck(string ownerID)
        {
            List<cart1> cartArray = new List<cart1>();
            cart1 Cart = new cart1();
            String sqlString = $@"select cartItem.cartID,product.name,product.type,product.price,product.background,cartItem.ownerID from cartItem left join product on cartItem.productID=product.productID where ownerID=@ownerID and checkSelectd=@checkSelectd ";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@ownerID"].Value = ownerID;
            command.Parameters.Add("@checkSelectd", System.Data.SqlDbType.NVarChar);
            command.Parameters["@checkSelectd"].Value = "1";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Cart.cartID = reader[0].ToString();
                Cart.name = reader[1].ToString();
                Cart.type = reader[2].ToString();
                Cart.price = int.Parse(reader[3].ToString());
                Cart.pictureUrl = reader[4].ToString();
                Cart.ownerID = reader[5].ToString();
                cartArray.Add(Cart);
                Cart = new cart1();
            }
            reader.Close();
            //sqlConnection.Close();
            return cartArray;
        }

        public void deleteCart(string cartID)
        {
            String sqlString = $@"delete from cartItem where cartID=@cartID";
            //執行sql語法
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@cartID"].Value = cartID;
            //取回結果並顯示
            int result = command.ExecuteNonQuery();
            
        }

        public void updateMember_aft_del_Cart(string ownerID,int SubcartNum)
        {
            
            String UpdateMemberSQLString = $@"update member set cartNum=cartNum-{SubcartNum.ToString()} where id=@id";

            SqlCommand command = new SqlCommand(UpdateMemberSQLString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = ownerID;
            int result = command.ExecuteNonQuery();
        }
        public void updateMember_aft_add_Cart(string ownerID, int AddcartNum)
        {

            String UpdateMemberSQLString = $@"update member set cartNum=cartNum+{AddcartNum.ToString()} where id=@id";

            SqlCommand command = new SqlCommand(UpdateMemberSQLString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = ownerID;
            int result = command.ExecuteNonQuery();
        }
        public void setCartSelectCheck(cartSelectCheck CartData)
        {
                String setCartSelectCheckString = $@"update cartItem set checkSelectd=@checkSelectd where cartID=@cartID";
                SqlCommand command = new SqlCommand(setCartSelectCheckString, sqlConnection);
                command.Parameters.Add("@checkSelectd", System.Data.SqlDbType.NVarChar);
                command.Parameters["@checkSelectd"].Value = CartData.checkSelectd;
                command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
                command.Parameters["@cartID"].Value = CartData.cartID;
                int result = command.ExecuteNonQuery();
        }
        public bool CartItemExist(string productID, string ownerID)
        {
            String sqlString = $@"select * from cartItem where productID=@productID and ownerID=@ownerID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@productID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@productID"].Value = productID;
            command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@ownerID"].Value = ownerID;
            SqlDataReader reader = command.ExecuteReader();
            bool HasRows = reader.HasRows;
            reader.Close();
            return HasRows;
        }
               
        public void insertCart(cart cart)
        {

            String sqlString = $@"insert into cartItem (cartID,productID,quantity,ownerID,checkSelectd)
                values(@cartID,@productID,@quantity,@ownerID,@checkSelectd)";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@cartID"].Value = cart.cartID;
            command.Parameters.Add("@productID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@productID"].Value = cart.productID;
            command.Parameters.Add("@quantity", System.Data.SqlDbType.Int);
            command.Parameters["@quantity"].Value = cart.quantity;
            command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@ownerID"].Value = cart.ownerID;
            command.Parameters.Add("@checkSelectd", System.Data.SqlDbType.NVarChar);
            command.Parameters["@checkSelectd"].Value = "1";
            int result = command.ExecuteNonQuery();

        }
        public void updateCart(string productID, string ownerID)
        {
            String sqlString = $@"update cartItem set quantity=quantity+1 where productID=@productID and ownerID=@ownerID ";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@productID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@productID"].Value = productID;
            command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@ownerID"].Value = ownerID;
            int result = command.ExecuteNonQuery();
        }

    }
}
