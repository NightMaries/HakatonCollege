using System.Data;
using Hakaton.API.Domen.Entities;
using Hakaton.API.Infrustructure.Data;
using Microsoft.Office.Interop.Excel;
using SqlKata.Execution;

public class RoleRepository : IRoleRepository
{
    private readonly QueryFactory _query;
    public RoleRepository(HakatonContext hakatonContext)
    {
        _query = hakatonContext.PostgresQueryFactory;
    }
    public async Task<int> CreateRole(Role role)
    {
        var result = _query.Query("Roles").AsInsert(new
        {
            Name = role.Name
        });
        return await _query.ExecuteAsync(result);
    }

    public async Task<Role> GetRoleById(int id)
    {
        var query = _query.Query("Roles").Where("Id",id).Select("Id","Name");
        var result = await _query.FirstOrDefaultAsync<Role>(query);
        if(result is null)
            throw new Exception("Роль не найдена");
        return result;
    }

    public async Task<IEnumerable<Role>> GetRoles()
    {
        var quert = _query.Query("Roles").Select("Id","Name");
        var result = await _query.GetAsync<Role>(quert);
        if(result is null)
            throw new Exception("Записей нет");
        return result;
    }
}
