using EF_example.Db;
using EF_example.Db.Models;
using EF_example.Service;
using Microsoft.AspNetCore.Mvc;

namespace EF_example.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _service;
    public UsersController(UserService service) { _service = service; }

    [HttpPost("adduser")]
    public IActionResult Create([FromBody] CreateUserDto dto)
    {
        _service.AddUser(dto.Name, dto.Age, dto.Email);
        return Ok(new { Message = "✅ Пользователь добавлен" });
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

