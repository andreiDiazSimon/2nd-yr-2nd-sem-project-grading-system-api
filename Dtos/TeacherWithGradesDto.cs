public class TeacherWithGradesDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Subject { get; set; }
    public List<GradeDto> Grades { get; set; }  // List of grades for this teacher
}
