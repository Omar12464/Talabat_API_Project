﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_API.DTOs;
using Talabat_API.Errors;
using Talabat_API.Helper;
using Talabat_Core.Models;
using Talabat_Core.Repositories_InterFaces;
using Talabat_Core.Specification.ProductSpecifications;
using Talabat_Repository.Data;
using Talabat_Repository.RepositoreisClasses;

namespace Talabat_API.Controllers
{

    public class ProductsController : BaseAPIController
    {
        private readonly StoreContext _context;
        private readonly IGenericIcs<Product> _genericrepo;
        private readonly IMapper _map;

        public ProductsController(StoreContext context,IGenericIcs<Product> genericrepo, IMapper map)
        {
            _context = context;
            _genericrepo = genericrepo;
            _map = map;
        }
        [CachedAttribute(300)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetProducts([FromQuery]ProductSpecification paramss)
        {

            var spec = new ProductWithBrand_Category(paramss);
            var products = await _genericrepo.GettAllWithSpecAsync(spec);
            var map = _map.Map<IReadOnlyList< Product>, IReadOnlyList< ProductDTO>>(products);
            return Ok(new Pagination<ProductDTO>(paramss.PageSize,paramss.pageIndex,map));
            
            
         }
        [ProducesResponseType(typeof(ProductDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProduct(int id)
        {
            var spec=new ProductWithBrand_Category(id);
            var product = await _genericrepo.GettWithSpecAsync(spec);
            if (product == null) { return NotFound(new APIResponse(404)); }
            var map = _map.Map<Product,ProductDTO>(product);


            return Ok(map);
        }
            
    }

}
