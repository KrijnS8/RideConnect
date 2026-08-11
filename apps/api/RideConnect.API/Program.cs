using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RideConnect.Application.Features.Authentication.Interfaces;
using RideConnect.Application.Features.Authentication.Services;
using RideConnect.Application.Features.Authentication.Validators;
using RideConnect.Application.Features.Users.Interfaces;
using RideConnect.Application.Features.Users.Services;
using RideConnect.Application.Persistence;
using RideConnect.Infrastructure.Authentication;
using RideConnect.Infrastructure.Persistence;
using RideConnect.Infrastructure.Repositories;

// Create builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme()
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "JWT Authorization header using the Bearer scheme.",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

// Add database context
builder.Services.AddDbContext<RideConnectDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    // TODO: remove for production, only for testing
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    
    var jwtSection = builder.Configuration.GetSection(JwtConfig.SectionName);
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSection["Secret"]!)),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
builder.Services.Configure<JwtConfig>(
    builder.Configuration.GetSection(JwtConfig.SectionName));

//  Register services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();