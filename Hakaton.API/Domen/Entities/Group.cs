namespace Hakaton.API.Domen.Entities;
public class Group
{
    public int Id {get; set;}
    public string Name {get; set;} 
    public int CuratorId {get; set;}
    public Curator Curator {get; set;}
    
}