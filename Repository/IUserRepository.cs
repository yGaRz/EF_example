using EF_example.Db.Models;

namespace EF_example.Repository;

public interface IUserRepository
{
    void AddUser(string name, int age);
    List<User> GetUsers();
    User? GetUserById(int id);
    void DeleteUser(int id);
}
