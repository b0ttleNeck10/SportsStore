using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StoreDBContext>(options => {
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
});

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();
app.UseSession();

app.MapControllerRoute("catpage", 
    "{category}/Page{ProductPage:int}",
    new { Controller = "Home", action = "Index" });

app.MapControllerRoute("page",
    "Page{ProductPage:int}",
    new { Controller = "Home", action = "Index", ProductPage = 1 });

app.MapControllerRoute("category",
    "{category}",
    new { Controller = "Home", action = "Index", ProductPage = 1 });

app.MapControllerRoute("pagination",
    "Products/Page{productPage}",
    new { Controller = "Home", action = "Index", ProductPage = 1 });

app.MapDefaultControllerRoute();
app.MapRazorPages();

SeedData.EnsurePopulated(app);

app.Run();
