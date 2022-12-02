using Microsoft.EntityFrameworkCore;
using FilesStorage.WebAPI.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args); //Equivalente ao ConfigureServices()

// Add services to the container.

builder.Services.AddControllers()
  .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string mySqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WebAPIContext>(options => options.UseSqlServer(mySqlServerConnection));

var app = builder.Build(); //Equivalente ao Configure()

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// middleware para redirecionar para https
app.UseHttpsRedirection();
// middleware para habilitar a autorização
app.UseAuthorization();
// middleware para adicionar os endpoints para as Actions dos controladores sem especificar rotas
app.MapControllers();

app.Run();