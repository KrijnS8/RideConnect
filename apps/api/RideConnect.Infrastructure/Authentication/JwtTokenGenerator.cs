using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RideConnect.Application.Features.Authentication.Interfaces;
using RideConnect.Domain.Entities;

namespace RideConnect.Infrastructure.Authentication;

public sealed class JwtTokenGenerator(IOptions<JwtConfig> options) : IJwtTokenGenerator
{
    private readonly JwtConfig _config = options.Value;
    
    public IJwtTokenGenerator.JwtToken Generate(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config.Secret));
        
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config.Issuer,
            audience: _config.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_config.ExpirationInMinutes),
            signingCredentials: credentials);
        
        return new IJwtTokenGenerator.JwtToken(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
    }
}
