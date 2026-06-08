using DAL;
using MODELS;
using System.Collections.Generic;
using System.Linq;

namespace BAL
{
    public class AdminInventoryBAL
    {
        AdminInventoryDAL dal = new AdminInventoryDAL();

        public List<UOMmodel> GetUom()
        {
            return dal.GetUom()
                      .Select(u => new UOMmodel
                      {
                          UOMId = u.UOMId,
                          UOMName = u.UOMName
                      }).ToList();
        }

        public List<InventoryItemModel> GetAllInventory()
        {
            return dal.GetAllInventory()
                      .Select(i => new InventoryItemModel
                      {
                          itemid = i.ItemId,
                          ItemName = i.ItemName,
                          CurrentStock = i.CurrentStock,
                          UOMId = i.UOMId,
                          LastAddedStockDate = i.LastAddedStockDate
                      }).ToList();
        }
        public bool AddInventory(InventoryItemModel item)
        {
            var entity = new DAL.InventoryItem
            {
                ItemName = item.ItemName,
                CurrentStock = item.CurrentStock,
                UOMId = item.UOMId,
                LastAddedStockDate = item.LastAddedStockDate
            };
            return dal.AddInventory(entity);
        }
        public decimal GetYesterdayTotalSales()
        {
            return dal.GetYesterdayTotalSales();
        }
    }
}