using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

namespace Hakaton.API.Application.Interfaces;

public interface ITeacherRepository
{
    public Task<IEnumerable<Teacher>> GetTeachers();
    
    public Task<Teacher> GetTeacherById(int id);

    public Task<int> EditTeacher(TeacherDto teacherDto,int id);

    public Task<bool> DeleteTeacher(int id);

    public Task<Teacher> CreateTeacher(TeacherDto teacherDto,int userId);

}