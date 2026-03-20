using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.Dao;
using trip_chill_backend_test.model;

namespace trip_chill_backend_test.Service
{
    public class HistoryOrderService
    {
        PayOrderDao payOrderDao = new PayOrderDao();
        HistoryOrderDao historyOrderDao = new HistoryOrderDao();
        public async Task<List<payOrder>> getPayOrder(string ownerID)
        {
            List<payOrder> payOrderList = await payOrderDao.getPayOrder(ownerID);
            return payOrderList;
        }

        public async Task<List<product>> getHisOrderProductList(string payOrderID)
        {
            List<product> HisOrderProductList = await historyOrderDao.getHisOrderProduct(payOrderID);
            return HisOrderProductList;
        }

        public async Task<payOrderReturn> getPayOrderReturn(string retrunCode)
        {
            payOrderReturn payOrderReturn = await historyOrderDao.getPayOrderReturn(retrunCode);
            if (payOrderReturn.hasData)
                await historyOrderDao.updatePayOrderReturn(retrunCode);
            return payOrderReturn;
        }
       
               
    }
}
