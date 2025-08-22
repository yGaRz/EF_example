using EF_example.Db;
using EF_example.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_example.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddUser(string name, int age)
    {
        var user = new User { Name = name, Age = age};
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public List<User> GetUsers()
    {
        return _context.Users.Include(u => u.Department).ToList();
    }

    public User? GetUserById(int id)
    {
        return _context.Users.Include(u => u.Department).FirstOrDefault(u => u.Id == id);
    }

    public void DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
