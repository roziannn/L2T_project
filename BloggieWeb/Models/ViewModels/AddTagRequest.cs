using System.ComponentModel.DataAnnotations;

namespace BloggieWeb.Models.ViewModels
{
    public class AddTagRequest
    {
        [Required]
        public String Name { get; set; }

        [Required]
        public String DisplayName { get; set; }
    }
}
