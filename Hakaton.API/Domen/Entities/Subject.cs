using Hakaton.API.Domen.Entities;


namespace Hakaton.API.Domen.Entities;
public class Subject
{
    public int Id {get; set;}
    public string Name {get; set;}

    public int TeacherId {get; set;}
    public Teacher Teacher {get; set;}
}