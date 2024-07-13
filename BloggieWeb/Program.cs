using BloggieWeb.Data;
using BloggieWeb.Repositories;
using BloggieWeb.Service;
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


builder.Services.Configure<IdentityOptions>(options =>
{
    // Default settings pass
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});
// added 3/3/24

// [Repository Pattern]
// added 1/3/24
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
// added 1/3/24

// added 3/3/
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();
// added 3/3/24

// added 4/3/24
builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();
// added 4/3/24

// added 11/3/24
builder.Services.AddScoped<IBlogPostCommentRepository, BlogPostCommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// added 11/3/24

// added 24/6/24
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IBroadcastService, BroadcastService>();
builder.Services.AddScoped<IMstProductService, MstProductService>();
builder.Services.AddScoped<IMstArticleService, MstArticleService>();
builder.Services.AddScoped<IMstUserService, MstUserService>();
builder.Services.AddScoped<IMstProfileService, MstProfileService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<ICartItemsService, CartItemsService>();

//added 4 juli 2024
//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//    .AddEntityFrameworkStores<AuthDbContext>()
//    .AddDefaultTokenProviders();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// added 3/3/24
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
