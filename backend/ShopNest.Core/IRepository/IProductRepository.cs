using System;
using System.Collections.Generic;
using System.Text;
using ShopNest.Core.Entities;

namespace ShopNest.Core.Interface

{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
    }
}
