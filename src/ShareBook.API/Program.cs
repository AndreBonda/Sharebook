using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareBook.Application.Books;
using ShareBook.Application.Shared;
using ShareBook.Domain.Books;
using ShareBook.Infrastructure;
using ShareBook.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Postgres
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

// MediatR
builder.Services.AddMediatR(typeof(ShareBook.Application.StartUp).Assembly);

// Services
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookQueries>();

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
