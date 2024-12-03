using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

namespace Hakaton.API.Application.Interfaces;

public interface IScheduleRepository
{
    public Task<IEnumerable<ScheduleDtoGet>> GetSchedules();
    
    public Task<ScheduleDtoGet> GetScheduleById(int id);

    public Task<int> EditSchedule(ScheduleDtoPost scheduleDto, int id,int scheduleStartHour,int scheduleStartMinute,
            int scheduleEndHour,int scheduleEndMinute);

    public Task<bool> DeleteSchedule(int id);

    public Task<int> CreateSchedule(ScheduleDtoPost scheduleDto, int scheduleStartHour,int scheduleStartMinute,
            int scheduleEndHour,int scheduleEndMinute);

}