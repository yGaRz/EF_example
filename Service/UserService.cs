using EF_example.Db.Models;
using EF_example.Repository;
using EF_example.Validation;
using FluentValidation.Validators;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace EF_example.Service;

public class UserService
{
    private readonly UserRepository _repo;
    private readonly IValidationStorage _validationStorage;
    public UserService(UserRepository repo, IValidationStorage validationStorage)
    {
        _repo = repo;
        _validationStorage = validationStorage;
    }

    //public void AddUser(string name, int age, string email) 
    //{ 
    //    _repo.AddUser(name, age, email); 
    //}

    public async Task<bool> AddUserWithBusinessValidation(string name, int age, string email, CancellationToken token)
    {
        //вынеосим в отдельный метод
        var exists = _repo.GetUsers().Any(u => u.Email == email);
        if (exists)
        {
            _validationStorage.AddError(ErrorCode.EmailIsCreated, "Email already registered");
        }


        if (!_validationStorage.IsValid)
        {
            return false;
        }

        await _repo.AddUserAsync(name, age, email);
        return true;
    }
    public List<User> GetUsers() => _repo.GetUsers();
    public User? GetUserById(int id) => _repo.GetUserById(id);
    public void DeleteUser(int id) => _repo.DeleteUser(id);

    public string? AuthenticateUser(string login, string password)
    {
        var user = _repo.GetUserByLogin(login);
        if (user == null || !VerifyPassword(password, user.PasswordHash))
        {
            _validationStorage.AddError(ErrorCode.InvalidCredentials, "Invalid login or password");
            return null;
        }

        var token = Guid.NewGuid().ToString();
        TokenStore.Add(token, user.Id);

        return token;
    }





    //Регистрация пользователя
    public async Task<bool> RegisterUser(string name, int age, string email, string login, string password, CancellationToken token = default)
    {
        // Проверка уникальности email
        if (IsEmailTaken(email))
        {
            _validationStorage.AddError(ErrorCode.EmailIsCreated, "Email already registered");
        }

        // Проверка уникальности login
        if (IsLoginTaken(login))
        {
            _validationStorage.AddError(ErrorCode.LoginIsCreated, "Login already taken");
        }

        if (!_validationStorage.IsValid)
        {
            return false;
        }

        // Хэшируем пароль
        var passwordHash = HashPassword(password);

        await _repo.AddUserAsync(name, age, email, login, passwordHash);
        return true;
    }
    private bool IsEmailTaken(string email)
    {
        return _repo.GetUsers().Any(u => u.Email == email);
    }
    private bool IsLoginTaken(string login)
    {
        return _repo.GetUsers().Any(u => u.Login == login);
    }
    private string HashPassword(string password, string salt="superSecretSeed")
    {
        using (var sha256 = SHA256.Create())
        {
            var combined = password + salt;
            var bytes = Encoding.UTF8.GetBytes(combined);
            var hashBytes = sha256.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }

    private bool VerifyPassword(string password, string storedHash, string salt = "superSecretSeed")
    {
        using (var sha256 = SHA256.Create())
        {
            var combined = password + salt;
            var bytes = Encoding.UTF8.GetBytes(combined);
            var hashBytes = sha256.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (var b in hashBytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString() == storedHash;
        }
    }
}
