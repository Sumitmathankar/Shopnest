using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopNest.Core.DTOs;
using ShopNest.Core.Interface;
using ShopNest.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNest.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopnestDbContext _context;
        public ProductRepository(ShopnestDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Products>> GetAllProductsAsync()
        {
            return await _context.Products.Select(p => new Products
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CreatedAt = p.CreatedAt,

            }).ToListAsync();
        }

        public async Task<Products> GetProductByIdAsync(int productId)
        {
            return await _context.Products.Where(p => p.ProductId == productId).Select(p => new Products

            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CreatedAt = p.CreatedAt,
            }).FirstOrDefaultAsync();

        }
    }
}
