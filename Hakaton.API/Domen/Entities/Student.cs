namespace Hakaton.API.Domen.Entities;
public class Student
{
    public int Id {get; set;}
    public string FIO {get; set;}
    public int UserId {get;set;}
    public User User {get; set;}

    public int GroupId {get; set;}
    public Group Group {get; set;}
}