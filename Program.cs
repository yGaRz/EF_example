// Program.cs
using EF_example.Db;
using EF_example.Repository;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

    })
    .Build();

var userRepo = host.Services.GetRequiredService<IUserRepository>();
var departmentRepo = host.Services.GetRequiredService<IDepartmentRepository>();

while (true)
{
    Console.WriteLine("Меню:");
    Console.WriteLine("1. Добавить пользователя");
    Console.WriteLine("2. Показать всех пользователей");
    Console.WriteLine("3. Найти пользователя по Id");
    Console.WriteLine("4. Удалить пользователя");
    Console.WriteLine("5. Добавить отдел");
    Console.WriteLine("6. Показать отделы");
    Console.WriteLine("7. Назначить пользователя в отдел");
    Console.WriteLine("8. Выход");
    Console.Write("Выбор: ");

    var choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            Console.Write("Имя: ");
            var name = Console.ReadLine();
            Console.Write("Возраст: ");
            var age = int.Parse(Console.ReadLine() ?? "0");
            userRepo.AddUser(name, age);
            Console.WriteLine("✅ Пользователь добавлен.\n");
            break;

        case "2":
            var users = userRepo.GetUsers();
            foreach (var u in users)
            {
                var deptName = u.Department != null ? u.Department.Name : "(нет отдела)";
                Console.WriteLine($"Id:{u.Id} Имя:{u.Name}, Возраст:{u.Age}, Отдел:{deptName ?? "Не задан"}");
            }
            Console.WriteLine();
            break;

        case "3":
            Console.Write("Введите Id: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            var user = userRepo.GetUserById(id);
            if (user != null)
            {
                var deptName = user.Department != null ? user.Department.Name : "(нет отдела)";
                Console.WriteLine($"Id:{user.Id} Имя:{user.Name}, Возраст:{user.Age}, Отдел:{deptName ?? "Не задан"}");
            }
            else
                Console.WriteLine("Пользователь не найден");
            Console.WriteLine();
            break;

        case "4":
            {
                Console.Write("Введите Id для удаления: ");
                var delId = int.Parse(Console.ReadLine() ?? "0");
                userRepo.DeleteUser(delId);
                Console.WriteLine("✅ Пользователь удален.\n");
                break;
            }

        case "5":
            {
                Console.Write("Название отдела: ");
                var deptName = Console.ReadLine();
                departmentRepo.AddDepartment(deptName);
                Console.WriteLine("✅ Отдел добавлен.\n");
                break;
            }
        case "6":
            var depts = departmentRepo.GetDepartments();
            foreach (var d in depts)
                Console.WriteLine($"Id:{d.Id} Название:{d.Name} Пользователей:{d.Users.Count}");
            Console.WriteLine();
            break;

        case "7":
            Console.Write("Id пользователя: ");
            var userId = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Id отдела: ");
            var deptId = int.Parse(Console.ReadLine() ?? "0");
            departmentRepo.AddUserToDepartment(userId, deptId);
            Console.WriteLine("✅ Пользователь назначен в отдел.\n");
            break;

        case "8":
            return;

        default:
            Console.WriteLine("Неверный ввод!\n");
            break;
    }
}
