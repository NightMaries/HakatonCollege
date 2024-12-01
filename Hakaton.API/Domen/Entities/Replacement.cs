namespace Hakaton.API.Domen.Entities;
public class Replacement
{
    public int Id {get; set;}
    public int GroupId {get; set;}
    public Group Group {get; set;}
    public DateTime Date {get; set;}
    public int OldSubjectId {get; set;}
    public Subject OldSubject {get; set;}
    public int NewSubjectId {get; set;}
    public Subject NewSubject {get; set;}
    public string Reason {get; set;}
}