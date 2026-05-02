using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopNest.Core.DTOs;
using ShopNest.Core.Interface;
using ShopNest.Core.IServices;

namespace Shopnest.Api.Controllers
{
    [Route("products/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var serviceResult = await _productService.GetAllProductsAsync();
            if (!serviceResult.IsSuccess)
            {
                return BadRequest(serviceResult); // Return the failure wrapper
            }

            // Pass only the data (.Result) to the helper
            return OkResponse(serviceResult.Result, "Products retrieved successfully");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var response = await _productService.GetProductDetailsAsync(id);

            if (!response.IsSuccess)
            {
                return NotFound(response.Errors);
            }

            return OkResponse(response);
        }
    }
}
