using BloggieWeb.Data;
using BloggieWeb.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// added 29/2/24
builder.Services.AddDbContext<BloggieDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));
// added 29/2/24

// added 3/3/24 Inject AuthDbContext
builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieAuthDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().
    AddEntityFrameworkStores<AuthDbContext>();
// added 3/3/24

// [Repository Pattern]
// added 1/3/24
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
// added 1/3/24

// added 3/3/
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
// added 3/3/24

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// added 3/3/24
app.UseAuthentication();
// added 3/3/24

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
