using EF_example.Service;
using EF_example.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EF_example.Controllers.Tokens_JWT;

[ApiController]
[Route("api/jwt")]
public class AuthController : BaseController
{
    private readonly UserService _userService;
    private readonly IConfiguration _config;

    public AuthController(IValidationStorage validationStorage, UserService userService, IConfiguration config)
        : base(validationStorage)
    {
        _userService = userService;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        var user = _userService.AuthenticateUser(dto.Login, dto.Password);
        if (user == null)
        {
            return BadRequest(new { message = "Invalid login or password" });
        }

        var token = GenerateJwtToken(dto.Login);
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(string login)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, login)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "DefaultSuperSecretKey"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"] ?? "EF_example",
            audience: _config["Jwt:Audience"] ?? "EF_exampleClient",
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(55),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
