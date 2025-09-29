using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EF_example.Filters;

public class AuthorizationTokenFilter : IAuthorizationFilter
{
    private readonly IConfiguration _config;

    public AuthorizationTokenFilter(IConfiguration config)
    {
        _config = config;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var provided = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        var expected = "Bearer " + _config["Authorization:SecretToken"];
        if (provided != expected)
        {
            context.Result = new UnauthorizedObjectResult("Invalid token");
        }
    }
}

public class AuthorizationTokenAttribute : TypeFilterAttribute
{
    public AuthorizationTokenAttribute() : base(typeof(AuthorizationTokenFilter)) { }
}

