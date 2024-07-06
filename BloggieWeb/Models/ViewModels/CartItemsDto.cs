namespace BloggieWeb.Models.ViewModels
{
    public class CartItemsDto
    {
        public Guid Id { get; set; }
        public string? UserProfileId { get; set; }
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCategory { get; set; }
        public string? ImageUrl { get; set; }

    }
}
