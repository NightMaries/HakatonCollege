using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route ("[controller]")]
public class StudyWeekController: ControllerBase
{
    private readonly IStudyWeekRepository _studyWeekRepository;

    public StudyWeekController(IStudyWeekRepository studyWeekRepository)
    {
        _studyWeekRepository = studyWeekRepository;
    }
    [HttpPost]
    public async Task<IActionResult> CreateStudyWeek(StudyWeekDto studyWeekDto)
    {
        var result = await _studyWeekRepository.CreateStudyWeek(studyWeekDto);
        if (result != 1)
            return BadRequest();
        return Ok(result);
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> GetStudyWeekById(int id)
    {
        var result = await _studyWeekRepository.GetStudyWeekById(id);
        if (result is null)
            return BadRequest();
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetStudyWeeks()
    {
        var result = await _studyWeekRepository.GetStudyWeeks();
        if (result is null)
            return BadRequest();
        return Ok(result);
    }
}
