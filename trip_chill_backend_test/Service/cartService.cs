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
        public List<cart1> getCart(string owner_id)
        {
            List<cart1> cartArray = cartDao.getCart(owner_id);
            return cartArray;
        }

        public List<cart1> getCartSelectCheck(string owner_id)
        {
            List<cart1> cartArray = cartDao.getCartSelectCheck(owner_id);
            return cartArray;
        }
        public string deleteCart(cart value)
        {
            cartDao.deleteCart(value.cartID);
            cartDao.updateMember_aft_del_Cart(value.ownerID,1);
            return "刪除完畢";
        }

        public string deleteSlect(List<cart> value)
        {
            foreach (cart c in value)
            {
                cartDao.deleteCart(c.cartID);
            }
            cartDao.updateMember_aft_del_Cart(value[0].ownerID, value.Count);
            return "刪除完畢";
        }

        public string setCartSelectCheck(List<cartSelectCheck> CartDataList)
        {
            foreach (cartSelectCheck CartData in CartDataList)
                cartDao.setCartSelectCheck(CartData);
            return "修改成功";
        }

        public bool addCart(string productID, string ownerID)
        {
            if (!cartDao.CartItemExist(productID, ownerID))
            {
                int id = baseDao.get_tableID("cartItem") + 1;
                cartDao.insertCart(new cart() { cartID = "tripViewCartItem" + id, productID = productID, quantity = 1, ownerID = ownerID });
                baseDao.update_tableID("cartItem", id);
                cartDao.updateMember_aft_add_Cart(ownerID, 1);            
                return false;
            }
            else
            {
                cartDao.updateCart(productID, ownerID);
                return true;
            }
        }

        public bool CartItemExist(string productID, string ownerID)
        {
            bool result = cartDao.CartItemExist(productID, ownerID);
            return result;
        }


    }
}
