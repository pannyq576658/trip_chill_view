using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.model;

namespace trip_chill_backend_test.Dao
{
    public class ProductDao
    {
        SqlConnection sqlConnection;
        ProjectSet Project_Set = new ProjectSet();
        public ProductDao()
        {
            sqlConnection = new SqlConnection(Project_Set.connectString);

            //開啟連線
            sqlConnection.Open();
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
                productArray.Add(new product() { productID = reader[0].ToString(), name = reader[1].ToString(), type = reader[2].ToString(), price = int.Parse(reader[3].ToString()), background = reader[4].ToString(), buyTimeNum = int.Parse(reader[5].ToString()) });
            }


            reader.Close();
            //sqlConnection.Close();
            return productArray;
        }
        public List<product> getProductRange(int min, int max)
        {
            List<product> productArray = new List<product>();
            String sqlString = $@"select TOP({max})* from product except SELECT TOP({min})* FROM product";

            SqlCommand command = new SqlCommand(sqlString, sqlConnection);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                productArray.Add(new product() { productID = reader[0].ToString(), name = reader[1].ToString(), type = reader[2].ToString(), price = int.Parse(reader[3].ToString()), background = reader[4].ToString(), buyTimeNum = int.Parse(reader[5].ToString()) });
            }


            reader.Close();
            //sqlConnection.Close();
            return productArray;
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

    }
}
