namespace Hakaton.API.Domen.Dto;

public class ScheduleDtoPost
{
    public int GroupId {get;set;}
    public int SubjectId {get;set;}
    public int TeacherId {get;set;}
    public string WeekDay {get;set;}
    public int StudyWeekId {get;set;}
    public int ScheludeNumber {get; set;}
    public DateTime Date {get;set;}
}