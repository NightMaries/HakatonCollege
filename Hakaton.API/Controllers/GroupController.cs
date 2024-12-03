using System.Security.Cryptography;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupController:ControllerBase
{
    private readonly IGroupRepository _groupRepository;

    public GroupController(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    [HttpPost]    
    public async Task<IActionResult> CreateGroup(GroupDto groupDto)
    {
        var result = await _groupRepository.CreateGroup(groupDto,groupDto.TeacherId);
        if(result is null)
            throw new Exception("Не удалось создать группу");
        return Created($"https://localhost:5058/Group/{result.Id}",result);
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> FindGroupById(int id)
    {
        var result = await _groupRepository.GetGroupById(id);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> ListGroups()
    {
        var result = await _groupRepository.GetGroups();
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> EditGroup(GroupDto groupDto, int id)
    {
        var result = await _groupRepository.EditGroup(groupDto,id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveGroup(int id)
    {
        var result = await _groupRepository.DeleteGroup(id);
        if(!result)
            return NotFound(result);
        else
            return BadRequest(result);
    }
}