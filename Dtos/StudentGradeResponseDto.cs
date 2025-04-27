using GradingSystemApi.Dtos;

public class StudentGradeResponseDto
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Section { get; set; } = "";
    public List<GradeDto> Grades { get; set; } = new();
}
