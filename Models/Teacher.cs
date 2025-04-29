namespace GradingSystemApi.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Subject { get; set; }

        // Navigation property for StudentTeachers
        public ICollection<StudentTeacher> StudentTeachers { get; set; } = new List<StudentTeacher>();

        // Adding collection for grades might be useful if you want to fetch the grades for each teacher
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
