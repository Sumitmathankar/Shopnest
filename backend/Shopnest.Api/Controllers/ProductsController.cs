using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopNest.Core.Interface;
using ShopNest.Core.DTOs;

namespace Shopnest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _prodcontext;

        public ProductsController(IProductRepository product)
        {
            _prodcontext = product;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            var products = await _prodcontext.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
            var product = await _prodcontext.GetProductByIdAsync(id);

            if (product == null) return NotFound();

            return Ok(product);
        }
    }
}
