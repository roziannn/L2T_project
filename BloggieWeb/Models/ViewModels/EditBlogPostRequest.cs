using Microsoft.AspNetCore.Mvc.Rendering;

namespace BloggieWeb.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; } //unique identi
        public String Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        // Display tags
        public IEnumerable<SelectListItem> Tags { get; set; } // property from which can display tags into drpdown

        public string[] SelectedTags { get; set; } = Array.Empty<string>(); // Collect tags (multiple tags)
    }
}
