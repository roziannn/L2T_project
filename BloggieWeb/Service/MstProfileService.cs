using BloggieWeb.Data;
using BloggieWeb.Models.Domain;
using BloggieWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BloggieWeb.Service
{
    public interface IMstProfileService
    {
        bool Update(Profile model);
    }


    public class MstProfileService(BloggieDbContext dbContext, AuthDbContext authDbContext) : IMstProfileService
    {
        private readonly BloggieDbContext _context = dbContext;
        private readonly AuthDbContext _authDbContext = authDbContext;


        public bool Update(Profile model)
        {
            var user = _authDbContext.Users.FirstOrDefault(u => u.Id == model.UserNewId);

            var profile = _context.Profile.FirstOrDefault(p => p.UserNewId == model.UserNewId);

            if (profile == null)
            {
                // Jika profil belum ada, buat profil baru
                profile = new Profile
                {
                    UserNewId = model.UserNewId,
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                    Email = user.Email,
                    IsSuscribed = model.IsSuscribed,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _context.Profile.Add(profile); // Tambahkan profil baru ke konteks
            }
            else
            {
                // Jika profil sudah ada, lakukan update
                profile.Email = user.Email;
                profile.FullName = model.FullName;
                profile.PhoneNumber = model.PhoneNumber;
                profile.Gender = model.Gender;
                profile.BirthDate = model.BirthDate;
                profile.IsSuscribed = model.IsSuscribed;
                profile.UpdatedAt = DateTime.Now;

                _context.Profile.Update(profile); // Update profil dalam konteks
            }

            try
            {
                _context.SaveChanges(); // Simpan perubahan ke basis data
                return true; // Operasi berhasil
            }
            catch (Exception ex)
            {
                // Tangani kesalahan jika ada
                Console.WriteLine($"Error updating profile: {ex.Message}");
                return false; // Operasi gagal
            }
        }
    }
}
