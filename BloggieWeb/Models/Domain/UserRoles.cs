using CloudinaryDotNet.Actions;
using System.ComponentModel.DataAnnotations;

namespace BloggieWeb.Models.Domain
{
    public class UserRoles
    {
        [Key]
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
