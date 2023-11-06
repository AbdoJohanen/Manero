using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Repositories.UserRepositories;
using Manero.Helpers.Services.DataServices;
using Manero.Helpers.Services.UserServices;
using Manero.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Contexts
builder.Services.AddDbContext<IdentityContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("UserDatabase")));
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ProductDatabase")));


// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AddressService>();
builder.Services.AddScoped<SeedService>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductTagService>();
builder.Services.AddScoped<ProductCategoryService>();
builder.Services.AddScoped<ProductTagService>();
builder.Services.AddScoped<ProductSizeService>();
builder.Services.AddScoped<SizeService>();
builder.Services.AddScoped<ColorService>();
builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<ProductColorService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<CategoryService>();


// Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AddressRepository>();
builder.Services.AddScoped<UserAddressRepository>();

builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ProductTagRepository>();
builder.Services.AddScoped<ProductCategoryRepository>();
builder.Services.AddScoped<ProductTagRepository>();
builder.Services.AddScoped<ProductSizeRepository>();
builder.Services.AddScoped<SizeRepository>();
builder.Services.AddScoped<ColorRepository>();
builder.Services.AddScoped<TagRepository>();
builder.Services.AddScoped<ProductColorRepository>();
builder.Services.AddScoped<ImageRepository>();
builder.Services.AddScoped<ReviewRepository>();
builder.Services.AddScoped<CategoryRepository>();


// Authentication
builder.Services.AddIdentity<AppUser, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedAccount = false;
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = true;
    x.Tokens.ProviderMap.Add("Default", new TokenProviderDescriptor(typeof(IUserTwoFactorTokenProvider<AppUser>)));
}).AddEntityFrameworkStores<IdentityContext>()
  .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/login";
    x.LogoutPath = "/";
    x.AccessDeniedPath = "/noway";
});

var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
