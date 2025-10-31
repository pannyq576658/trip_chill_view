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
        public List<payOrder> getPayOrder(string ownerID)
        {
            List<payOrder> payOrderList = payOrderDao.getPayOrder(ownerID);
            return payOrderList;
        }

        public List<product> getHisOrderProductList(string payOrderID)
        {
            List<product> HisOrderProductList = historyOrderDao.getHisOrderProduct(payOrderID);
            return HisOrderProductList;
        }

        public payOrderReturn getPayOrderReturn(string retrunCode)
        {
            payOrderReturn payOrderReturn = historyOrderDao.getPayOrderReturn(retrunCode);
            if (payOrderReturn.hasData)
                historyOrderDao.updatePayOrderReturn(retrunCode);
            return payOrderReturn;
        }

    }
}
