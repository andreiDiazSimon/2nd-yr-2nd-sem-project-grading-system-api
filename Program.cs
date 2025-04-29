using GradingSystemApi.Data;
using GradingSystemApi.Services;
using GradingSystemApi.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
            policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
                        .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();

builder.Services.AddScoped<ISignInService, SignInService>();
builder.Services.AddScoped<IAdminStudentService, AdminStudentService>();
builder.Services.AddScoped<IAdminTeacherService, AdminTeacherService>();
builder.Services.AddScoped<IAdminSectionService, AdminSectionService>();
builder.Services.AddScoped<TeacherService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseCors();

app.MapControllers();

app.Run();
