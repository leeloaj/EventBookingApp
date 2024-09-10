using Services.Dtos;

namespace Services.Interfaces;

/// <summary>
/// Service for users
/// </summary>
public interface IUsersService
{
    /// <summary>
    /// Log in admins
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    LoginResponse AdminLogin(LoginRequest loginRequest);
}