using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using trip_chill_backend_test.Dao;
using trip_chill_backend_test.model;

namespace trip_chill_backend_test.Service
{
    public class cartService
    {
        BaseDao baseDao = new BaseDao();
        CartDao cartDao = new CartDao();
        public async Task<List<cart1>> getCart(string owner_id)
        {
            List<cart1> cartArray = await cartDao.getCart(owner_id);
            return cartArray;
        }
        
        public async Task<List<cart1>> getCartSelectCheck(string owner_id)
        {
            List<cart1> cartArray = await cartDao.getCartSelectCheck(owner_id);
            return cartArray;
        }
        public async Task<string> deleteCart(cart value)
        {
            await cartDao.deleteCart(value.cartID);
            await cartDao.updateMember_aft_del_Cart(value.ownerID,1);
            return "刪除完畢";
        }
        public async Task<string> deleteSlect(List<cart> value)
        {
            if (value == null || value.Count == 0) return "無刪除資料";

            foreach (cart c in value)
            {
                await cartDao.deleteCart(c.cartID);
            }
            await cartDao.updateMember_aft_del_Cart(value[0].ownerID, value.Count);
            return "刪除完畢";
        }
        

        public async Task<string> setCartSelectCheck(List<cartSelectCheck> CartDataList)
        {
            foreach (cartSelectCheck CartData in CartDataList)
                await cartDao.setCartSelectCheck(CartData);
            return "修改成功";
        }

        public async Task<bool> addCart(string productID, string ownerID)
        {
            if (!await cartDao.CartItemExist(productID, ownerID))
            {
                int id = await baseDao.get_tableID("cartItem") + 1;
                await cartDao.insertCart(new cart() { cartID = "tripViewCartItem" + id, productID = productID, quantity = 1, ownerID = ownerID });
                await baseDao.update_tableID("cartItem", id);
                await cartDao.updateMember_aft_add_Cart(ownerID, 1);            
                return false;
            }
            else
            {
                await cartDao.updateCart(productID, ownerID);
                return true;
            }
        }

        public async Task<bool> CartItemExist(string productID, string ownerID)
        {
            bool result = await cartDao.CartItemExist(productID, ownerID);
            return result;
        }


    }
}
