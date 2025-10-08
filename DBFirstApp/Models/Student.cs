namespace DBFirstApp.Models;

public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; } = string.Empty;

    public int Age => DateTime.Now.Year - DateOfBirth.Year - 
                      (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
}
