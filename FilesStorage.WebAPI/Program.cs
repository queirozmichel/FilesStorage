using Microsoft.EntityFrameworkCore;
using FilesStorage.WebAPI.Context;
using System.Text.Json.Serialization;
using FilesStorage.WebAPI.Repository;
using AutoMapper;
using FilesStorage.WebAPI.DTOs.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args); //Equivalente ao ConfigureServices()

// Add services to the container.
string mySqlServerConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
IMapper mapper = mappingConfig.CreateMapper();

builder.Services.AddCors();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIFilesStorage", Version = "v1" });
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
  {
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Header de autoricação JWY usando o esquema Bearer. \r\n\r\nInforme 'Bearer [espaço] token'.\r\n\r\nExemplo: 'Bearer 123456abcdef'",
  });
  c.AddSecurityRequirement(new OpenApiSecurityRequirement
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      },
      new string[]{}
    }
  });
});
builder.Services.AddScoped<IUnitOfWork, UnityOfWork>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(mySqlServerConnection));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
//JWT - adiciona o manipulador de autenticação e define o esquema de autenticação usado(Bearer). Valida o emissor, a audiência e a chave. Usando a chave secreta valida a assinatura.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{
  ValidateIssuer = true,
  ValidateAudience = true,
  ValidateLifetime = true,
  ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
  ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
  ValidateIssuerSigningKey = true,
  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
});

var app = builder.Build(); //Equivalente ao Configure()

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// middleware para redirecionar para https
app.UseHttpsRedirection();
// middleware para habilitar a autenticação
app.UseAuthentication();
// middleware para habilitar a autorização
app.UseAuthorization();
// middleware para habilitar o CORS
app.UseCors(options => options.AllowAnyOrigin());
// middleware para adicionar os endpoints para as Actions dos controladores sem especificar rotas
app.MapControllers();

app.Run();