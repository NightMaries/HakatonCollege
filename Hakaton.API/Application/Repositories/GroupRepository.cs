using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Hakaton.API.Infrustructure.Data;
using SqlKata.Execution;

namespace Hakaton.API.Application.Repositories;
public class GroupRepository : IGroupRepository
{
    private readonly QueryFactory _query;
    public GroupRepository(HakatonContext hakatonContext)
    {
        _query = hakatonContext.PostgresQueryFactory;
    }
    public async Task<Group> CreateGroup(GroupDto groupDto, int userId)
    {
        var group = new Group() {
            CuratorId = userId
        };
        var result = _query.Query("Groups")
        .AsInsert(new 
        {
            TeacherId = userId
        }
        );
        var data = await _query.ExecuteAsync(result);
        return group;
    }

    public async Task<bool> DeleteGroup(int id)
    {
        var result = await _query.Query("Groups").Where("Id",id).DeleteAsync();
        if (result != 1)
            throw new Exception("Группа не найден");
        return true;

    }

    public async Task<int> EditGroup(GroupDto groupDto, int id)
    {
        var user = await GetGroupById(id);
        var result =await _query.Query("Groups").Where("Id",user.Id)
        .UpdateAsync(new{
            CuratorId = groupDto.CuratorId});
        return result;    
    }

    public async Task<Group> GetGroupById(int id)
    {
        var query = _query.Query("Groups").Where("Id",id)
        .Select("CuratorId");

        var result = await _query.FirstOrDefaultAsync<Group>(query);
        if(result is null) throw new Exception("Группа не найден");
        return result;
    }

    public async Task<IEnumerable<Group>> GetGroups()
    {
        
        var query =  _query.Query("Groups")
        .Select("CuratorId");

        var result = await _query.GetAsync<Group>(query);
        if(result is null) throw new Exception("Данных нет в БД");
        return result;
    }
}
