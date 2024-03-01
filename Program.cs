using Joboard.Context;
using NSwag;
using Microsoft.OpenApi.Models;
using NSwag.Generation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Joboard.Controllers;
using Joboard.Service;
using Joboard.Repository.Role;
using Joboard.Repository.Tag;
using Joboard.Repository;
using Joboard.Service.Role;
using Joboard.Service.Tag;
using Joboard.Service.Category;
using Joboard.Entities;
using Joboard.Repository.Category;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Joboard.Service.Auth;
using Joboard.Services;
using Joboard.Service.User;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Joboard.Service.User.QrCode;
using Joboard.Extensions;
using Hangfire;
using Joboard.Service.Company;
using Joboard.Repository.Company;
using Joboard.Service.Job;
using Joboard.Repository.Job;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});


//Add independencies to the controller
builder.Services.AddScoped<PasswordHelper>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IQrCodeService, QrCodeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

//Add independencies to the controller
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IUserRepository, UserRepositoy>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepositoy>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

//Config JWT to make authenication API
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"])),
            };
        });

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure NSwag for Swagger documentation
builder.Services.AddSwaggerDocument(config =>
{
    config.DocumentName = "v1";
    config.PostProcess = document =>
    {
        document.Info.Title = "API Manage";
        document.Info.Version = "v1";
        document.Info.Contact = new NSwag.OpenApiContact
        {
            Name = "PhamTienDat",
            Email = "Phamtiendat246@gmail.com"
        };
    };
});

//Config service to ElasticSearch
builder.Services.AddElasticSearch(builder.Configuration);

//Register the ReccuringJob
builder.Services.AddSingleton<IRecurringJobManager, RecurringJobManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Add NSwag middleware to serve Swagger
app.UseOpenApi(config =>
{
    config.Path = "/swagger/v1/swagger.json";
});

//IApplicationBuilder applicationBuilder = app.UseSwagger(config =>
//{
//    config.Path = """/swagger/v1/swagger.json""";
//});

app.UseSwaggerUi(config =>
{
    config.Path = "/api";
    config.DocumentPath = "/swagger/v1/swagger.json";
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
