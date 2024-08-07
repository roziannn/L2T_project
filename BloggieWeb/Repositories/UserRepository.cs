﻿using BloggieWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloggieWeb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext authDbContext;
        private readonly DbContext context;

        public UserRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }

        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
          var users =  await authDbContext.Users.ToListAsync();
          var superAdminUser = await authDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");

          if (superAdminUser != null)
            {
                users.Remove(superAdminUser);
            }

          return users;
        }

        //public async Task<IdentityUser> GetById(string id)
        //{
        //    return await authDbContext.Users.FindAsync(id);
        //}
    }
}
    