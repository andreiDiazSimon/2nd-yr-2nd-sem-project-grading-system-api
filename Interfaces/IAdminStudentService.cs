using GradingSystemApi.Dtos;
using GradingSystemApi.Models;

namespace GradingSystemApi.Interfaces
{
    public interface IAdminStudentService
    {
        Task<AdminAddStudentResponseDto> AdminAddStudent(string username, string password, string section);
        Task<List<AdminGetAllStudentResponseDto>> GetAllStudents();
        Task<bool> RemoveStudent(int studentId);
    }
}
