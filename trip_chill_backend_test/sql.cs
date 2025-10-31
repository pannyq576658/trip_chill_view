using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.model;

namespace trip_chill_backend_test
{
    public class sql
    {
     
        SqlConnection sqlConnection;
        ProjectSet Project_Set = new ProjectSet();
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

        public void update_tableID(string tableName,int id)
        {
            String sqlString = $@"update tableItem set currentlytableNameID=@currentlytableNameID where tableName=@tableName";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@currentlytableNameID", System.Data.SqlDbType.Int);
            command.Parameters["@currentlytableNameID"].Value = id;
            command.Parameters.Add("@tableName", System.Data.SqlDbType.NVarChar);
            command.Parameters["@tableName"].Value = tableName;
            int result = command.ExecuteNonQuery();
        }



        public void member_insert(member value)
        {
            //將sql語法組成字串
            String sqlString = $@"insert into member (id, name, cartNum,platform,gender,email,birthday,pictureUrl,password,phone)
                          values(@id,@name,@cartNum,@platform,@gender,@email,@birthday,@pictureUrl,@password,@phone)";

            //執行sql語法
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = value.id;
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
            command.Parameters["@name"].Value = value.name;
            command.Parameters.Add("@cartNum", System.Data.SqlDbType.Int);
            command.Parameters["@cartNum"].Value = value.cartNum;
            command.Parameters.Add("@platform", System.Data.SqlDbType.NVarChar);
            command.Parameters["@platform"].Value = value.platform;
            command.Parameters.Add("@gender", System.Data.SqlDbType.NVarChar);
            command.Parameters["@gender"].Value = value.gender;
            command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
            command.Parameters["@email"].Value = value.email;
            command.Parameters.Add("@birthday", System.Data.SqlDbType.NVarChar);
            command.Parameters["@birthday"].Value = value.birthday;
            command.Parameters.Add("@pictureUrl", System.Data.SqlDbType.NVarChar);
            command.Parameters["@pictureUrl"].Value = value.pictureUrl;
            command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
            command.Parameters["@password"].Value = value.password;
            command.Parameters.Add("@phone", System.Data.SqlDbType.NVarChar);
            command.Parameters["@phone"].Value = value.phone;

            //取回結果並顯示
            int result = command.ExecuteNonQuery();

            //sqlConnection.Close();
        }
        public void member_update(member value)
        {
            String sqlString = $@"update member set name=@name, cartNum=@cartNum,gender=@gender,email=@email,birthday=@birthday,pictureUrl=@pictureUrl where id=@id ";
            //執行sql語法
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
            command.Parameters["@name"].Value = value.name;
            command.Parameters.Add("@cartNum", System.Data.SqlDbType.Int);
            command.Parameters["@cartNum"].Value = value.cartNum;
            command.Parameters.Add("@gender", System.Data.SqlDbType.NVarChar);
            command.Parameters["@gender"].Value = value.gender;
            command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
            command.Parameters["@email"].Value = value.email;
            command.Parameters.Add("@birthday", System.Data.SqlDbType.NVarChar);
            command.Parameters["@birthday"].Value = value.birthday;
            command.Parameters.Add("@pictureUrl", System.Data.SqlDbType.NVarChar);
            command.Parameters["@pictureUrl"].Value = value.pictureUrl;
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = value.id;
            //取回結果並顯示
            int result = command.ExecuteNonQuery();
        }
        public string member_find(string id)
        {

            String sqlString = $@"select COUNT(*) from member where id=@id";
            
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = id;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string aaa = reader[0].ToString();
            reader.Close();
            //sqlConnection.Close();
            return aaa;
        }

        public string member_find_hasPwd(string id,string pwd)
        {

            String sqlString = $@"select COUNT(*) from member where id=@id and password=@password";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = id;
            command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
            command.Parameters["@password"].Value = pwd;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            string aaa = reader[0].ToString();
            reader.Close();
            //sqlConnection.Close();
            return aaa;
        }

        public member member_find_data(string id)
        {

            String sqlString = $@"select * from member where id=@id";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = id;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            member m = new member();

            m.id = reader[0].ToString();
            m.name = reader[1].ToString();
            m.cartNum = int.Parse(reader[2].ToString());
            m.gender = reader[4].ToString();
            m.platform = reader[3].ToString();
            m.email = reader[5].ToString();
            m.birthday = reader[6].ToString();
            m.pictureUrl = reader[7].ToString();
            reader.Close();
            //sqlConnection.Close();
            return m;
        }

        public member member_find_data_hasPwd(string id, string pwd)
        {

            String sqlString = $@"select * from member where id=@id and password=@password";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = id;
            command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
            command.Parameters["@password"].Value = pwd;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            member m = new member();

            m.id = reader[0].ToString();
            m.name = reader[1].ToString();
            m.cartNum = int.Parse(reader[2].ToString());
            m.gender = reader[4].ToString();
            m.platform = reader[3].ToString();
            m.email = reader[5].ToString();
            m.birthday = reader[6].ToString();
            m.pictureUrl = reader[7].ToString();
            reader.Close();
            //sqlConnection.Close();
            return m;
        }


        public void deleteCart(cart cart)
        {
           String sqlString = $@"delete from cartItem where cartID=@cartID";
           //執行sql語法
           SqlCommand command = new SqlCommand(sqlString, sqlConnection);
           command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
           command.Parameters["@cartID"].Value = cart.cartID;
           //取回結果並顯示
           int result = command.ExecuteNonQuery();

           String UpdateMemberSQLString = $@"update member set cartNum=cartNum-1 where id=@id";

           command = new SqlCommand(UpdateMemberSQLString, sqlConnection);
           command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
           command.Parameters["@id"].Value = cart.ownerID;
            result = command.ExecuteNonQuery();
        }

        public void deleteListCart(List<cart> cartList)
        {
            SqlCommand command;
            int result = 0;
            String sqlString = "";
            string ownerID = cartList[0].ownerID;
            foreach (cart c in cartList)
            {
                 sqlString = $@"delete from cartItem where cartID=@cartID";
                 command = new SqlCommand(sqlString, sqlConnection);
                 command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
                 command.Parameters["@cartID"].Value = c.cartID;
                 result = command.ExecuteNonQuery();                
            }

            String UpdateMemberSQLString = $@"update member set cartNum=cartNum-{cartList.Count} where id=@id";
            command = new SqlCommand(UpdateMemberSQLString, sqlConnection);
            command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
            command.Parameters["@id"].Value = ownerID;
            result = command.ExecuteNonQuery();
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

        public cart getCartItem(string productID, string ownerID)
        {
            cart Cart = new cart();
            String sqlString = $@"select * from cartItem where ownerID=@ownerID and productID=@productID ";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);          
            command.Parameters.Add("@ownerID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@ownerID"].Value = ownerID;
            command.Parameters.Add("@productID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@productID"].Value = productID;
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Cart.cartID = reader[0].ToString();
            Cart.productID = reader[1].ToString();
            Cart.quantity = int.Parse(reader[2].ToString());
            Cart.ownerID = reader[3].ToString();                                              
            reader.Close();
            return Cart;
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

        public bool addCart(string productID, string ownerID)
        {
            if (!CartItemExist(productID, ownerID))
            {
                int id = get_tableID("cartItem") + 1;
                insertCart(new cart() { cartID = "tripViewCartItem" + id, productID = productID, quantity = 1, ownerID = ownerID });
                update_tableID("cartItem", id);

                //member member = member_find_data(ownerID);
                //int cartNum = member.cartNum + 1;
                String UpdateMemberSQLString = $@"update member set cartNum=cartNum+1 where id=@id";
                SqlCommand command = new SqlCommand(UpdateMemberSQLString, sqlConnection);
                command.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
                command.Parameters["@id"].Value = ownerID;
                int result = command.ExecuteNonQuery();

                return false;
            }
            else
            {
                updateCart(productID, ownerID);
                return true;
            }
                                 
        }

        public void setCartSelectCheck(List<cartSelectCheck> CartDataList)
        {
            foreach (cartSelectCheck CartData in CartDataList)
            {
                String setCartSelectCheckString = $@"update cartItem set checkSelectd=@checkSelectd where cartID=@cartID";
                SqlCommand command = new SqlCommand(setCartSelectCheckString, sqlConnection);
                command.Parameters.Add("@checkSelectd", System.Data.SqlDbType.NVarChar);
                command.Parameters["@checkSelectd"].Value = CartData.checkSelectd;
                command.Parameters.Add("@cartID", System.Data.SqlDbType.NVarChar);
                command.Parameters["@cartID"].Value = CartData.cartID;
                int result = command.ExecuteNonQuery();
            }
        }

        public product getProduct(string productID)
        {
            
            product Product = new product();
            String sqlString = $@"select * from product where productID=@productID ";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@productID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@productID"].Value = productID;
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();
            Product.productID = reader[0].ToString();
            Product.name = reader[1].ToString();
            Product.type = reader[2].ToString();
            Product.price = int.Parse(reader[3].ToString());
            Product.background = reader[4].ToString();
            Product.buyTimeNum = int.Parse(reader[5].ToString());           
            reader.Close();
            //sqlConnection.Close();
            return Product;
        }
        public List<product> getProductList()
        {
            List<product> productArray = new List<product>();
            product Product = new product();
            String sqlString = $@"select * from product";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                productArray.Add(new product() { productID= reader[0].ToString(),name=reader[1].ToString(),type= reader[2].ToString(), price = int.Parse(reader[3].ToString()), background = reader[4].ToString(),buyTimeNum= int.Parse(reader[5].ToString()) });
            }


            reader.Close();
            //sqlConnection.Close();
            return productArray;
        }

        public List<product> getProductRange(int min,int max)
        {
            List<product> productArray = new List<product>();
            String sqlString = $@"select TOP({max})* from product except SELECT TOP({min})* FROM product";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                productArray.Add(new product() { productID = reader[0].ToString(), name = reader[1].ToString(),type=reader[2].ToString(), price = int.Parse(reader[3].ToString()), background = reader[4].ToString(),buyTimeNum=int.Parse(reader[5].ToString()) });
            }


            reader.Close();
            //sqlConnection.Close();
            return productArray;
        }

        public int productNum()
        {
            String sqlString = $@"select count(*) from product";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int n = int.Parse(reader[0].ToString());
            reader.Close();
            return n;
        }

        public bool OrderExist(string findIdName,string idValue)
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
        public string insertOrder(payOrderContect payOrderContect)
        {
            int id = get_tableID("payOrder");
            string payOrderID = "payOrder" + id;
            if (!OrderExist("ownerID", payOrderContect.ownerID))
            {
                id ++;
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
            return Project_Set.Route + "/goToPay/Index?payOrderID=" + payOrderID+"@"+ DateTime.Now.ToString("ddhmmss");

        }
        public void deleteOrderProduct(string payOrderID)
        {
            String sqlString = $@"delete from payOrderProduct where payOrderID=@payOrderID";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.Add("@payOrderID", System.Data.SqlDbType.NVarChar);
            command.Parameters["@payOrderID"].Value = payOrderID;
            int result = command.ExecuteNonQuery();          
        }
        public void insertOrderProduct(string payOrderID,List<string> cartID_List)
        {
            String sqlString = "";
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
            }
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
                payOrderList.Add(new payOrder() { payOrderID = reader[0].ToString(), name = reader[2].ToString(), phone = reader[3].ToString(), email = reader[4].ToString(), payDate = reader[6].ToString()});
            }
            reader.Close();
            return payOrderList;
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

        public void sql_close()
        {
            sqlConnection.Close();
        }
    }
}
