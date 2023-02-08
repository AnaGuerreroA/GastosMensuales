using Gastos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<GastosContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDbContext<GastosContext>(p => p.UseInMemoryDatabase("Gastos"));

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5241");
                      });
});

builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });


var app = builder.Build();

app.MapGet("/dbconexion", async([FromServices] GastosContext dbContext) =>
{
   dbContext.Database.EnsureCreated();
   return Results.Ok("Base de datos creada: " + dbContext.Database.IsInMemory());
});

app.MapGet("api/test/gastos", async([FromServices] GastosContext dbContext) =>
{
   var products = dbContext.Gastos.ToList();
   return Results.Ok(products);
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
