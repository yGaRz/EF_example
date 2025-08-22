using EF_example.Db;
using EF_example.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_example.Repository;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddDepartment(string name)
    {
        var dept = new Department { Name = name };
        _context.Departments.Add(dept);
        _context.SaveChanges();
    }

    public List<Department> GetDepartments()
    {
        return _context.Departments.Include(d => d.Users).ToList();
    }

    public void AddUserToDepartment(int userId, int departmentId)
    {
        var user = _context.Users.Find(userId);
        var dept = _context.Departments.Find(departmentId);
        if (user != null && dept != null)
        {
            user.DepartmentId = departmentId;
            _context.SaveChanges();
        }
    }
}
