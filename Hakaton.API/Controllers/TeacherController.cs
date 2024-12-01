using System.Security.Cryptography;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController:ControllerBase
{
    private readonly ITeacheRepository _teacheRepository;

    public TeacherController(ITeacheRepository teacheRepository)
    {
        _teacheRepository = teacheRepository;
    }

    [HttpPost]    
    public async Task<IActionResult> CreateTeacher(TeacherDto teacherDto)
    {
        var result = await _teacheRepository.CreateTeacher(teacherDto,teacherDto.UserId);
        if(result is null)
            throw new Exception("Неудалось создать пользователя");
        return Created($"https://localhost:5058/Teacher/{result.Id}",result);
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> FindTeacherById(int id)
    {
        var result = await _teacheRepository.GetTeacherById(id);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> ListTeachers()
    {
        var result = await _teacheRepository.GetTeachers();
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> EditTeacher(TeacherDto teacherDto, int id)
    {
        var result = await _teacheRepository.EditTeacher(teacherDto,id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveTeacher(int id)
    {
        var result = await _teacheRepository.DeleteTeacher(id);
        if(!result)
            return NotFound(result);
        else
            return BadRequest(result);
    }
}