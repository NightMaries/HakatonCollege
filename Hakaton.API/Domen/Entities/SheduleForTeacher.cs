using Microsoft.Identity.Client;

namespace Hakaton.API.Domen.Entities;

public class ScheduleForTeacher
{
    public int Id {get; set;}
    public int TeacherId {get; set;}
    public int GroupId {get;set;}
    public int[] StudyWeeks {get; set;} 

    public string WeekDay {get; set;}
    public int PairNumber {get; set;}
    public Teacher Teacher {get; set;}
    public Group Group {get; set;}

    public StudyWeek StudyWeek {get;set;}

}