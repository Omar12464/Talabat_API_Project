using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Core.Models;
using Talabat_Core.Repositories_InterFaces;
using Talabat_Repository.Data;
using Talabat_Repository.RepositoreisClasses;

namespace Talabat_API.Controllers
{

    public class ProductsController : BaseAPIController
    {
        private readonly StoreContext _context;
        private readonly IGenericIcs<Product> _genericrepo;

        public ProductsController(StoreContext context,IGenericIcs<Product> genericrepo)
        {
            _context = context;
            _genericrepo = genericrepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            
            
                var products = await _genericrepo.GetAllAsync();
                return Ok(products);
            
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int id)
        {
            var product = await _genericrepo.GetAsync(id);
            if (product == null) { return NotFound(); }

            return Ok(product);
        }
            
    }

}
