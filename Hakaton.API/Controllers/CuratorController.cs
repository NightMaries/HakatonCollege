using System.Security.Cryptography;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CuratorController:ControllerBase
{
    private readonly ICuratorRepository _curatorRepository;

    public CuratorController(ICuratorRepository curatorRepository)
    {
        _curatorRepository = curatorRepository;
    }

    [HttpPost]    
    public async Task<IActionResult> CreateCurator(CuratorDto curatorDto)
    {
        var result = await _curatorRepository.CreateCurator(curatorDto,curatorDto.TeacherId);
        if(result is null)
            throw new Exception("Не удалось создать пользователя");
        return Created($"https://localhost:5058/Curator/{result.Id}",result);
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> FindCuratorById(int id)
    {
        var result = await _curatorRepository.GetCuratorById(id);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> ListCurators()
    {
        var result = await _curatorRepository.GetCurators();
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> EditCurator(CuratorDto curatorDto, int id)
    {
        var result = await _curatorRepository.EditCurator(curatorDto,id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveCurator(int id)
    {
        var result = await _curatorRepository.DeleteCurator(id);
        if(!result)
            return NotFound(result);
        else
            return BadRequest(result);
    }
}