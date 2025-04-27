using GradingSystemApi.Dtos;

namespace GradingSystemApi.Interfaces
{
    public interface IAdminSectionService
    {
        Task<List<string>> GetAllSections();
        Task<List<AdminSectionGetStudentBySectionDto>> GetStudentsBySection(string section);
    }
}
