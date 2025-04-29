namespace GradingSystemApi.Dtos
{
    public class SignInResponseDto
    {
	public int? Id { get; set; }
        public bool Success { get; set; }
        public string? Role { get; set; }
        public string? Message { get; set; }
        public string? Username { get; set; }
    }
}
