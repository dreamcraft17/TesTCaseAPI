using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class PurchaseOrderDetailModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [Display(Name = "Supplier Code")]
        public Guid PurchaseOrderID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Supplier Name")]
        public Guid ProductID { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "Supplier City")]
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Unit Price is required")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

    }
}