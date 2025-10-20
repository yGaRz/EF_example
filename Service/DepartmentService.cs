using EF_example.Db.Models;
using EF_example.Repository;

namespace EF_example.Service;

public class DepartmentService
{
    private readonly DepartmentRepository _repo;
    public DepartmentService(DepartmentRepository repo) { _repo = repo; }

    public void AddDepartment(string name) => _repo.AddDepartment(name);
    public List<Department> GetDepartments() => _repo.GetDepartments();
    public void AddUserToDepartment(int userId, int deptId) => _repo.AddUserToDepartment(userId, deptId);
}
