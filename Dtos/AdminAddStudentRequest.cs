namespace GradingSystemApi.Dtos
{
    public class AdminAddStudentRequestDto
    {
        public required string username { get; set; }
        public required string password { get; set; }
        public required string section { get; set; }
    }
}
