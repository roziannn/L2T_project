using System.ComponentModel.DataAnnotations;

namespace BloggieWeb.Models.Domain
{
    public class Profile
    {
        [Key]
        public Guid UserNewId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly BirthDate { get; set; }
        public string AddressLine { get; set; }
        public string Province { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

    }
}
