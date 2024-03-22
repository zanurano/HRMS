using CoreApp.DAL;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json.Serialization;
//using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// connect to SQL server DBContext
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DBContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("ZConnectionString")
    )
);

//Keep PropertyName JSON in restfull
//method 1
//IServiceCollection services = builder.Services;
//services.AddControllersWithViews().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null); ;

//method 2
builder.Services.AddMvc().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

//method 3 using Microsoft.AspNetCore.Mvc;
//builder.Services.Configure<JsonOptions>(options =>
//{
//    options.JsonSerializerOptions.PropertyNamingPolicy = null;
//});

//MethodAccessException 4 using Microsoft.AspNetCore.Http.Json;
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.PropertyNamingPolicy = null;
//});
//==========================================================


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
