using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PermissionManagement.API.API.Middleware;
using PermissionManagement.API.Application.Interfaces;
using PermissionManagement.API.Application.Services;
using PermissionManagement.API.Infrastructure.Data;
using PermissionManagement.API.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            NameClaimType = "sub"
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGovernorateService, GovernorateService>();
builder.Services.AddScoped<IPermissionTemplateService, PermissionTemplateService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IPermissionEvaluator, PermissionEvaluator>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await DataSeeder.SeedAsync(db);
}

app.Run();
