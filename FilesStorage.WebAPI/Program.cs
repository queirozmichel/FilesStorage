using Microsoft.EntityFrameworkCore;
using FilesStorage.WebAPI.Context;
using System.Text.Json.Serialization;
using FilesStorage.WebAPI.Repository;
using AutoMapper;
using FilesStorage.WebAPI.DTOs.Mappings;

var builder = WebApplication.CreateBuilder(args); //Equivalente ao ConfigureServices()

// Add services to the container.
string mySqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
IMapper mapper = mappingConfig.CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUnitOfWork, UnityOfWork>();
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