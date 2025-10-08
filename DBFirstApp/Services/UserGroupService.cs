using Microsoft.EntityFrameworkCore;
using DBFirstApp.Data;
using DBFirstApp.Models;

namespace DBFirstApp.Services
{
    public static class UserGroupService
    {
        /// <summary>
        /// Создание групп и пользователей
        /// </summary>
        public static async Task CreateUsersAndGroups()
        {
            await using var db = new ApplicationContext();
            
            // Создание групп
            var group1 = new Group { GroupName = "ИТ-101", Course = "Программирование" };
            var group2 = new Group { GroupName = "МАТ-201", Course = "Математика" };
            var group3 = new Group { GroupName = "ФИЗ-301", Course = "Физика" };
            
            // Создание пользователей и связывание с группами
            var user1 = new User { Name = "Иван Иванов", Age = 20, Group = group1 };
            var user2 = new User { Name = "Мария Петрова", Age = 21, Group = group1 };
            var user3 = new User { Name = "Алексей Сидоров", Age = 22, Group = group2 };
            var user4 = new User { Name = "Ольга Кузнецова", Age = 19, Group = group2 };
            var user5 = new User { Name = "Дмитрий Васильев", Age = 23, Group = group3 };
            
            db.Groups.AddRange(group1, group2, group3);
            db.Users.AddRange(user1, user2, user3, user4, user5);
            await db.SaveChangesAsync();
            
            Console.WriteLine("Созданы группы и пользователи:");
            await PrintGroupsWithUsers(db);
        }
        
        /// <summary>
        /// Добавление пользователя в существующую группу
        /// </summary>
        public static async Task AddUserToExistingGroup()
        {
            await using var db = new ApplicationContext();
            
            // Нахождение группы
            var group = await db.Groups.FirstOrDefaultAsync(g => g.GroupName == "ИТ-101");
            
            if (group != null)
            {
                var newUser = new User { 
                    Name = "Елена Николаева", 
                    Age = 24, 
                    Group = group  // Связь через навигационное свойство
                };
                
                db.Users.Add(newUser);
                await db.SaveChangesAsync();
                
                Console.WriteLine("\nДобавлен новый пользователь в группу ИТ-101:");
                await PrintUsersInGroup("ИТ-101");
            }
        }
        
        /// <summary>
        /// Получение всех групп с пользователями
        /// </summary>
        public static async Task PrintGroupsWithUsers(ApplicationContext db)
        {
            var groups = await db.Groups
                .Include(g => g.Users)  // Включает пользователей для каждой группы
                .ToListAsync();
                
            foreach (var group in groups)
            {
                Console.WriteLine($"\nГруппа: {group.GroupName} (Курс: {group.Course})");
                Console.WriteLine($"Количество пользователей: {group.Users.Count}");
                
                foreach (var user in group.Users)
                {
                    Console.WriteLine($"  - {user.Name} ({user.Age} лет)");
                }
            }
        }
        
        /// <summary>
        /// Получение пользователей в конкретной группе
        /// </summary>
        private static async Task PrintUsersInGroup(string groupName)
        {
            await using var db = new ApplicationContext();
            
            var users = await db.Users
                .Where(u => u.Group != null && u.Group.GroupName == groupName)
                .Include(u => u.Group)
                .ToListAsync();
                
            Console.WriteLine($"\nПользователи в группе {groupName}:");
            foreach (var user in users)
            {
                Console.WriteLine($"- {user.Name} ({user.Age} лет)");
            }
        }
        
        /// <summary>
        /// Поиск групп по курсу
        /// </summary>
        public static async Task FindGroupsByCourse(string course)
        {
            await using var db = new ApplicationContext();
            
            var groups = await db.Groups
                .Where(g => g.Course.Contains(course))
                .Include(g => g.Users)
                .ToListAsync();
                
            Console.WriteLine($"\nГруппы по курсу '{course}':");
            foreach (var group in groups)
            {
                Console.WriteLine($"- {group.GroupName}: {group.Users.Count} пользователей");
            }
        }
        
        /// <summary>
        /// Перемещение пользователя в другую группу
        /// </summary>
        public static async Task MoveUserToAnotherGroup(string userName, string newGroupName)
        {
            await using var db = new ApplicationContext();
            
            // Загружает пользователя с его текущей группой
            var user = await db.Users
                .Include(u => u.Group)  // включает группу
                .FirstOrDefaultAsync(u => u.Name == userName);
                
            var newGroup = await db.Groups.FirstOrDefaultAsync(g => g.GroupName == newGroupName);
            
            if (user != null && newGroup != null)
            {
                user.Group = newGroup;  
                await db.SaveChangesAsync();
                
                Console.WriteLine($"\nПользователь {userName} перемещен в группу {newGroupName}");
            }
        }
        
        /// <summary>
        /// Создание группы с пользователями через навигационное свойство
        /// </summary>
        public static async Task CreateGroupWithUsers()
        {
            await using var db = new ApplicationContext();
            
            // Создает пользователей
            var user1 = new User { Name = "Сергей Орлов", Age = 25 };
            var user2 = new User { Name = "Наталья Воробьева", Age = 22 };
            
            // Создает группу и добавляет пользователей через навигационное свойство
            var newGroup = new Group 
            { 
                GroupName = "ХИМ-401", 
                Course = "Химия",
                Users = { user1, user2 }  // Добавляет пользователей в группу
            };
            
            db.Groups.Add(newGroup);
            await db.SaveChangesAsync();
            
            Console.WriteLine("\nСоздана новая группа с пользователями:");
            await PrintUsersInGroup("ХИМ-401");
        }
    }
}