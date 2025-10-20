using Microsoft.AspNetCore.Mvc;
using EF_example.Service;
using EF_example.Controllers;
using EF_example.Validation;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly UserService _userService;
    public AuthController(IValidationStorage validationStorage, UserService userService) : base(validationStorage)
    {   
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var token = _userService.AuthenticateUser(dto.Login, dto.Password);
        if (token == null)
            return Unauthorized("Invalid credentials");

        return Ok(new { Token = token });
    }
}
public class LoginDto
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}
