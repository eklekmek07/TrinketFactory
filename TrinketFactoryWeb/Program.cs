// tempdata notifaciaton
// pagination - search ve filtre 
// Checkout - Cart buy view düzenleme
// bakılabilirse bootstrap teması
// (Must) Details - Emre
// Genel veri düzeni - Emre
// Layout değişimi + Shopping cart
// Customer Name  logout dropdown muadili bişeye sığdır

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using TrinketFactoryWeb.Data;
using TrinketFactoryWeb.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();