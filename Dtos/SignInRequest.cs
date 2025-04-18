namespace GradingSystemApi.Dtos
{
    public class SignInRequestDto
    {
        public required string username { get; set; }
        public required string password { get; set; }
    }
}
