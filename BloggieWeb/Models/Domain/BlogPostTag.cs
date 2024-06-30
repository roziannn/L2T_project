namespace BloggieWeb.Models.Domain
{
    public class BlogPostTag
    {
        public Guid BlogPostsId { get; set; } 
        public Guid TagsId { get; set; } 
    }
}
