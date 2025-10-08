using DBFirstApp.Data;
using DBFirstApp.Models;
using Microsoft.EntityFrameworkCore;
using DBFirstApp.Services;

namespace DBFirstApp;

class Program
{
    static async Task Main()
    {
        Console.WriteLine(" CRUD операции со Students\n");

        try
        {
            // Очистка базы данных перед началом (для тестирования)
            Console.WriteLine("0. Подготовка базы данных:");
            using var db = new StudentContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            Console.WriteLine("База данных очищена и готова к работе");

            // CREATE - Создание тестовых данных
            Console.WriteLine("1. CREATE - Добавление студентов:");
            var sampleStudents = StudentCreateService.GenerateSampleStudents();
            await StudentCreateService.CreateMultipleStudentsAsync(sampleStudents);
            Console.WriteLine($"Добавлено тестовых студентов: {sampleStudents.Count}");

            // READ - Чтение данных
            Console.WriteLine("\n2. READ - Получение всех студентов:");
            var allStudents = await StudentReadService.GetAllStudentsAsync();
            PrintStudents(allStudents);

            // UPDATE - Обновление данных
            Console.WriteLine("\n3. UPDATE - Обновление первого студента:");
            if (allStudents.Any())
            {
                var updated = await StudentUpdateService.UpdateStudentAsync(
                    studentId: allStudents[0].StudentId,
                    firstName: "Екатерина",
                    lastName: "Волкова",
                    email: "ekaterina.volkova@university.ru"
                );
                Console.WriteLine(updated ? "Студент обновлен!" : "Студент не найден!");
            }

            // READ - Проверка обновления
            Console.WriteLine("\n4. READ - Данные после обновления:");
            var updatedStudents = StudentReadService.GetAllStudents();
            PrintStudents(updatedStudents);

            // DELETE - Удаление данных
            Console.WriteLine("\n5. DELETE - Удаление последнего студента:");
            if (updatedStudents.Any())
            {
                var lastStudentId = updatedStudents.Last().StudentId;
                var deleted = await StudentDeleteService.DeleteStudentAsync(lastStudentId);
                Console.WriteLine(deleted ? "Студент удален!" : "Студент не найден!");
            }

            // Финальное состояние
            Console.WriteLine("\n6. Финальное состояние базы данных:");
            var finalStudents = await StudentReadService.GetAllStudentsAsync();
            PrintStudents(finalStudents);

            Console.WriteLine("\nГотово! База данных создана с таблицей Students согласно требованиям.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
            Console.WriteLine($"Подробности: {ex.InnerException?.Message}");
        }
    }

    static void PrintStudents(List<Student> students)
    {
        if (!students.Any())
        {
            Console.WriteLine("Студентов не найдено");
            return;
        }

        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.StudentId}, {student.FirstName} {student.LastName}, " +
                            $"Дата рождения: {student.DateOfBirth:yyyy-MM-dd}, " +
                            $"Возраст: {student.Age}, Email: {student.Email}");
        }
        Console.WriteLine($"Всего студентов: {students.Count}");
    }
}