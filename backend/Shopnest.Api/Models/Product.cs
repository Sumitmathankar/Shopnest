namespace Shopnest.Api.Models
{
    public class Product
    {
        public int Id { get; set; } // SQL Server will make this the Primary Key
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; } // Use decimal for money!
        public string? ImageUrl { get; set; }
        public int Stock { get; set; }
    }
}
