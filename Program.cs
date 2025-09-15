// Program.cs
using EF_example.Db;
using EF_example.Repository;
using EF_example.Service;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DepartmentService>();

builder.Services.AddControllers();
//builder.Services.AddControllers()
//    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserDtoValidator>());

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001); // слушать на порту 5001 на всех IP
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate(); 
}

app.UseRouting();

app.MapControllers();

app.Run();
