using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Infrustructure.Data;
using SqlKata.Execution;

namespace Hakaton.API.Application.Repositories
{
    public class TeacherStatisticsRepository : ITeacherStatisticsRepository
    {
        private readonly QueryFactory _query;

        public TeacherStatisticsRepository(HakatonContext hakatonContext)
        {
            _query = hakatonContext.PostgresQueryFactory;
        }

        // Получение количества занятий, проведенных преподавателем по предмету.

        public async Task<int> GetLessonCountBySubjectAsync(int teacherId, int subjectId)
        {
            var query = _query.Query("Subjects")
                .Where("TeacherId", teacherId)                
                .Select("Id");

            return await _query.CountAsync<int>(query);
        }

        
        // Получение списка проведенных занятий с детализацией по датам и группам.
        
        public async Task<IEnumerable<TeacherLessonDto>> GetLessonDetailsAsync(int teacherId,int subjectId)
        {
            var query = _query.Query("Schedules")
                .Where("Schedules.TeacherId", teacherId)
                .Join("Subjects","Subjects.Id","Schedules.SubjectId")
                .Join("Groups","Groups.Id","Schedules.GroupId")
                .Select("Schedules.Id","Groups.GroupName","Subjects.Name","Date");

            List<TeacherLessonDto> lists = new List<TeacherLessonDto>();
            var result = await _query.GetAsync<TeacherLessonDto>(query);
            foreach(var item in result)
            {
                var subject = await _query.FirstOrDefaultAsync<SubjectDto>(_query.Query("Subjects").Where("Id",subjectId).Select());
            
                lists.Add(new TeacherLessonDto
                {
                    GroupName = item.GroupName,
                    SubjectName = subject.Name,
                    Date = item.Date

                });
            }

            return lists;
        }
    }
}
