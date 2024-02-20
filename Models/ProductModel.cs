using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ProductModel
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Code is required")]
        [Display(Name = "Supplier Code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Supplier Name")]
        public string Name { get; set; }
     
       
    }
}