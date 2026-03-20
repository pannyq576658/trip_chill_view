using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.Dao;
using trip_chill_backend_test.model;

namespace trip_chill_backend_test.Service
{
    public class ProductService
    {
        ProductDao _Dao = new ProductDao();
       
        public async Task<List<product>> getProductList()
        {
            return await _Dao.getProductList();
        }
        public async Task<List<product>> getProductRange(int id)
        {
            int max = id * 10;
            int min = (id - 1) * 10;
            return await _Dao.getProductRange(min, max);
        }

        public async Task<product> getProduct(string productId)
        {
            return await _Dao.getProduct(productId);
        }

        public async Task<int> productNum()
        {
            return await _Dao.productNum();
        }

       

    }
}
