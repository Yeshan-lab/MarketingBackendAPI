using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// ✅ PostgreSQL connection string
string connectionString = "Host=dpg-d1d325fdiees73ce9idg-a.singapore-postgres.render.com;Port=5432;Database=marketing_db_3jm1;Username=marketing_db_3jm1_user;Password=8VTWBsDXJSkoPo2e9btFQaGY38JV2om4;Ssl Mode=Require;Trust Server Certificate=true;";
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
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
    c.RoutePrefix = string.Empty;
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
    Console.WriteLine("✅ Connected to PostgreSQL");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ DB error: {ex.Message}");
}

app.Run();
