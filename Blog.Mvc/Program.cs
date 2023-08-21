using Blog.Mvc.AutoMapper.Profiles;
using Blog.Services.AutoMapper.Profiles;
using Blog.Services.Extensions;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

builder.Services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile), typeof(UserProfile));
builder.Services.LoadMyServices();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Admin/User/Login");
    options.LogoutPath = new PathString("/Admin/User/Logout");
    options.Cookie = new CookieBuilder
    {
        Name = "Blog",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest
    };
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
    options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");
});
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePages();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); // Doðru sýralama burada olmalý

app.UseSession();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.Run();
