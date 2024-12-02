using System.Security.Cryptography;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController:ControllerBase
{
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleController(IScheduleRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    [HttpPost]    
    public async Task<IActionResult> CreateSchedule(ScheduleDto scheduleDto)
    {
        var result = await _scheduleRepository.CreateSchedule(scheduleDto);
        if(result is null)
            throw new Exception("Не удалось создать запись в расписании");
        return Created($"https://localhost:5058/Schedule/{result.Id}",result);
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> FindScheduleById(int id)
    {
        var result = await _scheduleRepository.GetScheduleById(id);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> ListSchedules()
    {
        var result = await _scheduleRepository.GetSchedules();
        if(result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> EditSchedule(ScheduleDto scheduleDto, int id)
    {
        var result = await _scheduleRepository.EditSchedule(scheduleDto,id);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveSchedule(int id)
    {
        var result = await _scheduleRepository.DeleteSchedule(id);
        if(!result)
            return NotFound(result);
        else
            return BadRequest(result);
    }
}