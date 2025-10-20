using Microsoft.AspNetCore.Mvc;
using EF_example.Service;

[ApiController]
[Route("api/[controller]")]
public class WorkController : ControllerBase
{
    private readonly UserService _userService;

    public WorkController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("do")]
    public IActionResult DoWork()
    {
        var token = Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
        if (string.IsNullOrWhiteSpace(token) || !TokenStore.TryGetUserId(token, out var userId))
            return Unauthorized("Invalid or missing token");

        var user = _userService.GetUserById(userId);
        if (user == null) return Unauthorized("User not found");

        return Ok(new { Message = $"Hello {user.Name}, your department is {user.Department?.Name ?? "None"}." });
    }
}
