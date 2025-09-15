namespace EF_example.Db;

public class AddDepartmentDto
{
    public string Name { get; set; }
}

public class AddUserToDepartmentDto
{
    public int UserId { get; set; }
    public int DepartmentId { get; set; }
}
