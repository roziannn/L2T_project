using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Service
{
    public interface IMstUserService
    {
        Task<List<UserDto>> GetAll();
    }

    public class MstUserService(IUserRepository userRepository, BloggieDbContext dbContext,
        AuthDbContext authContext) : IMstUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly BloggieDbContext _context = dbContext;
        private readonly AuthDbContext _authContext = authContext;

        public async Task<List<UserDto>> GetAll()
        {
            try
            {
                var users = await (from user in _authContext.Users
                                      join userRole in _authContext.UserRoles on user.Id equals userRole.UserId
                                      join role in _authContext.Roles on userRole.RoleId equals role.Id
                                      select new UserDto
                                      {
                                          UserId = Guid.Parse(user.Id),
                                          Username = user.UserName,
                                          EmailAddress = user.Email,
                                          RoleId = Guid.Parse(userRole.RoleId),
                                          Role = role.Name
                                      })
                               .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch notifications.", ex);
            }
        }
    }
}
