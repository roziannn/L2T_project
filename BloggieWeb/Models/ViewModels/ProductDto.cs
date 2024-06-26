namespace BloggieWeb.Models.ViewModels
{
    public class ProductDto
    {

        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCategory { get; set; }
        public string? Description { get; set; }
        public decimal NormalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
