
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using DocumentFormat.OpenXml.Office.CustomUI;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Hakaton.API.Infrustructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using SqlKata;
using SqlKata.Execution;

namespace Hakaton.API.Application.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly QueryFactory _query;
    private readonly ITokenService _tokenService;
    private readonly ICryptService _cryptService;
    
    private HMACSHA256 hmac = new HMACSHA256();
    public ScheduleRepository(HakatonContext hakatonContext, ITokenService tokenService,ICryptService cryptService)
    {
        _query = hakatonContext.PostgresQueryFactory;
        _tokenService = tokenService;
        _cryptService = cryptService;
    }
    public async Task<int> CreateSchedule(ScheduleDtoPost scheduleDto,int scheduleStartHour,int scheduleStartMinute,
            int scheduleEndHour,int scheduleEndMinute)
    {
        TimeSpan scheduleStart1 = new TimeSpan(scheduleStartHour,scheduleStartMinute,0);
        TimeSpan scheduleEnd1 =new TimeSpan(scheduleEndHour,scheduleEndMinute,0);
        
        var result = _query.Query("Schedules")
        .AsInsert(new 
        {
            GroupId = scheduleDto.GroupId,
            SubjectId = scheduleDto.SubjectId,
            TeacherId = scheduleDto.TeacherId,
            WeekDay = scheduleDto.WeekDay,
            StudyWeekId = scheduleDto.StudyWeekId,
            ScheduleStart = scheduleStart1,
            ScheduleEnd = scheduleEnd1,
            ScheduleNumber = scheduleDto.ScheludeNumber,
            Date = scheduleDto.Date
        }
        );
        return await _query.ExecuteAsync(result);
        
    }

    public async Task<bool> DeleteSchedule(int id)
    {
        var result = await _query.Query("Schedules").Where("Id", id).DeleteAsync();
        if (result != 1)
            throw new Exception("Запись в расписании не найдена");
        
            return true;
    }

    public async Task<int> EditSchedule(ScheduleDtoPost scheduleDto, int id,int scheduleStartHour,int scheduleStartMinute,
            int scheduleEndHour,int scheduleEndMinute)
    {
    
        
        TimeSpan scheduleStart1 = new TimeSpan(scheduleStartHour,scheduleStartMinute,0);
        TimeSpan scheduleEnd1 =new TimeSpan(scheduleEndHour,scheduleEndMinute,0);

        int affected = await _query.Query("Schedules")
                .Where("Id", id)
                .UpdateAsync(new { 
                    GroupId = scheduleDto.GroupId,
                    SubjectId = scheduleDto.SubjectId,
                    TeacherId = scheduleDto.TeacherId,
                    WeekDay = scheduleDto.WeekDay,
                    StudyWeekId = scheduleDto.StudyWeekId,
                    ScheduleStart = scheduleStart1,
                    ScheduleEnd = scheduleEnd1,
                    ScheduleNumber = scheduleDto.ScheludeNumber,
                    Date = scheduleDto.Date
                });
        return affected;
    }

    public async Task<ScheduleDtoGet> GetScheduleById(int id)
    {
        //Сделать пометку для текущей пары
        //Также изменить DTO для этого
        var query = _query.Query("Schedules")
            .Where("Id",id)
            .Select("Id", "GroupId","SubjectId","TeacherId","WeekDay","StudyWeekId",
                "ScheduleStart","ScheduleEnd","ScheduleNumber","Date");
        
        var result = await _query.FirstOrDefaultAsync<Schedule>(query);
        
        var queryGroup = _query.Query("Groups").Where("Id",result.GroupId).Select("GroupName");
            
            var queryTeacher = _query.Query("Teachers").Where("Id",result.TeacherId).Select("FIO");
            
            var querySubject = _query.Query("Subjects").Where("Id",result.SubjectId).Select("Name");

            
            var dateTime = DateTime.UtcNow.TimeOfDay;
            bool currentPair = false;
            if(result.ScheduleStart <= dateTime && dateTime >= result.ScheduleEnd)
                currentPair = true;

            ScheduleDtoGet schedule = new ScheduleDtoGet
            {
                Id = result.Id,
                GroupName = await _query.FirstOrDefaultAsync<string>(queryGroup),
                TeacherFIO = await _query.FirstOrDefaultAsync<string>(queryTeacher),
                SubjectName = await _query.FirstOrDefaultAsync<string>(querySubject),
                WeekDay = result.WeekDay,
                StudyWeekId = result.StudyWeekId,
                ScheludeNumber = result.ScheduleNumber,
                ScheduleStart = result.ScheduleStart,
                ScheduleEnd = result.ScheduleEnd,
                Date = result.Date,
                CurrentPair = currentPair
                };
        if(result is null) throw new Exception("Запись о расписании не найдена");
        return schedule;
    }

    public async Task<IEnumerable<ScheduleDtoGet>> GetSchedules()
    {

        var query = _query.Query("Schedules")
                    .Select("Schedules.Id","GroupId","TeacherId",
                    "SubjectId","WeekDay","StudyWeekId","ScheduleStart","ScheduleEnd","ScheduleNumber","Date");

        var result = await _query.GetAsync<Schedule>(query);
        List<ScheduleDtoGet> list = new List<ScheduleDtoGet>();
       
       foreach(var item in result)
       {
            var queryGroup = _query.Query("Groups").Where("Id",item.GroupId).Select("GroupName");
            
            var queryTeacher = _query.Query("Teachers").Where("Id",item.TeacherId).Select("FIO");
            
            var querySubject = _query.Query("Subjects").Where("Id",item.SubjectId).Select("Name");
            
            var dateTime = DateTime.Now.TimeOfDay;
            bool currentPair = false;
            if(item.ScheduleStart <= dateTime && dateTime >= item.ScheduleEnd)
                currentPair = true;

            list.Add(new ScheduleDtoGet
            {
                Id = item.Id,
                GroupName = await _query.FirstOrDefaultAsync<string>(queryGroup),
                TeacherFIO = await _query.FirstOrDefaultAsync<string>(queryTeacher),
                SubjectName = await _query.FirstOrDefaultAsync<string>(querySubject),
                WeekDay = item.WeekDay,
                StudyWeekId = item.StudyWeekId,
                ScheludeNumber = item.ScheduleNumber,
                ScheduleStart = item.ScheduleStart,
                ScheduleEnd = item.ScheduleEnd,
                Date = item.Date,
                CurrentPair = currentPair
                
            });

       }
        
        if(result is null) throw new Exception("Ошибка в запросе");
        return list;
        }
}