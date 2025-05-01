using GradingSystemApi.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradingSystemApi.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentGradeDto>> GetCombinedGrades(int studentId);
    }
}
