namespace EF_example.Db;

public class CreateUserDto
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Email { get; set; } = null!;
}
