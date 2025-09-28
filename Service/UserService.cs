using EF_example.Db.Models;
using EF_example.Repository;
using EF_example.Validation;

namespace EF_example.Service;

public class UserService
{
    private readonly IUserRepository _repo;
    private readonly IValidationStorage _validationStorage;
    public UserService(IUserRepository repo, IValidationStorage validationStorage)
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

        _repo.AddUser(name, age, email);
        return true;
    }
    public List<User> GetUsers() => _repo.GetUsers();
    public User? GetUserById(int id) => _repo.GetUserById(id);
    public void DeleteUser(int id) => _repo.DeleteUser(id);

    private bool IsEmailTaken(string email)
    {
        return _repo.GetUsers().Any(u => u.Email == email);
    }





}
