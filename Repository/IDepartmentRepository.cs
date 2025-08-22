using EF_example.Db.Models;

namespace EF_example.Repository;

public interface IDepartmentRepository
{
    void AddDepartment(string name);
    List<Department> GetDepartments();
    void AddUserToDepartment(int userId, int departmentId);
}