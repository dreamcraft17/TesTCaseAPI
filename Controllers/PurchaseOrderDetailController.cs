using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Controllers
{
    [System.Web.Http.RoutePrefix("api/PurchaseOrderDetail")]
    public class PurchaseOrderDetailController : ApiController
    {
        // GET: PurchaseOrderDetail
        PurchaseOrderDAL purcahseorderDAL = new PurchaseOrderDAL();
        SupplierDAL supplierDAL = new SupplierDAL();
        PurchaseOrderDetailDAL purchaseorderdetailDAL = new PurchaseOrderDetailDAL();

        // GET: api/Supplier
        public IEnumerable<PurchaseOrderDetailModel> Get()
        {
            return purchaseorderdetailDAL.GetPODetail();
        }

        // POST: api/Supplier
        public IHttpActionResult Post([FromBody] PurchaseOrderDetailModel pod)
        {
            if (purchaseorderdetailDAL.InsertPO(pod))
            {
                return Ok("Purchase Order saved successfully.");
            }
            else
            {
                return BadRequest("Data not saved.");
            }
        }

        // PUT: api/Supplier/5
        public IHttpActionResult Put(Guid id, [FromBody] PurchaseOrderDetailModel pod)
        {
            if (purchaseorderdetailDAL.Updatepo(pod))
            {
                return Ok("Purchase Order updated successfully.");
            }
            else
            {
                return BadRequest("Data not updated.");
            }
        }

        // DELETE: api/Supplier/5
        public IHttpActionResult Delete(Guid id)
        {
            int result = purchaseorderdetailDAL.Deletepo(id);
            if (result > 0)
            {
                return Ok("Purchase Order deleted successfully.");
            }
            else
            {
                return BadRequest("Data not deleted.");
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/PurchaseOrderDetail/GetSuppliers")]
        public IEnumerable<Guid> GetSuppliers()
        {
            IEnumerable<SupplierModel> suppliers = supplierDAL.GetSupplier(); 
            IEnumerable<Guid> supplierIds = suppliers.Select(supplier => supplier.ID); 
            return supplierIds;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/PurchaseOrderDetail/GetPurchaseOrder")]
        public IEnumerable<Guid> GetPurchaseOrder()
        {
            IEnumerable<PurchaseOrderModel> purchases = purcahseorderDAL.GetPurchaseOrder(); 
            IEnumerable<Guid> purchasesIds = purchases.Select(purchase => purchase.ID); 
            return purchasesIds;
        }
    }
}