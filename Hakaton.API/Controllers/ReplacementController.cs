using System.Security.Cryptography;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReplacementController: ControllerBase
{
    private readonly IReplacementRepository _replacementRepository;
    
    HMACSHA256 hmacReplace = new HMACSHA256();

    public ReplacementController(IReplacementRepository replacementRepository)
    {
        _replacementRepository = replacementRepository;
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> FindReplacementById(int id)
    {
        var result = await _replacementRepository.GetReplacementById(id);
        return Ok(result);
    }
    [HttpPost]    
    public async Task<IActionResult> CreateReplacement(ReplacementDtoPost replacement)
    {
        var result = await _replacementRepository.CreateReplacement(replacement);
        if(result is null)
            throw new Exception("Не удалось создать замены");
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> ListSchedules()
    {
        var result = await _replacementRepository.GetReplacements();
        if(result is null)
            return NotFound();
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> EditReplacement(ReplacementDtoPost replacementDto, int id)
    {
        var result = await _replacementRepository.EditReplacement(replacementDto,id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveTeacher(int id)
    {
        var result = await _replacementRepository.DeleteReplacement(id);
        if(!result)
            return NotFound(result);
        else
            return BadRequest(result);
    }
}