using Microsoft.EntityFrameworkCore;
using DBFirstApp.Data;
using DBFirstApp.Models;

namespace DBFirstApp.Services
{
    public static class StudentCreateService
    {
        /// <summary>
        /// CREATE - Добавление одного студента
        /// </summary>
        public static int CreateStudent(Student student)
        {
            using var db = new StudentContext();
            db.Students.Add(student);
            db.SaveChanges();
            return student.StudentId;
        }

        /// <summary>
        /// CREATE - Асинхронное добавление одного студента
        /// </summary>
        public static async Task<int> CreateStudentAsync(Student student)
        {
            await using var db = new StudentContext();
            await db.Students.AddAsync(student);
            await db.SaveChangesAsync();
            return student.StudentId;
        }

        /// <summary>
        /// CREATE - Добавление нескольких студентов
        /// </summary>
        public static void CreateMultipleStudents(List<Student> students)
        {
            using var db = new StudentContext();
            db.Students.AddRange(students);
            db.SaveChanges();
        }

        /// <summary>
        /// CREATE - Асинхронное добавление нескольких студентов
        /// </summary>
        public static async Task CreateMultipleStudentsAsync(List<Student> students)
        {
            await using var db = new StudentContext();
            await db.Students.AddRangeAsync(students);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// Генерация тестовых данных 
        /// </summary>
        public static List<Student> GenerateSampleStudents()
        {
            return
            [
                new Student
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    DateOfBirth = new DateTime(2000, 5, 15),
                    Email = "ivan.ivanov@university.ru"
                },

                new Student
                {
                    FirstName = "Мария",
                    LastName = "Петрова",
                    DateOfBirth = new DateTime(2001, 8, 22),
                    Email = "maria.petrova@university.ru"
                },

                new Student
                {
                    FirstName = "Дмитрий",
                    LastName = "Сидоров",
                    DateOfBirth = new DateTime(1999, 12, 10),
                    Email = "dmitry.sidorov@university.ru"
                },

                new Student
                {
                    FirstName = "Анна",
                    LastName = "Кузнецова",
                    DateOfBirth = new DateTime(2002, 3, 30),
                    Email = "anna.kuznetsova@university.ru"
                }
            ];
        }
    }
}