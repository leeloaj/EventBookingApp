using Services.Dtos;

namespace Services.Interfaces;

public interface IUsersService
{
    LoginResponse AdminLogin(LoginRequest loginRequest);
}