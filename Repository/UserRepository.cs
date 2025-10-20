using EF_example.Db;
using EF_example.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_example.Repository;

public class UserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<User> GetUsers()
    {
        return _context.Users.Include(u => u.Department).ToList();
    }

    public User? GetUserById(int id)
    {
        return _context.Users.Include(u => u.Department).FirstOrDefault(u => u.Id == id);
    }
    public User? GetUserByLogin(string login)
    {
        return _context.Users.Include(u => u.Department).FirstOrDefault(u => u.Login == login);
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

    public async Task AddUserAsync(string name, int age, string email, string login, string passwordHash)
    {
        var user = new User { Name = name, Age = age, Email = email, Login = login, PasswordHash = passwordHash };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task AddUserAsync(string name, int age, string email)
    {
        var user = new User { Name = name, Age = age, Email = email };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }


}
