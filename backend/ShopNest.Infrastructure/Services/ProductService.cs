using Azure;
using ShopNest.Core.Common;
using ShopNest.Core.DTOs;
using ShopNest.Core.Entities;
using ShopNest.Core.Interface;
using ShopNest.Core.IServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNest.Infrastructure.Services
{
    public class ProductService:IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ApiResponse<IEnumerable<ProductDto>>> GetAllProductsAsync()
            {
            var response = new ApiResponse<IEnumerable<ProductDto>>();

            var result = await _productRepository.GetAllProductsAsync();

            if (result != null && result.Any())
            {
                var products = result.Select(p => new ProductDto(
                     p.ProductId,
                     p.Name!,
                     p.Description,
                     p.Price,
                     p.Stock,               
                     p.CategoryId,
                     p.Category?.Name,          
                     p.CreatedAt
                 )).ToList();

                response.Result = products;
                response.Errors = new List<string>();
            }
            else
            {
                response.Result = new List<ProductDto>();
                response.Errors = new List<string> { "No products found." };
            }

            return response;
        }

            public async Task<ApiResponse<ProductDto>> GetProductDetailsAsync(int id)
            {
               var response = new ApiResponse<ProductDto>();

                var product = await _productRepository.GetProductByIdAsync(id);

                // 2. Check if it exists
                if (product == null)
                {
                    response.Errors.Add($"Product with ID {id} was not found.");
                    return response; // IsSuccess will be false
                }

            // 3. Map to DTO
                    response.Result = new ProductDto
                    (
                        product.ProductId,
                        product.Name,
                        product.Description,
                        product.Price,
                        product.Stock,
                        product.CategoryId,
                        product.Category?.Name,
                        product.CreatedAt
                    );

                return response;
            }
    }
}   
          
