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
       
        public CartDao()
        {
            
        }      
        public async Task<List<cart1>> getCart(string ownerID)
        {
            List<cart1> cartArray = new List<cart1>();
            string sqlString = $@"select cartItem.cartID,cartItem.productID,product.name,product.type,product.price,product.background,cartItem.ownerID from cartItem left join product on cartItem.productID=product.productID where ownerID=@ownerID";

            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@ownerID"].Value = ownerID;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cartArray.Add(new cart1()
                            {
                                cartID = reader[0].ToString(),
                                productID = reader[1].ToString(),
                                name = reader[2].ToString(),
                                type = reader[3].ToString(),
                                price = int.Parse(reader[4].ToString()),
                                pictureUrl = reader[5].ToString(),
                                ownerID = reader[6].ToString()
                            });
                        }
                    }
                }
            }
            return cartArray;
        }

        public async Task<List<cart1>> getCartSelectCheck(string ownerID)
        {
            List<cart1> cartArray = new List<cart1>();
            string sqlString = $@"select cartItem.cartID,product.name,product.type,product.price,product.background,cartItem.ownerID from cartItem left join product on cartItem.productID=product.productID where ownerID=@ownerID and checkSelectd=@checkSelectd ";

            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@ownerID"].Value = ownerID;
                    command.Parameters.Add("@checkSelectd", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@checkSelectd"].Value = "1";
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cartArray.Add(new cart1()
                            {
                                cartID = reader[0].ToString(),
                                name = reader[1].ToString(),
                                type = reader[2].ToString(),
                                price = int.Parse(reader[3].ToString()),
                                pictureUrl = reader[4].ToString(),
                                ownerID = reader[5].ToString()
                            });
                        }
                    }
                }
            }
            return cartArray;
        }

       
        public async Task deleteCart(string cartID)
        {
            string sqlString = $@"delete from cartItem where cartID=@cartID";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@cartID"].Value = cartID;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
      
        public async Task updateMember_aft_del_Cart(string ownerID, int SubcartNum)
        {
            string sqlString = $@"update member set cartNum=cartNum-{SubcartNum.ToString()} where id=@id";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@id"].Value = ownerID;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task updateMember_aft_add_Cart(string ownerID, int AddcartNum)
        {
            string sqlString = $@"update member set cartNum=cartNum+{AddcartNum.ToString()} where id=@id";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@id"].Value = ownerID;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task setCartSelectCheck(cartSelectCheck CartData)
        {
            string sqlString = $@"update cartItem set checkSelectd=@checkSelectd where cartID=@cartID";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@checkSelectd", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@checkSelectd"].Value = CartData.checkSelectd;
                    command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@cartID"].Value = CartData.cartID;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
     
        public async Task<bool> CartItemExist(string productID, string ownerID)
        {
            string sqlString = $@"select * from cartItem where productID=@productID and ownerID=@ownerID";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@productID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@productID"].Value = productID;
                    command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@ownerID"].Value = ownerID;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }      
        public async Task insertCart(cart cart)
        {
            string sqlString = $@"insert into cartItem (cartID,productID,quantity,ownerID,checkSelectd)
                values(@cartID,@productID,@quantity,@ownerID,@checkSelectd)";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
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
                    await command.ExecuteNonQueryAsync();
                }
            }
        }       
        public async Task updateCart(string productID, string ownerID)
        {
            string sqlString = $@"update cartItem set quantity=quantity+1 where productID=@productID and ownerID=@ownerID ";
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync();
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.Add("@productID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@productID"].Value = productID;
                    command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@ownerID"].Value = ownerID;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

    }
}
