using EF_example.Db.Models;
using EF_example.Repository;

namespace EF_example.Service;

public class DepartmentService
{
    private readonly IDepartmentRepository _repo;
    public DepartmentService(IDepartmentRepository repo) { _repo = repo; }

    public void AddDepartment(string name) => _repo.AddDepartment(name);
    public List<Department> GetDepartments() => _repo.GetDepartments();
    public void AddUserToDepartment(int userId, int deptId) => _repo.AddUserToDepartment(userId, deptId);
}
