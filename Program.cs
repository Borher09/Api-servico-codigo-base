using aula2ApiServico.DataContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection string
var connectionString = builder.Configuration.GetConnectionString("default");

// DbContext registration
builder.Services.AddDbContext<AppDbContexts>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
