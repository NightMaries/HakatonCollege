using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Hakaton.API.Infrustructure.Data;
using SqlKata.Execution;

namespace Hakaton.API.Application.Repositories;
public class CuratorRepository : ICuratorRepository
{
    private readonly QueryFactory _query;
    public CuratorRepository(HakatonContext hakatonContext)
    {
        _query = hakatonContext.PostgresQueryFactory;
    }
    public async Task<Curator> CreateCurator(CuratorDto curatorDto, int userId)
    {
        var curator = new Curator() {
            TeacherId = userId
        };
        var result = _query.Query("Curators")
        .AsInsert(new 
        {
            TeacherId = userId
        }
        );
        var data = await _query.ExecuteAsync(result);
        return curator;
    }

    public async Task<bool> DeleteCurator(int id)
    {
        var result = await _query.Query("Curators").Where("Id",id).DeleteAsync();
        if (result != 1)
            throw new Exception("Куратор не найден");
        return true;

    }

    public async Task<int> EditCurator(CuratorDto curatorDto, int id)
    {
        var user = await GetCuratorById(id);
        var result =await _query.Query("Curators").Where("Id",user.Id)
        .UpdateAsync(new{
            TeacherId = curatorDto.TeacherId});
        return result;    
    }

    public async Task<Curator> GetCuratorById(int id)
    {
        var query = _query.Query("Curators").Where("Id",id)
        .Select("TeacherId");

        var result = await _query.FirstOrDefaultAsync<Curator>(query);
        if(result is null) throw new Exception("Куратор не найден");
        return result;
    }

    public async Task<IEnumerable<Curator>> GetCurators()
    {
        
        var query =  _query.Query("Curators")
        .Select("TeacherId");

        var result = await _query.GetAsync<Curator>(query);
        if(result is null) throw new Exception("Данных нет в БД");
        return result;
    }
}
