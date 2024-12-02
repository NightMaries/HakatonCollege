
using System.Security.Cryptography;
using System.Text;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Hakaton.API.Infrustructure.Data;
using Microsoft.AspNetCore.Identity;
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
    public async Task<Schedule> CreateSchedule(ScheduleDto scheduleDto)
    {
        var schedule = new Schedule() {
            GroupId = scheduleDto.GroupId,
            SubjectId = scheduleDto.SubjectId,
            TeacherId = scheduleDto.TeacherId
        };
        var result = _query.Query("Schedules")
        .AsInsert(new 
        {
            GroupId = scheduleDto.GroupId,
            SubjectId = scheduleDto.SubjectId,
            TeacherId = scheduleDto.TeacherId
        }
        );
        var data = await _query.ExecuteAsync(result);
        return schedule;
    }

    public async Task<bool> DeleteSchedule(int id)
    {
        var result = await _query.Query("Schedules").Where("Id", id).DeleteAsync();
        if (result != 1)
            throw new Exception("Запись в расписании не найдена");
        
            return false;
    }

    public async Task<int> EditSchedule(ScheduleDto scheduleDto, int id)
    {
    
        int affected = await _query.Query("Schedules")
                .Where("Id", id)
                .UpdateAsync(new { GroupId = scheduleDto.GroupId,
                                SubjectId = scheduleDto.SubjectId,
                                TeacherId = scheduleDto.TeacherId
                });
        return affected;
    }

    public async Task<Schedule> GetScheduleById(int id)
    {
        var query = _query.Query("Schedules")
            .Where("Id",id)
            .Join("Group","Group.Id","Schedules.GroupId")
            .Select("Id", "Schedules.GroupId");

        var result = await _query.FirstOrDefaultAsync<Schedule>(query);
        if(result is null) throw new Exception("Запись о расписании не найдена");
        return result;
    }

    public async Task<IEnumerable<Schedule>> GetSchedules()
    {
        var query = _query.Query("Schedules")
                    .Join("Group","Group.Id","Schedules.GroupId")
                    .Select("Id", "Schedules.GroupId");
        var result = await _query.GetAsync<Schedule>(query);
        if(result is null) throw new Exception("Ошибка в запросе");
        return result;
        }
}