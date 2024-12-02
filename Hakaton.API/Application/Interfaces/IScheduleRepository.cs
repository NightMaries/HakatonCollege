using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

namespace Hakaton.API.Application.Interfaces;
public interface IScheduleRepository
{
    public Task<IEnumerable<Schedule>> GetSchedules();
    
    public Task<Schedule> GetScheduleById(int id);

    public Task<int> EditSchedule(ScheduleDto scheduleDto, int id);

    public Task<bool> DeleteSchedule(int id);

    public Task<Schedule> CreateSchedule(ScheduleDto scheduleDto);

}