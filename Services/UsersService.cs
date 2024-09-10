using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Dtos;
using Services.Interfaces;
using Services.Settings;
using WebApi.Settings;

namespace Services;

public class UsersService(
    IOptions<AdminUser> adminUserSettings,
    IOptions<JwtSettings> jwtSettings) : IUsersService
{
    public LoginResponse AdminLogin(LoginRequest loginRequest)
    {
        if (loginRequest.Email != adminUserSettings.Value.Email ||
            loginRequest.Password != adminUserSettings.Value.Password)
        {
            throw new ArgumentException("Email või salasõna on vale!");
        }

        return new LoginResponse
        {
            Email = loginRequest.Email,
            Token = GenerateJwtToken(loginRequest.Email)
        };
    }
    
    private string GenerateJwtToken(string email)
    {
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, email),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings.Value.ExpirationInMinutes)),
            Issuer = jwtSettings.Value.Issuer,
            Audience = jwtSettings.Value.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}