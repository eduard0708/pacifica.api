global using Pacifica.API.Models;
global using Pacifica.API.Helper;
global using Pacifica.API.Data;
global using AutoMapper;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Pacifica.API.Mapper;
using Pacifica.API.Services.BranchProductService;
using Pacifica.API.Services.BranchService;
using Pacifica.API.Services.CategoryService;
using Pacifica.API.Services.ProductService;
using Pacifica.API.Services.SupplierService;
using Pacifica.API.Services.AuthService;
using Pacifica.API.Services.EmployeeService;
using Pacifica.API.Services.RoleService;
using Pacifica.API.Services.TokenService;
using Pacifica.API.Services.StatusService;
using Pacifica.API.Services.StockInReferenceService;
using Pacifica.API.Services.StockOutReferenceService;
using Pacifica.API.Services.StockInService;
using Pacifica.API.Services.StockOutService;
using Pacifica.API.Services.InventoryService;
using Pacifica.API.Services.InventoryNormalizationService;
using Pacifica.API;
using Pacifica.API.Services.PaymentMethodService;
using Pacifica.API.Models.Inventory;
using Pacifica.API.Models.EmployeManagement;
using Pacifica.API.Services.EmployeeManagementService;
using Pacifica.API.Services.F154ReportService;
using Pacifica.API.Services.MenuService;
using Pacifica.API.Services.BeginningInventoryService;
using Pacifica.API.Middleware;
using Pacifica.API.Services.F152ReportService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(); // **This is the fix**
builder.Services.AddLogging();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //  options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.ReferenceHandler = null;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

    });

// **Enable CORS** - Add this code to configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Allow the frontend to access the backend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Adding the DbContext to the service container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Registering Identity services for Employee
builder.Services.AddIdentity<Employee, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Register AutoMapper for DTOs mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register Application services (like Employee, Role, and Auth Services)
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IStockInReferenceService, StockInReferenceService>();
builder.Services.AddScoped<IStockOutReferenceService, StockOutReferenceService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IStockOutService, StockOutService>();
builder.Services.AddScoped<IStockInService, StockInService>();
builder.Services.AddScoped<IBranchProductService, BranchProductService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IInventoryNormalizationService, InventoryNormalizationService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IF154ReportService, F154ReportService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IBeginningInventoryService, BeginningInventoryService>();
builder.Services.AddScoped<IF152ReportService, F152ReportService>();



builder.Services.AddScoped<ProductAuditTrailHelper>();


// Register Logger service
builder.Services.AddLogging(configure => configure.AddConsole()); // You can also add other providers (e.g., File, Debug)

// Adding JWT Authentication (if you plan to use JWT tokens for Authentication)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
    };
});

// Add Authorization service to the DI container
builder.Services.AddAuthorization();

var app = builder.Build();

// Seed data on application startup

// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//     // Call the SeedData.Initialize method to populate the database
//     SeedData.Initialize(dbContext);
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure HTTPS redirection is done early
app.UseHttpsRedirection();

// **Use CORS Middleware** - Add this line to enable CORS globally
app.UseCors("AllowLocalhost"); // This will apply the CORS policy

// Add middleware for Authentication & Authorization (if using JWT)
app.UseAuthentication();  // Handle authentication (JWT token parsing)
app.UseAuthorization();   // Handle authorization (roles, claims, etc.)

// Register the DateTimeLocalTimeMiddleware (your custom middleware)
app.UseMiddleware<DateTimeLocalTimeMiddleware>(); // This middleware modifies DateTime values in requests.

// Map controllers for your API
app.MapControllers();

// Final run statement
app.Run();