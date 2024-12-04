using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Hakaton.API.Infrustructure.Data;
using SqlKata.Execution;

namespace Hakaton.API.Application.Repositories;
public class PushReplacementUserRepository : IPushReplacementUserRepository
{
    private readonly QueryFactory _query;
    public PushReplacementUserRepository(HakatonContext hakatonContext)
    {
        _query = hakatonContext.PostgresQueryFactory;
    }
    public async Task<int> CreatePushReplacementUser(int userId , int replacementId)
    {
        var query = _query.Query("PushReplacementUsers").AsInsert
        ( new{
            ReplacementId =  replacementId,
            UserId =  userId
        });
        if(query is null) throw new Exception("Неудалось создать запись");
        return await _query.ExecuteAsync(query);
    }

    public async Task<bool> DeletePushReplacementUser(int id)
    {
        var result = await _query.Query("PushReplacementUsers").Where("Id", id).DeleteAsync();
        if (result != 1)
            throw new Exception("Запись не найден");
        
        return true;
    }

    public async Task<int> EditPushReplacementUser(PushReplacementUser pushReplacementUser, int id)
    {
        var affected = await _query.Query("PushReplacementUsers").UpdateAsync
        ( 
            new{
            Id = pushReplacementUser.Id,
            ReplacementId =  pushReplacementUser.ReplacementId,
            UserId =  pushReplacementUser.UserId
        });
        if(affected != 1) throw new Exception("Неудалось найти запись");
        return affected;
    }

    public async Task<PushReplacementUser> GetPushReplacementUserById(int id)
    {
        var query = _query.Query("PushReplacementUsers").Where("Id",id)
        .Select("Id","UserId","ReplacementId");

        var result = await _query.FirstOrDefaultAsync<PushReplacementUser>(query);
        if(result is null) throw new Exception("Запись не найдена");
        return result;  
    }

    public async  Task<IEnumerable<PushReplacementUser>> GetPushReplacementUsers(int replacementId)
    {
        var query = _query.Query("PushReplacementUsers").Where("ReplacementId", replacementId)
            .Select("Id","UserId","ReplacementId");

        var result = await _query.GetAsync<PushReplacementUser>(query);
        if(result is null) throw new Exception("Студент не найден");
        return result;
    }
}