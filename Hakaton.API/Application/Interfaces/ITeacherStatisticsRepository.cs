using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

namespace Hakaton.API.Application.Interfaces;

public interface ITeacherStatisticsRepository
{
    public Task<int> GetLessonCountBySubjectAsync(int teacherId, int subjectId); //Количество отведенных занятий
    public Task<IEnumerable<TeacherLessonDto>> GetLessonDetailsAsync(int teacherId,int subjectid); //Список занятий с информацией о датах и группах
    
}