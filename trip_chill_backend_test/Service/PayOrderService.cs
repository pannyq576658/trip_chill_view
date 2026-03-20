using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.Dao;
using trip_chill_backend_test.model;

namespace trip_chill_backend_test.Service
{
    public class PayOrderService
    {
        BaseDao baseDao = new BaseDao();
        PayOrderDao payOrderDao = new PayOrderDao();
        ProjectSet Project_Set = new ProjectSet();
        
        public async Task<bool> payOrderExist(string payOrderID)
        {
            return await payOrderDao.OrderExist("payOrderID", payOrderID);
        }
        public async Task<string> setContact_and_goToPay(payOrderContect payOrderContect)
        {
            int id = await baseDao.get_tableID("payOrder");
            string payOrderID = "payOrder" + id;
            if (!await payOrderDao.OrderExist("ownerID", payOrderContect.ownerID))
            {
                id++;
                payOrderID = "payOrder" + id;
                await payOrderDao.insertOrder(payOrderID, payOrderContect);
                await baseDao.update_tableID("payOrder", id);
                foreach (string cartID in payOrderContect.cartItem)
                {
                    int payOrderProduct_id = await baseDao.get_tableID("payOrderProduct") + 1;
                    string payOrderProductId = "payOrderProduct" + payOrderProduct_id;
                    await payOrderDao.insertOrderProduct(payOrderID, payOrderProductId, cartID);
                    await baseDao.update_tableID("payOrderProduct", payOrderProduct_id);
                }
            }
            else
            {
                await payOrderDao.deleteOrderProduct(payOrderID);
                foreach (string cartID in payOrderContect.cartItem)
                {
                    int payOrderProduct_id = await baseDao.get_tableID("payOrderProduct") + 1;
                    string payOrderProductId = "payOrderProduct" + payOrderProduct_id;
                    await payOrderDao.insertOrderProduct(payOrderID, payOrderProductId, cartID);
                    await baseDao.update_tableID("payOrderProduct", payOrderProduct_id);
                }
            }
            return Project_Set.Route + "/goToPay/Index?payOrderID=" + payOrderID + "@" + DateTime.Now.ToString("ddhmmss");
        }
           
        public async Task<List<payOrderProduct>> getOrderProductList(string payOrderID)
        {
            List<payOrderProduct> payOrderProductList = await payOrderDao.getOrderProductList(payOrderID);
            return payOrderProductList;
        }

    }
}
