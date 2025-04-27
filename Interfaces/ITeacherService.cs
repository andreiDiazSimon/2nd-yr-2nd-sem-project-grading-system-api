using GradingSystemApi.Dtos;

namespace GradingSystemApi.Interfaces
{
    public interface ITeacherService
    {
        Task<List<string>> GetSectionsAsync();
        Task<List<StudentGradeResponseDto>> GetGradesBySectionAsync(string section);
	Task<string> SaveGradesAsync(List<SaveGradesDto> gradesDto);
    }
}
