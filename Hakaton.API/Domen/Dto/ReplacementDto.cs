namespace Hakaton.API.Domen.Dto;

public class ReplacementDto
{
    public int Id {get; set;} 
    public int GroupId {get; set;}
    public int OldSubjectId {get; set;}
    public int NewSubjectId {get; set;}
    public int PairNumber {get; set;}
    public string? Reason {get; set;}
    public DateTime Date {get; set;}
}