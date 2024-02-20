using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {
        ProductDAL productDAL = new ProductDAL();

        // GET: api/Product
        public IEnumerable<ProductModel> Get()
        {
            return productDAL.GetProduct();
        }

        // POST: api/Product
        public IHttpActionResult Post([FromBody] ProductModel product)
        {
            if (productDAL.InsertProduct(product))
            {
                return Ok("Product saved successfully.");
            }
            else
            {
                return BadRequest("Data not saved.");
            }
        }

        // PUT: api/Product/5
        public IHttpActionResult Put(Guid id, [FromBody] ProductModel product)
        {
            if (productDAL.UpdateProduct(product))
            {
                return Ok("Product updated successfully.");
            }
            else
            {
                return BadRequest("Data not updated.");
            }
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(Guid id)
        {
            int result = productDAL.DeleteProduct(id);
            if (result > 0)
            {
                return Ok("Product deleted successfully.");
            }
            else
            {
                return BadRequest("Data not deleted.");
            }
        }
    }
}

