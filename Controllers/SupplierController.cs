using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Controllers
{
    public class SupplierController : ApiController
    {
        SupplierDAL supplierDAL = new SupplierDAL();

        // GET: api/Supplier
        public IEnumerable<SupplierModel> Get()
        {
            return supplierDAL.GetSupplier();
        }

        // POST: api/Supplier
        public IHttpActionResult Post([FromBody] SupplierModel supplier)
        {
            if (supplierDAL.InsertSupplier(supplier))
            {
                return Ok("Supplier saved successfully.");
            }
            else
            {
                return BadRequest("Data not saved.");
            }
        }

        // PUT: api/Supplier/5
        public IHttpActionResult Put(Guid id, [FromBody] SupplierModel supplier)
        {
            if (supplierDAL.UpdateSupplier(supplier))
            {
                return Ok("Supplier updated successfully.");
            }
            else
            {
                return BadRequest("Data not updated.");
            }
        }

        // DELETE: api/Supplier/5
        public IHttpActionResult Delete(Guid id)
        {
            int result = supplierDAL.DeleteSupplier(id);
            if (result > 0)
            {
                return Ok("Supplier deleted successfully.");
            }
            else
            {
                return BadRequest("Data not deleted.");
            }
           
        }
    }
}
