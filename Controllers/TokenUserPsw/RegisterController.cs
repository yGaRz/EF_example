using EF_example.Models;
using EF_example.Service;
using EF_example.Validation;
using Microsoft.AspNetCore.Mvc;

namespace EF_example.Controllers.TokenUserPsw;

[ApiController]
[Route("api/[controller]")]
public class RegisterController : BaseController
{
    private readonly UserService _userService;
    public RegisterController(IValidationStorage validationStorage, UserService service) : base(validationStorage)
    {
        _userService = service;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken token)
    {
        return await HandleRequestAsync(async token => await _userService.RegisterUser(dto.Name, dto.Age, dto.Email, dto.Login, dto.Password), token);
    }
}
