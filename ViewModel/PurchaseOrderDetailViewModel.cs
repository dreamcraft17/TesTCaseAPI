using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestCaseWebAPI.ViewModel
{
    public class PurchaseOrderDetailViewModel
    {

        public Guid ID { get; set; }

      
        public Guid PurchaseOrderID { get; set; }

       
        public Guid ProductID { get; set; }

       
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}