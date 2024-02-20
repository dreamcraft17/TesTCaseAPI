using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class PurchaseOrderModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [StringLength(10, ErrorMessage = "Code must be a string with a maximum length of 10")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Purchase Date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Supplier ID is required")]
        [Display(Name = "Supplier ID")]
        public Guid SupplierID { get; set; }

        [MaxLength]
        public string Remarks { get; set; }
    }
}