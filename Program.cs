using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBackendApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Set your MySQL connection string (using public URL from Railway)
string connectionString = "server=centerbeam.proxy.rlwy.net;port=12170;database=railway;user=root;password=yZCrCqokQbzFZZqPIQLVpEMABwiwmPeC";

// Log the connection string setup
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    Console.WriteLine("Attempting to connect to MySQL Database...");
});

// Add services to the container (add other services as needed)
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(); // Enable Swagger UI
    app.UseSwaggerUI(); // To display Swagger UI
}

app.UseHttpsRedirection();  // Comment this line out if not using HTTPS during dev
app.UseAuthorization();
app.MapControllers();

try
{
    // Optional: You can verify the DB connection at startup here
    var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.OpenConnection();
    Console.WriteLine("Successfully connected to the database!");
}
catch (Exception ex)
{
    Console.WriteLine($"Error connecting to database: {ex.Message}");
}

// Run the application
app.Run();
