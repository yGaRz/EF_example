﻿namespace EF_example.Db.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }=null!;
    public ICollection<User> Users { get; set; } = new List<User>();
}
