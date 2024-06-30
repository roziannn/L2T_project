namespace BloggieWeb.Models.ViewModels
{

    // try to merge user and userRole from auth db
    public class UserDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
    }
}
