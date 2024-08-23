using FluentValidation;
using Infraestrutura.Data;
using Infraestrutura.Repositories;
using Infraestrutura.Repositorys;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Servicos.Interfaces;
using Servicos.Pipelines;
using Servicos.RepositoryInterfaces;
using Servicos.Services;
using Servicos.Services.ServiceInterfaces;
using Servicos.UseCases.FilmeUseCases;
using Servicos.UseCases.UserUseCases;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer Auth", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Put **_ONLY_** your JWT Bearer token inthe text input below.\r\n\r\nExample: \"12345abcdef\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer Auth"
            }
        },
        Array.Empty<string>()
    } }
    );
});

var keyEmString = builder.Configuration.GetValue<string>("ENCRYPT_TOKEN_SECRET_KEY")!;

var key = Encoding.ASCII.GetBytes(keyEmString);

builder.Services.Configure<TokenService.Options>(tokenServiceOptions =>
{
    tokenServiceOptions.Secret = keyEmString;
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    // Middleware
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FilmeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ServerConnection")));

builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IValidator<AdicionarUsuarioRequest>, IAdicionarUsuarioRequest.Validator<AdicionarUsuarioRequest>>();
builder.Services.AddScoped<IValidator<AdicionarAdministradorRequest>, IAdicionarUsuarioRequest.Validator<AdicionarAdministradorRequest>>();
builder.Services.AddValidatorsFromAssemblyContaining<AdicionarFilmeRequest.Validator>();

builder.Services.AddLogging();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(IAdicionarUsuarioRequest).Assembly);

    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestFormatValidatorBehavior<,>));
});

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
