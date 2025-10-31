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
        public List<product> getProductList()
        {
            return _Dao.getProductList();
        }
        public List<product> getProductRange(int id)
        {
            int max = id * 10;
            int min = (id - 1) * 10;
            return _Dao.getProductRange(min, max);
        }

        public product getProduct(string ProductID)
        {
            return _Dao.getProduct(ProductID);
        }

        public int productNum()
        {          
            return _Dao.productNum();
        }

    }
}
