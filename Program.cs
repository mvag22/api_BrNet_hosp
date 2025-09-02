using System.Text;
using ApiBrnetEstoque.Models;
using ApiBrnetEstoque.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// ==========================
// BANCO DE DADOS (MySQL)
// ==========================
var host = Environment.GetEnvironmentVariable("MYSQLHOST") ?? "localhost";
var port = Environment.GetEnvironmentVariable("MYSQLPORT") ?? "3306";
var user = Environment.GetEnvironmentVariable("MYSQLUSER") ?? "root";
var pwd = Environment.GetEnvironmentVariable("MYSQLPASSWORD") ?? "";
var db = Environment.GetEnvironmentVariable("MYSQLDATABASE") ?? "appdb";

var connString = $"server={host};port={port};database={db};user={user};password={pwd};SslMode=none";

builder.Services.AddDbContext<BdBrnetEstoqueContext>(options =>
    options.UseMySql(connString, ServerVersion.AutoDetect(connString))
);

/* banco de dados (MySQL)
builder.Services.AddDbContext<BdBrnetEstoqueContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);*/

// passwordhasher
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

// controladores 
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });


// Swagger para testar a API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Lei a seção Jwt do appsettings (Issuer, Audience)
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
                                   Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                                 )
    }
  );

// services
builder.Services.AddScoped<EquipamentoReservaService>();
builder.Services.AddScoped<ChecklistVeiculoService>();
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<MaterialUtilizadoService>();
builder.Services.AddScoped<MaterialUtilizadoService>();
builder.Services.AddScoped<ControleKmService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<CategoriaMaterialService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<VeiculoService>();



// roles
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("admin_estoque"));

    options.AddPolicy("TecnicoPolicy", policy =>
        policy.RequireRole("tecnico"));
});

//configuração do kestrel
//O Railway injeta a variável PORT. Caso não exista, define 8080.
builder.WebHost.ConfigureKestrel(options =>
{
    var portEnv = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    options.ListenAnyIP(int.Parse(portEnv));
});



// Configura CORS para permitir requisições do frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Middleware do Swagger
    app.UseSwagger();
    app.UseSwaggerUI();

// Ativa CORS
app.UseCors("AllowAll");

// Middleware HTTPS
//app.UseHttpsRedirection();
//No Railway, o tráfego HTTPS já é feito pelo proxy. Você pode desabilitar:

// Middleware de autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();



// Mapeia os endpoints dos controladores
app.MapControllers();

app.Run();
