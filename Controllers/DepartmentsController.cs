using EF_example.Db;
using EF_example.Db.Models;
using EF_example.Service;
using Microsoft.AspNetCore.Mvc;

namespace EF_example.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly DepartmentService _service;
    public DepartmentsController(DepartmentService service) { _service = service; }

    [HttpPost]
    public IActionResult Create([FromBody] AddDepartmentDto dto)
    {
        _service.AddDepartment(dto.Name);
        return Ok(new { Message = "✅ Отдел добавлен" });
    }

    [HttpGet]
    public ActionResult<List<Department>> GetAll() => _service.GetDepartments();

    [HttpPost("assign")]
    public IActionResult Assign([FromBody] AddUserToDepartmentDto dto)
    {
        _service.AddUserToDepartment(dto.UserId, dto.DepartmentId);
        return Ok(new { Message = "✅ Пользователь назначен в отдел" });
    }
}

