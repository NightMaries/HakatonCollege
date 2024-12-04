namespace Hakaton.API.Domen.Entities;
public class Schedule {
    public int Id {get; set;}
    public int GroupId {get; set;}
    public Group? Group {get; set;}
    public int SubjectId {get; set;}
    public Subject? Subject {get; set;}
    public int TeacherId {get; set;}
    public Teacher? Teacher {get; set;}
    public string? WeekDay {get; set;}
    public int StudyWeekId {get; set;}
    public StudyWeek? StudyWeek {get; set;}
    public DateTime Date {get; set;}
    public TimeSpan ScheduleStart {get; set;}
    public TimeSpan ScheduleEnd {get; set;}
    public int ScheduleNumber {get; set;}
    }
