using Microsoft.EntityFrameworkCore;
using DBFirstApp.Data;
using DBFirstApp.Models;

namespace DBFirstApp.Services
{
    public static class StudentUpdateService
    {
        /// <summary>
        /// UPDATE - Обновление студента
        /// </summary>
        public static bool UpdateStudent(int studentId, string? firstName = null, string? lastName = null, 
                                       DateTime? dateOfBirth = null, string? email = null)
        {
            using var db = new StudentContext();
            var student = db.Students.FirstOrDefault(s => s.StudentId == studentId);
            
            if (student != null)
            {
                if (!string.IsNullOrEmpty(firstName)) student.FirstName = firstName;
                if (!string.IsNullOrEmpty(lastName)) student.LastName = lastName;
                if (dateOfBirth.HasValue) student.DateOfBirth = dateOfBirth.Value;
                if (!string.IsNullOrEmpty(email)) student.Email = email;
                
                db.SaveChanges();
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// UPDATE - Асинхронное обновление студента
        /// </summary>
        public static async Task<bool> UpdateStudentAsync(int studentId, string? firstName = null, 
                                                         string? lastName = null, DateTime? dateOfBirth = null, 
                                                         string? email = null)
        {
            await using var db = new StudentContext();
            var student = await db.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);
            
            if (student != null)
            {
                if (!string.IsNullOrEmpty(firstName)) student.FirstName = firstName;
                if (!string.IsNullOrEmpty(lastName)) student.LastName = lastName;
                if (dateOfBirth.HasValue) student.DateOfBirth = dateOfBirth.Value;
                if (!string.IsNullOrEmpty(email)) student.Email = email;
                
                await db.SaveChangesAsync();
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// UPDATE - Обновление с явным вызовом Update
        /// </summary>
        public static bool UpdateStudentWithExplicitUpdate(Student student)
        {
            using var db = new StudentContext();
            db.Students.Update(student);
            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// UPDATE - Обновление email студента
        /// </summary>
        public static bool UpdateStudentEmail(int studentId, string newEmail)
        {
            using var db = new StudentContext();
            var student = db.Students.FirstOrDefault(s => s.StudentId == studentId);
            
            if (student != null)
            {
                student.Email = newEmail;
                db.SaveChanges();
                return true;
            }
            
            return false;
        }
    }
}