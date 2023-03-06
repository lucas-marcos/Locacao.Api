using System.Globalization;
using System.Text;
using AutoMapper;
using Locacao.Api.Configuration;
using Locacao.Api.Data;
using Locacao.Api.Data.Interfaces;
using Locacao.Api.Data.Repositories;
using Locacao.Api.Models;
using Locacao.Api.Models.Mapping;
using Locacao.Api.Services;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        p => p.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(180), errorNumbersToAdd: null)), ServiceLifetime.Scoped);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurarAutenticacao(builder.Services);

InjecaoDepedenciaDosServices(builder.Services);
InjecaoDepedenciaDosRepositories(builder.Services);

var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin 
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void InjecaoDepedenciaDosServices(IServiceCollection services)
{
    services.AddScoped<IAutenticacaoServices, AutenticacaoServices>();
    services.AddScoped<IProdutoServices, ProdutoServices>();
    services.AddScoped<IEstoqueServices, EstoqueServices>();
    services.AddScoped<ITokenServices, TokenServices>();
    services.AddScoped<ILocacaoServices, LocacaoServices>();
}

void InjecaoDepedenciaDosRepositories(IServiceCollection services)
{
    services.AddScoped<IProdutoRepository, ProdutoRepository>();
    services.AddScoped<ILocacaoRepository, LocacaoRepository>();
}

void ConfigurarAutenticacao(IServiceCollection services)
{
    services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 0;
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;

            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    var key = Encoding.ASCII.GetBytes(Settings.Secret);

    services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
           
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
}