using AutoMapper;
using Locacao.Api.Data;
using Locacao.Api.Data.Interfaces;
using Locacao.Api.Data.Repositories;
using Locacao.Api.Models.Mapping;
using Locacao.Api.Services;
using Locacao.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        p => p.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(180), errorNumbersToAdd: null)), ServiceLifetime.Scoped);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void InjecaoDepedenciaDosServices(IServiceCollection services)
{
    services.AddScoped<IAutenticacaoServices, AutenticacaoServices>();
    services.AddScoped<IProdutoServices, ProdutoServices>();
}

void InjecaoDepedenciaDosRepositories(IServiceCollection services)
{
    services.AddScoped<IProdutoRepository, ProdutoRepository>();
}