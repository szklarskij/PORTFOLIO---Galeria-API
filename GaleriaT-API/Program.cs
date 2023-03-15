using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using GaleriaT_API;
using GaleriaT_API.Entities;
using GaleriaT_API.Middleware;
using GaleriaT_API.Models;
using GaleriaT_API.Models.Validators;
using GaleriaT_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);


//builder.Logging.ClearProviders();
//builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();


var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";

}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<GalleryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GaleriaDbConnection")));
builder.Services.AddScoped<GallerySeeder>();
builder.Services.AddScoped<IGalleryService, GalleryService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddScoped<IPasswordHasher<Admin>,PasswordHasher<Admin>>();
builder.Services.AddScoped<IValidator<AdminPasswordDto>, UpdateAdminPasswordDtoValidator>();
builder.Services.AddScoped<IValidator<GalleryQuery>, GalleryQueryValidator>();
string[] origins = new String[]{ "https://spichlerz-form-i-ksztaltow.netlify.app/", "https://spichlerz-form-i-ksztaltow.netlify.app/admin", "http://localhost:8080" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policyBuilder =>

        policyBuilder.AllowAnyMethod()
                     .AllowAnyHeader()
                     .WithExposedHeaders("Location")
                     //.WithOrigins(origins)
                     .AllowAnyOrigin()
                     
       );
});




builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<GallerySeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.
//app.UseResponseCaching();
app.UseStaticFiles();
app.UseCors("FrontEndClient");
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gallery API")
);

app.UseRouting();

app.UseAuthorization();


app.MapControllers();

app.Run();
