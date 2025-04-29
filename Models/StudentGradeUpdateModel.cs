namespace GradingSystemApi.Models
{
   public class StudentGradeUpdateModel
    {
        public int StudentId { get; set; }
        public int TeacherId { get; set; }
        public string Term { get; set; } = string.Empty;
        public double Week1 { get; set; }
        public double Week2 { get; set; }
        public double Week3 { get; set; }
        public double Week4 { get; set; }
        public double Week5 { get; set; }
        public double Exam { get; set; }
    }
}
