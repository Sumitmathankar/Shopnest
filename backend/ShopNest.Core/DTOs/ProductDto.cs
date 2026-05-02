using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNest.Core.DTOs
{
    // ── Request DTOs ──────────────────────────────────────────
    public record CreateProductDto(
     string Name,
     string? Description,
     decimal Price,
     int Stock,
     int CategoryId,
     string? ImageUrl
    );

    public record UpdateProductDto(
        string? Name,
        string? Description,
        decimal? Price,
        int? Stock,
        bool? IsActive,
        string? ImageUrl,
        int? CategoryId
    );

        public record ProductDto(
         int ProductId,
         string Name,
         string? Description,
         decimal Price,
         int Stock,
         int? CategoryId,
         string? CategoryName,
         DateTime? CreatedAt   
     );

    public record CategoryDto(
        int Id,
        string Name,
        string? Description,
        string? ImageUrl,
        int ProductCount
    );
}
