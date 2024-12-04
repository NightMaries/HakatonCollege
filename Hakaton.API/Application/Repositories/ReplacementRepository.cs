using System.Security.Cryptography;
using System.Text;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Application.Services;
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
    private readonly IPushReplacementService _pushReplacementService;
    HMACSHA256 hmacReplacement = new HMACSHA256();
    public ReplacementRepository(HakatonContext hakatonContext, ITokenService TokenService,
                ICryptService cryptService,IPushReplacementService pushReplacementService)
    {
        _query = hakatonContext.PostgresQueryFactory;
        _pushReplacementService = pushReplacementService;
    }

    public async Task<IEnumerable<PushReplacementUser>> CreateReplacement(ReplacementDtoPost replacementDtoPost)
    {

        var query = _query.Query("Replacements").AsInsert(new 
        {
                GroupId = replacementDtoPost.GroupId,
                OldSubjectId = replacementDtoPost.OldSubjectId,
                NewSubjectId = replacementDtoPost.NewSubjectId,
                OldTeacherId = replacementDtoPost.OldTeacherId,
                NewTeacherId = replacementDtoPost.NewTeacherId,
                PairNumber = replacementDtoPost.PairNumber,
                Reason = replacementDtoPost.Reason,
                Date = replacementDtoPost.Date

        });
        await _query.ExecuteAsync(query);
        var replacementDtoGet = _query.FirstOrDefault<ReplacementDtoGet>( _query.Query("Replacements").Where("Date" , replacementDtoPost.Date).Select());
        
        return  await _pushReplacementService.SendingPush(replacementDtoGet);
        
    }

    public async Task<bool> DeleteReplacement(int id)
    {
        var result = await _query.Query("Users").Where("Id", id).DeleteAsync();
        if (result != 1)
            throw new Exception("Замены не найдены");
        
        return true;
    }

    public async Task<int> EditReplacement(ReplacementDtoPost replacementDtoPost, int id)
    {
        
        var affected = await _query.Query("Replacements")
            .Where("Id", id)
            .UpdateAsync(new
            {
               GroupId = replacementDtoPost.GroupId,
                OldSubjectId = replacementDtoPost.OldSubjectId,
                NewSubjectId = replacementDtoPost.NewSubjectId,
                OldTeacherId = replacementDtoPost.OldTeacherId,
                NewTeacherId = replacementDtoPost.NewTeacherId,
                PairNumber = replacementDtoPost.PairNumber,
                Reason = replacementDtoPost.Reason,
                Date = replacementDtoPost.Date

            });

        if (affected != 1) throw new Exception("не удалось изменить запись");
        return affected;
    }

    public async Task<ReplacementDtoGet> GetReplacementById(int id)
    {
        var query = _query.Query("Replacements").Where("Id",id)
            .Select("Id","GroupId","OldSubjectId","NewSubjectId","NewTeacherId","OldTeacherId","PairNumber", "Reason", "Date");

        var result = await _query.FirstOrDefaultAsync<Replacement>(query);
        string groupName = await _query.FirstOrDefaultAsync<string>(_query.Query("Groups")
                .Where("Id",result.GroupId).Select("GroupName"));
            string oldSubjectName = await _query.FirstOrDefaultAsync<string>(_query.Query("Subjects")
                .Where("Id",result.OldSubjectId).Select("Name"));
            string newSubjectName = await _query.FirstOrDefaultAsync<string>(_query.Query("Subjects")
                .Where("Id",result.NewSubjectId).Select("Name"));
            string oldTeacherFIO = await _query.FirstOrDefaultAsync<string>(_query.Query("Teachers")
                .Where("Id",result.OldTeacherId).Select("FIO"));
            string newTeacherFIO = await _query.FirstOrDefaultAsync<string>(_query.Query("Teachers")
                .Where("Id",result.NewTeacherId).Select("FIO"));
            
        ReplacementDtoGet replacementDtoGet = new ReplacementDtoGet()
        {
                Id = id,
                GroupId = result.GroupId,
                OldSubjectId = result.OldSubjectId,
                NewSubjectId = result.NewSubjectId,
                OldTeacherId = result.OldTeacherId,
                NewTeacherId = result.NewTeacherId,
                GroupName =  groupName,
                OldSubjectName = oldSubjectName,
                NewSubjectName = newSubjectName,
                OldTeacherFIO = oldTeacherFIO,
                NewTeacherFIO = newTeacherFIO,
                PairNumber = result.PairNumber,
                Reason  = result.Reason,
                Date  = result.Date

        };
            
          return replacementDtoGet;
        }
    


    public async Task<IEnumerable<ReplacementDtoGet>> GetReplacements()
    {

        List<ReplacementDtoGet> list = new List<ReplacementDtoGet>();
        var query = _query.Query("Replacements")
            .Select("Id","GroupId","OldSubjectId","NewSubjectId","NewTeacherId","OldTeacherId","PairNumber", "Reason", "Date");

        var result = await _query.GetAsync<Replacement>(query);
        foreach(var item in result)
        {
            string groupName = await _query.FirstOrDefaultAsync<string>(_query.Query("Groups")
                .Where("Id",item.GroupId).Select("GroupName"));
            string oldSubjectName = await _query.FirstOrDefaultAsync<string>(_query.Query("Subjects")
                .Where("Id",item.OldSubjectId).Select("Name"));
            string newSubjectName = await _query.FirstOrDefaultAsync<string>(_query.Query("Subjects")
                .Where("Id",item.NewSubjectId).Select("Name"));
            string oldTeacherFIO = await _query.FirstOrDefaultAsync<string>(_query.Query("Teachers")
                .Where("Id",item.OldTeacherId).Select("FIO"));
            string newTeacherFIO = await _query.FirstOrDefaultAsync<string>(_query.Query("Teachers")
                .Where("Id",item.NewTeacherId).Select("FIO"));
            
            
            list.Add(new ReplacementDtoGet
            {
                Id = item.Id,
                GroupId = item.GroupId,
                OldSubjectId = item.OldSubjectId,
                NewSubjectId = item.NewSubjectId,
                OldTeacherId = item.OldTeacherId,
                NewTeacherId = item.NewTeacherId,
                GroupName =  groupName,
                OldSubjectName = oldSubjectName,
                NewSubjectName = newSubjectName,
                OldTeacherFIO = oldTeacherFIO,
                NewTeacherFIO = newTeacherFIO,
                PairNumber = item.PairNumber,
                Reason  = item.Reason,
                Date  = item.Date
            });

        }

        if (result is null || !result.Any()) throw new Exception("Нет данных");
        return list;
    }
}