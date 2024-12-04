using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hakaton.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherStatisticController : ControllerBase
    {
        private readonly ITeacherStatisticsRepository _statisticService;

        public TeacherStatisticController(ITeacherStatisticsRepository statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet("lesson-count")]
        public async Task<IActionResult> GetLessonCountBySubject(int teacherId, int subjectId)
        {
            try
            {
                var count = await _statisticService.GetLessonCountBySubjectAsync(teacherId, subjectId);
                return Ok(new { LessonCount = count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("lesson-details")]
        public async Task<IActionResult> GetLessonDetails(int teacherId,[FromQuery]int subjectId)
        {
            try
            {
                var lessons = await _statisticService.GetLessonDetailsAsync(teacherId,subjectId);
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
