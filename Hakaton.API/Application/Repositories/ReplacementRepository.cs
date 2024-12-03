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

public class ReplacementRepository : IReplacementRepository
{
    private readonly QueryFactory _query;

    HMACSHA256 hmacReplacement = new HMACSHA256();
    public ReplacementRepository(HakatonContext hakatonContext, ITokenService TokenService, ICryptService cryptService)
    {
        _query = hakatonContext.PostgresQueryFactory;
    }

    public async Task<int> CreateReplacement(ReplacementDto replacementDto)
    {
        if(replacementDto.Id <= 0 || replacementDto.GroupId <= 0)
        {
            throw new Exception("Заполните поля");
        }

        var query = _query.Query("Replacement").AsInsert(new 
        {
            Id = replacementDto.Id,
            GroupId = replacementDto.GroupId,
            OldSubjectId = replacementDto.OldSubjectId,
            NewSubjectId = replacementDto.NewSubjectId,
            PairNumber = replacementDto.PairNumber,
            Reason = replacementDto.Reason,
            Date = replacementDto.Date

        });
        return await _query.ExecuteAsync(query);
        
    }

    public async Task<bool> DeleteReplacement(int id)
    {
        var result = await _query.Query("Users").Where("Id", id).DeleteAsync();
        if (result != 1)
            throw new Exception("Замены не найдены");
        
        return true;
    }

    public async Task<int> EditReplacement(ReplacementDto replacementDto, int id)
    {
         if (replacementDto.Id <= 0 || replacementDto.GroupId <= 0)
            throw new Exception("Id и GroupId обязательны для заполнения");

        var affected = await _query.Query("Replacements")
            .Where("Id", id)
            .UpdateAsync(new
            {
                Id = replacementDto.Id,
                GroupId = replacementDto.GroupId,
                OldSubjectId = replacementDto.OldSubjectId,
                NewSubjectId = replacementDto.NewSubjectId,
                PairNumber = replacementDto.PairNumber,
                Reason = replacementDto.Reason,
                Date = replacementDto.Date
            });

        if (affected != 1) throw new Exception("Студент не найден");
        return affected;
    }

    public async Task<ReplacementDto> GetReplacementById(int id)
    {
        var query = _query.Query("Students").Where("Id",id)
        .Select("Id","GroupId","OldSubjectId","NewSubjectId", "PairNumber", "Reason", "Date");

        var result = await _query.FirstOrDefaultAsync<ReplacementDto>(query);
        if(result is null) throw new Exception("Замены не найдены");
        return result;
    }

    public async Task<IEnumerable<ReplacementDto>> GetReplacements()
    {
        var query = _query.Query("Replacements")
            .Select("Id","GroupId","OldSubjectId","NewSubjectId", "PairNumber", "Reason", "Date");

        var result = await _query.GetAsync<ReplacementDto>(query);
        if (result is null || !result.Any()) throw new Exception("Нет данных");
        return result;
    }
}