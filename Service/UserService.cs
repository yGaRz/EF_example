using EF_example.Db.Models;
using EF_example.Repository;

namespace EF_example.Service;

public class UserService
{
    private readonly IUserRepository _repo;
    public UserService(IUserRepository repo) { _repo = repo; }

    public void AddUser(string name, int age, string email) 
    { 
        _repo.AddUser(name, age, email); 
    }
    public List<User> GetUsers() => _repo.GetUsers();
    public User? GetUserById(int id) => _repo.GetUserById(id);
    public void DeleteUser(int id) => _repo.DeleteUser(id);
}
