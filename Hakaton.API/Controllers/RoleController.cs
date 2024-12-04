using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class RoleController :ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(Role role)
    {
        var result =await _roleRepository.CreateRole(role);
        if(result != 1)
            return BadRequest();
        return Ok(result);

    }
    
    [HttpGet ]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _roleRepository.GetRoles();
        if(result is null)
            return BadRequest();
        return Ok(result);
    }
    
    [HttpGet ("{id}")]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var result = await _roleRepository.GetRoleById(id);
        if(result is null)
            return BadRequest();
        return Ok(result);
    }
    
}