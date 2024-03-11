using Microsoft.AspNetCore.Identity;

namespace BloggieWeb.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
