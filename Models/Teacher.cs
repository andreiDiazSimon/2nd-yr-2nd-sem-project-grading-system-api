namespace GradingSystemApi.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Subject { get; set; }

        public ICollection<StudentTeacher> StudentTeachers { get; set; } = new List<StudentTeacher>();
    }
}
