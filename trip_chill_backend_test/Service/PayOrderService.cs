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
        public bool payOrderExist(string payOrderID)
        {
            bool OrderExist = payOrderDao.OrderExist("payOrderID", payOrderID);
            return OrderExist;
        }

        public string setContact_and_goToPay(payOrderContect payOrderContect)
        {
            int id = baseDao.get_tableID("payOrder");
            string payOrderID = "payOrder" + id;
            if (!payOrderDao.OrderExist("ownerID", payOrderContect.ownerID))
            {
                id++;
                payOrderID = "payOrder" + id;
                payOrderDao.insertOrder(payOrderID, payOrderContect);
                baseDao.update_tableID("payOrder", id);
                foreach (string cartID in payOrderContect.cartItem)
                {
                    int payOrderProduct_id = baseDao.get_tableID("payOrderProduct") + 1;
                    string payOrderProductId = "payOrderProduct" + payOrderProduct_id;
                    payOrderDao.insertOrderProduct(payOrderID, payOrderProductId, cartID);
                    baseDao.update_tableID("payOrderProduct", payOrderProduct_id);
                }
            }
            else
            {
                payOrderDao.deleteOrderProduct(payOrderID);
                foreach (string cartID in payOrderContect.cartItem)
                {
                    int payOrderProduct_id = baseDao.get_tableID("payOrderProduct") + 1;
                    string payOrderProductId = "payOrderProduct" + payOrderProduct_id;
                    payOrderDao.insertOrderProduct(payOrderID, payOrderProductId, cartID);
                    baseDao.update_tableID("payOrderProduct", payOrderProduct_id);
                }

            }
            return Project_Set.Route + "/goToPay/Index?payOrderID=" + payOrderID + "@" + DateTime.Now.ToString("ddhmmss");
        }
        public List<payOrderProduct> getOrderProductList(string payOrderID)
        {
            List<payOrderProduct> payOrderProductList = payOrderDao.getOrderProductList(payOrderID);
            return payOrderProductList;
        }

}
}
