using EF_example.Db;
using EF_example.Db.Models;
using EF_example.Service;
using EF_example.Validation;
using Microsoft.AspNetCore.Mvc;

namespace EF_example.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : BaseController
{
    private readonly UserService _service;

    public UsersController(IValidationStorage validationStorage, UserService service):base(validationStorage)
    {
        _service = service;
    }

    [HttpPost("adduser")]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto, CancellationToken token)
    {
        return await HandleRequestAsync(async token => await _service.AddUserWithBusinessValidation(dto.Name, dto.Age, dto.Email, token), token);
    }

    [HttpGet("getall")]
    public ActionResult<List<User>> GetAll() => _service.GetUsers();

    [HttpGet("{id}")]
    public ActionResult<User?> GetById(int id)
    {
        var user = _service.GetUserById(id);
        if (user != null) return Ok(user);
        return NotFound(new { Message = "Пользователь не найден" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.DeleteUser(id);
        return Ok(new { Message = "✅ Пользователь удален" });
    }
}

