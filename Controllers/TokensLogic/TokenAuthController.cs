using Microsoft.AspNetCore.Mvc;

namespace EF_example.Controllers.TokensLogic;

[ApiController]
[Route("api/[controller]")]//api/TokenAuth")
public class TokenAuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public TokenAuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("secure_demo")]
    public IActionResult SecureDemoEndpoint()
    {
        var token = Request.Headers["Authorization"].FirstOrDefault();
        var expected = "Bearer " + _config["Authorization:SecretToken"];
        if (token != expected)
            return Unauthorized("Invalid token");

        return Ok("secure_demo granted!");
    }

    [HttpGet("secure_demo2")]
    public IActionResult SecureDemo2Endpoint()
    {
        var token = Request.Headers["Authorization"].FirstOrDefault();
        var expected = "Bearer " + _config["Authorization:SecretToken"];
        if (token != expected)
            return Unauthorized("Invalid token");

        return Ok("secure_demo2 granted!");
    }
    [HttpGet("non_secure_demo")]
    public IActionResult NonSecureDemoEndpoint()
    {
        return Ok("non_secure_demo granted!");
    }
}