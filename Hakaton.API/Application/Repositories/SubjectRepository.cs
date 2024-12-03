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

public class SubjectRepository : ISubjectRepository
{
    private readonly QueryFactory _query;

    HMACSHA256 hmacSubject = new HMACSHA256();
    public SubjectRepository(HakatonContext hakatonContext, ITokenService tokenService,ICryptService cryptService)
    {
        _query = hakatonContext.PostgresQueryFactory;
    }



    public async Task<int> CreateSubject(SubjectDto subjectDto)
    {
        if(subjectDto.Name is null || subjectDto.TeacherId <= 0)
            throw new Exception("Заполните поля");
        

        var query = _query.Query("Subjects").AsInsert(new 
        {
            Name = subjectDto.Name,
            TeacherId = subjectDto.TeacherId
        });
        
        return await _query.ExecuteAsync(query);;
    }

    public async Task<bool> DeleteSubject(int id)
    {
        var result = await _query.Query("Subjects").Where("Id", id).DeleteAsync();
        if (result != 1)
            throw new Exception("Предмет не найден");
        
        return true;
    }

    public async Task<int> EditSubject(SubjectDto subjectDto, int id)
    {
        if (subjectDto.Name is null || subjectDto.TeacherId <= 0)
            throw new Exception("Name и TeacherId обязательны для заполнения");

        var affected = await _query.Query("Subjects")
            .Where("Id", id)
            .UpdateAsync(new
            {
                Name = subjectDto.Name,
                TeacherId = subjectDto.TeacherId
            });

        if (affected == 0) throw new Exception("Предмет не найден");
        
        return affected;
    }
    public async Task<Subject> GetSubjectById(int id)
    {
        var query = _query.Query("Subjects")
            .Where("Id",id)
            .Select("Id","Name","Subjects.TeacherId");

        var result = await _query.FirstOrDefaultAsync<Subject>(query);
        if(result is null) throw new Exception("Предмет не найден");
        return result;
    }
    public async Task<IEnumerable<Subject>> GetSubjects()
    {
        var query = _query.Query("Subjects")
            .Select(
                "Subjects.Id",
                "Subjects.Name",
                "Subjects.TeacherId"
            );

        var result = await _query.GetAsync<Subject>(query);
        if (result is null || !result.Any()) throw new Exception("Нет данных");
        return result;

    }

}