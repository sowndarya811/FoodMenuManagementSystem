
using Microsoft.EntityFrameworkCore;
using Food_Menu_Management_System.Data;
using Food_Menu_Management_System.Repositories;
using Food_Menu_Management_System.Services;
using Food_Menu_Management_System.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1. Register Controllers
builder.Services.AddControllers();

// 2. Configure SQL Server Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Register Repository Dependency Injection
builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();

// 4. Register Service Dependency Injection
builder.Services.AddScoped<IFoodItemService, FoodItemService>();

// 5. Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 6. Register Global Exception Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// 7. Configure Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 8. Configure HTTPS
app.UseHttpsRedirection();

// 9. Map Controller Endpoints
app.MapControllers();

// 10. Start Application
app.Run();
