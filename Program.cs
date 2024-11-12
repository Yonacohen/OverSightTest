using Microsoft.AspNetCore.Identity;
using OverSightTest;
using OverSightTest.Interfaces;
using OverSightTest.Services;

var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthentication();
app.UseAuthorization();

app.Run();
