using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class PizzaMasterModel
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
    public class PizzaMasterModelCreate
    {
        [Required(ErrorMessage = "Pizza name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Pizza name must be between 2 and 100 characters.")]
        [Display(Name = "Pizza Name")]
        public string PizzaName { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
        [Display(Name = "Category")]
        public int categoryid { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }

}
