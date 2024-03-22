using CoreApp.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMvc().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

// connect SQL Server Context
//builder.Services.AddDbContext<DBContext>(options => 
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ZConnectionString"))
//);

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
//app.UseFileServer();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\assets")),
    RequestPath = new PathString("/assets")
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\assets")),
    RequestPath = new PathString("/assets")
});

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    //endpoints.MapAreaControllerRoute(
    //    name: "Admin",
    //    areaName: "Admin",
    //    pattern: "Admin/{controller=Dashboard}/{action=Index}"
    //);

    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area}/{controller}/{action}",
        //new { area = "Admin", controller = "Dashboard", action = "Index" }
        new { area = "Site", controller = "Auth", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area}/{controller}/{action}/{id}",
        //new { area = "Admin", controller = "Dashboard", action = "Index", id = "" }
        new { area = "Site", controller = "Auth", action = "Index", id = "" }
    );

    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area}/{controller}/{action}/{source}/{id}",
        //new { area = "Admin", controller = "Dashboard", action = "Index", id = "" }
        new { area = "Site", controller = "Auth", action = "Index", source="", id = "" }
    );

    //endpoints.MapControllerRoute(
    //    name: "areaRoute",
    //    pattern: "{area:exists}/{controller}/{action}"
    //);

    //endpoints.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}"
    //);

    endpoints.MapDefaultControllerRoute();
});

app.Run();
