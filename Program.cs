using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OverSightTest;
using OverSightTest.Interfaces;
using OverSightTest.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Authentication configuration
var key = Encoding.ASCII.GetBytes("511536EF-F270-4058-80CA-1C89C192F69A"); // Use a secret key stored securely
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add services to the container.
//test
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OversightDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<OversightDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IReportService, ReportsService>();
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();
await accountService.CreateRoleAsync("Admin");
await accountService.AddUserAsync("yonatanc", "Yona1234$");
await accountService.AssignRoleToUserAsync("yonatanc", "Admin");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();



app.Run();
