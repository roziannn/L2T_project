namespace BloggieWeb.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCategory { get; set; }
        public string? Description { get; set; }
        public decimal NormalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedPrice => Math.Round(NormalPrice - (NormalPrice * (Discount / 100)), 2);
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? ImageUrl { get; set; }
        public string? UrlHandle { get; set; }
    }
}
