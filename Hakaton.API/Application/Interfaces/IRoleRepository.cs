using Hakaton.API.Domen.Entities;

public interface IRoleRepository
{
    public Task<int> CreateRole(Role role);
    
    public Task<Role> GetRoleById(int id);
    
    public Task<IEnumerable<Role>> GetRoles();
}