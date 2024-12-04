using System.Text.RegularExpressions;

namespace Hakaton.API.Domen.Dto;

public class ScheduleDtoGet
{
    public int Id {get;set;}
    public string GroupName {get;set;} 
    public string SubjectName {get;set;} 
    public string TeacherFIO {get;set;} 
    public string WeekDay {get;set;}
    public int StudyWeekId {get;set;}
    public int ScheludeNumber {get; set;}
    public DateTime Date{get; set;}
    public TimeSpan ScheduleStart {get; set;}
    public TimeSpan ScheduleEnd {get; set;}
    public bool CurrentPair {get;set;} 
    
}