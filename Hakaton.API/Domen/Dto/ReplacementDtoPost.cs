namespace Hakaton.API.Domen.Dto;

public class ReplacementDtoPost
{
    public int GroupId {get; set;}
    public int OldSubjectId {get; set;}
    public int NewSubjectId {get; set;}
    public int OldTeacherId {get; set;}
    public int NewTeacherId {get; set;}
    public int PairNumber {get; set;}
    public string? Reason {get; set;}
    public DateTime Date {get; set;}
}