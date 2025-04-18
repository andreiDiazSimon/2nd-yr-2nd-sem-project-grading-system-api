using GradingSystemApi.Dtos;

namespace GradingSystemApi.Interfaces
{
    public interface IAdminStudentService
    {
        Task<AdminAddStudentResponseDto> AdminAddStudent(string username, string password, string section);
    }
}
