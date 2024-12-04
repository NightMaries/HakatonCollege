using System.Security.Cryptography;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.API.Controllers;
[ApiController]
[Route("[controller]")]

public class ParcingScheduleForTeachersController : ControllerBase
{
    
    private readonly IParsingScheduleForTeachersService _scheduleService;
    
    public ParcingScheduleForTeachersController(IParsingScheduleForTeachersService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet("parse-schedule")]
    public IActionResult ParseSchedule()
    {
        try
        {
            var parsedData = _scheduleService.Parse();
            return Ok(new { Message = "Парсинг выполнен успешно", Data = parsedData });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Ошибка при парсинге", Error = ex.Message });
        }
    }
}