using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Hakaton.API.Infrustructure.Data;
using SqlKata.Execution;

public class StudyWeekRepository : IStudyWeekRepository
{
    private readonly QueryFactory _query;

    public StudyWeekRepository(HakatonContext hakatonContext)
    {
        _query = hakatonContext.PostgresQueryFactory;
    }
    public async Task<int> CreateStudyWeek(StudyWeekDto studyWeekDto)
    {
        var result = _query.Query("StudyWeeks").AsInsert(new
        {
            StudyWeekNumber = studyWeekDto.StudyWeekNumber,
            StartDate = studyWeekDto.StartDate,
            EndDate = studyWeekDto.EndDate
        });
        return await _query.ExecuteAsync(result);
    }

    public async Task<StudyWeek> GetStudyWeekById(int id)
    {
        var query = _query.Query("StudyWeeks").Where("Id",id)
        .Select("Id","StudyWeekNumber","StartDate","EndDate");
        var result = await _query.FirstOrDefaultAsync<StudyWeek>(query);
        if(result is null)
            throw new Exception("Запись не найдена");
        return result;
    
    }

    public async Task<IEnumerable<StudyWeek>> GetStudyWeeks()
    {
    
        var query = _query.Query("StudyWeeks").Select("Id","StudyWeekNumber","StartDate","EndDate");
        var result = await _query.GetAsync<StudyWeek>(query);
        if(result is null)
            throw new Exception("Записей нет");
        return result;
    }
}