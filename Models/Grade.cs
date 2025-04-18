namespace GradingSystemApi.Models
{
    public class Grade
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public required string Term { get; set; }

        public double Week1 { get; set; }
        public double Week2 { get; set; }
        public double Week3 { get; set; }
        public double Week4 { get; set; }
        public double Week5 { get; set; }
        public double Exam { get; set; }
    }
}
