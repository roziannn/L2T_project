using BloggieWeb.Models.Domain;

namespace BloggieWeb.Models.ViewModels
{
    public class HomeViewModel
    {
        // so we can display multiple sets of informations
        public IEnumerable<BlogPost> BlogPosts { get; set; }    

        public IEnumerable<Tag> Tags { get; set; }
    }
}
