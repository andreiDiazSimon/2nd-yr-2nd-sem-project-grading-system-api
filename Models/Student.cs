namespace GradingSystemApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Section { get; set; }
        public required ICollection<Grade> Grades { get; set; }
    }
}
