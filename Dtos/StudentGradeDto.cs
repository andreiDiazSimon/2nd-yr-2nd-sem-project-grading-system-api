namespace GradingSystemApi.Dtos
{
    public class StudentGradeDto
    {
        public string TeacherName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public double? CombinedPrelimGrade { get; set; }
        public double? CombinedMidtermGrade { get; set; }
        public double? CombinedFinalsGrade { get; set; }
    }
}
