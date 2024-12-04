using System.Security.Cryptography;
using System.Text;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Hakaton.API.Infrustructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using SqlKata;
using SqlKata.Execution;

namespace Hakaton.API.Application.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly QueryFactory _query;

    HMACSHA256 hmacStudent = new HMACSHA256();
    public StudentRepository(HakatonContext hakatonContext, ITokenService tokenService,ICryptService cryptService)
    {
        _query = hakatonContext.PostgresQueryFactory;
    }



    public async Task<int> CreateStudent(StudentDto studentDto)
    {
        if(studentDto.UserId <= 0 || studentDto.GroupId <= 0)
        {
            throw new Exception("Заполните поля");
        }

        var query = _query.Query("Students").AsInsert(new 
        {
            FIO = studentDto.FIO,
            UserId = studentDto.UserId,
            GroupId = studentDto.GroupId
        });
        return await _query.ExecuteAsync(query);
        
    }

    public async Task<bool> DeleteStudent(int id)
    {
        var result = await _query.Query("Users").Where("Id", id).DeleteAsync();
        if (result != 1)
            throw new Exception("Студент не найден");
        
        return true;
    }

    public async Task<int> EditStudent(StudentDto studentDto, int id)
    {
        if (studentDto.UserId <= 0 || studentDto.GroupId <= 0)
            throw new Exception("UserId и GroupId обязательны для заполнения");

        var affected = await _query.Query("Students")
            .Where("Id", id)
            .UpdateAsync(new
            {
                UserId = studentDto.UserId,
                GroupId = studentDto.GroupId,
                FIO = studentDto.FIO
            });

        if (affected != 1) throw new Exception("Студент не найден");
        return affected;
    }

    public async Task<IEnumerable<Student>> GetStudents()
    {
        var query = _query.Query("Students")
            .Select("Id","UserId","GroupId","FIO");

        var result = await _query.GetAsync<Student>(query);
        if (result is null ) throw new Exception("Нет данных");
        return result;

    }
    public async Task<Student> GetStudentById(int id)
    {
        var query = _query.Query("Students").Where("Id",id)
        .Select("Id","FIO","UserId","GroupId");

        var result = await _query.FirstOrDefaultAsync<Student>(query);
        if(result is null) throw new Exception("Студент не найден");
        return result;
    }

    
}