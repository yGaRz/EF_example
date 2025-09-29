using EF_example.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EF_example.Controllers.TokensLogic;

[ApiController]
[Route("api/[controller]")]
[RequireHttps]
[AuthorizationToken]
public class SecureController : ControllerBase
{
    [HttpGet]
    public IActionResult DoWork()
    {
        //Do somwthing work
        return Ok("Access granted!");
    }
}
