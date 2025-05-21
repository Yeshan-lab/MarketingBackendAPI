using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ Database connection
string connectionString = "server=centerbeam.proxy.rlwy.net;port=12170;database=railway;user=root;password=yZCrCqokQbzFZZqPIQLVpEMABwiwmPeC";
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// ✅ Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Enable CORS for any frontend
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

// ✅ Use CORS (before anything else)
app.UseCors("AllowAll");

// ✅ Enable Swagger on root
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBackendApi V1");
    c.RoutePrefix = string.Empty; // this makes Swagger UI appear at "/"
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ✅ Optional DB connection test
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.OpenConnection();
    Console.WriteLine("✅ Connected to MySQL");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ DB error: {ex.Message}");
}

app.Run();