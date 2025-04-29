namespace GradingSystemApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Section { get; set; }

        // Navigation properties for Grades and StudentTeachers
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<StudentTeacher> StudentTeachers { get; set; } = new List<StudentTeacher>();
    }
}
