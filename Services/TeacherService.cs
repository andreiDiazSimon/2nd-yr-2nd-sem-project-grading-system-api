using GradingSystemApi.Models;
using GradingSystemApi.Data;
using GradingSystemApi.Interfaces;
using GradingSystemApi.Dtos;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradingSystemApi.Services
{
    public class TeacherService 
    {
        private readonly AppDbContext _appDbContext;
        public TeacherService(AppDbContext ctx) => _appDbContext = ctx;



    }
}
