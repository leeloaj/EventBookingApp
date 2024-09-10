using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUsersService usersService) : Controller
{
    /// <summary>
    /// Admin log in
    /// </summary>
    /// <param name="loginRequest">Login request</param>
    /// <returns>Token and email</returns>
    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult AdminLogin([FromBody] LoginRequest loginRequest)
    {
        try
        {
            var loginResponse = usersService.AdminLogin(loginRequest);
            return Ok(loginResponse);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}