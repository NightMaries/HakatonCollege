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
    public async Task<IActionResult> CreateSchedule(ScheduleDtoPost scheduleDto,[FromQuery]int scheduleStartHour,[FromQuery]int scheduleStartMinute,
            [FromQuery]int scheduleEndHour,[FromQuery]int scheduleEndMinute)
    {
        var result = await _scheduleRepository.CreateSchedule(scheduleDto, scheduleStartHour,scheduleStartMinute,
        scheduleEndHour,scheduleEndMinute);
        if(result != 1)
            throw new Exception("Не удалось создать запись в расписании");
        return Ok(scheduleDto);
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
    public async Task<IActionResult> EditSchedule(ScheduleDtoPost scheduleDto, int id,int scheduleStartHour, 
    int scheduleStartMinute,  int scheduleEndHour,int scheduleEndMinute)
    {
        var result = await _scheduleRepository.EditSchedule(scheduleDto,id,scheduleStartHour,scheduleStartMinute,
        scheduleEndHour,scheduleEndMinute);
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