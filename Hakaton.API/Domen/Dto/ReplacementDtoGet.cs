namespace Hakaton.API.Domen.Dto;

public class ReplacementDtoGet
{
    public int Id {get; set;} 
    public int GroupId {get; set;}
    public int OldSubjectId {get; set;}
    public int NewSubjectId {get; set;}
    public int OldTeacherId {get; set;}
    public int NewTeacherId {get; set;}
    public string GroupName {get; set;}
    public string OldSubjectName {get; set;}
    public string NewSubjectName {get; set;}
    public string OldTeacherFIO {get; set;}
    public string NewTeacherFIO {get; set;}
    public int PairNumber {get; set;}
    public string? Reason {get; set;}
    public DateTime Date {get; set;}
}