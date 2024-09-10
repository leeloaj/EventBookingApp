namespace Services.Dtos;

public class LoginResponse
{
    public required string Token { get; set; }
    public required string Email { get; set; }
}