using GradingSystemApi.Dtos;

namespace GradingSystemApi.Interfaces
{
    public interface IAdminTeacherService
    {
        Task<List<AdminGetAllTeacherResponseDto>> GetAllTeachers();
    }
}
