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
        }       
        public async Task<List<product>> getProductList()
        {
            List<product> productArray = new List<product>();
            string sqlString = $@"select * from product";

            using (SqlConnection sqlConnection = new SqlConnection(Project_Set.connectString))
            {
                await sqlConnection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sqlString, sqlConnection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productArray.Add(new product() { productID = reader[0].ToString(), name = reader[1].ToString(), type = reader[2].ToString(), price = int.Parse(reader[3].ToString()), background = reader[4].ToString(), buyTimeNum = int.Parse(reader[5].ToString()) });
                        }
                    }
                }
            }
            return productArray;
        }
        public async Task<List<product>> getProductRange(int min, int max)
        {
            List<product> productArray = new List<product>();
            String sqlString = $@"select TOP({max})* from product except SELECT TOP({min})* FROM product";
            using (SqlConnection sqlConnection = new SqlConnection(Project_Set.connectString))
            {
                await sqlConnection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sqlString, sqlConnection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            productArray.Add(new product() { productID = reader[0].ToString(), name = reader[1].ToString(), type = reader[2].ToString(), price = int.Parse(reader[3].ToString()), background = reader[4].ToString(), buyTimeNum = int.Parse(reader[5].ToString()) });
                        }
                    }
                }
            }           
            return productArray;
        }
        public async Task<product> getProduct(string productID)
        {

            product Product = null; // 1. 初始化為 null
            string sqlString = @"SELECT * FROM product WHERE productID = @productID";

            // 2. 使用 using 確保連線被正確關閉
            using (var sqlConnection = new SqlConnection(new ProjectSet().connectString))
            {
                await sqlConnection.OpenAsync(); // 3. 非同步開啟連線
                using (var command = new SqlCommand(sqlString, sqlConnection))
                {
                    command.Parameters.AddWithValue("@productID", productID);

                    using (var reader = await command.ExecuteReaderAsync()) // 4. 非同步執行查詢
                    {
                        if (await reader.ReadAsync()) // 5. 非同步讀取資料
                        {
                            Product = new product()
                            {
                                // 6. 建議使用欄位名稱，而不是索引
                                productID = reader["productID"].ToString(),
                                name = reader["name"].ToString(),
                                type = reader["type"].ToString(),
                                price = Convert.ToInt32(reader["price"]),
                                background = reader["background"].ToString(),
                                buyTimeNum = Convert.ToInt32(reader["buyTimeNum"])
                            };
                        }
                    }
                }
            }
            
            return Product;
        }

        public async Task<int> productNum()
        {
            int n = 0;
            String sqlString = $@"select count(*) from product";

            using (SqlConnection sqlConnection = new SqlConnection(Project_Set.connectString))
            {
                await sqlConnection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sqlString, sqlConnection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        n = int.Parse(reader[0].ToString());
                    }
                }
            }
            return n;
        }

    }
}
