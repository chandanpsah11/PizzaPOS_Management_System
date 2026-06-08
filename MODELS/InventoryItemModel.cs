using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class InventoryItemModel
    {
        public int itemid { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required]
        [Display(Name = "Current Stock")]
        public decimal CurrentStock { get; set; }

        [Required]
        [Display(Name = "Unit of Measure")]
        public int UOMId { get; set; }

        [Display(Name = "Last Added Stock Date")]
        [DataType(DataType.Date)]
        public DateTime? LastAddedStockDate { get; set; }


    }
}
