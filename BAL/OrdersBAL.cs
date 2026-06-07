using System.Collections.Generic;
using DAL;
using MODELS;

namespace BAL
{
    public class OrdersBAL
    {
        OrdersDAL dal = new OrdersDAL();

        public List<OrderListViewModel> GetAllOrders()
        {
            return dal.GetAllOrders();
        }
    }
}