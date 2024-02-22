using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Controllers
{
    [RoutePrefix("api/PurchaseOrder")]
    public class PurchaseOrderController : ApiController
    {

        // GET: PurchaseOrder
        PurchaseOrderDAL purcahseorderDAL = new PurchaseOrderDAL();
        SupplierDAL supplierDAL = new SupplierDAL();

        // GET: api/Supplier
        public IEnumerable<PurchaseOrderModel> Get()
        {
            return purcahseorderDAL.GetPurchaseOrder();
        }

        // POST: api/Supplier
        public IHttpActionResult Post([FromBody] PurchaseOrderModel po)
        {
            if (purcahseorderDAL.InsertPO(po))
            {
                return Ok("Purchase Order saved successfully.");
            }
            else
            {
                return BadRequest("Data not saved.");
            }
        }

        // PUT: api/Supplier/5
        public IHttpActionResult Put(Guid id, [FromBody] PurchaseOrderModel po)
        {
            if (purcahseorderDAL.Updatepo(po))
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
            int result = purcahseorderDAL.Deletepo(id);
            if (result > 0)
            {
                return Ok("Purchase Order deleted successfully.");
            }
            else
            {
                return BadRequest("Data not deleted.");
            }
        }

        [HttpGet]
        [Route("api/PurchaseOrder/GetSuppliers")]
        public IEnumerable<Guid> GetSuppliers()
        {
            IEnumerable<SupplierModel> suppliers = supplierDAL.GetSupplier(); // Mengambil semua objek SupplierModel
            IEnumerable<Guid> supplierIds = suppliers.Select(supplier => supplier.ID); // Mengambil hanya nilai ID dari setiap objek SupplierModel
            return supplierIds;
        }

    }
}
