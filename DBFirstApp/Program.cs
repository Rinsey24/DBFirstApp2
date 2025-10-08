using DBFirstApp.Data;
using DBFirstApp.Models;
using Microsoft.EntityFrameworkCore;
using DBFirstApp.Services;

namespace DBFirstApp;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Связи между сущностями: Пользователи и Группы\n");
        
        try
        {
            Console.WriteLine("0. Подготовка базы данных:");
            using var db = new ApplicationContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            Console.WriteLine("1. Создание пользователей и групп:");
            await UserGroupService.CreateUsersAndGroups();
            
            Console.WriteLine("\n2. Добавление пользователя в существующую группу:");
            await UserGroupService.AddUserToExistingGroup();
            
            Console.WriteLine("\n3. Поиск групп по курсу:");
            await UserGroupService.FindGroupsByCourse("Программирование");
            
            Console.WriteLine("\n4. Перемещение пользователя:");
            await UserGroupService.MoveUserToAnotherGroup("Мария Петрова", "МАТ-201");
            
            Console.WriteLine("\nФинальное состояние базы");
            await UserGroupService.PrintGroupsWithUsers(db);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}