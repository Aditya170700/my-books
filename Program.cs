using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MyBooks.Data;
using MyBooks.Data.Services;
using MyBooks.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add db context
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services
builder.Services.AddTransient<BookService>();
builder.Services.AddTransient<AuthorService>();
builder.Services.AddTransient<PublisherService>();

// Add controllers
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

    // Db seeder
    // AppDbInitializer.Seed(app);
}

// Exception middleware
app.ConfigureBuildInExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

