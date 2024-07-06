namespace BloggieWeb.Models.Domain
{
    public class CartItems
    {
        public Guid Id { get; set; }
        public string? UserProfileId { get; set; }
        public Guid ProductId { get; set; }
        public int? Quantity { get; set; }

    }
}
