namespace Hakaton.API.Domen.Entities;
public class Group
{
    public int Id {get; set;}
    public string? GroupName {get; set;} 
    public int TeacherId {get; set;}
    public Teacher? Teacher {get; set;}
    
}