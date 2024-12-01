namespace Hakaton.API.Domen.Entities;

public class Teacher 
{
    public int Id {get; set;}
    public string FIO {get; set;}
    public string Classroom {get; set;}
    
    public int UserId {get;set;}
    public User User {get; set;}

}