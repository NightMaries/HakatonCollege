using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Hakaton.API.Infrustructure.Data;
using SqlKata.Execution;

namespace Hakaton.API.Application.Repositories;
public class TeacherRepository : ITeacherRepository
{
    private readonly QueryFactory _query;
    public TeacherRepository(HakatonContext hakatonContext)
    {
        _query = hakatonContext.PostgresQueryFactory;
    }
    public async Task<Teacher> CreateTeacher(TeacherDto teacherDto,int userId)
    {
        var teacher = new Teacher() {
            FIO = teacherDto.FIO,
            Classroom = teacherDto.Classroom,
            UserId = userId
        };
        var result = _query.Query("Teachers")
        .AsInsert(new 
        {
            FIO = teacherDto.FIO,
            Classroom = teacherDto.Classroom,
            UserId = userId
        }
        );
        var data = await _query.ExecuteAsync(result);
        return teacher;
    }

    public async Task<bool> DeleteTeacher(int id)
    {
        var result = await _query.Query("Teachers").Where("Id",id).DeleteAsync();
        if (result != 1)
            throw new Exception("Преподаватель не найден");
        return true;

    }

    public async Task<int> EditTeacher(TeacherDto teacherDto, int id)
    {
        var user = await GetTeacherById(id);
        var result =await _query.Query("Teachers").Where("Id",user.Id)
        .UpdateAsync(new{
            FIO = teacherDto.FIO,
            Classroom = teacherDto.Classroom,
            UserId = teacherDto.UserId});
        return result;    
    }

    public async Task<Teacher> GetTeacherById(int id)
    {
        var query = _query.Query("Teachers").Where("Id",id)
        .Select("Id","FIO","Classroom","UserId");

        var result = await _query.FirstOrDefaultAsync<Teacher>(query);
        if(result is null) throw new Exception("Преподаватель не найден");
        return result;
    }

    public async Task<IEnumerable<Teacher>> GetTeachers()
    {
        
        var query =  _query.Query("Teachers")
        .Select("Id","FIO","Classroom","UserId");

        var result = await _query.GetAsync<Teacher>(query);
        if(result is null) throw new Exception("Данных нет в БД");
        return result;
    }

    
}
