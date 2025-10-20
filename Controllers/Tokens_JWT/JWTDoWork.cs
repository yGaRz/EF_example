using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EF_example.Controllers.Tokens_JWT;

[ApiController]
[Route("api/jwt/")]
public class DoWorkController : ControllerBase
{
    [HttpGet("dowork")]
    [Authorize] // Требует аутентификации
    public IActionResult DoWork()
    {
        // Получаем имя пользователя из claims
        var username = User.FindFirst(ClaimTypes.Name)?.Value
            ?? User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

        // Получаем другие claims
        var issuer = User.FindFirst("iss")?.Value;
        var audience = User.FindFirst("aud")?.Value;

        return Ok(new
        {
            Message = $"Hello, {username}! DoWork executed successfully.",
            User = username,
            Issuer = issuer,
            Audience = audience,
            Timestamp = DateTime.UtcNow,
            Data = new { WorkId = 1, Status = "Completed", Description = "Some important work" }
        });
    }

    [HttpPost]
    [Authorize]
    public IActionResult PostWork([FromBody] WorkRequest request)
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value
            ?? User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

        // Проверяем права пользователя или другую логику
        if (string.IsNullOrEmpty(request?.WorkName))
        {
            return BadRequest(new { Error = "WorkName is required" });
        }

        return Ok(new
        {
            Message = $"Work '{request.WorkName}' created by {username}",
            WorkId = Guid.NewGuid(),
            CreatedBy = username,
            CreatedAt = DateTime.UtcNow,
            WorkName = request.WorkName
        });
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")] // Требует роль Admin
    public IActionResult AdminWork()
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        return Ok(new { Message = $"Admin work executed by {username}" });
    }

    [HttpGet("profile")]
    [Authorize]
    public IActionResult GetUserProfile()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

        return Ok(new
        {
            Username = User.Identity.Name,
            Claims = claims,
            IsAuthenticated = User.Identity.IsAuthenticated
        });
    }
}

public class WorkRequest
{
    public string WorkName { get; set; }
    public string Description { get; set; }
}
