using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

public interface IStudyWeekRepository 
{
    public Task<int> CreateStudyWeek(StudyWeekDto studyWeekDto);
    
    public Task<StudyWeek> GetStudyWeekById(int id);
    
    public Task<IEnumerable<StudyWeek>> GetStudyWeeks();
}