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
        bool UpdateAddress(Profile model);
    }


    public class MstProfileService(BloggieDbContext dbContext, AuthDbContext authDbContext) : IMstProfileService
    {
        private readonly BloggieDbContext _context = dbContext;
        private readonly AuthDbContext _authDbContext = authDbContext;

        public bool Update(Profile model)
        {
            try
            {
                var user = _authDbContext.Users.FirstOrDefault(u => u.Id == model.UserNewId);

                if (user != null)
                {
                    user.UserName = model.FullName; // Update nama pengguna dengan nama lengkap baru
                    _authDbContext.Users.Update(user); // Update entitas pengguna dalam konteks

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

                    _context.SaveChanges(); // Simpan perubahan ke basis data
                    _authDbContext.SaveChanges(); // Simpan perubahan nama pengguna ke basis data auth

                    return true;
                }
                else
                {
                    Console.WriteLine($"User with id {model.UserNewId} not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Tangani kesalahan jika ada
                Console.WriteLine($"Error updating profile: {ex.Message}");
                return false; // Operasi gagal
            }
        }

        public bool UpdateAddress(Profile model)
        {
            try
            {
                var user = _authDbContext.Users.FirstOrDefault(u => u.Id == model.UserNewId);

                if (user != null)
                {
                    var profile = _context.Profile.FirstOrDefault(p => p.UserNewId == model.UserNewId);

                    if (profile == null)
                    {
                        Console.WriteLine($"Profile with UserNewId {model.UserNewId} does not exist.");
                        return false;
                    }

                    profile.AddressLine = model.AddressLine;
                    profile.Province = model.Province;
                    profile.State = model.State;
                    profile.City = model.City;
                    profile.ZipCode = model.ZipCode;

                    _context.Profile.Update(profile);
                    _context.SaveChanges(); 

                    return true;
                }
                else
                {
                    Console.WriteLine($"User with id {model.UserNewId} not found.");
                    return false;
                }
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
