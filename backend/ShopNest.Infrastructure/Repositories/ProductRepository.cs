using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopNest.Core.DTOs;
using ShopNest.Core.Entities;
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

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();

        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);

        }
    }
}
