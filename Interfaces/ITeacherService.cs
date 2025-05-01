using GradingSystemApi.Dtos;
using GradingSystemApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystemApi.Interfaces
{
    public interface ITeacherService
    {
        Task<List<string>> GetSectionsAsync();
        Task<dynamic> GetStudentsAndGradesBySectionAndTeacherAsync(string section, int teacherId);
        Task<bool> UpdateGradeAsync(StudentGradeUpdateModel updatedGrade);
    }
}
