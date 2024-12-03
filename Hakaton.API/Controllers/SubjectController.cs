using System.Security.Cryptography;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Mvc;
using Hakaton.API.Application.Repositories;

namespace Hakaton.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SubjectController:ControllerBase
{
    private readonly ISubjectRepository _SubjectRepository;

    public SubjectController(ISubjectRepository SubjectRepository)
    {
        _SubjectRepository = SubjectRepository;
    }

    [HttpPost]    
    public async Task<IActionResult> CreateSubject(SubjectDto SubjectDto)
    {
        var result = await _SubjectRepository.CreateSubject(SubjectDto);
        if(result <= 0)
            throw new Exception("Не удалось создать предмет");
        return Ok(result);
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> FindSubjectById(int id)
    {
        var result = await _SubjectRepository.GetSubjectById(id);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> ListSubjects()
    {
        var result = await _SubjectRepository.GetSubjects();
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> EditSubject(SubjectDto SubjectDto, int id)
    {
        var result = await _SubjectRepository.EditSubject(SubjectDto, id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveSubject(int id)
    {
        var result = await _SubjectRepository.DeleteSubject(id);
        if(!result)
            return NotFound(result);
        else
            return BadRequest(result);
    }
}