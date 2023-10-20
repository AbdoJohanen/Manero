using Manero.Contexts;
using Manero.Helpers.Repositories.DataRepositories;
using Manero.Helpers.Services.DataServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// Contexts
builder.Services.AddDbContext<IdentityContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDatabase")));
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ProductDatabase")));


// Services
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


// Repositories
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


// Authentication


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
