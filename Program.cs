using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Connection string to Railway MySQL DB
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

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Use Swagger even in Production
app.UseSwagger();
app.UseSwaggerUI();

// Use CORS
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

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
