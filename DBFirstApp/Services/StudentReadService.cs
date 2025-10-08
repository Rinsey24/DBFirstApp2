using Microsoft.EntityFrameworkCore;
using DBFirstApp.Data;
using DBFirstApp.Models;

namespace DBFirstApp.Services
{
    public static class StudentReadService
    {
        /// <summary>
        /// READ - Получение всех студентов
        /// </summary>
        public static List<Student> GetAllStudents()
        {
            using var db = new StudentContext();
            return db.Students.ToList();
        }

        /// <summary>
        /// READ - Асинхронное получение всех студентов
        /// </summary>
        public static async Task<List<Student>> GetAllStudentsAsync()
        {
            await using var db = new StudentContext();
            return await db.Students.ToListAsync();
        }

        /// <summary>
        /// READ - Получение студента по ID
        /// </summary>
        public static Student? GetStudentById(int studentId)
        {
            using var db = new StudentContext();
            return db.Students.FirstOrDefault(s => s.StudentId == studentId);
        }

        /// <summary>
        /// READ - Асинхронное получение студента по ID
        /// </summary>
        public static async Task<Student?> GetStudentByIdAsync(int studentId)
        {
            await using var db = new StudentContext();
            return await db.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        /// <summary>
        /// READ - Поиск студентов по имени
        /// </summary>
        public static List<Student> GetStudentsByFirstName(string firstName)
        {
            using var db = new StudentContext();
            return db.Students
                    .Where(s => s.FirstName.Contains(firstName))
                    .ToList();
        }

        /// <summary>
        /// READ - Поиск студентов по фамилии
        /// </summary>
        public static List<Student> GetStudentsByLastName(string lastName)
        {
            using var db = new StudentContext();
            return db.Students
                    .Where(s => s.LastName.Contains(lastName))
                    .ToList();
        }

        /// <summary>
        /// READ - Получение студентов по email
        /// </summary>
        public static Student? GetStudentByEmail(string email)
        {
            using var db = new StudentContext();
            return db.Students.FirstOrDefault(s => s.Email == email);
        }

        /// <summary>
        /// READ - Получение студентов с пагинацией
        /// </summary>
        public static List<Student> GetStudentsPaged(int pageNumber, int pageSize)
        {
            using var db = new StudentContext();
            return db.Students
                    .OrderBy(s => s.StudentId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        /// <summary>
        /// READ - Получение студентов по диапазону дат рождения
        /// </summary>
        public static List<Student> GetStudentsByBirthDateRange(DateTime startDate, DateTime endDate)
        {
            using var db = new StudentContext();
            return db.Students
                    .Where(s => s.DateOfBirth >= startDate && s.DateOfBirth <= endDate)
                    .ToList();
        }
    }
}