using Blog.Services.AutoMapper.Profiles;
using Blog.Services.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.JsonSerializerOptions.ReferenceHandler= ReferenceHandler.Preserve;
});

builder.Services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile));//7,0!! Startup?
builder.Services.LoadMyServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseStatusCodePages();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//Bayrama sor
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapAreaControllerRoute(
//        name: "Admin",
//        areaName: "Admin",
//        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
//    );
//    endpoints.MapDefaultControllerRoute();
//});

app.MapAreaControllerRoute(
            name: "Admin",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
