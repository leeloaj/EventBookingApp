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

/// <summary>
/// Service for log in
/// </summary>
/// <param name="adminUserSettings"></param>
/// <param name="jwtSettings"></param>
public class UsersService(
    
    IOptions<AdminUser> adminUserSettings,
    IOptions<JwtSettings> jwtSettings) : IUsersService
{
    /// <summary>
    /// Log in for admin
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
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
    
    /// <summary>
    /// Generating token for admin user to log in
    /// </summary>
    /// <param name="email"></param>
    /// <returns>token</returns>
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