using ShopNest.Core.Common;
using ShopNest.Core.DTOs;

namespace ShopNest.Core.IServices
{
    public interface IProductService
    {
        Task<ApiResponse<IEnumerable<ProductDto>>> GetAllProductsAsync();
        Task<ApiResponse<ProductDto>> GetProductDetailsAsync(int id);
    }
}
