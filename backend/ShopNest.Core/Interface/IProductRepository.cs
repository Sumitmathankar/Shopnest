using System;
using System.Collections.Generic;
using System.Text;
using ShopNest.Core.DTOs;
namespace ShopNest.Core.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAllProductsAsync();
        Task<Products> GetProductByIdAsync(int productId);
    }
}
