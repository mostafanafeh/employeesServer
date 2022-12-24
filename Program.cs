using employeesServer.Data;
using employeesServer.Services.EmailService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
var ConnectionString = $"server={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Trust server certificate=true";
builder.Services.AddDbContext<EmployeeDbContext>(options =>
   options.UseSqlServer(ConnectionString));

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/health");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
