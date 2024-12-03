using System.Security.Cryptography;
using System.Text;
using Azure.Core.Pipeline;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Application.Repositories;
using Hakaton.API.Application.Services;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.API.Controllers;

[ApiController]
[Route("[controller]")]

public class StudentController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;
    
    HMACSHA256 hmacStudent = new HMACSHA256();

    public StudentController(IStudentRepository repository)
    {
        _studentRepository = repository;
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> FindStudentById(int id)
    {
        var result = await _studentRepository.GetStudentById(id);
        return Ok(result);
    }
    [HttpPost]    
    public async Task<IActionResult> CreateStudent(StudentDto student)
    {
        var result = await _studentRepository.CreateStudent(student);
        if(result != 1)
            throw new Exception("Не удалось создать студента");
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> ListSchedules()
    {
        var result = await _studentRepository.GetStudents();
        if(result is null)
            return NotFound();
        return Ok(result);
    }
    [HttpPut]
    public async Task<IActionResult> EditStudent(StudentDto studentDto, int id)
    {
        var result = await _studentRepository.EditStudent(studentDto,id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveTeacher(int id)
    {
        var result = await _studentRepository.DeleteStudent(id);
        if(!result)
            return NotFound(result);
        else
            return BadRequest(result);
    }
}