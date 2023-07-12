
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PlatformWellAssessment2.Data;
using PlatformWellAssessment2.Operations;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager config = builder.Configuration;

//For Entity Framework
builder.Services.AddDbContext<PlatformWelldbContext>(options => options.UseSqlServer(config["ConnectionString:PlatformWellConnectionString"]));

builder.Services.AddScoped<PlatformWellManagement>();
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();