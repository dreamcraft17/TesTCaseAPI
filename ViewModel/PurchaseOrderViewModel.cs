using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCaseWebAPI.ViewModel
{
    public class PurchaseOrderViewModel
    {
        public Guid ID { get; set; }

     
        public string Code { get; set; }

        
        public DateTime PurchaseDate { get; set; }

       
        public Guid SupplierID { get; set; }

        public string Remarks { get; set; }
    }
}