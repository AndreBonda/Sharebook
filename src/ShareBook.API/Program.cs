using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Books;
using ShareBook.Application.Shared;
using ShareBook.Domain.Books;
using ShareBook.Domain.Shared;
using ShareBook.Domain.Shippings;
using ShareBook.Domain.Users;
using ShareBook.Infrastructure;
using ShareBook.Infrastructure.Queries;
using ShareBook.Infrastructure.Repositories;
using ShareBook.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Postgres
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options
        .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"))
        .UseSnakeCaseNamingConvention();
});

// MediatR
// builder.Services.AddMediatR(typeof(ShareBook.Application.StartUp).Assembly);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ShareBook.Application.StartUp).Assembly);
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddAuthorization();
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("Jwt"));

// Services
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();
builder.Services.AddScoped<IBookQueries, BookQueries>();
builder.Services.AddScoped<DomainEventDispatcher>();
builder.Services.AddScoped<IHashingProvider, HashingProvider>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

// swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization using Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
