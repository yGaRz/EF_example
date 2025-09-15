using Microsoft.EntityFrameworkCore;    

namespace EF_example.Db.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string Email { get; set; } = null!;
    public int? DepartmentId { get; set; }
    public virtual Department Department { get; set; }
}
