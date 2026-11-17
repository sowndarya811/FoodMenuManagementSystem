
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

// 3. Register Food Item Repository
builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();

// 4. Register Food Item Service
builder.Services.AddScoped<IFoodItemService, FoodItemService>();

// 5. Register Category Repository
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// 6. Register Category Service
builder.Services.AddScoped<ICategoryService, CategoryService>();

// 7. Configure CORS for Angular Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 8. Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 9. Register Global Exception Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

// 10. Configure Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 11. Configure HTTPS
app.UseHttpsRedirection();

// 12. Enable CORS
app.UseCors("AllowAngular");

// 13. Configure Authorization
app.UseAuthorization();

// 14. Map Controller Endpoints
app.MapControllers();

// 15. Start Application
app.Run();
