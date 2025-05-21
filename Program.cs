using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Connection string
string connectionString = "server=centerbeam.proxy.rlwy.net;port=12170;database=railway;user=root;password=yZCrCqokQbzFZZqPIQLVpEMABwiwmPeC";

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS for browser access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// ✅ Enable Swagger in all environments (important for Render)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBackendApi V1");
    c.RoutePrefix = string.Empty; // Makes Swagger available at root URL
});

// Enable CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Optional DB test
try
{
    var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.OpenConnection();
    Console.WriteLine("✅ Successfully connected to MySQL DB");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ DB connection failed: {ex.Message}");
}

app.Run();
