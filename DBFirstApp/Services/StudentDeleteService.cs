using Microsoft.EntityFrameworkCore;
using DBFirstApp.Data;
using DBFirstApp.Models;

namespace DBFirstApp.Services
{
    public static class StudentDeleteService
    {
        /// <summary>
        /// DELETE - Удаление студента по ID
        /// </summary>
        public static bool DeleteStudent(int studentId)
        {
            using var db = new StudentContext();
            var student = db.Students.FirstOrDefault(s => s.StudentId == studentId);
            
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// DELETE - Асинхронное удаление студента
        /// </summary>
        public static async Task<bool> DeleteStudentAsync(int studentId)
        {
            await using var db = new StudentContext();
            var student = await db.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
            
            if (student != null)
            {
                db.Students.Remove(student);
                await db.SaveChangesAsync();
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// DELETE - Удаление нескольких студентов
        /// </summary>
        public static void DeleteMultipleStudents(List<int> studentIds)
        {
            using var db = new StudentContext();
            var studentsToDelete = db.Students.Where(s => studentIds.Contains(s.StudentId)).ToList();
            
            if (studentsToDelete.Any())
            {
                db.Students.RemoveRange(studentsToDelete);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// DELETE - Удаление всех студентов
        /// </summary>
        public static void DeleteAllStudents()
        {
            using var db = new StudentContext();
            var allStudents = db.Students.ToList();
            db.Students.RemoveRange(allStudents);
            db.SaveChanges();
        }
    }
}